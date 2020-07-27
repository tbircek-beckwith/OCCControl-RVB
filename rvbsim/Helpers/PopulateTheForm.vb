Imports System.IO
Imports System.Reflection
Imports System.Threading

Module Populate

    ''' <summary>
    ''' Reads a settings.json file and populates form with read information.
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

            Dim isProtocolValid As Boolean = True

            baseJsonSettings = jsonRead.GetSettings(Of JsonRoot)(Path.Combine(path1:=BaseJsonSettingsFileLocation,
                                                                                 path2:=$"{SettingFileName}.json"))
            If IsNothing(baseJsonSettings) Then

                MessageBox.Show(text:=$"Please check settings.json file values.", caption:="Unsupported data", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
                isProtocolValid = False

                Return
            End If

            baseJsonSettingsRegulators = baseJsonSettings.Test

            ' populate RVBSim controls
            PopulateControls(rvbForm:=RVBSim, regulators:=baseJsonSettingsRegulators, enable:=True, isSetting:=True)

            Select Case baseJsonSettings.Protocol
                Case "dnp"
                    RVBSim.dnpbutton.PerformClick()

                Case "modbus"
                    RVBSim.modbusbox.PerformClick()

                Case "iec"
                    RVBSim.iec61850box.PerformClick()

                Case Else
                    MessageBox.Show(text:=$"Please check Settings.json file protocol value.{vbCrLf}Read value: {testSetting.Protocol} <- not accurate", caption:="Unsupported Protocol", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
                    isProtocolValid = False
                    Exit Select
            End Select

            ' is protocol accurate?
            If isProtocolValid Then

                ' Populate Communication Details and Enable all of the controls
                SetValuesFromJson(True)

            End If

        Catch ex As Exception
            Dim message As String = $"{Now}: ({NameOf(Populatetheform)}) {vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)
        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Populatetheform)} -- ENDS")
        End Try
    End Sub

End Module
