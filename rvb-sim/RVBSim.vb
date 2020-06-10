
Imports System.Threading

Public Class RVBSim

    ''' <summary>
    ''' Writes periodically writes to write IP Address.
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="timeOut"></param>
    Protected Friend Sub PeriodicWriteEvent(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut Then

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicWriteEvent)}")

            ' Heart_Beat_Timer = 0
            Interlocked.Exchange(Heart_Beat_Timer, 0)
            ReadRegisterWait.Unregister(Nothing)
            GenerateRVBVoltage2Transfer(rvbForm:=Me)

            periodicWrite.Write(rvbForm:=Me)

        Else
            WriteRegisterWait.Unregister(Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Reads periodically
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="timeOut"></param>
    Protected Friend Sub PeriodicReadEvent(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut Then

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicReadEvent)}")

            periodicRead.Read(rvbForm:=Me)

        Else
            ReadRegisterWait.Unregister(Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Updates Protocol
    ''' </summary>
    Protected Friend Sub UpdateProtocol()
        If modbusbox.Checked Then
            ProtocolInUse = "modbus"
        ElseIf dnpbutton.Checked Then
            ProtocolInUse = "dnp"
        ElseIf iec61850box.Checked Then
            ProtocolInUse = "iec"
        End If

        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(UpdateProtocol)}")
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        start.Start()
    End Sub

    Private Sub BtnStop_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        pause.Pause()
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CloseForm()
    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Main()
    End Sub

    Public Sub Main()
        Dim proc As Process
        Try
            processID = 0
            For Each proc In Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)
                processID += 1
                If processID = 0 Then

                Else 'processID = 2 Then
                    For i = 1 To processID
                        proc.CloseMainWindow()
                    Next i
                End If
                proc = Nothing
            Next
            Populatetheform()
            If My.Application.CommandLineArgs.Count > 1 Then
                CheckCommandLine()
            Else
                ReadIpAddr.Focus()
            End If
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Main)}")
        End Try
    End Sub

    ''' <summary>
    ''' Handles <see cref="RadioButton"/> check events
    ''' </summary>
    ''' <param name="sender"></param>
    Protected Friend Sub CheckHandler(sender As RadioButton)
        Try
            Select Case sender.Name
                Case $"{NameOf(dnpbutton)}"
                    If sender.Checked Then
                        CommunicationDetails.Text = "DNP3.0 Addresses"
                        lblwarning.Text = "Don't forget to download DNP default file"
                        PortReg1.Text = Regulators(0).DnpCommunication(0).Port ' first regulator dnpSetting.Port
                    End If

                Case $"{NameOf(modbusbox)}"
                    If sender.Checked Then
                        CommunicationDetails.Text = "Modbus registers"
                        PortReg1.Text = Regulators(0).ModbusCommunication(0).Port ' first regulator modbusRegister.Port
                    End If

                Case $"{NameOf(iec61850box)}"
                    If sender.Checked Then
                        CommunicationDetails.Text = "IEC61850 Datasets"
                        lblwarning.Text = "Don't forget to purchase IEC61850"
                        PortReg1.Text = Regulators(0).IECCommunication(0).Port ' first regulator iecSetting.Port
                    End If
            End Select

            ' dnp3.0 stuff.
            lbldestination.Visible = dnpbutton.Checked
            lblsource.Visible = dnpbutton.Checked
            DNPSourceReg1.Visible = dnpbutton.Checked
            DNPDestinationReg1.Visible = dnpbutton.Checked

            ' warn the user about communication file uploads
            lblwarning.Visible = dnpbutton.Checked Or iec61850box.Checked

            ' set visibility per the user selection
            ModbusSettingsGroup.Visible = modbusbox.Checked
            DnpSettingsGroup.Visible = dnpbutton.Checked
            IecSettingsGroup.Visible = iec61850box.Checked

            ' set focus on read ip address text box.
            ReadIpAddr.Select()

        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(CheckHandler)}")
        End Try
    End Sub

    Private Sub Dnpbutton_CheckedChanged(sender As Object, e As EventArgs) Handles dnpbutton.CheckedChanged
        CheckHandler(sender)
    End Sub

    Private Sub Modbusbox_CheckedChanged(sender As Object, e As EventArgs) Handles modbusbox.CheckedChanged
        CheckHandler(sender)
    End Sub

    Private Sub Iec61850box_CheckedChanged(sender As Object, e As EventArgs) Handles iec61850box.CheckedChanged
        CheckHandler(sender)
    End Sub

    ''' <summary>
    ''' Handles <see cref="RadioButton.CheckedChanged"/> events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub Radio_CheckedChanged(sender As RadioButton, e As EventArgs) Handles useDeltaVoltage.CheckedChanged, useFixedVoltage.CheckedChanged
        Try
            Select Case sender.Name
                Case $"{NameOf(useDeltaVoltage)}"
                    If sender.Checked Then
                        Forward_Voltage_Label.Text = DeltaMessage
                        Reverse_Voltage_Label.Text = DeltaMessage
                        FwdDeltaVoltageReg1.Minimum = MinDeltaVoltage
                        FwdDeltaVoltageReg1.Maximum = MaxDeltaVoltage
                        RevDeltaVoltageReg1.Minimum = MinDeltaVoltage
                        RevDeltaVoltageReg1.Maximum = MaxDeltaVoltage
                        FwdDeltaVoltageReg1.Value = 0.0
                        RevDeltaVoltageReg1.Value = 0.0
                    End If

                Case $"{NameOf(useFixedVoltage)}"
                    If sender.Checked Then
                        Forward_Voltage_Label.Text = DirectMessage
                        Reverse_Voltage_Label.Text = DirectMessage
                        FwdDeltaVoltageReg1.Minimum = RVBMin.Value 'MinSpecValue
                        FwdDeltaVoltageReg1.Maximum = RVBMax.Value 'MaxSpecValue
                        RevDeltaVoltageReg1.Minimum = RVBMin.Value 'MinSpecValue
                        RevDeltaVoltageReg1.Maximum = RVBMax.Value 'MaxSpecValue
                        If Readresult / M2001D_Comm_Scale >= FwdDeltaVoltageReg1.Minimum Then FwdDeltaVoltageReg1.Value = Readresult / M2001D_Comm_Scale Else FwdDeltaVoltageReg1.Value = FwdDeltaVoltageReg1.Maximum
                        If Readresult / M2001D_Comm_Scale >= RevDeltaVoltageReg1.Minimum Then RevDeltaVoltageReg1.Value = Readresult / M2001D_Comm_Scale Else RevDeltaVoltageReg1.Value = RevDeltaVoltageReg1.Minimum
                    End If
            End Select
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Radio_CheckedChanged)}")
        End Try
    End Sub

End Class

