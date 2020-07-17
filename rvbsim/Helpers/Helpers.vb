Imports System.Threading

Module Helpers

    Friend Function PingIPAddresses(ByVal IPs As String()) As Boolean
        PingIPAddresses = False
        SetText(RVBSim.lblMsgCenter, ReceivedErrorMsg)

        Dim siteResponds As Boolean = My.Computer.Network.Ping(IPs(0), 5000)
        PingIPAddresses = My.Computer.Network.Ping(IPs(1), 5000) And siteResponds

        Return PingIPAddresses
    End Function

    Friend Sub CheckErrors()
        If Not Interlocked.Read(errorCounter) >= 10 Then

            For i = 0 To SupportedRegulatorNumber - 1
                periodicReset.Timers(rvbForm:=RVBSim, regulatorID:=i)

            Next
        End If
    End Sub

End Module
