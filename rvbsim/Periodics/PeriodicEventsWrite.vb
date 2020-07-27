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

                                                DnpSend(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(FwdRVBVoltages2Write(regulatorId)))

                                            ElseIf registerBox.Name.Contains("RRVB") Then

                                                Debug.WriteLine($"Src:{Interlocked.Read(RevRVBVoltages2Write(regulatorId))}")

                                                DnpSend(rvbForm:=rvbForm,
                                                     address:=registerBox.Value,
                                                     value:=Interlocked.Read(RevRVBVoltages2Write(regulatorId)))

                                            End If


                                    End Select


                                    Dim s = New UpdateMeteringValues(rvbForm:=rvbForm, registerBox:=registerBox, regulatorId:=regulatorId)

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
                ResetMeteringLabels()
                CheckErrors()
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                ' SetText(rvbForm.lblMsgCenter, message)
                SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
                sb.AppendLine(message)

            Finally

                Debug.WriteLine($"{Date.Now:hh:mm:ss.ffff} -- {NameOf(WriteNew)} is running... ENDS")

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
