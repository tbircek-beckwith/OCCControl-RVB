Imports System.Threading
'Imports tcpdnp
Imports tcpdnp.AsyncDNP3_0

Namespace PeriodicOperations
    Public Class ReadEvents

        ''' <summary>
        ''' Reads
        ''' </summary>
        ''' <param name="rvbForm"></param>
        Protected Friend Sub Read(ByRef rvbForm As RVBSim)
            Try

                'Dim iecTextboxes = RVBSim.CommunicationDetails.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.StartsWith("Iec"))
                'Dim dnpNumericUpDown = RVBSim.CommunicationDetails.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.StartsWith("Dnp"))
                'Dim modbusNumericUpDown = RVBSim.CommunicationDetails.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.StartsWith("Modbus"))

                For Each regulator As Regulator In Regulators

                    Dim ReadEvent As New ManualResetEvent(False)

                    If ProtocolInUse() = "dnp" Then
                        For Each model As DnpCommunicationModel In regulator.DnpCommunication

                            ' TODO: Replace Reg1 in localVoltage & sourceVoltage with Reg{dnp30Register.Id}
                            Debug.WriteLine("------------------- Reading Local & Source Voltage (DNP3.0) -------------------")

                            ' Interlocked.Exchange()
                            'read the user specified dnp3.0 objects.
                            Dim localVoltage = rvbForm.DnpSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.LocalVoltage)}Reg1"))(0)    '{dnp30Register.Id}"))
                            'dnp.Send(ReadEvent, rvbForm.DNPDestinationReg1.Value, rvbForm.DNPSourceReg1.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, localVoltage.Value)
                            'Dim newValue As UShort = 0

                            'Interlocked.Exchange(newValue, localVoltage.Value)
                            Debug.WriteLine($" ---------------------------------> localName: {localVoltage.Name}", "information")

                            dnp.Send(ManualEvent:=ReadEvent, Destination:=rvbForm.DNPDestinationReg1.Value, Source:=rvbForm.DNPSourceReg1.Value, FunctionCode:=Mode.Read, ObjectX:=Objects.AnalogInput, Variation:=Variations.AnaInput16bitVar4, Qualifier:=QualifierField.AnaInput16bitStartStop, Start16Bit:=localVoltage.Value, Stop16Bit:=localVoltage.Value)
                            ReadEvent.WaitOne()
                            LocalVoltageReadresult = result
                            ReceivedErrorMsg = ErrorReceived

                            'read the user specified dnp3.0 objects.
                            Dim sourceVoltage = rvbForm.DnpSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.SourceVoltage)}Reg1"))(0)     '{dnp30Register.Id}"))

                            ' Interlocked.Exchange(newValue, sourceVoltage.Value)

                            Debug.WriteLine($" ---------------------------------> sourceName: {sourceVoltage.Name}-{sourceVoltage.Value}", "information")

                            dnp.Send(ManualEvent:=ReadEvent, Destination:=rvbForm.DNPDestinationReg1.Value, Source:=rvbForm.DNPSourceReg1.Value, FunctionCode:=Mode.Read, ObjectX:=Objects.AnalogInput, Variation:=Variations.AnaInput16bitVar4, Qualifier:=QualifierField.AnaInput16bitStartStop, Start16Bit:=sourceVoltage.Value, Stop16Bit:=sourceVoltage.Value)
                            ReadEvent.WaitOne()
                            SourceVoltageReadresult = result
                            ReceivedErrorMsg = ErrorReceived

                            Debug.WriteLine("------------------- Reading Local & Source Voltage (DNP3.0) Done -------------------")

                        Next

                    ElseIf ProtocolInUse() = "modbus" Then
                        For Each model As ModbusCommunicationModel In regulator.ModbusCommunication

                            ' TODO: Replace Reg1 in localVoltage & sourceVoltage with Reg{modbusRegister.Id}
                            Debug.WriteLine("------------------- Reading Local & Source Voltage (MODBUS) -------------------")

                            'read the user specified single modbus register.
                            Dim localVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.LocalVoltage)}Reg1"))(0)       '{modbusRegister.Id}"))
                            LocalVoltageReadresult = CUShort(modbusRead.ReadHoldingRegisters(localVoltage.Value, 1).ElementAt(0))

                            Dim sourceVoltage = rvbForm.ModbusSettingsGroup.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.Equals($"{model.Name}{NameOf(model.SourceVoltage)}Reg1"))(0)     '{modbusRegister.Id}"))
                            SourceVoltageReadresult = CUShort(modbusRead.ReadHoldingRegisters(sourceVoltage.Value, 1).ElementAt(0))

                            Debug.WriteLine("------------------- Reading Local & Source Voltage (MODBUS) Done -------------------")

                        Next

                    ElseIf ProtocolInUse() = "iec" Then
                        Debug.WriteLine("------------------- Reading Local Voltage -------------------")
                        iec61850.Send(ReadEvent, rvbForm.IecLocalVoltageReg1.Text, "Read")
                        ReadEvent.WaitOne()
                        LocalVoltageReadresult = iec.AsyncIEC61850.result
                        ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                        Debug.WriteLine("------------------- Reading Local Voltage Done -------------------")
                    End If

                    SetText(rvbForm.lblLocalVoltageValue, $"Readings: Fwd Voltage: {FormatNumber(CDbl(LocalVoltageReadresult / BecoCommunicationScaleFactor), 1)}V {vbTab} Src Voltage: {FormatNumber(CDbl(SourceVoltageReadresult / BecoCommunicationScaleFactor), 1)}V")
                    SetText(rvbForm.lblMsgCenter, String.Format("Error: {0}", ReceivedErrorMsg))

                    'If ConsoleWriteEnable Then
                    Interlocked.Add(Heart_Beat_Timer, ReadInterval)
                    Debug.WriteLine($"Reading local voltage: {LocalVoltageReadresult} - source voltage: {SourceVoltageReadresult} - {Heart_Beat_Timer}")
                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Read)}")
                    'End If

                    If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                    ReadEvent.SafeWaitHandle.Close()

                Next
            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(rvbForm.lblMsgCenter, message)
                sb.AppendLine(message)
            End Try
        End Sub
    End Class
End Namespace
