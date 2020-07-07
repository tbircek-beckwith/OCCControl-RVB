
''' <summary>
''' Calculate RVBVoltage by fixed or a relative value.
''' </summary>
Public Class RelativeOrFixedValue

    ''' <summary>
    ''' decide if selected the regulator's RVB values fixed or relative
    ''' </summary>
    ''' <param name="sender">RadioButton the user clicked</param>
    Public Sub Decide(ByRef rvbForm As RVBSim, ByVal sender As RadioButton)

        Dim rb As RadioButton = TryCast(sender, RadioButton)

        If rb Is Nothing Or String.IsNullOrWhiteSpace(rb.Name) Then
            Debug.WriteLine("Sender is not a RadioButton")
            Return
        End If

        '  If rb.Checked Then

        Dim regulatorNumber As Int16 = If(String.IsNullOrWhiteSpace(rb.Name), 1, Char.GetNumericValue(rb.Name.Last()))
            Dim isFixed As Boolean = rb.Name.Contains("fixed")

            Dim fwdLabel = rvbForm.RVBSettings.GetChildControls(Of Label)().Where(Function(tb) tb.Name.Equals($"FwdVoltageLabelReg{regulatorNumber}"))(0)

            Dim revLabel = rvbForm.RVBSettings.GetChildControls(Of Label)().Where(Function(tb) tb.Name.Equals($"RevVoltageLabelReg{regulatorNumber}"))(0)

            Dim fwdVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsFwdrvbvoltageReg{regulatorNumber}"))(0)

            Dim revVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsRevrvbvoltageReg{regulatorNumber}"))(0)

            Dim rvbMin = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsRvbminReg{regulatorNumber}"))(0)

            Dim rvbMax = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsRvbmaxReg{regulatorNumber}"))(0)



            Select Case rb.Text
                Case $"Use Relative"
                    fwdLabel.Text = DeltaMessage
                    revLabel.Text = DeltaMessageSource
                    fwdVoltage.Minimum = MinDeltaVoltage
                    fwdVoltage.Maximum = MaxDeltaVoltage
                    fwdVoltage.Value = 0.0
                    revVoltage.Minimum = MinDeltaVoltage
                    revVoltage.Maximum = MaxDeltaVoltage
                    revVoltage.Value = 0.0

                Case $"Use Absolute"
                    fwdLabel.Text = DirectMessage
                    revLabel.Text = DirectMessageSource
                    fwdVoltage.Minimum = rvbMin.Value 'MinSpecValue
                    fwdVoltage.Maximum = rvbMax.Value 'MaxSpecValue
                    revVoltage.Minimum = rvbMin.Value 'MinSpecValue
                    revVoltage.Maximum = rvbMax.Value 'MaxSpecValue
                    If LocalVoltageReadresult / BecoCommunicationScaleFactor >= fwdVoltage.Minimum Then fwdVoltage.Value = LocalVoltageReadresult / BecoCommunicationScaleFactor Else fwdVoltage.Value = fwdVoltage.Maximum
                    If SourceVoltageReadresult / BecoCommunicationScaleFactor >= revVoltage.Minimum Then revVoltage.Value = SourceVoltageReadresult / BecoCommunicationScaleFactor Else revVoltage.Value = revVoltage.Minimum
            End Select
        ' End If

        Debug.WriteLine("whoowhoo")
    End Sub
End Class
