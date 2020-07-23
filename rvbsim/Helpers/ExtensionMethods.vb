
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading

''' <summary>
''' Extension methods
''' </summary>
Public Module ExtensionMethods

    ''' <summary>
    ''' Gets Child controls of the specified container control.
    ''' </summary>
    ''' <typeparam name="T">Container control to get child controls</typeparam>
    ''' <param name="parent">Container</param>
    ''' <returns>Returns a List of Child controls of the specified container control.</returns>
    <Extension()>
    Public Function GetChildControls(Of T As Control)(ByVal parent As Control) As List(Of T)
        Dim result As New List(Of Control)

        For Each control As Control In parent.Controls
            If TypeOf control Is T Then result.Add(control)
            result.AddRange(control.GetChildControls(Of T)())
        Next
        Return result.ToArray().Select(Of T)(Function(arg1) CType(arg1, T)).ToList()
    End Function

    ''' <summary>
    ''' Sets values of controls and enable/disable
    ''' </summary>
    ''' <param name="enable">True sets the control enabled, false disables the control</param>
    <Extension()>
    Public Sub SetValuesFromJson(ByRef enable As Boolean)

        Try

            ' retrieve test settings per selected protocol
            testJsonSettings = jsonRead.GetSettings(Of JsonRoot)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"{SettingFileName}-{ProtocolInUse}.json"))
            testJsonSettingsRegulators = testJsonSettings.Test

            ' populate communication data
            RVBSim.ReadIpAddr.Text = baseJsonSettings.Read
            RVBSim.WriteIpAddr.Text = baseJsonSettings.Write
            RVBSim.PortReg1.Text = testJsonSettings.Port

            ' populate test settings
            PopulateControlsNew(rvbForm:=RVBSim, regulators:=testJsonSettingsRegulators, enable:=True)

        Catch ex As Exception
            Dim trace = New StackTrace(ex, True)
            Dim message As String = $"{Now}:{vbCrLf}Line #: {trace.GetFrame(0).GetFileLineNumber().ToString()}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)

        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(SetValuesFromJson)}")
        End Try

    End Sub

    ''' <summary>
    ''' Populates Controls per selected protocol on boot up.
    ''' </summary>
    ''' <param name="rvbForm">the active form</param>
    ''' <param name="regulators">.json file values</param>
    ''' <param name="enable">is the controls enabled</param>
    ''' <param name="isSetting">is it the setting populating</param>
    Friend Sub PopulateControlsNew(ByRef rvbForm As RVBSim, regulators As JsonTest, enable As Boolean, Optional isSetting As Boolean = False)

        Try

            ' scan the values
            For Each model In regulators.Regulator

                For Each regulator In model.Values  '.Regulator

                    ' stitch the control name
                    Dim controlName As String = String.Empty

                    ' if "NO" Protocol specified use settings.json file value
                    If isSetting Then

                        controlName = $"Settings{regulator.Name}Reg{model.Id}"
                    Else

                        controlName = $"{ProtocolInUse}{regulator.Name}Reg{model.Id}"
                    End If

                    Debug.WriteLine($"Control name: {controlName}, value: {regulator.Value} -- 1st")

                    'If String.Equals("SettingsfwdrvbvoltageReg1", controlName) Then Debugger.Break()
                    'If String.Equals("SettingsfwdrvbvoltageReg2", controlName) Then Debugger.Break()

                    ' find the control name
                    Dim t() As Control = rvbForm.Controls.Find(controlName, True)
                    If t.Length > 0 Then

                        ' assigned values to the control
                        Dim textValue As String = regulator.Value
                        If String.Equals(ProtocolInUse, "iec") Then
                            textValue = $"{regulator.Value}${regulator.Fc}${regulator.Sdi}${regulator.Dai}"
                        End If

                        Select Case t(0).GetType()
                            Case GetType(RadioButton)
                                Dim useRelative As RadioButton = CType(t(0), RadioButton)

                                Dim isUseRelative As Boolean = regulator.Value

                                Dim alternateName As String = $"SettingsUsefixedReg{model.Id}"
                                Dim useFixed As RadioButton = CType(rvbForm.Controls.Find(alternateName, True)(0), RadioButton)

                                If isUseRelative Then
                                    useRelative.PerformClick()      ' Checked = isUseRelative ' 
                                Else
                                    useFixed.PerformClick()         ' Checked = Not isUseRelative '
                                End If

                            Case GetType(NumericUpDown)
                                Dim settingControl As NumericUpDown = CType(t(0), NumericUpDown)

                                If isSetting Then
                                    settingControl.Minimum = -150.0
                                    settingControl.Maximum = 150.0
                                End If
                                settingControl.Value = textValue

                            Case Else
                                t(0).Text = textValue
                        End Select

                        t(0).Enabled = enable
                    End If
                Next
            Next
        Catch ex As Exception
            Dim message As String = $"{Now}: ({NameOf(Populatetheform)}) {vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub

End Module
