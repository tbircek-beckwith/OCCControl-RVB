Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
Imports iec.AsyncIEC61850

Module SetupTestUnit

    ''' <summary>
    ''' Sets factory of options for the unit under test.
    ''' </summary>
    Friend Sub SendSettings()

        Try
            For Each regulator As Regulator In Regulators

                With RVBSim

                    Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- START")
                    Dim WriteEvent As New ManualResetEvent(False)
                    SetText(.lblMsgCenter, "Sending settings to the units ...")

                    If ProtocolInUse = "dnp" Then
                        'Enable RVB using dnp
                        WriteEvent.Reset()
                        dnp.Send(ManualEvent:=WriteEvent, Destination:= .DNPDestinationReg1.Value, Source:= .DNPSourceReg1.Value, FunctionCode:=Mode.DirectOp, ObjectX:=Objects.AnalogOutput, Variation:=Variations.AnaOutBlockShort, Qualifier:=QualifierField.AnaOutBlock16bitIndex, Start16Bit:=1, Stop16Bit:=dnpSetting.RVBEnable, Value:=1, Status:=0)
                        WriteEvent.WaitOne()

                        'set RVB heartbeat timer
                        WriteEvent.Reset()
                        dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBHeartBeatTimer, .heartBeatTimer.Value, 0)
                        WriteEvent.WaitOne()

                        'set RVB Max
                        WriteEvent.Reset()
                        dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMax, .RVBMax.Value * BecoCommunicationScaleFactor, 0)
                        WriteEvent.WaitOne()

                        'set RVB Min
                        WriteEvent.Reset()
                        dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMin, .RVBMin.Value * BecoCommunicationScaleFactor, 0)
                        WriteEvent.WaitOne()

                        'set Fwd RVB Scale Factor
                        WriteEvent.Reset()
                        dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBScale, .FRVBScaleReg1.Value * BecoCommunicationScaleFactor, 0)
                        WriteEvent.WaitOne()

                        'set Rev RVB Scale Factor 
                        WriteEvent.Reset()
                        dnp.Send(WriteEvent, .DNPDestinationReg1.Value, .DNPSourceReg1.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBScale, .RevRVBScaleFactorReg1.Value * BecoCommunicationScaleFactor, 0)
                        WriteEvent.WaitOne()

                        ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                    ElseIf ProtocolInUse = "modbus" Then
                        ' TODO: a method to update modbusRegister with the current regulator values or
                        ' use ModbusCommunicationModel values.

                        For Each modbusRegister As ModbusCommunicationModel In regulator.ModbusCommunication

                            'Enable RVB using modbus
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBEnable, 1)

                            'set RVB heartbeat timer
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBHeartBeatTimer, .heartBeatTimer.Value)

                            'set RVB Max
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBMax, .RVBMax.Value * BecoCommunicationScaleFactor)

                            'set RVB Min
                            modbusWrite.WriteSingleRegister(modbusRegister.RVBMin, .RVBMin.Value * BecoCommunicationScaleFactor)

                            'set Fwd RVB Scale Factor
                            modbusWrite.WriteSingleRegister(modbusRegister.FRVBScale, .FRVBScaleReg1.Value * BecoCommunicationScaleFactor)

                            'set Rev RVB Scale Factor 
                            modbusWrite.WriteSingleRegister(modbusRegister.RRVBScale, .RevRVBScaleFactorReg1.Value * BecoCommunicationScaleFactor)

                        Next

                    ElseIf ProtocolInUse = "iec" Then
                        'enable RVB using IEC61850
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.RVBEnable, "Write", 1, DataType.bool)
                        WriteEvent.WaitOne()

                        'set RVB heartbeat timer
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.RVBHeartBeatTimer, "Write", .heartBeatTimer.Value, DataType.int)
                        WriteEvent.WaitOne()

                        'set RVB Max
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.RVBMax, "Write", .RVBMax.Value * BecoCommunicationScaleFactor, DataType.int)
                        WriteEvent.WaitOne()

                        'set RVB Min
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.RVBMin, "Write", .RVBMin.Value * BecoCommunicationScaleFactor, DataType.int)
                        WriteEvent.WaitOne()

                        'set Fwd RVB Scale Factor
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.FRVBScale, "Write", .FRVBScaleReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                        WriteEvent.WaitOne()

                        'set Rev RVB Scale Factor
                        WriteEvent.Reset()
                        iec61850.Send(WriteEvent, iecSetting.RRVBScale, "Write", .RevRVBScaleFactorReg1.Value * BecoCommunicationScaleFactor, DataType.int)
                        WriteEvent.WaitOne()

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
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        End Try
    End Sub

End Module
