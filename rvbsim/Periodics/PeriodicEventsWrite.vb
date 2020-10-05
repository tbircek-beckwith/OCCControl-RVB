Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
'Imports iec.AsyncIEC61850

Namespace PeriodicOperations

    Public Class WriteEvents

        ''' <summary>
        ''' Writes periodically to write IP Address.
        ''' </summary>
        ''' <param name="rvbForm">Reference to the main window</param>
        ''' <param name="regulatorId">the current regulator</param>
        Protected Friend Sub Write(ByRef rvbForm As RVBSim, regulatorId As Integer)

            Try

                Console.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(Write)} is running... STARTS")

                Dim controlId = regulatorId + 1

                Dim query = From regulator In testJsonSettingsRegulators.Regulator
                            Where regulator.Id = controlId
                            Select regulator.Values


                'If rvbForm.SinglePhaseCheckBox.Checked AndAlso controlId > 1 Then
                '    Return ' Exit For
                'End If

                ' GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulatorId)

                For Each values In query

                    ' GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulatorId)

                    For Each value In values

                        If value.Name.Contains("RVBValue") Then

                            Dim settingControlName As String = $"{ProtocolInUse}{value.Name}Reg{controlId}"

                            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                            If v.Length > 0 Then

                                If v(0).Visible Then

                                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)

                                    Console.WriteLine($"------------------- Writing RVB Voltage ({ProtocolInUse}) -------------------")

                                    Console.Write($"{Date.Now}{vbTab}Control name: {registerBox.Name}, Comm writes to Reg: {registerBox.Value}, ")

                                    'Dim writeTask As Tasks.Task 

                                    Select Case ProtocolInUse

                                        Case "modbus"
                                            If registerBox.Name.Contains("FRVB") Then

                                                Console.WriteLine($"Local: {Interlocked.Read(FwdRVBVoltages2Write(regulatorId))}")

                                                Tasks.Task.Run(Sub() modbusWrite.WriteSingleRegister(startingAddress:=registerBox.Value, value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)))).Wait(10)

                                            ElseIf registerBox.Name.Contains("RRVB") Then

                                                Console.WriteLine($"Src: {Interlocked.Read(RevRVBVoltages2Write(regulatorId))}")

                                                Tasks.Task.Run(Sub() modbusWrite.WriteSingleRegister(startingAddress:=registerBox.Value, value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)))).Wait(10)
                                            End If

                                        Case "dnp"
                                            If registerBox.Name.Contains("FRVB") Then

                                                Debug.WriteLine($"Local: {Interlocked.Read(FwdRVBVoltages2Write(regulatorId))}")

                                                DnpSend(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)))

                                            ElseIf registerBox.Name.Contains("RRVB") Then

                                                Debug.WriteLine($"Src: {Interlocked.Read(RevRVBVoltages2Write(regulatorId))}")

                                                DnpSend(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)))

                                            End If


                                    End Select


                                    Dim s = New UpdateMeteringValues(rvbForm:=rvbForm, registerBox:=registerBox, regulatorId:=regulatorId)

                                    Console.WriteLine($"------------------- Writing RVB Voltage ({ProtocolInUse}) Done -------------------")

                                End If
                            End If

                        End If
                    Next

                    'If rvbForm.SinglePhaseCheckBox.Checked Then
                    '    Exit For
                    'End If
                Next


            Catch ex As Exception

                Interlocked.Increment(errorCounter)
                ResetMeteringLabels()
                CheckErrors()
                Dim message As String = $"{Now}{vbCrLf}{ex.Message}:{vbCrLf}{ex.StackTrace}"
                SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message, append:=True)
                sb.AppendLine(message)

            Finally

                Console.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(Write)} is running... ENDS")

            End Try

        End Sub

        Private Sub DnpSend(rvbForm As RVBSim, address As UShort, value As UShort)

            Dim WriteEvent As New ManualResetEvent(False)

            'write back calculated Forward RVB Voltage to specified dnp point
            dnp.Send(ManualEvent:=WriteEvent,
                     Destination:=rvbForm.DNPDestinationReg1.Value,
                     Source:=rvbForm.DNPSourceReg1.Value,
                     FunctionCode:=Mode.DirectOp,
                     ObjectX:=Objects.AnalogOutput,
                     Variation:=Variations.AnaOutBlockShort,
                     Qualifier:=QualifierField.AnaOutBlock16bitIndex,
                     Start16Bit:=1,
                     Stop16Bit:=address,
                     Value:=value,
                     Status:=0)

            WriteEvent.WaitOne()
            ReceivedErrorMsg = ErrorReceived

            'transmit Reverse RVB Voltage
            WriteEvent.Reset()

        End Sub
    End Class
End Namespace
