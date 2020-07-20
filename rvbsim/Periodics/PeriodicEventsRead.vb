Imports System.Threading
'Imports tcpdnp
Imports tcpdnp.AsyncDNP3_0

Namespace PeriodicOperations
    Public Class ReadEvents

        Protected Friend Sub ReadNew(ByRef rvbForm As RVBSim)
            Try

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(ReadNew)} --- START")

                Dim ReadEvent As New ManualResetEvent(False)

                For Each regulator In testJsonSettingsRegulators.Regulator

                    Debug.WriteLine($"3-ph Active: {rvbForm.RVBSettings3Phase.Visible}")

                    ' only send to regulator 1 when Single Phase checked
                    If rvbForm.RVBSettings3Phase.Visible Or regulator.Id = 1 Then

                        ' generate control names with regulator values.
                        For Each value In regulator.Values

                            ' it is not a metering control
                            If value.Name.Contains("RVB") Then
                                ' Debug.WriteLine($" <------------------- SKIPPED ...")
                                Continue For
                            End If

                            Dim settingControlName As String = $"{ProtocolInUse}{value.Name}Reg{regulator.Id}"

                            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                            If v.Length > 0 Then

                                If v(0).Visible Then

                                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)

                                    Select Case ProtocolInUse
                                        Case "modbus"

                                            'read the user specified single modbus register.
                                            If registerBox.Name.Contains("Source") Then

                                                Interlocked.Exchange(SourceVoltageReadings.Item(regulator.Id - 1), modbusRead.ReadHoldingRegisters(registerBox.Value, 1).ElementAt(0))
                                                'Continue For
                                            ElseIf registerBox.Name.Contains("Local") Then

                                                Interlocked.Exchange(LocalVoltageReadings.Item(regulator.Id - 1), modbusRead.ReadHoldingRegisters(registerBox.Value, 1).ElementAt(0))

                                                ' Continue For
                                            End If

                                        Case "dnp"

                                            ' updates "result" variable internally
                                            dnp.Send(ManualEvent:=ReadEvent,
                                                     Destination:=rvbForm.DNPDestinationReg1.Value,
                                                     Source:=rvbForm.DNPSourceReg1.Value,
                                                     FunctionCode:=Mode.Read,
                                                     ObjectX:=Objects.AnalogInput,  ' .AnalogOutputStatus, '
                                                     Variation:=Variations.AnaInput16bitVar4,   '.AnaOutBlockShort, '
                                                     Qualifier:=QualifierField.AnaInput16bitStartStop,
                                                     Start16Bit:=registerBox.Value,
                                                     Stop16Bit:=registerBox.Value)

                                            'read the user specified single dnp point.
                                            If registerBox.Name.Contains("Source") Then

                                                Interlocked.Exchange(SourceVoltageReadings.Item(regulator.Id - 1), result)

                                            ElseIf registerBox.Name.Contains("Local") Then

                                                Interlocked.Exchange(LocalVoltageReadings.Item(regulator.Id - 1), result)
                                            End If

                                            ReadEvent.WaitOne()
                                            ReceivedErrorMsg = ErrorReceived

                                        Case "iec"


                                        Case Else

                                    End Select

                                    'For i = 0 To WriteRegisterWaits.Count - 1

                                    SetText(rvbForm.lblLocalVoltageValue, $"Regulator: {regulator.Id}, Readings: Fwd Voltage: {FormatNumber(CDbl(LocalVoltageReadings(regulator.Id - 1) / BecoCommunicationScaleFactor), 1)}V {vbTab} Src Voltage: {FormatNumber(CDbl(SourceVoltageReadings(regulator.Id - 1) / BecoCommunicationScaleFactor), 1)}V")

                                    SetText(rvbForm.lblMsgCenter, $"Error: {ReceivedErrorMsg}")

                                    ' Next
                                End If
                            End If
                        Next
                    End If


                Next

                Debug.WriteLine($"Current thread Is # {Thread.CurrentThread.GetHashCode} {NameOf(ReadNew)} --- END")

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                ReadEvent.SafeWaitHandle.Close()

                '    ElseIf ProtocolInUse() = "iec" Then

                '        For Each model As IECCommunicationModel In regulator.IECCommunication

                '            Debug.WriteLine("------------------- Reading Local &  Source Voltage (61850) -------------------")

                '            'read the user specified 61850 objects.
                '            Dim localVoltage = rvbForm.IecSettingsGroup.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.LocalVoltage)}Reg1"))(0)    '{model.Id}"))

                '            iec61850.Send(ReadEvent, localVoltage.Text, "Read")
                '            ReadEvent.WaitOne()
                '            LocalVoltageReadresult = iec.AsyncIEC61850.result
                '            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                '            'read the user specified 61850 objects.
                '            Dim sourceVoltage = rvbForm.IecSettingsGroup.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.SourceVoltage)}Reg1"))(0)     '{model.Id}"))

                '            iec61850.Send(ReadEvent, sourceVoltage.Text, "Read")
                '            ReadEvent.WaitOne()
                '            LocalVoltageReadresult = iec.AsyncIEC61850.result
                '            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                '            Debug.WriteLine("------------------- Reading Local & Source Voltage Done (61850) -------------------")

                '        Next
                '    End If

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)
            End Try
        End Sub

        ''' <summary>
        ''' Reads
        ''' </summary>
        ''' <param name="rvbForm"></param>
        <Obsolete("will remove next iteration", True)>
        Protected Friend Sub Read(ByRef rvbForm As RVBSim)
            Try

                '' TODO: delete up to exit sub
                'Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(Read)} is running... Reads everything.")

                'Debug.WriteLine($"-----------------------> Elapsed time: {ReadingTimer.ElapsedMilliseconds} msec")
                'Exit Sub

                '' TODO: add loop to thru multi phase regulators

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Read)} --- START")

                Dim ReadEvent As New ManualResetEvent(False)

                'LocalVoltageReadings = Enumerable.Repeat(Of UShort)(65535, 3).ToList()   ' New List(Of UShort) '(3)
                'SourceVoltageReadings = Enumerable.Repeat(Of UShort)(65535, 3).ToList()  'New List(Of UShort) '(3)

                For Each regulator In testJsonSettingsRegulators.Regulator


                    Debug.WriteLine($"3-ph Active: {rvbForm.RVBSettings3Phase.Visible}")

                    ' only send to regulator 1 when Single Phase checked
                    If rvbForm.RVBSettings3Phase.Visible Or regulator.Id = 1 Then

                        ' generate control names with regulator values.
                        For Each value In regulator.Values

                            Debug.Write($"value: {value.Name}")

                            ' it is not a metering control
                            If value.Name.Contains("RVB") Then
                                Debug.WriteLine($" <------------------- SKIPPED ...")
                                Continue For
                            End If

                            Dim settingControlName As String = $"{ProtocolInUse}{value.Name}Reg{regulator.Id}"
                            Debug.WriteLine($" <------------------- {settingControlName} processing ...")

                            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                            If v.Length > 0 Then

                                If v(0).Visible Then

                                    Debug.Write($"--- {v(0).Name} is VISIBLE --- ")

                                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)

                                    Debug.WriteLine($"Control name: {registerBox.Name}, register: {registerBox.Value}, value: read this")
                                    Debug.WriteLine($"------------------- Reading Local & Source Voltage ({ProtocolInUse.ToUpperInvariant()}) -------------------")

                                    Select Case ProtocolInUse
                                        Case "modbus"

                                            'read the user specified single modbus register.
                                            If registerBox.Name.Contains("Source") Then

                                                Interlocked.Exchange(SourceVoltageReadings.Item(regulator.Id - 1), modbusRead.ReadHoldingRegisters(registerBox.Value, 1).ElementAt(0))
                                            Else

                                                Interlocked.Exchange(LocalVoltageReadings.Item(regulator.Id - 1), modbusRead.ReadHoldingRegisters(registerBox.Value, 1).ElementAt(0))
                                            End If

                                        Case "dnp"

                                            ' updates "result" variable internally
                                            dnp.Send(ManualEvent:=ReadEvent,
                                                     Destination:=rvbForm.DNPDestinationReg1.Value,
                                                     Source:=rvbForm.DNPSourceReg1.Value,
                                                     FunctionCode:=Mode.Read,
                                                     ObjectX:=Objects.AnalogInput,  ' .AnalogOutputStatus, '
                                                     Variation:=Variations.AnaInput16bitVar4,   '.AnaOutBlockShort, '
                                                     Qualifier:=QualifierField.AnaInput16bitStartStop,
                                                     Start16Bit:=registerBox.Value,
                                                     Stop16Bit:=registerBox.Value)

                                            'read the user specified single dnp point.
                                            If registerBox.Name.Contains("Source") Then

                                                Interlocked.Exchange(SourceVoltageReadings.Item(regulator.Id - 1), result)
                                            Else

                                                Interlocked.Exchange(LocalVoltageReadings.Item(regulator.Id - 1), result)
                                            End If

                                            ReadEvent.WaitOne()
                                            ReceivedErrorMsg = ErrorReceived

                                        Case "iec"


                                        Case Else

                                    End Select

                                    For i = 0 To SupportedRegulatorNumber - 1

                                        SetText(rvbForm.lblLocalVoltageValue, $"Readings: Fwd Voltage: {FormatNumber(CDbl(LocalVoltageReadings(i) / BecoCommunicationScaleFactor), 1)}V {vbTab} Src Voltage: {FormatNumber(CDbl(SourceVoltageReadings(i) / BecoCommunicationScaleFactor), 1)}V")

                                        SetText(rvbForm.lblMsgCenter, $"Error: {ReceivedErrorMsg}")

                                        Debug.WriteLine($"Reading local voltage: {LocalVoltageReadings(i)} - source voltage: {SourceVoltageReadings(i)} - {HeartBeatTimers(i)}")
                                    Next

                                    'Debug.WriteLine($"Reading local voltageS: {LocalVoltageReadings(regulator.Id - 1)} - source voltage: { SourceVoltageReadings(regulator.Id - 1)} - {Heart_Beat_Timer}")
                                    Debug.WriteLine($"------------------- Reading Local & Source Voltage ({ProtocolInUse.ToUpperInvariant()}) Done -------------------")

                                End If
                            End If

                        Next

                        Debug.WriteLine($"Reading local voltageS: {LocalVoltageReadings(regulator.Id - 1)} - source voltage: { SourceVoltageReadings(regulator.Id - 1)} - {HeartBeatTimers(regulator.Id - 1)}")
                    End If
                Next

                For i = 0 To SupportedRegulatorNumber - 1

                    Interlocked.Add(HeartBeatTimers(i), ReadInterval)

                Next

                ' Interlocked.Add(Heart_Beat_Timer, ReadInterval)
                Debug.WriteLine($"Current thread Is # {Thread.CurrentThread.GetHashCode} {NameOf(Read)} --- END")

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                ReadEvent.SafeWaitHandle.Close()

                '    ElseIf ProtocolInUse() = "iec" Then

                '        For Each model As IECCommunicationModel In regulator.IECCommunication

                '            Debug.WriteLine("------------------- Reading Local &  Source Voltage (61850) -------------------")

                '            'read the user specified 61850 objects.
                '            Dim localVoltage = rvbForm.IecSettingsGroup.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.LocalVoltage)}Reg1"))(0)    '{model.Id}"))

                '            iec61850.Send(ReadEvent, localVoltage.Text, "Read")
                '            ReadEvent.WaitOne()
                '            LocalVoltageReadresult = iec.AsyncIEC61850.result
                '            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                '            'read the user specified 61850 objects.
                '            Dim sourceVoltage = rvbForm.IecSettingsGroup.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.SourceVoltage)}Reg1"))(0)     '{model.Id}"))

                '            iec61850.Send(ReadEvent, sourceVoltage.Text, "Read")
                '            ReadEvent.WaitOne()
                '            LocalVoltageReadresult = iec.AsyncIEC61850.result
                '            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                '            Debug.WriteLine("------------------- Reading Local & Source Voltage Done (61850) -------------------")

                '        Next
                '    End If

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)
            End Try
        End Sub
    End Class
End Namespace
