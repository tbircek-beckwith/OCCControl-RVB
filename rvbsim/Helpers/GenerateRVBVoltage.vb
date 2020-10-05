Imports System.Threading

Module GenerateRVBVoltage

    Private Property ControlId As Integer = 0D

    ''' <summary>
    ''' generates a RVB voltage to send to the unit
    ''' </summary>
    ''' <param name="rvbForm">main form</param>
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim, ByVal regulatorNumber As Integer)


        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)} -- regulator: {regulatorNumber} -- STARTS")

        Try

            ' Dim controlId As Integer = 0D ' = regulatorNumber + 1
            Dim Forward_RVBVoltage2OperateWith As Decimal = 0.0D
            Dim Reverse_RVBVoltage2OperateWith As Decimal = 0.0D

            Interlocked.Exchange(ControlId, regulatorNumber + 1)
            Dim fwdVoltage = CType(rvbForm.Controls.Find($"SettingsFwdRVBVoltageReg{ControlId}", True)(0), NumericUpDown)
            Dim revVoltage = CType(rvbForm.Controls.Find($"SettingsRevRVBVoltageReg{ControlId}", True)(0), NumericUpDown)
            Dim useFixedVoltage = CType(rvbForm.Controls.Find($"SettingsUsefixedReg{ControlId}", True)(0), RadioButton)
            Dim fwdScale = CType(rvbForm.Controls.Find($"SettingsFwdScaleFactorReg{ControlId}", True)(0), NumericUpDown)
            Dim revScale = CType(rvbForm.Controls.Find($"SettingsRevScaleFactorReg{ControlId}", True)(0), NumericUpDown)

            Select Case useFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = LocalVoltageReadings(regulatorNumber) / BecoCommunicationScaleFactor
                    ActualSourceVoltage = SourceVoltageReadings(regulatorNumber) / BecoCommunicationScaleFactor

                    If Not ActualLocalVoltage = 0.0D Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + fwdVoltage.Value)
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                    End If

                    If Not ActualSourceVoltage = 0.0D Then
                        Reverse_RVBVoltage2OperateWith = (ActualSourceVoltage + revVoltage.Value)
                    Else
                        Reverse_RVBVoltage2OperateWith = 0.0D
                    End If

                Case True
                    Forward_RVBVoltage2OperateWith = fwdVoltage.Value
                    Reverse_RVBVoltage2OperateWith = revVoltage.Value

            End Select

            Forward_RVBVoltage2OperateWith *= fwdScale.Value
            Reverse_RVBVoltage2OperateWith *= revScale.Value

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Interlocked.Exchange(FwdRVBVoltages2Write.Item(regulatorNumber), Forward_RVBVoltage2Write)

            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith
            Interlocked.Exchange(RevRVBVoltages2Write.Item(regulatorNumber), Reverse_RVBVoltage2Write)

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)

        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)} -- regulator: {regulatorNumber} --- ENDS")

        End Try
    End Sub

End Module
