
Imports System.IO
Imports System.Threading

Public Class RVBSim

    ''' <summary>
    ''' Reads periodically
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="timeOut"></param>
    Protected Friend Sub PeriodicReadEventNew(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut AndAlso StopButton.Enabled Then
            ' TODO: add loop to thru multi phase regulators

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicReadEventNew)} -- STARTS")

            Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- Reading is in progress... Reads everything.")

            Debug.WriteLine($"-----------------------> Elapsed time: {ReadingTimer.ElapsedMilliseconds} msec")

            periodicRead.ReadNew(rvbForm:=Me)

            ' periodicRead.Read(rvbForm:=Me)
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicReadEventNew)} -- ENDS")

        Else
            If StopButton.Enabled Then
                ReadTickerDone.Set()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Writes periodically writes to write IP Address.
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="timeOut"></param>
    Protected Friend Sub PeriodicWriteEventNew(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut AndAlso StopButton.Enabled Then
            ' TODO: add loop to thru multi phase regulators

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicWriteEventNew)} -- STARTS")

            Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- Writing is in progress...")

            Debug.WriteLine($"-----------------------> Writing: Regulator {Val(state)}, Writing: {WritingTimers(Val(state)).ElapsedMilliseconds} msec, Reading:{ReadingTimer.ElapsedMilliseconds} msec")

            periodicWrite.WriteNew(rvbForm:=Me, regulatorId:=Val(state))

            ' periodicRead.Read(rvbForm:=Me)
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(PeriodicWriteEventNew)} -- ENDS")

        Else
            If StopButton.Enabled Then
                WriteTickerDones(Val(state)).Set()
            End If
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
        Else
            ProtocolInUse = baseJsonSettings.Protocol
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Main()
    End Sub

    Public Async Sub Main()

        Dim proc As Process
        Try
            jsonRead = New JsonFile()
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

            ' check for updates
            Dim update = New AppStartup()
            Await update.CheckForUpdates()

            Populatetheform()
            If My.Application.CommandLineArgs.Count > 1 Then
                CheckCommandLine()
            Else
                ReadIpAddr.Focus()
            End If

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(lblMsgCenter, message)
            sb.AppendLine(message)
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

            ' PortReg1.Text = baseJsonSettings.Port   'dnpRegulators.Port
            If sender.Checked Then

                Select Case sender.Name
                    Case $"{NameOf(dnpbutton)}"
                        CommunicationDetails.Text = "DNP3.0 Addresses"
                        lblwarning.Text = "Don't forget to download DNP default file"
                        'PortReg1.Text = baseJsonSettings.Port   'dnpRegulators.Port
                        ProtocolInUse = "dnp"
                        SetValuesFromJson(True)

                    Case $"{NameOf(modbusbox)}"
                        CommunicationDetails.Text = "Modbus registers"
                        ' PortReg1.Text = modbusRegulators.Port
                        ProtocolInUse = "modbus"
                        SetValuesFromJson(True)

                    Case $"{NameOf(iec61850box)}"
                        CommunicationDetails.Text = "IEC61850 Datasets"
                        lblwarning.Text = "Don't forget to purchase IEC61850"
                        'PortReg1.Text = iecRegulators.Port
                        ProtocolInUse = "iec"
                        SetValuesFromJson(True)
                End Select
            End If

            ' dnp3.0 stuff.
            lbldestination.Visible = dnpbutton.Checked
            lblsource.Visible = dnpbutton.Checked
            DNPSourceReg1.Visible = dnpbutton.Checked
            DNPDestinationReg1.Visible = dnpbutton.Checked

            ' warn the user about communication file uploads
            lblwarning.Visible = dnpbutton.Checked Or iec61850box.Checked

            ' show a single phase or 3-phase interface
            ' set visibility per the user selection
            ModbusSettingsGroup.Visible = modbusbox.Checked
            DnpSettingsGroup.Visible = dnpbutton.Checked
            IecSettingsGroup.Visible = iec61850box.Checked

            ' show these items when 3-phase enabled in settings.json file
            ' initial presentation
            With baseJsonSettings
                ModbusSettingsGroup3Phase.Visible = Not .SinglePhase    ' testSetting.IsSinglePhase
                DnpSettingsGroup3Phase.Visible = Not .SinglePhase
                IecSettingsGroup3Phase.Visible = Not .SinglePhase
                RVBSettings3Phase.Visible = Not .SinglePhase
                Regulator3PhReadings.Visible = Not .SinglePhase
                SinglePhaseCheckBox.Checked = .SinglePhase
            End With

            If sender.Checked Then
                ' update the protocol in use
                UpdateProtocol()

                ' set focus on read ip address text box.
                ReadIpAddr.Select()
            End If


        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(lblMsgCenter, message)
            sb.AppendLine(message)
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(CheckHandler)}")
        End Try
    End Sub

    Private Sub SinglePhaseCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SinglePhaseCheckBox.CheckedChanged

        ' show these items when 3-phase enabled in settings.json file
        ' handles changes
        ModbusSettingsGroup3Phase.Visible = Not SinglePhaseCheckBox.Checked
        DnpSettingsGroup3Phase.Visible = Not SinglePhaseCheckBox.Checked
        IecSettingsGroup3Phase.Visible = Not SinglePhaseCheckBox.Checked
        RVBSettings3Phase.Visible = Not SinglePhaseCheckBox.Checked
        Regulator3PhReadings.Visible = Not SinglePhaseCheckBox.Checked

    End Sub

    Private Sub ProtocolChanged(sender As Object, e As EventArgs) Handles dnpbutton.CheckedChanged, modbusbox.CheckedChanged, iec61850box.CheckedChanged

        Dim rb As RadioButton = CType(sender, RadioButton)

        ' no need to update at load since it would change immediately.
        If Not String.IsNullOrWhiteSpace(rb.Text) Then
            If rb.Checked Then
                CheckHandler(rb)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles <see cref="RadioButton.CheckedChanged"/> events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub Radio_CheckedChanged(sender As RadioButton, e As EventArgs) Handles SettingsUsefixedReg3.CheckedChanged, SettingsUsefixedReg2.CheckedChanged, SettingsUsefixedReg1.CheckedChanged, SettingsUserelativeReg3.CheckedChanged, SettingsUserelativeReg2.CheckedChanged, SettingsUserelativeReg1.CheckedChanged
        Try

            Dim rb As RadioButton = CType(sender, RadioButton)

            If rb.Checked AndAlso Not String.IsNullOrWhiteSpace(rb.Name) Then
                Dim soome = New RelativeOrFixedValue
                soome.Decide(rvbForm:=Me, sender:=sender)
            End If

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(lblMsgCenter, message)
            sb.AppendLine(message)
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Radio_CheckedChanged)}")
        End Try
    End Sub
End Class

