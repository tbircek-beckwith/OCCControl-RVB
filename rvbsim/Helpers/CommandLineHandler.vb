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
                        RVBSim.SettingsUsefixedReg1.Checked = True
                        RVBSim.SettingsUserelativeReg1.Checked = False
                        testSetting.FwdRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-vr"  'rev voltage offset value
                        testSetting.RevRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-vb" 'fwd voltage apply value NOT IN HELP FILE
                        RVBSim.SettingsUsefixedReg1.Checked = True
                        RVBSim.SettingsUserelativeReg1.Checked = False
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
            If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then RVBSim.SettingsFwdRVBVoltageReg1.Value = 0.0 Else RVBSim.SettingsFwdRVBVoltageReg1.Value = testSetting.FwdRVBVoltage
            If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RVBSim.SettingsRevRVBVoltageReg1.Value = 0.0 Else RVBSim.SettingsRevRVBVoltageReg1.Value = testSetting.RevRVBVoltage

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.Message}:{vbCrLf}{ex.StackTrace}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(CheckCommandLine)}")
        End Try
    End Sub

End Module
