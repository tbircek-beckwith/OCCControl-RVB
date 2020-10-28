


Module SetVoltageOffsetControls

    Public Sub SetVoltageOffset(keyValue As String, powerDirection As String, regulatorId As Integer)

        Try

            ' Decimal.TryParse(value.Value, RVBSim.SettingsFwdRVBVoltageReg1.Value)
            '
            '' setting1 = RVBSim.SettingsFwdRVBVoltageReg1.Value
            '' settings2 = RVBSim.SettingsRevRVBVoltageReg1.Value

            Dim settingControlName As String = $"Settings{powerDirection}RVBVoltageReg{regulatorId}"

            Dim v() As Control = RVBSim.Controls.Find(settingControlName, True)

            If v.Length > 0 Then

                If v(0).Visible Then

                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)
                    Decimal.TryParse(keyValue, registerBox.Value)
                End If
            End If


        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)

        End Try
    End Sub

End Module
