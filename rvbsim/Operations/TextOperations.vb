Imports System.Threading

Module TextOperations

    Private Delegate Sub SetTextDelegate(ByVal [label] As Label, ByVal [text] As String)
    Private Delegate Sub SetTextBoxDelegate(ByVal textbox As TextBox, ByVal text As String)

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
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub

    Friend Sub SetTextBox(ByVal textbox As TextBox, ByVal text As String)
        Try
            If textbox.InvokeRequired Then
                Dim del As New SetTextBoxDelegate(AddressOf SetTextBox)
                textbox.Invoke(del, New Object() {textbox, text})
            Else
                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(SetTextBox)} --- Text is {text}")
                textbox.Text = text
            End If
        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub
End Module