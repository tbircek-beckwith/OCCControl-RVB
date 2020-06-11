Imports System.Threading

Module TextOperations

    Private Delegate Sub SetTextDelegate(ByVal [label] As Label, ByVal [text] As String)
    
    Friend Sub SetText(ByVal [label] As Label, ByVal [text] As String)

        Try
            If [label].InvokeRequired Then
                Dim del As New SetTextDelegate(AddressOf SetText)
                [label].Invoke(del, New Object() {[label], [text]})
            Else
                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(SetText)} --- Text is {[text]}")
                [label].Text = [text]
            End If
        Catch ex As Exception
            sb.AppendLine($"{Now} {ex.Message}")
        End Try
    End Sub

End Module