Imports System.Threading

Module GenerateRVBVoltage

    ''' <summary>
    ''' generates a RVB voltage to send to the unit
    ''' </summary>
    ''' <param name="rvbForm">main form</param>
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim)

        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)}")

        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

            Select Case rvbForm.SettingsUsefixedReg1.Checked
                Case False
                    ActualLocalVoltage = LocalVoltageReadresult / BecoCommunicationScaleFactor
                    ActualSourceVoltage = SourceVoltageReadresult / BecoCommunicationScaleFactor

                    If Not ActualLocalVoltage = 0.0 Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(rvbForm.SettingsFwdrvbvoltageReg1.Value))
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                    End If

                    If Not ActualSourceVoltage = 0.0 Then
                        Reverse_RVBVoltage2OperateWith = (ActualSourceVoltage + CDbl(rvbForm.SettingsRevrvbvoltageReg1.Value))
                    Else
                        Reverse_RVBVoltage2OperateWith = 0.0
                    End If

                Case True
                    Forward_RVBVoltage2OperateWith = CDbl(rvbForm.SettingsFwdrvbvoltageReg1.Value)
                    Reverse_RVBVoltage2OperateWith = CDbl(rvbForm.SettingsRevrvbvoltageReg1.Value)

            End Select

            Forward_RVBVoltage2OperateWith *= CDbl(rvbForm.SettingsFwdscalefactorReg1.Value)
            Reverse_RVBVoltage2OperateWith *= CDbl(rvbForm.SettingsRevscalefactorReg1.Value)

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith

            Debug.WriteLine("wait here.")

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub


    ''' <summary>
    ''' generates a RVB voltage to send to the unit
    ''' </summary>
    ''' <param name="rvbForm">main form</param>
    Friend Sub GenerateRVBVoltage2Transfer(ByRef rvbForm As RVBSim, ByVal regulatorNumber As Integer)

        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(GenerateRVBVoltage2Transfer)} -- regulator: {regulatorNumber}")

        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0


            Dim fwdVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"FwdDeltaVoltageReg{regulatorNumber}"))(0)

            Dim revVoltage = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"RevDeltaVoltageReg{regulatorNumber}"))(0)

            Dim useFixedVoltage = rvbForm.RVBSettings.GetChildControls(Of RadioButton)().Where(Function(tb) tb.Name.Equals($"useFixedVoltageReg{regulatorNumber}"))(0)

            Dim fwdScale = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"FRVBScaleReg{regulatorNumber}"))(0)

            Dim revScale = rvbForm.RVBSettings.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"RRVBScaleReg{regulatorNumber}"))(0)

            Select Case useFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = LocalVoltageReadresult / BecoCommunicationScaleFactor
                    ActualSourceVoltage = SourceVoltageReadresult / BecoCommunicationScaleFactor

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
            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith

            Debug.WriteLine($"Fwd: {Forward_RVBVoltage2Write}, Rev: {Reverse_RVBVoltage2Write}")
            Debug.WriteLine("wait here.")

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub

End Module
