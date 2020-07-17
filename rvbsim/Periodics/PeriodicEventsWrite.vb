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

                Dim WriteEvent As New ManualResetEvent(False)

                Dim query = From regulator In testJsonSettingsRegulators.Regulator
                            Where regulator.Id = controlId
                            Select regulator.Values

                GenerateRVBVoltage2Transfer(rvbForm:=rvbForm, regulatorNumber:=regulatorId)

                For Each value In query

                    For Each somethinElse In value

                        If somethinElse.Name.Contains("RVBValue") Then

                            Dim settingControlName As String = $"{ProtocolInUse}{somethinElse.Name}Reg{controlId}"
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
                                    End Select

                                    Debug.WriteLine($"------------------- Writing RVB Voltage ({ProtocolInUse}) Done -------------------")

                                End If
                            End If

                        End If
                    Next
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
