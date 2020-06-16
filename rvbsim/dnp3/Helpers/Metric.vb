''' <summary>
''' 
''' </summary>
Public Class Metric
    Public Sub New(ByVal id As String, ByVal value As String)
        Me.Id = id
        Me.Value = value
    End Sub

    Public ReadOnly Property Id As String

    Public ReadOnly Property Value As String

End Class
