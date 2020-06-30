Imports System.IO
Imports System.Reflection
Imports System.Threading

Module Populate

    ''' <summary>
    ''' Reads a Settings.xml file and populates form with read information.
    ''' </summary>
    Friend Sub Populatetheform()
        Try

            ' update the form title
            RVBSim.Text = $"RVB Simulator({Assembly.GetEntryAssembly().GetName().Version.ToString(3)})"

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

            jsonRead = New JsonFile()

            Dim isProtocolValid As Boolean = True

            Select Case baseJsonSettings.Protocol
                Case "dnp"
                    testJsonValues = jsonRead.GetSettings(Of DnpProtocolSettingsModel)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))

                    RVBSim.dnpbutton.Checked = True
                    RVBSim.PortReg1.Text = CType(testJsonValues, DnpProtocolSettingsModel).Port ' Regulators(0).DnpCommunication(0).Port 'dnpSetting.Port  '.dnpport
                    RVBSim.CheckHandler(RVBSim.dnpbutton)
                Case "modbus"
                    testJsonValues = jsonRead.GetSettings(Of ModbusProtocolSettingsModel)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))
                    RVBSim.modbusbox.Checked = True
                    RVBSim.PortReg1.Text = CType(testJsonValues, ModbusProtocolSettingsModel).Port ' Regulators(0).ModbusCommunication(0).Port ' modbusRegister.Port  '.mdport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.modbusbox)
                Case "iec"
                    testJsonValues = jsonRead.GetSettings(Of IecProtocolSettingsModel)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))
                    RVBSim.iec61850box.Checked = True
                    RVBSim.PortReg1.Text = CType(testJsonValues, IecProtocolSettingsModel).Port ' Regulators(0).IECCommunication(0).Port ' iecSetting.Port '.iecport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.iec61850box)
                Case Else
                    MessageBox.Show(text:=$"Please check Settings.json file protocol value.{vbCrLf}Read value: {testSetting.Protocol} <- not accurate", caption:="Unsupported Protocol", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
                    isProtocolValid = False
                    Exit Select
            End Select

            ' is protocol accurate?
            If isProtocolValid Then

                ' TODO: Start using Json file
                ' Populate Communication Details and Enable all of the controls
                SetValuesFromJson(True)
                ' SetValues(True)

                'RVBSim.PortReg1.Text = testJsonValues.Port ' dnpSetting.Port  '.dnpport
                'Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")

                RVBSim.ReadIpAddr.Text = testSetting.readIpAddress  '.IPAddressToRead
                RVBSim.WriteIpAddr.Text = testSetting.writeIpAddress
                RVBSim.RRVBScaleReg1.Value = testSetting.RevRVBVoltageScale
                RVBSim.FRVBScaleReg1.Value = testSetting.FwdRVBVoltageScale

            End If

        Catch ex As Exception
            Dim message As String = $"{Now}: ({NameOf(Populatetheform)}) {vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Populatetheform)}")
        End Try
    End Sub

End Module
