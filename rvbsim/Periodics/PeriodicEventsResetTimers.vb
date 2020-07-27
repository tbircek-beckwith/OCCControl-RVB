Imports System.Threading

Namespace PeriodicOperations

    ''' <summary>
    ''' generates periodic reset events
    ''' </summary>
    Public Class ResetEvents

        Protected Friend Sub Timers(ByRef rvbForm As RVBSim, Optional regulatorID As Object = Nothing)

            'If Val(regulatorID) < 0 Or Val(regulatorID) > 2 Or IsNothing(regulatorID) Then
            '    Debugger.Break()
            'End If

            Try

                Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(Timers)} is running... Regulator: {regulatorID}")

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} ResetTimers.ErrorCounter(old): {Interlocked.Read(errorCounter)}")

                If Not Interlocked.Read(errorCounter) >= 10 Then

                    ReadRegisterWait.Unregister(Nothing)
                    WriteRegisterWait.Unregister(Nothing)

                    ReadTickerDone.Reset()

                    ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(waitObject:=ReadTickerDone,
                                                                              callBack:=New WaitOrTimerCallback(AddressOf rvbForm.PeriodicReadEvent),
                                                                              state:=regulatorID,
                                                                              millisecondsTimeOutInterval:=ReadInterval,
                                                                              executeOnlyOnce:=False)

                    ReadingTimer.Restart()

                    WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(waitObject:=WriteTickerDone,
                                                                               callBack:=New WaitOrTimerCallback(AddressOf rvbForm.PeriodicWriteEventNew),
                                                                               state:=regulatorID,
                                                                               millisecondsTimeOutInterval:=WriteIntervals(regulatorID),
                                                                               executeOnlyOnce:=False)

                    WritingTimers(regulatorID).Restart()

                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} ResetTimers.ErrorCounter(new): {Interlocked.Read(errorCounter)}")
                Else

                    pause.Pause()
                    Throw New CustomExceptions("Too many errors")

                End If

            Catch ex As Exception
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                ' SetText(rvbForm.lblMsgCenter, message)
                SetTextBox(textbox:=rvbForm.ErrorsTextBox, text:=message)
                sb.AppendLine(message)
            End Try
        End Sub
    End Class
End Namespace
