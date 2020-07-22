
''' <summary>
''' Calculate RVBVoltage by fixed or a relative value.
''' </summary>
Public Class RelativeOrFixedValue

    ''' <summary>
    ''' decide if selected the regulator's RVB values fixed or relative
    ''' </summary>
    ''' <param name="rvbForm">the active form</param>
    ''' <param name="sender">RadioButton the user clicked</param>
    Public Sub Decide(ByRef rvbForm As RVBSim, ByVal sender As RadioButton)

        If sender Is Nothing Or String.IsNullOrWhiteSpace(sender.Name) Then
            Debug.WriteLine("Sender is not a RadioButton")
            Return
        End If

        Dim regulatorNumber As Int16 = If(String.IsNullOrWhiteSpace(sender.Name), 1, Char.GetNumericValue(sender.Name.Last()))
        Dim isFixed As Boolean = sender.Name.Contains("fixed")

        Dim fwdLabel = CType(rvbForm.Controls.Find($"FwdVoltageLabelReg{regulatorNumber}", True)(0), Label)
        Dim revLabel = CType(rvbForm.Controls.Find($"RevVoltageLabelReg{regulatorNumber}", True)(0), Label)
        Dim fwdVoltage = CType(rvbForm.Controls.Find($"SettingsFwdRVBVoltageReg{regulatorNumber}", True)(0), NumericUpDown)
        Dim revVoltage = CType(rvbForm.Controls.Find($"SettingsRevRVBVoltageReg{regulatorNumber}", True)(0), NumericUpDown)
        Dim rvbMin = CType(rvbForm.Controls.Find($"SettingsRvbminReg{regulatorNumber}", True)(0), NumericUpDown)
        Dim rvbMax = CType(rvbForm.Controls.Find($"SettingsRvbmaxReg{regulatorNumber}", True)(0), NumericUpDown)

        Select Case sender.Text
            Case $"Use Relative"
                fwdLabel.Text = DeltaMessage
                revLabel.Text = DeltaMessageSource
                fwdVoltage.Minimum = MinDeltaVoltage
                fwdVoltage.Maximum = MaxDeltaVoltage
                revVoltage.Minimum = MinDeltaVoltage
                revVoltage.Maximum = MaxDeltaVoltage

            Case $"Use Absolute"
                fwdLabel.Text = DirectMessage
                revLabel.Text = DirectMessageSource
                fwdVoltage.Minimum = rvbMin.Value
                fwdVoltage.Maximum = rvbMax.Value
                revVoltage.Minimum = rvbMin.Value
                revVoltage.Maximum = rvbMax.Value

        End Select
    End Sub
End Class
