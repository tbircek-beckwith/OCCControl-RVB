Imports System.Threading

Module GenerateRVBVoltage

    ''' <summary>
    ''' generates a RVB voltage to send to the unit
    ''' </summary>
    ''' <param name="rvbForm">main form</param>
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim, ByVal regulatorNumber As Integer)

        Dim controlId = regulatorNumber + 1

        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)} -- regulator: {regulatorNumber} -- STARTS")

        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

            Dim fwdVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsFwdRVBVoltageReg{controlId}"))(0)

            Dim revVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsRevRVBVoltageReg{controlId}"))(0)

            Dim useFixedVoltage = rvbForm.RVBSettings.GetChildControls(Of RadioButton)().Where(Function(tb) tb.Name.Equals($"SettingsUsefixedReg{controlId}"))(0)

            Dim fwdScale = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsFwdScaleFactorReg{controlId}"))(0)

            Dim revScale = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"SettingsRevScaleFactorReg{controlId}"))(0)

            Select Case useFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = LocalVoltageReadings(regulatorNumber) / BecoCommunicationScaleFactor
                    ActualSourceVoltage = SourceVoltageReadings(regulatorNumber) / BecoCommunicationScaleFactor

                    If Not ActualLocalVoltage = 0.0 Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(fwdVoltage.Value))
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                    End If

                    If Not ActualSourceVoltage = 0.0 Then
                        Reverse_RVBVoltage2OperateWith = (ActualSourceVoltage + CDbl(revVoltage.Value))
                    Else
                        Reverse_RVBVoltage2OperateWith = 0.0
                    End If

                Case True
                    Forward_RVBVoltage2OperateWith = CDbl(fwdVoltage.Value)
                    Reverse_RVBVoltage2OperateWith = CDbl(revVoltage.Value)

            End Select

            Forward_RVBVoltage2OperateWith *= CDbl(fwdScale.Value)
            Reverse_RVBVoltage2OperateWith *= CDbl(revScale.Value)

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Interlocked.Exchange(FwdRVBVoltages2Write.Item(regulatorNumber), Forward_RVBVoltage2Write)

            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith
            Interlocked.Exchange(RevRVBVoltages2Write.Item(regulatorNumber), Reverse_RVBVoltage2Write)

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)

        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)} -- regulator: {regulatorNumber} --- ENDS")

        End Try
    End Sub

End Module
