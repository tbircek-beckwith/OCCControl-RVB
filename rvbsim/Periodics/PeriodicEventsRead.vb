Imports System.Threading
Imports tcpdnp.AsyncDNP3_0

Namespace PeriodicOperations
    Public Class ReadEvents

        Protected Friend Sub Read(ByRef rvbForm As RVBSim)
            Try

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Read)} --- START")

                Dim ReadEvent As New ManualResetEvent(False)

                For Each regulator In testJsonSettingsRegulators.Regulator

                    ' only send to regulator 1 when Single Phase checked
                    If rvbForm.RVBSettings3Phase.Visible Or regulator.Id = 1 Then

                        Debug.WriteLine($"3-ph Active: {rvbForm.RVBSettings3Phase.Visible}")

                        ' generate control names with regulator values.
                        For Each value In regulator.Values

                            ' it is not a metering control
                            If value.Name.Contains("RVB") Then
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

                                            ElseIf registerBox.Name.Contains("Local") Then

                                                Interlocked.Exchange(LocalVoltageReadings.Item(regulator.Id - 1), modbusRead.ReadHoldingRegisters(registerBox.Value, 1).ElementAt(0))

                                            End If

                                        Case "dnp"

                                            ' updates "result" variable internally
                                            dnp.Send(ManualEvent:=ReadEvent,
                                                     Destination:=rvbForm.DNPDestinationReg1.Value,
                                                     Source:=rvbForm.DNPSourceReg1.Value,
                                                     FunctionCode:=Mode.Read,
                                                     ObjectX:=Objects.AnalogInput,
                                                     Variation:=Variations.AnaInput16bitVar4,
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

                                            ' TODO: wait until M-6200B supports this protocol.

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

                                        Case Else

                                    End Select

                                    Dim s = New UpdateMeteringValues(rvbForm:=rvbForm, registerBox:=registerBox, regulatorId:=regulator.Id - 1)

                                    GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulator.Id - 1)

                                    SetTextBox(rvbForm.ErrorsTextBox, $"Error: {ReceivedErrorMsg}")
                                End If
                            End If
                        Next
                    End If


                Next

                Debug.WriteLine($"Current thread Is # {Thread.CurrentThread.GetHashCode} {NameOf(Read)} --- END")

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                ReadEvent.SafeWaitHandle.Close()

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                Dim message As String = $"{Now}{vbCrLf}{ex.Message}:{vbCrLf}{ex.StackTrace}"
                SetTextBox(textbox:=rvbForm.ErrorsTextBox, text:=message, append:=True)
                sb.AppendLine(message)
                ResetMeteringLabels()
            End Try
        End Sub

    End Class
End Namespace
