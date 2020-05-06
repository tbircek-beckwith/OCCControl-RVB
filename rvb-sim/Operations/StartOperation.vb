Imports System.Threading
Imports System.Net

Namespace Communication.Operations

    Public Class StartOperation

        Protected Friend Sub Start()

            Try

                With RVBSim

                    Dim Connection As New ManualResetEvent(False)
                    TimersEvent = New ManualResetEvent(False)

                    errorCounter = 0
                    ReceivedErrorMsg = "None"

                    'Update protocol per user selection
                    .UpdateProtocol()

                    'set texts and buttons
                    SetText(.lblMsgCenter, "Establishing communication ...")
                    SetEnable(.btnStop, True)
                    SetEnable(.btnStart, False)
                    Disenable()

                    IPs = {.txtRead.Text, .txtWrite.Text}
                    SetText(.lblMsgCenter, "Connecting to the units ...")

                    Dim success As Boolean = False

                    If PingIPAddresses(IPs) Then

                        If ProtocolInUse = "modbus" Then
                            'modbus read communication channel
                            modbusRead = New EasyModbus.ModbusClient With
                                         {
                                             .IPAddress = RVBSim.txtRead.Text,
                                             .Port = CUShort(RVBSim.txtPort.Text),
                                             .ConnectionTimeout = 10000
                                         }

                            'modbus write communication channel
                            modbusWrite = New EasyModbus.ModbusClient With
                                         {
                                             .IPAddress = RVBSim.txtWrite.Text,
                                             .Port = CUShort(RVBSim.txtPort.Text),
                                             .ConnectionTimeout = 10000
                                         }
                            'connect the channels
                            modbusRead.Connect()
                            modbusWrite.Connect()

                            'is both channel successfully connected?
                            success = modbusRead.Connected And modbusWrite.Connected

                        ElseIf ProtocolInUse = "dnp" Then
                            tcpdnp.AsyncDNP3_0.ConsoleWriteEnable = ConsoleWriteEnable
                            dnp = New tcpdnp.AsyncDNP3_0(IPs.Length, DNP_BufferSize)
                            dnp.AsyncConnectTo(IPs, CUShort(.txtPort.Text), Connection)
                            ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived
                            success = Connection.WaitOne(1000)

                        ElseIf ProtocolInUse = "iec" Then
                            iec.AsyncIEC61850.ConsoleWriteEnable = ConsoleWriteEnable
                            iec61850 = New iec.AsyncIEC61850(iecSetting.ReadIEDName, iecSetting.WriteIEDName, IPs.Length, IEC_BufferSize)
                            iec61850.AsyncConnectTo(IPs, CUShort(.txtPort.Text), Connection)
                            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                            success = Connection.WaitOne(1000)

                        End If

                        Thread.CurrentThread.Join(100)

                        If success Then
                            SetText(.lblMsgCenter, "Connection successful ...")
                            sb.AppendLine(String.Format("{0} Successfully connected to read {1}", Now, IPs(0)))
                            sb.AppendLine(String.Format("{0} Successfully connected to write {1}", Now, IPs(1)))

                            SendSettings()

                            TimersEvent.Set()

                            ReadInterval = .heartbeattimer.Value * 250
                            WriteInterval = .heartbeattimer.Value * 900

                            'initial read of local voltage
                            ReadTickerDone.Reset()
                            ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf .PeriodicReadEvent), Nothing, ReadInterval, True)

                            WriteTickerDone.Reset()
                            WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf .PeriodicWriteEvent), Nothing, WriteInterval, False)

                        Else
                            Throw New Sockets.SocketException(CInt(Sockets.SocketError.HostUnreachable))
                        End If

                        Connection.SafeWaitHandle.Close()
                        If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Start", Thread.CurrentThread.GetHashCode)

                    Else
                        Throw New CustomExceptions("Cannot connect to the unit(s)")
                    End If
                End With

            Catch ex As Exception
                SetText(RVBSim.lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
                SetEnable(RVBSim.btnStart, True)
                SetEnable(RVBSim.btnStop, False)
                Disenable()
            End Try
        End Sub

    End Class
End Namespace

