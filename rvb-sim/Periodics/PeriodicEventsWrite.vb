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
                Dim WriteEvent As New ManualResetEvent(False)
                If ProtocolInUse() = "dnp" Then
                    'transmit Forward RVB Voltage
                    dnp.Send(WriteEvent, rvbForm.DNPDestinationReg1.Value, rvbForm.DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, rvbForm.DNPDestinationReg1.Value, rvbForm.DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then

                    'write back calculated Forward RVB Voltage to specified modbus register
                    modbusWrite.WriteSingleRegister(rvbForm.ModbusFRVBValueReg1.Value, CUShort(Forward_RVBVoltage2Write))

                    'write back calculated Reverse RVB Voltage to specified modbus register
                    modbusWrite.WriteSingleRegister(rvbForm.ModbusRRVBValueReg1.Value, CUShort(Reverse_RVBVoltage2Write))

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

                SetText(rvbForm.lblFwdRVBValue, $"Fwd RVB: {FormatNumber(Forward_RVBVoltage2Write / M2001D_Comm_Scale, 1)}")
                SetText(rvbForm.lblRevRVBValue, $"Rev RVB: {FormatNumber(Reverse_RVBVoltage2Write / M2001D_Comm_Scale, 1)}")

                WriteEvent.SafeWaitHandle.Close()

                Debug.WriteLine($" ---------------------- Writing Fwd voltage: {Forward_RVBVoltage2Write}, Writing Rev voltage: {Reverse_RVBVoltage2Write}")

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Error: {ReceivedErrorMsg}")

                WriteRegisterWait.Unregister(Nothing)
                periodicReset.Timers(rvbForm:=rvbForm)

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                CheckErrors()
                SetText(rvbForm.lblMsgCenter, ex.Message)
                sb.AppendLine($"{Now} {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace
