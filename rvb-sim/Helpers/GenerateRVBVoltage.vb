Imports System.Threading

Module GenerateRVBVoltage

    ''' generates a RVB voltage to send to the unit
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim)
        If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} GenerateRVBVoltage2Transfer", Thread.CurrentThread.GetHashCode)

        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

            Select Case rvbForm.radUseFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = readresult / M2001D_Comm_Scale
                    If Not ActualLocalVoltage = 0.0 Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(rvbForm.FwdDeltaVoltage.Value))
                        Reverse_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(rvbForm.RevDeltaVoltage.Value))
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                        Reverse_RVBVoltage2OperateWith = 0.0
                    End If
                Case True
                    Forward_RVBVoltage2OperateWith = CDbl(rvbForm.FwdDeltaVoltage.Value)
                    Reverse_RVBVoltage2OperateWith = CDbl(rvbForm.RevDeltaVoltage.Value)
            End Select
            Forward_RVBVoltage2OperateWith *= CDbl(rvbForm.FwdRVBScaleFactor.Value)
            Reverse_RVBVoltage2OperateWith *= CDbl(rvbForm.RevRVBScaleFactor.Value)

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith

        Catch ex As Exception

            SetText(rvbForm.lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

End Module
