Imports System.Threading.Tasks
Imports Automatak.DNP3.Interface

Public Class StringCallback
    Implements ITaskCallback

    ReadOnly tcs As TaskCompletionSource(Of TaskCompletion) = New TaskCompletionSource(Of TaskCompletion)

    Public ReadOnly Property Task As Task(Of TaskCompletion)

    Public Sub OnStart() Implements ITaskCallback.OnStart
        'Do nothing extra at OnStart()
    End Sub

    Public Sub OnComplete(result As TaskCompletion) Implements ITaskCallback.OnComplete
        tcs.SetResult(result)
    End Sub

    Public Sub OnDestroyed() Implements ITaskCallback.OnDestroyed
        'Do nothing extra at OnStart()
    End Sub
End Class
