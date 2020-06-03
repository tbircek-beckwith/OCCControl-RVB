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
                Dim ReadEvent As New ManualResetEvent(False)

                If ProtocolInUse() = "dnp" Then
                    dnp.Send(ReadEvent, rvbForm.NumericUpDownDNPDestinationAddress.Value, rvbForm.NumericUpDownDNPSourceAddress.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, dnpSetting.LocalVoltage)
                    ReadEvent.WaitOne()
                    Readresult = tcpdnp.AsyncDNP3_0.result
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then
                    Debug.WriteLine("------------------- Reading Local Voltage (MODBUS) -------------------")
                    'read the user specified single modbus register.
                    Readresult = CUShort(modbusRead.ReadHoldingRegisters(CInt(rvbForm.NumericUpDownModbusLocalVoltageRegister.Value), 1).ElementAt(0))
                    Debug.WriteLine("------------------- Reading Local Voltage (MODBUS) Done -------------------")

                ElseIf ProtocolInUse() = "iec" Then
                    Debug.WriteLine("------------------- Reading Local Voltage -------------------")
                    iec61850.Send(ReadEvent, rvbForm.txtIECLocalVoltage.Text, "Read")
                    ReadEvent.WaitOne()
                    Readresult = iec.AsyncIEC61850.result
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                    Debug.WriteLine("------------------- Reading Local Voltage Done -------------------")
                End If

                SetText(rvbForm.lblLocalVoltageValue, String.Format("Remote Voltage: {0}", FormatNumber(CDbl(Readresult / M2001D_Comm_Scale), 1)))
                SetText(rvbForm.lblMsgCenter, String.Format("Error: {0}", ReceivedErrorMsg))

                'If ConsoleWriteEnable Then
                Interlocked.Add(Heart_Beat_Timer, ReadInterval)
                Debug.WriteLine($"Reading local voltage: {Readresult} - {Heart_Beat_Timer}")
                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Read)}")
                'End If

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                ReadEvent.SafeWaitHandle.Close()

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                SetText(rvbForm.lblMsgCenter, ex.Message)
                sb.AppendLine($"{Now} {ex.Message}")
            End Try
        End Sub
    End Class
End Namespace
