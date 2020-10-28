
Imports System.Text.RegularExpressions
Imports System.Threading

Module CommandLineHandler

    ' Public ReadOnly SetPointPatterns As String = "(?<commandLineKeywords>-(vf)[\s\d]|-(vr)[\s\d]|-(s\s)"

    Friend Sub CheckCommandLine()

        Try

            'Dim expectedArguments = "s start, vf1 1.3, vr1 -1.4, vf2 -5.3, vr2 3.2, vf3 1.2, vr3 -1.8"
            'Dim expectedValues As Dictionary(Of String, String) = expectedArguments.Split(",").ToDictionary(Function(x) x.Trim.Split(" ")(0), Function(y) y.Trim.Split(" ")(1))

            Dim inputArguments As Dictionary(Of String, String) = Environment.CommandLine.Split("""")(2).Split(",").ToDictionary(Function(x) x.Trim.Split(" ")(0), Function(y) y.Trim.Split(" ")(1))

            If inputArguments.Count < 1 Then

                Throw New ArgumentException("No parameters provided. Please check help file.")

            ElseIf inputArguments.Count > 8 Then

                Throw New ArgumentException("Extra parameters provided. Please check help file.")

                'ElseIf inputArguments(1).Split(",").Count < expectedArguments.Split(",").Count Then

                '    Throw New ArgumentException("arguments missing value(s): Parameters: " & inputArguments(1))

                'ElseIf inputArguments(1).Split(",").Count > expectedArguments.Split(",").Count Then

                '    Throw New ArgumentException("arguments extra value(s): Parameters: " & inputArguments(1))

            Else

                For Each value In inputArguments

                    Dim regulatorId As Integer = 0

                    Integer.TryParse(value.Key.Last, regulatorId)

                    ' SetRelative(regulatorId)

                    Select Case value.Key.ToLower

                        Case "vf1", "vf2", "vf3"

                            ' testSetting.FwdRVBVoltage = value.Value ' My.Application.CommandLineArgs.Item(i + 1)
                            ' Decimal.TryParse(value.Value, RVBSim.SettingsFwdRVBVoltageReg1.Value)
                            SetVoltageOffset(value.Value, "Fwd", regulatorId)

                        Case "vr1", "vr2", "vr3"  'rev voltage offset value

                            SetVoltageOffset(value.Value, "Rev", regulatorId)

                        'Case "va" 'fwd voltage apply value NOT IN HELP FILE
                        '    RVBSim.SettingsUsefixedReg1.Checked = True
                        '    RVBSim.SettingsUserelativeReg1.Checked = False
                        '    testSetting.FwdRVBVoltage = value.Value ' My.Application.CommandLineArgs.Item(i + 1)

                        'Case "vb" 'fwd voltage apply value NOT IN HELP FILE
                        '    RVBSim.SettingsUsefixedReg1.Checked = True
                        '    RVBSim.SettingsUserelativeReg1.Checked = False
                        '    testSetting.RevRVBVoltage = value.Value ' My.Application.CommandLineArgs.Item(i + 1)

                        Case "s"
                            Select Case value.Value.ToLower ' My.Application.CommandLineArgs.Item(i + 1)
                                Case "start"
                                    start.Start()
                                Case "pause"
                                    pause.Pause()
                                Case "end"
                                    pause.Pause()
                                    RVBSim.Close()
                            End Select
                    End Select

                Next

            End If

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.Message}:{vbCrLf}{ex.StackTrace}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)
        End Try

    End Sub

    Friend Sub OldCheckCommandLine()
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
