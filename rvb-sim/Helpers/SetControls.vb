Module SetControls

    Private Delegate Sub SetEnableDelegate(ByVal [control] As Control, ByVal [enable] As Boolean)

    ''' <summary>
    ''' Set control enable or disable with thread safe
    ''' </summary>
    ''' <param name="[control]"></param>
    ''' <param name="[enable]"></param>
    Friend Sub SetEnable(ByVal [control] As Control, ByVal [enable] As Boolean)

        Try
            If [control].InvokeRequired Then
                Dim del As New SetEnableDelegate(AddressOf SetEnable)
                [control].Invoke(del, New Object() {[control], [enable]})
            Else
                [control].Enabled = [enable]
            End If
        Catch ex As Exception
            sb.AppendLine($"{Now} {ex.Message}")
        End Try
    End Sub

End Module
