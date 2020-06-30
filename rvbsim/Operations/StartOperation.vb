'Imports System.Net
Imports System.Threading
'Imports Automatak.DNP3.Adapter
'Imports Automatak.DNP3.Interface
'Imports rvb_sim.dnp

Namespace Communication.Operations

    Public Class StartOperation

        Friend Sub ReceiveData()
            Debug.WriteLine("we receive something...")
        End Sub

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
                    SetEnable(.StopButton, True)
                    SetEnable(.StartButton, False)
                    Disenable()

                    IPs = { .ReadIpAddr.Text, .WriteIpAddr.Text}
                    SetText(.lblMsgCenter, "Connecting to the units ...")

                    Dim success As Boolean = False

                    If PingIPAddresses(IPs) Then

                        If ProtocolInUse = "modbus" Then
                            'modbus read communication channel
                            modbusRead = New EasyModbus.ModbusClient With
                                         {
                                             .IPAddress = RVBSim.ReadIpAddr.Text,
                                             .Port = CUShort(RVBSim.PortReg1.Text),
                                             .ConnectionTimeout = 10000,
                                             .UnitIdentifier = 255                      ' to prevent any trouble if the unit set to different address
                                         }

                            'modbus write communication channel
                            modbusWrite = New EasyModbus.ModbusClient With
                                         {
                                             .IPAddress = RVBSim.WriteIpAddr.Text,
                                             .Port = CUShort(RVBSim.PortReg1.Text),
                                             .ConnectionTimeout = 10000,
                                             .UnitIdentifier = 255                      ' to prevent any trouble if the unit set to different address
                                         }
                            'connect the channels
                            modbusRead.Connect()
                            modbusWrite.Connect()

                            ' AddHandler modbusRead.ReceiveDataChanged, AddressOf ReceiveData

                            'is both channel successfully connected?
                            success = modbusRead.Connected And modbusWrite.Connected

                        ElseIf ProtocolInUse = "dnp" Then
                            tcpdnp.AsyncDNP3_0.ConsoleWriteEnable = True
                            dnp = New tcpdnp.AsyncDNP3_0(IPs.Length, DNP_BufferSize)
                            dnp.AsyncConnectTo(IPs, .PortReg1.Text, Connection)
                            ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived
                            success = Connection.WaitOne(1000)


                            'dnpReadManager = DNP3ManagerFactory.CreateManager(1, New PrintingLogAdapter())

                            'Dim channel As IChannel = dnpReadManager.AddTCPClient(id:="dnpReadingChannel",
                            '                                                      filters:=LogLevels.NORMAL,
                            '                                                      retry:=New ChannelRetry(minRetryDelay:=TimeSpan.FromSeconds(1), maxRetryDelay:=TimeSpan.FromSeconds(10), reconnectDelay:=TimeSpan.FromSeconds(5)),
                            '                                                      remotes:=New List(Of IPEndpoint) From {New IPEndpoint(RVBSim.txtRead.Text, RVBSim.txtPort.Text)},
                            '                                                      listener:=ChannelListener.Print())
                            ' Dim newDnpRead = New Dnp30().OpenConnection(RVBSim.txtRead.Text, RVBSim.txtPort.Text)

                            'Dim newDnp = New Dnp30

                            'newDnp.OpenConnection(RVBSim.txtRead.Text, RVBSim.txtPort.Text)


                            ' Debug.WriteLine($"dnp reads: {newDnpRead}")

                        ElseIf ProtocolInUse = "iec" Then
                            ' iec.AsyncIEC61850.ConsoleWriteEnable = ConsoleWriteEnable
                            iec61850 = New iec.AsyncIEC61850(iecSetting.ReadIEDName, iecSetting.WriteIEDName, IPs.Length, IEC_BufferSize)
                            iec61850.AsyncConnectTo(IPs, .PortReg1.Text, Connection)
                            ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                            success = Connection.WaitOne(1000)

                        End If

                        Thread.CurrentThread.Join(100)

                        If success Then
                            SetText(.lblMsgCenter, "Connection successful ...")

                            For Each ip As String In IPs
                                sb.AppendLine($"{Now} Successfully connected to read {ip}")
                            Next

                            ' set factory option and etc.
                            SendSettings()

                            TimersEvent.Set()

                            ReadInterval = .HeartbeatTimerReg1.Value * 250
                            WriteInterval = .HeartbeatTimerReg1.Value * 900

                            'initial read of local voltage
                            ReadTickerDone.Reset()
                            ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf .PeriodicReadEvent), Nothing, ReadInterval, True)

                            WriteTickerDone.Reset()
                            WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf .PeriodicWriteEvent), Nothing, WriteInterval, False)

                        Else
                            'Throw New Sockets.SocketException(CInt(Sockets.SocketError.HostUnreachable))
                            Throw New CustomExceptions("Host(s) is(are) unreachable")
                        End If

                        Connection.SafeWaitHandle.Close()
                        Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Start)}")

                    Else
                        Throw New CustomExceptions("Cannot connect to the unit(s)")
                    End If
                End With

            Catch ex As Exception
                Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
                SetText(RVBSim.lblMsgCenter, message)
                sb.AppendLine(message)
                SetEnable(RVBSim.StartButton, True)
                SetEnable(RVBSim.StopButton, False)
                Disenable()
            End Try
        End Sub

    End Class
End Namespace

