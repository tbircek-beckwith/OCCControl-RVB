Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
'Imports iec.AsyncIEC61850

Namespace PeriodicOperations

    Public Class WriteEvents

        '''<summary>Writes periodically writes to write IP Address.</summary>
        '''<param name='RVBSim'>Reference to the main window</param>
        Protected Friend Sub Write(ByRef rvbForm As RVBSim)

            Try
                Dim WriteEvent As New ManualResetEvent(False)
                If ProtocolInUse() = "dnp" Then
                    'transmit Forward RVB Voltage
                    dnp.Send(WriteEvent, rvbForm.NumericUpDownDNPDestinationAddress.Value, rvbForm.NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, rvbForm.NumericUpDownDNPDestinationAddress.Value, rvbForm.NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then
                    'TODO: if 3-phase requires more register to write it will be done here.
                    'write back calculated Forward RVB Voltage to specified modbus register
                    modbusWrite.WriteSingleRegister(rvbForm.NumericUpDownModbusFwdRVBVoltageRegister.Value, CUShort(Forward_RVBVoltage2Write))

                    'write back calculated Reverse RVB Voltage to specified modbus register
                    modbusWrite.WriteSingleRegister(rvbForm.NumericUpDownModbusRevRVBVoltageRegister.Value, CUShort(Reverse_RVBVoltage2Write))

                ElseIf ProtocolInUse() = "iec" Then
                    'transmit Forward RVB Voltage
                    iec61850.Send(WriteEvent, rvbForm.txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, rvbForm.txtIECRevRVBVoltage.Text, "Write", CUShort(Reverse_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                End If

                SetText(rvbForm.lblFwdRVBValue, String.Format("Fwd RVB: {0}", FormatNumber((Forward_RVBVoltage2Write / M2001D_Comm_Scale), 1)))
                SetText(rvbForm.lblRevRVBValue, String.Format("Rev RVB: {0}", FormatNumber((Reverse_RVBVoltage2Write / M2001D_Comm_Scale), 1)))

                WriteEvent.SafeWaitHandle.Close()

                If ConsoleWriteEnable Then
                    Console.WriteLine(" ---------------------- Writing Fwd voltage: {0}", Forward_RVBVoltage2Write)
                    Console.WriteLine(" ---------------------- Writing Rev voltage: {0}", Reverse_RVBVoltage2Write)
                End If

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))

                WriteRegisterWait.Unregister(Nothing)
                periodicReset.Timers(rvbForm:=rvbForm)

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                CheckErrors()
                SetText(rvbForm.lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            End Try
        End Sub
    End Class
End Namespace
