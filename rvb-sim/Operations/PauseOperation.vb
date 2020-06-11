Imports System.Threading

Namespace Communication.Operations

    ''' <summary>
    ''' Pauses communication before stopping it completely
    ''' </summary>
    Public Class PauseOperation

        Protected Friend Sub Pause()

            Dim Disconnect As New ManualResetEvent(False)

            Try
                WriteRegisterWait.Unregister(Nothing)
                ReadRegisterWait.Unregister(Nothing)

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

                TimersEvent.Dispose()
                Disconnect.Dispose()

                Interlocked.Exchange(Heart_Beat_Timer, 0)
                LocalVoltageReadresult = 0
                SourceVoltageReadresult = 0
                Forward_RVBVoltage2Write = 0.0
                Reverse_RVBVoltage2Write = 0.0
                SetEnable(RVBSim.StopButton, False)
                SetEnable(RVBSim.StartButton, True)

                Disenable()

                'if no errors show comm stop msg
                If ReceivedErrorMsg = "None" Then
                    SetText(RVBSim.lblMsgCenter, "Comm stopped ...")
                    sb.AppendLine($"{Now} Successfully disconnected")
                Else
                    sb.AppendLine($"{Now} Disconnect failed {ReceivedErrorMsg}")
                End If

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Pause)}")
                SetText(RVBSim.lblLocalVoltageValue, String.Format(""))
                SetText(RVBSim.lblFwdRVBValue, String.Format(""))
                SetText(RVBSim.lblRevRVBValue, String.Format(""))

            Catch ex As Exception

            End Try
        End Sub

    End Class
End Namespace