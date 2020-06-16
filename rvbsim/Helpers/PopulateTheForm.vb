Imports System.Reflection
Imports System.Threading

Module Populate

    ''' <summary>
    ''' Reads a Settings.xml file and populates form with read information.
    ''' </summary>
    Friend Sub Populatetheform()
        Try
            ' Read Settings.xml file
            Dim xmlRead As New ReadXmlFile
            xmlRead.Read()

            ' update the form title
            RVBSim.Text = $"RVB Simulator({Assembly.GetEntryAssembly().GetName().Version.ToString(3)})"

            ' Populate Communication Details and Enable all of the controls
            SetValues(True)

            ' get the user settings
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
                        RVBSim.DesktopLocation = New Point(20, 20)
                    Else
                        RVBSim.DesktopLocation = .location
                    End If
                Else
                    RVBSim.DesktopLocation = New Point(32000, 32000)
                    ' Me.DesktopLocation = New System.Drawing.Point(20, 20)
                End If
                '************************************************************
            End With
            Select Case testSetting.Protocol    '.protocol
                Case "dnp"
                    RVBSim.dnpbutton.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).DnpCommunication(0).Port ' dnpSetting.Port  '.dnpport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.dnpbutton)
                Case "modbus"
                    RVBSim.modbusbox.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).ModbusCommunication(0).Port ' modbusRegister.Port  '.mdport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.modbusbox)
                Case "iec"
                    RVBSim.iec61850box.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).IECCommunication(0).Port ' iecSetting.Port '.iecport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.iec61850box)
            End Select
            RVBSim.ReadIpAddr.Text = testSetting.readIpAddress  '.IPAddressToRead
            RVBSim.WriteIpAddr.Text = testSetting.writeIpAddress
            RVBSim.RRVBScaleReg1.Value = testSetting.RevRVBVoltageScale
            RVBSim.FRVBScaleReg1.Value = testSetting.FwdRVBVoltageScale

            '
        Catch ex As Exception
            Dim message As String = $"{Now}: ({NameOf(Populatetheform)}) {vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Populatetheform)}")
        End Try
    End Sub

End Module
