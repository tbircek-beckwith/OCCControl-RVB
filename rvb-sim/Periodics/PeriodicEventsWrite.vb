Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
'Imports iec.AsyncIEC61850

Namespace PeriodicOperations

    Public Class WriteEvents

        '''<summary>Writes periodically writes to write IP Address.</summary>
        '''<param name='rvbForm'>Reference to the main window</param>
        Protected Friend Sub Write(ByRef rvbForm As RVBSim)

            'TODO: if 3-phase requires more register to write it will be done here.

            Try

                For Each regulator As Regulator In Regulators

                    Dim WriteEvent As New ManualResetEvent(False)
                    If ProtocolInUse() = "dnp" Then
                        For Each dnpRegister As DnpCommunicationModel In regulator.DnpCommunication
                            'transmit Forward RVB Voltage
                            'dnp.Send(WriteEvent, rvbForm.DNPDestinationReg1.Value, rvbForm.DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                            dnp.Send(WriteEvent, dnpRegister.DestinationAddress, dnpRegister.SourceAddress, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpRegister.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                            WriteEvent.WaitOne()
                            ReceivedErrorMsg = ErrorReceived

                            'transmit Reverse RVB Voltage
                            WriteEvent.Reset()
                            ' dnp.Send(WriteEvent, rvbForm.DNPDestinationReg1.Value, rvbForm.DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                            dnp.Send(WriteEvent, dnpRegister.DestinationAddress, rvbForm.DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                            WriteEvent.WaitOne()
                            ReceivedErrorMsg = ErrorReceived
                        Next

                    ElseIf ProtocolInUse() = "modbus" Then
                        For Each modbusRegister As ModbusCommunicationModel In regulator.ModbusCommunication

                            ' TODO: Replace Reg1 in fRVBVoltage & rRVBVoltage with Reg{modbusRegister.Id}
                            Debug.WriteLine("------------------- Writing RVB Voltage (MODBUS) -------------------")

                            'write back calculated Forward RVB Voltage to specified modbus register
                            Dim fRVBVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{modbusRegister.Name}{NameOf(modbusRegister.FRVBValue)}Reg1"))(0)       '{modbusRegister.Id}"))
                            modbusWrite.WriteSingleRegister(fRVBVoltage.Value, CUShort(Forward_RVBVoltage2Write))

                            'write back calculated Reverse RVB Voltage to specified modbus register
                            Dim rRVBVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{modbusRegister.Name}{NameOf(modbusRegister.RRVBValue)}Reg1"))(0)     '{modbusRegister.Id}"))
                            modbusWrite.WriteSingleRegister(rRVBVoltage.Value, CUShort(Reverse_RVBVoltage2Write))

                            Debug.WriteLine("------------------- Writing RVB Voltage (MODBUS) Done -------------------")

                        Next

                    ElseIf ProtocolInUse() = "iec" Then
                        'transmit Forward RVB Voltage
                        iec61850.Send(WriteEvent, rvbForm.IecFRVBValueReg1.Text, "Write", CUShort(Forward_RVBVoltage2Write))
                        WriteEvent.WaitOne()
                        ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                        'transmit Reverse RVB Voltage
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, rvbForm.IecRRVBValueReg1.Text, "Write", CUShort(Reverse_RVBVoltage2Write))
                        WriteEvent.WaitOne()
                        ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                    End If

                    SetText(rvbForm.lblFwdRVBValue, $"Fwd RVB: {FormatNumber(Forward_RVBVoltage2Write / BecoCommunicationScaleFactor, 1)}")
                    SetText(rvbForm.lblRevRVBValue, $"Rev RVB: {FormatNumber(Reverse_RVBVoltage2Write / BecoCommunicationScaleFactor, 1)}")

                    WriteEvent.SafeWaitHandle.Close()

                    Debug.WriteLine($" ---------------------- Writing Fwd voltage: {Forward_RVBVoltage2Write}, Writing Rev voltage: {Reverse_RVBVoltage2Write}")

                    If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Error: {ReceivedErrorMsg}")

                    WriteRegisterWait.Unregister(Nothing)
                    periodicReset.Timers(rvbForm:=rvbForm)
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
