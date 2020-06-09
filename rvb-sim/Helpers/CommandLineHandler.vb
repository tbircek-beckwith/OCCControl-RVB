Imports System.Threading

Module CommandLineHandler

    Friend Sub CheckCommandLine()
        Try
            Dim i As Integer = 0
            Dim cmdlines As String() = New String(My.Application.CommandLineArgs.Count - 1) {}
            For Each item As String In My.Application.CommandLineArgs
                Select Case item.ToLower
                    Case "-vf"  'fwd voltage offset value
                        testSetting.FwdRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-va" 'fwd voltage apply value NOT IN HELP FILE
                        RVBSim.useFixedVoltage.Checked = True
                        RVBSim.useDeltaVoltage.Checked = False
                        testSetting.FwdRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-vr"  'rev voltage offset value
                        testSetting.RevRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-vb" 'fwd voltage apply value NOT IN HELP FILE
                        RVBSim.useFixedVoltage.Checked = True
                        RVBSim.useDeltaVoltage.Checked = False
                        testSetting.RevRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-s"
                        Select Case My.Application.CommandLineArgs.Item(i + 1)
                            Case "start"
                                start.Start()
                            Case "pause"
                                pause.Pause()
                            Case "end"
                                pause.Pause()
                                RVBSim.Close()
                        End Select
                End Select
                i += 1
            Next
            If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then RVBSim.FwdDeltaVoltageReg1.Value = 0.0 Else RVBSim.FwdDeltaVoltageReg1.Value = testSetting.FwdRVBVoltage
            If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RVBSim.RevDeltaVoltageReg1.Value = 0.0 Else RVBSim.RevDeltaVoltageReg1.Value = testSetting.RevRVBVoltage

        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(CheckCommandLine)}")
        End Try
    End Sub

End Module
