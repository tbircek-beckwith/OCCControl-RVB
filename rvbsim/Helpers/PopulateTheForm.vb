Imports System.IO
Imports System.Reflection
Imports System.Threading

Module Populate

    ''' <summary>
    ''' Reads a Settings.xml file and populates form with read information.
    ''' </summary>
    Friend Sub Populatetheform()
        Try
            '' Read Settings.xml file
            'Dim xmlRead As New ReadXmlFile
            'xmlRead.Read()

            Dim jsonRead As New JsonFile()

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

            'Select Case testSetting.Protocol    '.protocol

            baseJsonTestSettings = baseJsonSettings.Test.Regulator

            Select Case baseJsonSettings.Protocol
                Case "dnp"
                    testJsonValues = jsonRead.GetSettings(Of DnpProtocolSettingsModel)(Path.Combine(path1:=baseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))

                    RVBSim.dnpbutton.Checked = True
                    'RVBSim.PortReg1.Text = testJsonValues.Port ' dnpSetting.Port  '.dnpport
                   ' RVBSim.CheckHandler(RVBSim.dnpbutton)
                Case "modbus"
                    testJsonValues = jsonRead.GetSettings(Of ModbusProtocolSettingsModel)(Path.Combine(path1:=baseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))
                    RVBSim.modbusbox.Checked = True
                    'RVBSim.PortReg1.Text = Regulators(0).ModbusCommunication(0).Port ' modbusRegister.Port  '.mdport
                    'Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    ' RVBSim.CheckHandler(RVBSim.modbusbox)
                Case "iec"
                    testJsonValues = jsonRead.GetSettings(Of IecProtocolSettingsModel)(Path.Combine(path1:=baseJsonSettingsFileLocation, path2:=$"Settings-{baseJsonSettings.Protocol}.json"))

                    RVBSim.iec61850box.Checked = True
                    ' RVBSim.PortReg1.Text = Regulators(0).IECCommunication(0).Port ' iecSetting.Port '.iecport
                    ' Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    ' RVBSim.CheckHandler(RVBSim.iec61850box)
            End Select

            RVBSim.PortReg1.Text = testJsonValues.Port ' dnpSetting.Port  '.dnpport
            Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")

            RVBSim.ReadIpAddr.Text = baseJsonSettings.Read ' testSetting.readIpAddress  '.IPAddressToRead
            RVBSim.WriteIpAddr.Text = baseJsonSettings.Write ' testSetting.writeIpAddress
            RVBSim.RRVBScaleReg1.Value = baseJsonTestSettings(0).RevScaleFactor(0).Value ' testSetting.RevRVBVoltageScale
            RVBSim.FRVBScaleReg1.Value = baseJsonTestSettings(0).FwdScaleFactor(0).Value ' testSetting.FwdRVBVoltageScale

            Debug.WriteLine("delete here")
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
