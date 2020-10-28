


Module SetRelativeChecks

    Public Sub SetRelative(regulatorId As Integer)

        Try

            '    RVBSim.SettingsUsefixedReg1.Checked = True
            '    RVBSim.SettingsUserelativeReg1.Checked = False
            Dim controlNames As String() = {$"SettingsUsefixedReg{regulatorId}", $"SettingsUserelativeReg{regulatorId}"}

            For Each settingControlName In controlNames

                Dim v() As Control = RVBSim.Controls.Find(settingControlName, True)

                If v.Length > 0 Then

                    If v(0).Visible Then

                        Dim relativeOrFixedButton As RadioButton = CType(v(0), RadioButton)

                        'Dim isUseRelative As Boolean = regulator.Value

                        'Dim alternateName As String = $"SettingsUsefixedReg{model.Id}"
                        'Dim useFixed As RadioButton = CType(rvbForm.Controls.Find(alternateName, True)(0), RadioButton)

                        If settingControlName.Contains("fixed") Then
                            relativeOrFixedButton.PerformClick()      ' Checked = isUseRelative ' 
                        Else
                            relativeOrFixedButton.PerformClick()         ' Checked = Not isUseRelative '
                        End If

                    End If
                End If
            Next

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)

        End Try
    End Sub

End Module
