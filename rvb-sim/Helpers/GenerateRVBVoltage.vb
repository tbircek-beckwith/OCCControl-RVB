Imports System.Threading

Module GenerateRVBVoltage

    ''' <summary>
    ''' generates a RVB voltage to send to the unit
    ''' </summary>
    ''' <param name="rvbForm"></param>
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim)

        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)}")

        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

            Select Case rvbForm.useFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = Readresult / M2001D_Comm_Scale
                    If Not ActualLocalVoltage = 0.0 Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(rvbForm.FwdDeltaVoltageReg1.Value))
                        Reverse_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(rvbForm.RevDeltaVoltageReg1.Value))
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                        Reverse_RVBVoltage2OperateWith = 0.0
                    End If
                Case True
                    Forward_RVBVoltage2OperateWith = CDbl(rvbForm.FwdDeltaVoltageReg1.Value)
                    Reverse_RVBVoltage2OperateWith = CDbl(rvbForm.RevDeltaVoltageReg1.Value)
            End Select
            Forward_RVBVoltage2OperateWith *= CDbl(rvbForm.FRVBScaleReg1.Value)
            Reverse_RVBVoltage2OperateWith *= CDbl(rvbForm.RevRVBScaleFactorReg1.Value)

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith

        Catch ex As Exception

            SetText(rvbForm.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        End Try
    End Sub

End Module
