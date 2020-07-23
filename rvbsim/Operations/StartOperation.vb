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
                    ' TimersEvent = New ManualResetEvent(False)

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

                        ElseIf ProtocolInUse = "iec" Then

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

                            WriteRegisterWaits = New List(Of RegisteredWaitHandle)
                            SetMultiPhase(rvbForm:=RVBSim)

                        Else
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

        Private Sub SetMultiPhase(ByRef rvbForm As RVBSim)

            Try
                Dim heartbeat = GetSpecificControl(rvbForm:=RVBSim, featureType:="Settings", featureName:="HeartbeatTimer")

                ' always reading at 500msec, this is not effected by the user selection.
                ' this way the application would have the highest sensitivity to voltage changes.
                ' does not produce a lot of traffic
                ReadInterval = 500
                ReadTickerDone.Reset()

                ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(waitObject:=ReadTickerDone,
                                                                           callBack:=New WaitOrTimerCallback(AddressOf RVBSim.PeriodicReadEvent),
                                                                           state:=Nothing,  ' regulator - 1,    ' 
                                                                           millisecondsTimeOutInterval:=ReadInterval,
                                                                           executeOnlyOnce:=False)

                ReadingTimer.Start()

                For Each beat In heartbeat

                    TimersEvent = New ManualResetEvent(True)
                    TimersEvents.Add(TimersEvent)

                    With CType(beat, NumericUpDown)

                        ' write events effected by the user selections.
                        WriteInterval = (.Value - 0.2) * 1000 ' * 900

                        Dim regulator = Val(beat.Name.Last())

                        HeartBeatTimers.Item(regulator - 1) = .Value

                        Interlocked.Exchange(WriteIntervals(regulator - 1), WriteInterval)

                        WriteTickerDone.Reset()

                        Interlocked.Exchange(WriteTickerDones(regulator - 1), WriteTickerDone)
                        WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(waitObject:=WriteTickerDones(regulator - 1),
                                                                                   callBack:=New WaitOrTimerCallback(AddressOf RVBSim.PeriodicWriteEventNew),
                                                                                   state:=regulator - 1,
                                                                                   millisecondsTimeOutInterval:=WriteInterval,
                                                                                   executeOnlyOnce:=False)

                        WriteRegisterWaits.Add(WriteRegisterWait)

                        WritingTimers(regulator - 1).Start()
                    End With

                    If rvbForm.SinglePhaseCheckBox.Checked Then
                        Exit For
                    End If
                Next

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

