Imports System.Threading

Module TextOperations

    Private Delegate Sub SetTextDelegate(ByVal [label] As Label, ByVal [text] As String)
    
    Friend Sub SetText(ByVal [label] As Label, ByVal [text] As String)

        Try
            If [label].InvokeRequired Then
                Dim del As New SetTextDelegate(AddressOf SetText)
                [label].Invoke(del, New Object() {[label], [text]})
            Else
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} SetText --- Text is {1}", Thread.CurrentThread.GetHashCode, [text])
                [label].Text = [text]
            End If
        Catch ex As Exception
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

End Module