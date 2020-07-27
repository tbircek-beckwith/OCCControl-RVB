Imports System.Threading

Namespace Communication.Operations

    ''' <summary>
    ''' Pauses communication before stopping it completely
    ''' </summary>
    Public Class PauseOperation

        Protected Friend Sub Pause()

            Try

                Dim Disconnect As New ManualResetEvent(False)

                For i = 0 To WriteRegisterWaits.Count - 1   ' SupportedRegulatorNumber - 1

                    WritingTimers(i).Reset()
                    WriteTickerDones(i).Reset()
                    WriteRegisterWaits(i).Unregister(Nothing)

                    ' reset measurements
                    Interlocked.Exchange(LocalVoltageReadings(i), UShort.MaxValue)
                    Interlocked.Exchange(SourceVoltageReadings(i), UShort.MaxValue)
                    Interlocked.Exchange(FwdRVBVoltages2Write(i), UShort.MaxValue)
                    Interlocked.Exchange(RevRVBVoltages2Write(i), UShort.MaxValue)
                    Interlocked.Exchange(HeartBeatTimers(i), 120)

                Next

                If ProtocolInUse = "modbus" Then
                    'disconnect modbus
                    modbusRead.Disconnect()
                    modbusWrite.Disconnect()

                ElseIf ProtocolInUse = "dnp" Then
                    dnp.Disconnect(Disconnect)
                    Disconnect.WaitOne()
                    dnp.Dispose()
                ElseIf ProtocolInUse = "iec" Then
                    iec61850.Disconnect(Disconnect)
                    Disconnect.WaitOne()
                    iec61850.Dispose()
                End If

                ReadingTimer.Reset()
                ReadTickerDone.Reset()
                ReadRegisterWait.Unregister(Nothing)

                TimersEvent.Dispose()
                Disconnect.Dispose()

                SetEnable(RVBSim.StopButton, False)
                SetEnable(RVBSim.StartButton, True)

                Disenable()

                'if no errors show comm stop msg
                If ReceivedErrorMsg = "None" Then
                    ' SetText(RVBSim.lblMsgCenter, "Comm stopped ...")
                    SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:="Communication stops...")
                    sb.AppendLine($"{Now} Successfully disconnected")
                Else
                    SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=$"Failed to stop communication...{vbCrLf}{ReceivedErrorMsg}")
                    sb.AppendLine($"{Now} Disconnect failed {ReceivedErrorMsg}")
                End If

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Pause)}")

            Catch ex As Exception
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                ' SetText(RVBSim.lblMsgCenter, message)
                SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
                sb.AppendLine(message)

            Finally

                ResetMeteringLabels()
            End Try
        End Sub
    End Class
End Namespace