Imports System.Threading

Namespace PeriodicOperations

    ''' <summary>generates periodic reset events</summary>
    Public Class ResetEvents

        Protected Friend Sub Timers(ByRef rvbForm As RVBSim)

            Try
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} ResetTimers{1} ---------------------------- errorCounter: {2}", Thread.CurrentThread.GetHashCode, vbCrLf, Interlocked.Read(errorCounter))

                If Not Interlocked.Read(errorCounter) >= 10 Then

                    ReadRegisterWait.Unregister(Nothing)
                    WriteRegisterWait.Unregister(Nothing)

                    ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf rvbForm.PeriodicReadEvent), Nothing, ReadInterval, False)

                    WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf rvbForm.PeriodicWriteEvent), Nothing, WriteInterval, False)

                    Debug.WriteLine("Current thread is # {0} ResetTimers{1} ---------------------------- errorCounter: {2}", Thread.CurrentThread.GetHashCode, vbCrLf, Interlocked.Read(errorCounter))
                Else

                    pause.Pause()
                    Throw New CustomExceptions("Too many errors")

                End If

            Catch ex As Exception
                SetText(rvbForm.lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            End Try
        End Sub
    End Class
End Namespace
