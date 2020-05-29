
Imports System.Threading

Public Class RVBSim

    '''<summary>Writes periodically writes to write IP Address.</summary>
    Protected Friend Sub PeriodicWriteEvent(ByVal state As Object, ByVal timeOut As Boolean) '(ByVal mip As IPAddress, ByVal m_port As UShort)

        If timeOut Then
            If ConsoleWriteEnable Then
                Console.WriteLine("Current thread is # {0} PeriodicWriteEvent", Thread.CurrentThread.GetHashCode)
                Heart_Beat_Timer = 0
            End If

            ReadRegisterWait.Unregister(Nothing)
            GenerateRVBVoltage2Transfer(rvbForm:=Me)

            periodicWrite.Write(rvbForm:=Me)
            
        Else
            WriteRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Protected Friend Sub PeriodicReadEvent(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut Then
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} PeriodicReadEvent", Thread.CurrentThread.GetHashCode)

            periodicRead.Read(rvbForm:=Me)

        Else
            ReadRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Protected Friend Sub UpdateProtocol()
        If modbusbox.Checked Then
            ProtocolInUse = "modbus"
        ElseIf dnpbutton.Checked Then
            ProtocolInUse = "dnp"
        ElseIf iec61850box.Checked Then
            ProtocolInUse = "iec"
        End If
        If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- UpdateProtocol", Thread.CurrentThread.GetHashCode)
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        start.Start()
    End Sub

    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        pause.Pause()
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
                txtRead.Focus()
            End If
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Main", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub

    Protected Friend Sub CheckHandler(sender As RadioButton)
        Try
            Select Case sender.Text
                Case "DNP3.0"
                    AddressBox.Text = "DNP3.0 Addresses"
                    lblwarning.Text = "Don't forget to download DNP default file"
                    txtPort.Text = dnpSetting.Port
                Case "Modbus"
                    AddressBox.Text = "Modbus registers"
                    txtPort.Text = modbusRegister.Port
                Case "IEC61850"
                    AddressBox.Text = "IEC61850 Datasets"
                    lblwarning.Text = "Don't forget to purchase IEC61850"
                    txtPort.Text = iecSetting.Port
            End Select

            lbldestination.Visible = dnpbutton.Checked
            lblsource.Visible = dnpbutton.Checked
            NumericUpDownDNPSourceAddress.Visible = dnpbutton.Checked
            NumericUpDownDNPDestinationAddress.Visible = dnpbutton.Checked
            lblwarning.Visible = dnpbutton.Checked Or iec61850box.Checked

            lbllocalvoltage.Visible = modbusbox.Checked
            Modbus_F_RVBVoltage_Label.Visible = modbusbox.Checked
            Modbus_R_RVBVoltage_Label.Visible = modbusbox.Checked And support
            NumericUpDownModbusLocalVoltageRegister.Visible = modbusbox.Checked
            NumericUpDownModbusFwdRVBVoltageRegister.Visible = modbusbox.Checked
            NumericUpDownModbusRevRVBVoltageRegister.Visible = modbusbox.Checked And support

            IEC_LocalVoltage_Label.Visible = iec61850box.Checked
            IEC_F_RVBVoltage_Label.Visible = iec61850box.Checked
            IEC_R_RVBVoltage_Label.Visible = iec61850box.Checked And support
            txtIECLocalVoltage.Visible = iec61850box.Checked
            txtIECFwdRVBVoltage.Visible = iec61850box.Checked
            txtIECRevRVBVoltage.Visible = iec61850box.Checked And support
            txtRead.Select()
            'rev 15 items
            R_RVBScaleFactor_Label.Visible = support
            RevRVBScaleFactor.Visible = support
            Reverse_Voltage_Label.Visible = support
            RevDeltaVoltage.Visible = support

        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
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

    Public Sub Radio_CheckedChanged(sender As RadioButton, e As EventArgs) Handles radUseDeltaVoltage.CheckedChanged, radUseFixedVoltage.CheckedChanged
        Try
            Select Case sender.Name
                Case "radUseDeltaVoltage"
                    Forward_Voltage_Label.Text = DeltaMessage
                    Reverse_Voltage_Label.Text = DeltaMessage
                    FwdDeltaVoltage.Minimum = MinDeltaVoltage
                    FwdDeltaVoltage.Maximum = MaxDeltaVoltage
                    RevDeltaVoltage.Minimum = MinDeltaVoltage
                    RevDeltaVoltage.Maximum = MaxDeltaVoltage
                    FwdDeltaVoltage.Value = 0.0
                    RevDeltaVoltage.Value = 0.0
                Case "radUseFixedVoltage"
                    Forward_Voltage_Label.Text = DirectMessage
                    Reverse_Voltage_Label.Text = DirectMessage
                    FwdDeltaVoltage.Minimum = RVBMin.Value 'MinSpecValue
                    FwdDeltaVoltage.Maximum = RVBMax.Value 'MaxSpecValue
                    RevDeltaVoltage.Minimum = RVBMin.Value 'MinSpecValue
                    RevDeltaVoltage.Maximum = RVBMax.Value 'MaxSpecValue
                    If readresult / M2001D_Comm_Scale >= FwdDeltaVoltage.Minimum Then FwdDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else FwdDeltaVoltage.Value = FwdDeltaVoltage.Maximum
                    If readresult / M2001D_Comm_Scale >= RevDeltaVoltage.Minimum Then RevDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else RevDeltaVoltage.Value = RevDeltaVoltage.Minimum
            End Select
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Radio_CheckedChanged)}")
        End Try
    End Sub
End Class

