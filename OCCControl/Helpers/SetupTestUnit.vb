Imports System.Threading

'custom libraries
Imports tcpdnp.AsyncDNP3_0
Imports iec.AsyncIEC61850

Module SetupTestUnit

    Friend Sub SendSettings()

        Try

            With RVBSim

                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- SendSettings --- START", Thread.CurrentThread.GetHashCode)
                Dim WriteEvent As New ManualResetEvent(False)
                SetText(.lblMsgCenter, "Sending settings to the units ...")

                If ProtocolInUse = "dnp" Then
                    'Enable RVB using dnp
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBEnable, 1, 0)
                    WriteEvent.WaitOne()

                    'set RVB heartbeat timer
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBHeartBeatTimer, .heartbeattimer.Value, 0)
                    WriteEvent.WaitOne()

                    'set RVB Max
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMax, .RVBMax.Value * M2001D_Comm_Scale, 0)
                    WriteEvent.WaitOne()

                    'set RVB Min
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMin, .RVBMin.Value * M2001D_Comm_Scale, 0)
                    WriteEvent.WaitOne()

                    'set Fwd RVB Scale Factor
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBScale, .FwdRVBScaleFactor.Value * M2001D_Comm_Scale, 0)
                    WriteEvent.WaitOne()

                    'set Rev RVB Scale Factor 
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, .NumericUpDownDNPDestinationAddress.Value, .NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBScale, .RevRVBScaleFactor.Value * M2001D_Comm_Scale, 0)
                    WriteEvent.WaitOne()

                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse = "modbus" Then

                    'Enable RVB using modbus
                    modbusWrite.WriteSingleRegister(modbusRegister.RVBEnable, 1)

                    'set RVB heartbeat timer
                    modbusWrite.WriteSingleRegister(modbusRegister.RVBHeartBeatTimer, .heartbeattimer.Value)

                    'set RVB Max
                    modbusWrite.WriteSingleRegister(modbusRegister.RVBMax, .RVBMax.Value * M2001D_Comm_Scale)

                    'set RVB Min
                    modbusWrite.WriteSingleRegister(modbusRegister.RVBMin, .RVBMin.Value * M2001D_Comm_Scale)

                    'set Fwd RVB Scale Factor
                    modbusWrite.WriteSingleRegister(modbusRegister.FRVBScale, .FwdRVBScaleFactor.Value * M2001D_Comm_Scale)

                    'set Rev RVB Scale Factor 
                    modbusWrite.WriteSingleRegister(modbusRegister.RRVBScale, .RevRVBScaleFactor.Value * M2001D_Comm_Scale)

                ElseIf ProtocolInUse = "iec" Then
                    'enable RVB using IEC61850
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.RVBEnable, "Write", 1, DataType.bool)
                    WriteEvent.WaitOne()

                    'set RVB heartbeat timer
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.RVBHeartBeatTimer, "Write", .heartbeattimer.Value, DataType.int)
                    WriteEvent.WaitOne()

                    'set RVB Max
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.RVBMax, "Write", .RVBMax.Value * M2001D_Comm_Scale, DataType.int)
                    WriteEvent.WaitOne()

                    'set RVB Min
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.RVBMin, "Write", .RVBMin.Value * M2001D_Comm_Scale, DataType.int)
                    WriteEvent.WaitOne()

                    'set Fwd RVB Scale Factor
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.FRVBScale, "Write", .FwdRVBScaleFactor.Value * M2001D_Comm_Scale, DataType.int)
                    WriteEvent.WaitOne()

                    'set Rev RVB Scale Factor
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, iecSetting.RRVBScale, "Write", .RevRVBScaleFactor.Value * M2001D_Comm_Scale, DataType.int)
                    WriteEvent.WaitOne()

                Else
                    MsgBox("Unsupported communication protocol")
                    pause.Pause()
                End If

                SetText(.lblMsgCenter, "Sending completed ... reading Local Voltage")
                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- SendSettings --- END", Thread.CurrentThread.GetHashCode)

            End With

        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

End Module
