Imports System.Threading

Namespace Communication.Operations

    Public Class PauseOperation

        Protected Friend Sub Pause()

            Dim Disconnect As New ManualResetEvent(False)

            Try
                WriteRegisterWait.Unregister(Nothing)
                ReadRegisterWait.Unregister(Nothing)

                If ProtocolInUse = "modbus" Then
                    modbus.Disconnect(Disconnect)
                    Disconnect.WaitOne()
                    modbus.Dispose()
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

                Heart_Beat_Timer = 0
                readresult = 0
                Forward_RVBVoltage2Write = 0.0
                Reverse_RVBVoltage2Write = 0.0
                SetEnable(RVBSim.btnStop, False)
                SetEnable(RVBSim.btnStart, True)

                Disenable()

                'if no errors show comm stop msg
                If ReceivedErrorMsg = "None" Then
                    SetText(RVBSim.lblMsgCenter, "Comm stopped ...")
                    sb.AppendLine(String.Format("{0} Successfully disconnected", Now))
                Else
                    sb.AppendLine(String.Format("{0} Disconnect failed {1}", Now, ReceivedErrorMsg))
                End If
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Pause", Thread.CurrentThread.GetHashCode)
                SetText(RVBSim.lblLocalVoltageValue, String.Format(""))
                SetText(RVBSim.lblFwdRVBValue, String.Format(""))
                SetText(RVBSim.lblRevRVBValue, String.Format(""))

            Catch ex As Exception

            End Try
        End Sub

    End Class
End Namespace