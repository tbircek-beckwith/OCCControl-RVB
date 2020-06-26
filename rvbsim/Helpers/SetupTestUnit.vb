Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
Imports iec.AsyncIEC61850
'Imports rvb_sim.dnp

Module SetupTestUnit

    ''' <summary>
    ''' Sets factory of options for the unit under test.
    ''' </summary>
    Friend Sub SendSettings()

        Try
            For Each regulator As RegulatorCommunication In Regulators

                With RVBSim

                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- START")
                    Dim WriteEvent As New ManualResetEvent(False)
                    SetText(.lblMsgCenter, "Sending settings to the units ...")

                    If ProtocolInUse = "dnp" Then

                        For Each model As DnpCommunicationModel In regulator.DnpCommunication

                            ' Start16Bit = Quantity of items

                            'Enable RVB using dnp
                            WriteEvent.Reset()
                            dnp.Send(ManualEvent:=WriteEvent,
                                     Destination:= .DNPDestinationReg1.Value,
                                     Source:= .DNPSourceReg1.Value,
                                     FunctionCode:=Mode.DirectOp,
                                     ObjectX:=Objects.AnalogOutput,
                                     Variation:=Variations.AnaOutBlockShort,
                                     Qualifier:=QualifierField.AnaOutBlock16bitIndex,
                                     Start16Bit:=1,
                                     Stop16Bit:=model.RVBEnable,
                                     Value:=1,
                                     Status:=0)
                            WriteEvent.WaitOne()

                            'set RVB heartbeat timer
                            WriteEvent.Reset()
                            dnp.Send(ManualEvent:=WriteEvent, Destination:= .DNPDestinationReg1.Value, Source:= .DNPSourceReg1.Value, FunctionCode:=Mode.DirectOp, ObjectX:=Objects.AnalogOutput, Variation:=Variations.AnaOutBlockShort, Qualifier:=QualifierField.AnaOutBlock16bitIndex, Start16Bit:=1, Stop16Bit:=model.RVBHeartBeatTimer, Value:= .HeartbeatTimerReg1.Value, Status:=0)
                            WriteEvent.WaitOne()

                            'set RVB Max
                            WriteEvent.Reset()
                            dnp.Send(ManualEvent:=WriteEvent, Destination:= .DNPDestinationReg1.Value, Source:= .DNPSourceReg1.Value, FunctionCode:=Mode.DirectOp, ObjectX:=Objects.AnalogOutput, Variation:=Variations.AnaOutBlockShort, Qualifier:=QualifierField.AnaOutBlock16bitIndex, Start16Bit:=1, Stop16Bit:=model.RVBMax, Value:= .RVBMaxReg1.Value * BecoCommunicationScaleFactor, Status:=0)
                            WriteEvent.WaitOne()

                            'set RVB Min
                            WriteEvent.Reset()
                            dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, model.RVBMin, Value:= .RVBMinReg1.Value * BecoCommunicationScaleFactor, Status:=0)
                            WriteEvent.WaitOne()

                            'set Fwd RVB Scale Factor
                            WriteEvent.Reset()
                            dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, model.FRVBScale, .FRVBScaleReg1.Value * BecoCommunicationScaleFactor, 0)
                            WriteEvent.WaitOne()

                            'set Rev RVB Scale Factor 
                            WriteEvent.Reset()
                            dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, model.RRVBScale, .RRVBScaleReg1.Value * BecoCommunicationScaleFactor, 0)
                            WriteEvent.WaitOne()

                            ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                        Next

                    ElseIf ProtocolInUse = "modbus" Then
                        ' TODO: a method to update modbusRegister with the current regulator values or
                        ' use ModbusCommunicationModel values.

                        For Each modbusRegister As ModbusCommunicationModel In regulator.ModbusCommunication

                            'Enable RVB using modbus
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBEnable, 1)

                            'set RVB heartbeat timer
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBHeartBeatTimer, .HeartbeatTimerReg1.Value)

                            'set RVB Max
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBMax, .RVBMaxReg1.Value * BecoCommunicationScaleFactor)

                            'set RVB Min
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBMin, .RVBMinReg1.Value * BecoCommunicationScaleFactor)

                            'set Fwd RVB Scale Factor
                            modbusWrite.WriteSingleRegister(modbusRegister.FRVBScale, .FRVBScaleReg1.Value * BecoCommunicationScaleFactor)

                            'set Rev RVB Scale Factor 
                            modbusWrite.WriteSingleRegister(modbusRegister.RRVBScale, .RRVBScaleReg1.Value * BecoCommunicationScaleFactor)

                        Next

                    ElseIf ProtocolInUse = "iec" Then

                        For Each iec61850Values As IECCommunicationModel In regulator.IECCommunication

                            'enable RVB using IEC61850
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.RVBEnable, "Write", 1, DataType.bool)
                            WriteEvent.WaitOne()

                            'set RVB heartbeat timer
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.RVBHeartBeatTimer, "Write", .HeartbeatTimerReg1.Value, DataType.int)
                            WriteEvent.WaitOne()

                            'set RVB Max
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.RVBMax, "Write", .RVBMaxReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                            WriteEvent.WaitOne()

                            'set RVB Min
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.RVBMin, "Write", .RVBMinReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                            WriteEvent.WaitOne()

                            'set Fwd RVB Scale Factor
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.FRVBScale, "Write", .FRVBScaleReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                            WriteEvent.WaitOne()

                            'set Rev RVB Scale Factor
                            WriteEvent.Reset()
                            iec61850.Send(WriteEvent, iec61850Values.RRVBScale, "Write", .RRVBScaleReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                            WriteEvent.WaitOne()

                        Next
                    Else
                        MsgBox("Unsupported communication protocol")
                        pause.Pause()
                    End If

                    SetText(.lblMsgCenter, "Sending completed ... reading Local Voltage")
                    If Not String.Equals(ReceivedErrorMsg, "None") Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")
                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- END")

                End With
            Next

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        End Try
    End Sub

End Module
