Imports System.Threading

Namespace PeriodicOperations

    ''' <summary>
    ''' generates periodic reset events
    ''' </summary>
    Public Class ResetEvents

        Protected Friend Sub Timers(ByRef rvbForm As RVBSim)

            Try
                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} ResetTimers.ErrorCounter(old): {Interlocked.Read(errorCounter)}")

                If Not Interlocked.Read(errorCounter) >= 10 Then

                    ReadRegisterWait.Unregister(Nothing)
                    WriteRegisterWait.Unregister(Nothing)

                    ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf rvbForm.PeriodicReadEvent), Nothing, ReadInterval, False)

                    WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf rvbForm.PeriodicWriteEvent), Nothing, WriteInterval, False)

                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} ResetTimers.ErrorCounter(new): {Interlocked.Read(errorCounter)}")
                Else

                    pause.Pause()
                    Throw New CustomExceptions("Too many errors")

                End If

            Catch ex As Exception
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)
            End Try
        End Sub
    End Class
End Namespace
