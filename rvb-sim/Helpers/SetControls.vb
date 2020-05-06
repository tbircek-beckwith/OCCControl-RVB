Module SetControls

    Private Delegate Sub SetEnableDelegate(ByVal [control] As Control, ByVal [enable] As Boolean)

    ''' Set control enable or disable with thread safe
    Friend Sub SetEnable(ByVal [control] As Control, ByVal [enable] As Boolean)

        Try
            If [control].InvokeRequired Then
                Dim del As New SetEnableDelegate(AddressOf SetEnable)
                [control].Invoke(del, New Object() {[control], [enable]})
            Else
                [control].Enabled = [enable]
            End If
        Catch ex As Exception
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

End Module
