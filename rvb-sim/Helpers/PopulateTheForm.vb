Imports System.Threading

Module Populate

    Friend Sub Populatetheform()
        Try
            Dim xmlRead As New ReadXmlFile
            xmlRead.Read()
            RVBSim.Text = String.Format("RVB Simulator v-{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

            If CInt(SupportedRVBRevision) >= 15 Then
                support = True
                RVBSim.grpRevSettings.Visible = True
            Else
                support = False
                RVBSim.grpRevSettings.Visible = False
            End If

            ' With My.Settings
            RVBSim.NumericUpDownDNPSourceAddress.Value = dnpSetting.source              '.source
            RVBSim.NumericUpDownDNPDestinationAddress.Value = dnpSetting.destination   '.destination

            RVBSim.NumericUpDownModbusLocalVoltageRegister.Value = modbusRegister.LocalVoltage  '.mdLocalvoltage
            RVBSim.NumericUpDownModbusFwdRVBVoltageRegister.Value = modbusRegister.FRVBValue  '.mdFRVBvoltage
            RVBSim.NumericUpDownModbusRevRVBVoltageRegister.Value = modbusRegister.RRVBValue  '.mdRRVBvoltage

            RVBSim.heartbeattimer.Value = testSetting.HeartbeatTimer                  '.heartbeat
            If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then RVBSim.FwdDeltaVoltage.Value = 0.0 Else RVBSim.FwdDeltaVoltage.Value = testSetting.FwdRVBVoltage
            If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RVBSim.RevDeltaVoltage.Value = 0.0 Else RVBSim.RevDeltaVoltage.Value = testSetting.RevRVBVoltage

            RVBSim.FwdRVBScaleFactor.Value = testSetting.FwdRVBVoltageScale  '.Fmultiplier
            RVBSim.RevRVBScaleFactor.Value = testSetting.RevRVBVoltageScale   '.Rmultiplier
            RVBSim.txtIECLocalVoltage.Text = iecSetting.LocalVoltage       '.IECLocalVoltage
            RVBSim.txtIECFwdRVBVoltage.Text = iecSetting.FRVBValue         ' .IECFwdRVBVoltage
            RVBSim.txtIECRevRVBVoltage.Text = iecSetting.RRVBValue         ' .IECRevRVBVoltage

            With My.Settings
                '************************************************************
                '*  if visibility set to true by the user                   *
                '*  Verify the form is in visible area                      *
                '*  If NOT reset location to 0,0                            *
                '************************************************************
                If visibility Then
                    Dim x As Integer = My.Computer.Screen.WorkingArea.Left
                    Dim y As Integer = My.Computer.Screen.WorkingArea.Right
                    If .location.X < x Or .location.X > y Then
                        RVBSim.DesktopLocation = New System.Drawing.Point(20, 20)
                    Else
                        RVBSim.DesktopLocation = .location
                    End If
                Else
                    RVBSim.DesktopLocation = New System.Drawing.Point(32000, 32000)
                    ' Me.DesktopLocation = New System.Drawing.Point(20, 20)
                End If
                '************************************************************
            End With
            Select Case testSetting.Protocol    '.protocol
                Case "dnp"
                    RVBSim.dnpbutton.Checked = True
                    RVBSim.txtPort.Text = dnpSetting.Port  '.dnpport
                    RVBSim.checkHandler(RVBSim.dnpbutton)
                Case "modbus"
                    RVBSim.modbusbox.Checked = True
                    RVBSim.txtPort.Text = modbusRegister.Port  '.mdport
                    RVBSim.checkHandler(RVBSim.modbusbox)
                Case "iec"
                    RVBSim.iec61850box.Checked = True
                    RVBSim.txtPort.Text = iecSetting.Port '.iecport
                    RVBSim.checkHandler(RVBSim.iec61850box)
            End Select
            RVBSim.txtRead.Text = testSetting.readIpAddress  '.IPAddressToRead
            RVBSim.txtWrite.Text = testSetting.writeIpAddress
            '
        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- populatetheform", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub

End Module
