Imports System.Threading

'custom libraries
Imports tcpmodbus.AsyncModbus
Imports tcpdnp.AsyncDNP3_0
Imports iec.AsyncIEC61850

Namespace PeriodicOperations
    Public Class ReadEvents

        Protected Friend Sub Read(ByRef rvbForm As RVBSim)
            Try
                Dim ReadEvent As New ManualResetEvent(False)

                If ProtocolInUse() = "dnp" Then
                    dnp.Send(ReadEvent, rvbForm.NumericUpDownDNPDestinationAddress.Value, rvbForm.NumericUpDownDNPSourceAddress.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, dnpSetting.LocalVoltage)
                    ReadEvent.WaitOne()
                    readresult = tcpdnp.AsyncDNP3_0.result
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then
                    modbus.Send(ReadEvent, tcpmodbus.AsyncModbus.Functions.Read, rvbForm.NumericUpDownModbusLocalVoltageRegister.Value, 1)
                    ReadEvent.WaitOne()
                    readresult = tcpmodbus.AsyncModbus.result
                    ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

                ElseIf ProtocolInUse() = "iec" Then
                    If ConsoleWriteEnable Then Console.WriteLine("{0}------------------- Reading Local Voltage -------------------", vbCrLf)
                    iec61850.Send(ReadEvent, rvbForm.txtIECLocalVoltage.Text, "Read")
                    ReadEvent.WaitOne()
                    readresult = iec.AsyncIEC61850.result
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                    If ConsoleWriteEnable Then Console.WriteLine("{0}------------------- Reading Local Voltage Done -------------------", vbCrLf)
                End If

                SetText(rvbForm.lblLocalVoltageValue, String.Format("Remote Voltage: {0}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1)))
                SetText(rvbForm.lblMsgCenter, String.Format("Error: {0}", ReceivedErrorMsg))

                If ConsoleWriteEnable Then
                    Heart_Beat_Timer += ReadInterval
                    Console.WriteLine("Reading local voltage: {0} - {1}", readresult, Heart_Beat_Timer)
                    Console.WriteLine("Current thread is # {0} --- PeriodicReadEvent", Thread.CurrentThread.GetHashCode)
                End If

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))

                ReadEvent.SafeWaitHandle.Close()

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                SetText(rvbForm.lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            End Try
        End Sub
    End Class
End Namespace
