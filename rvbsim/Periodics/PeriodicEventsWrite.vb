Imports System.Text
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
        Protected Friend Sub WriteNew(ByRef rvbForm As RVBSim, regulatorId As Integer)

            Try

                Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(WriteNew)} is running... STARTS")

                Dim controlId = regulatorId + 1

                'Dim WriteEvent As New ManualResetEvent(False)

                Dim query = From regulator In testJsonSettingsRegulators.Regulator
                            Where regulator.Id = controlId
                            Select regulator.Values

                GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulatorId)

                For Each values In query

                    For Each value In values

                        If value.Name.Contains("RVBValue") Then

                            Dim settingControlName As String = $"{ProtocolInUse}{value.Name}Reg{controlId}"
                            ' Debug.WriteLine($" <------------------- {settingControlName} processing ...")

                            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                            If v.Length > 0 Then

                                If v(0).Visible Then

                                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)

                                    Debug.WriteLine($"------------------- Writing RVB Voltage ({ProtocolInUse}) -------------------")

                                    Debug.Write($"{Date.Now}{vbTab}Control name: {registerBox.Name}, Comm writes to Reg: {registerBox.Value}, ")

                                    Select Case ProtocolInUse
                                        Case "modbus"
                                            If registerBox.Name.Contains("FRVB") Then

                                                Debug.WriteLine($"Local: {Interlocked.Read(FwdRVBVoltages2Write(regulatorId))}")

                                                modbusWrite.WriteSingleRegister(startingAddress:=registerBox.Value, value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)))
                                            ElseIf registerBox.Name.Contains("RRVB") Then

                                                Debug.WriteLine($"Src:{Interlocked.Read(RevRVBVoltages2Write(regulatorId))}")

                                                modbusWrite.WriteSingleRegister(startingAddress:=registerBox.Value, value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)))
                                            End If

                                        Case "dnp"
                                            If registerBox.Name.Contains("FRVB") Then

                                                Debug.WriteLine($"Local: {Interlocked.Read(FwdRVBVoltages2Write(regulatorId))}")

                                                Send(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)))

                                                'write back calculated Forward RVB Voltage to specified dnp point
                                                'dnp.Send(ManualEvent:=WriteEvent,
                                                '         Destination:=rvbForm.DNPDestinationReg1.Value,
                                                '         Source:=rvbForm.DNPSourceReg1.Value,
                                                '         FunctionCode:=Mode.DirectOp,
                                                '         ObjectX:=Objects.AnalogOutput,
                                                '         Variation:=Variations.AnaOutBlockShort,
                                                '         Qualifier:=QualifierField.AnaOutBlock16bitIndex,
                                                '         Start16Bit:=1,
                                                '         Stop16Bit:=registerBox.Value,                                      'fRVBVoltage.Value,
                                                '         Value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)),        'CUShort(Forward_RVBVoltage2Write),
                                                '         Status:=0)
                                                'WriteEvent.WaitOne()
                                                'ReceivedErrorMsg = ErrorReceived

                                                ''transmit Reverse RVB Voltage
                                                'WriteEvent.Reset()

                                            ElseIf registerBox.Name.Contains("RRVB") Then

                                                Debug.WriteLine($"Src:{Interlocked.Read(RevRVBVoltages2Write(regulatorId))}")

                                                'write back calculated Reverse RVB Voltage to specified dnp point
                                                'dnp.Send(ManualEvent:=WriteEvent,
                                                '         Destination:=rvbForm.DNPDestinationReg1.Value,
                                                '         Source:=rvbForm.DNPSourceReg1.Value,
                                                '         FunctionCode:=Mode.DirectOp,
                                                '         ObjectX:=Objects.AnalogOutput,
                                                '         Variation:=Variations.AnaOutBlockShort,
                                                '         Qualifier:=QualifierField.AnaOutBlock16bitIndex,
                                                '         Start16Bit:=1,
                                                '         Stop16Bit:=registerBox.Value,                                      'rRVBVoltage.Value,
                                                '         Value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)),        'CUShort(Reverse_RVBVoltage2Write),
                                                '         Status:=0)
                                                'WriteEvent.WaitOne()
                                                'ReceivedErrorMsg = ErrorReceived

                                                ''transmit Reverse RVB Voltage
                                                'WriteEvent.Reset()

                                                Send(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)))

                                            End If


                                    End Select

                                    Debug.WriteLine($"------------------- Writing RVB Voltage ({ProtocolInUse}) Done -------------------")

                                End If
                            End If

                        End If
                    Next

                    If rvbForm.SinglePhaseCheckBox.Checked Then
                        Exit For
                    End If
                Next


            Catch ex As Exception

                Interlocked.Increment(errorCounter)
                CheckErrors()
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)

            Finally

                Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(WriteNew)} is running... ENDS")

            End Try

        End Sub

        Private Sub Send(rvbForm As RVBSim, address As UShort, value As UShort)

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

        <Obsolete("will remove in next iteration", True)>
        Protected Friend Sub Write(ByRef rvbForm As RVBSim, regulatorId As Integer)

            Try


                ' TODO: delete up to exit sub
                Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(Write)} is running... Regulator: {regulatorId + 1}")

                If regulatorId < 0 Or regulatorId > 2 Or IsNothing(regulatorId) Then
                    Debugger.Break()
                End If

                Debug.WriteLine($"<----------------------- Regulator: {regulatorId + 1}, Writing Elapsed time: {WritingTimers(regulatorId).ElapsedMilliseconds} msec, Reading Elapsed time: {ReadingTimer.ElapsedMilliseconds} msec")

                WriteRegisterWaits.Item(regulatorId).Unregister(Nothing)
                periodicReset.Timers(rvbForm:=rvbForm, regulatorID:=regulatorId)

                Exit Sub

                Dim WriteEvent As New ManualResetEvent(False)

                Dim query = From regulator In testJsonSettingsRegulators.Regulator
                            Where regulator.Id = regulatorId
                            Select regulator.Values

                For Each value In query

                    For Each somethinElse In value


                        If somethinElse.Name.Contains("RVBValue") Then

                            Dim settingControlName As String = $"{ProtocolInUse}{somethinElse.Name}Reg{regulatorId}"
                            Debug.WriteLine($" <------------------- {settingControlName} processing ...")

                            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                            If v.Length > 0 Then

                                If v(0).Visible Then

                                    Debug.Write($"--- {v(0).Name} is VISIBLE --- ")

                                    Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)


                                    Debug.WriteLine($"{Date.Now():mm:ss.ffff} - What????{ProtocolInUse}{somethinElse.Name}Reg{regulatorId}")

                                    Debug.WriteLine("------------------- Writing RVB Voltage (MODBUS) -------------------")

                                    GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulatorId)


                                    'write back calculated Forward RVB Voltage to specified modbus register
                                    ' Dim fRVBVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.FRVBValue)}Reg1"))(0)       '{model.Id}"))

                                    modbusWrite.WriteSingleRegister(registerBox.Value, CUShort(Forward_RVBVoltage2Write))

                                    'write back calculated Reverse RVB Voltage to specified modbus register
                                    ' Dim rRVBVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.RRVBValue)}Reg1"))(0)     '{model.Id}"))

                                    modbusWrite.WriteSingleRegister(registerBox.Value, CUShort(Reverse_RVBVoltage2Write))

                                    ' outputFwdString.Append($"Reg{model.Id}: Fwd: {FormatNumber(Forward_RVBVoltage2Write / BecoCommunicationScaleFactor, 1)}{vbTab} -- {vbTab}")
                                    ' outputRevString.Append($"Reg{model.Id}: Rev: {FormatNumber(Reverse_RVBVoltage2Write / BecoCommunicationScaleFactor, 1)}{vbTab} -- {vbTab}")
                                    Debug.WriteLine("------------------- Writing RVB Voltage (MODBUS) Done -------------------")

                                    WriteRegisterWaits.Item(regulatorId).Unregister(Nothing)
                                    periodicReset.Timers(rvbForm:=rvbForm, regulatorID:=regulatorId)

                                End If
                            End If

                        End If
                    Next



                    '    Debug.Write($"value: {value.Name}")

                    '    ' it is not a metering control
                    '    If value.Name.Contains("RVB") Then
                    '        Debug.WriteLine($" <------------------- SKIPPED ...")
                    '        Continue For
                    '    End If

                    '    Dim settingControlName As String = $"{ProtocolInUse}{value.Name}Reg{regulator.Id}"
                    '    Debug.WriteLine($" <------------------- {settingControlName} processing ...")

                    '    Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

                    '    If v.Length > 0 Then

                    '        If v(0).Visible Then

                Next


            Catch ex As Exception

                Interlocked.Increment(errorCounter)
                CheckErrors()
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)
            End Try

        End Sub
    End Class
End Namespace
