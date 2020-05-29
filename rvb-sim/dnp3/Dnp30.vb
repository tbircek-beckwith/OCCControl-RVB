
Imports Automatak.DNP3.Interface
Imports Automatak.DNP3.Adapter
Imports System.Threading

Public Class Dnp30

    Private Shared Function GetCommandHeaders() As ICommandHeaders
        Dim crob = New ControlRelayOutputBlock(opType:=OperationType.PULSE_ON, tcc:=TripCloseCode.NUL, clear:=False, count:=1, onTime:=100, offTime:=100)
        Dim ao = New AnalogOutputDouble64(1.37)

        Return CommandSet.From(CommandHeader.From(IndexedValue.From(crob, 0)), CommandHeader.From(IndexedValue.From(ao, 1)))

    End Function

    'TODO: A better function name
    Protected Friend Async Function DNPFunctionsAsync(ByVal ipaddress As String, ByVal port As UShort) As Tasks.Task(Of Integer)
        Dim logger = New PrintingLogAdapter()
        Dim mgr As IDNP3Manager = DNP3ManagerFactory.CreateManager(concurrency:=1, logHandler:=logger)

        'TODO: IPADDRESS need to be here.
        Dim channel = mgr.AddTCPClient(id:="client",
                                       filters:=LogLevels.NORMAL Or LogLevels.APP_COMMS,
                                       retry:=New ChannelRetry(minRetryDelay:=TimeSpan.FromSeconds(1), maxRetryDelay:=TimeSpan.FromSeconds(5), reconnectDelay:=TimeSpan.FromSeconds(5)),
                                       remotes:=New List(Of IPEndpoint) From {New IPEndpoint(ipaddress, port)},
                                       listener:=ChannelListener.Print())

        Dim config = New MasterStackConfig()

        ' dnp.Send(ManualEvent:=WriteEvent, Destination:= .NumericUpDownDNPDestinationAddress.Value, Source:= .NumericUpDownDNPSourceAddress.Value, FunctionCode:=Mode.DirectOp, ObjectX:=Objects.AnalogOutput, Variation:=Variations.AnaOutBlockShort, Qualifier:=QualifierField.AnaOutBlock16bitIndex, Start16Bit:=1, Stop16Bit:=dnpSetting.RVBEnable, Value:=1, Status:=0)
        config.link.localAddr = RVBSim.NumericUpDownDNPSourceAddress.Value
        config.link.remoteAddr = RVBSim.NumericUpDownDNPDestinationAddress.Value
        config.master.disableUnsolOnStartup = False
        'config.master.

        'Dim key = New Byte(15) {}

        'For i As Integer = 0 To key.Length - 1
        '    key(i) = &HFF
        'Next
        ' Dim items As ISOEHandler

        Dim soeHandler = DataSOEHandler.Instance ' PrintingSOEHandler.Instance
        Dim application = DefaultMasterApplication.Instance

        Dim master = channel.AddMaster(id:="master",               'id for logging
                                       publisher:=soeHandler,      'callback for data processing
                                       application:=application,   ' master application instance
                                       config:=config              ' stack configuration
                                       )

        '' you a can optionally add various kinds of polls
        'Dim forwardValuePolling = master.
        'Dim integrityPoll = master.AddClassScan(ClassField.AllClasses, TimeSpan.FromMinutes(1), TaskConfig.Default)

        'dnp.Send(ReadEvent, rvbForm.NumericUpDownDNPDestinationAddress.Value, rvbForm.NumericUpDownDNPSourceAddress.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, dnpSetting.LocalVoltage)

        Dim rangePoll = master.AddRangeScan(group:=Objects.AnalogInput,                 ' object type is Analog Input or 30
                                             variation:=Variations.AnaInput16bitVar4,
                                             start:=dnpSetting.LocalVoltage,
                                             [stop]:=dnpSetting.LocalVoltage + 1,       'read one extra points
                                             period:=TimeSpan.FromSeconds(1.5),
                                             soeHandler:=soeHandler,
                                             config:=TaskConfig.Default)
        ' Dim classPoll = master.AddClassScan(ClassField.AllEventClasses, TimeSpan.FromSeconds(5), TaskConfig.Default)

        'you can also do very custom scans
        ' Dim headers = New Header() {Header.Range8(1, 2, 7, 8), Header.Count8(2, 3, 7)}

        Dim headers = New Header() {Header.Range8(group:=Objects.AnalogInput,
                                                  variation:=Variations.AnaInput16bitVar4,
                                                  startIndex:=dnpSetting.LocalVoltage,
                                                  stopIndex:=dnpSetting.LocalVoltage + 1)}

        '  Dim weirdPoll = master.AddScan(headers, TimeSpan.FromSeconds(1.5), TaskConfig.Default)

        '
        master.Enable()
        Console.WriteLine("Enter a command")

        Dim counter = 0
        Dim timer = New Stopwatch

        Dim dnpPollingData As List(Of String) = New List(Of String)()

        While RVBSim.btnStop.Enabled

            'timer.Start()

            'If timer.ElapsedMilliseconds() > 3000 Then

            '    Dim pollinResult = master.Scan(headers:=headers, config:=TaskConfig.Default).ContinueWith(Sub(result) Debug.WriteLine($"Result: {result}"))

            '    Dim readResult = master.PerformFunction("test", FunctionCode.READ, headers, TaskConfig.Default).Result
            '    counter += 1

            Dim r = Await master.Scan(headers:=headers, soeHandler:=soeHandler, config:=TaskConfig.Default)

            '    timer.Reset()
            '    If counter >= 2 Then
            '        timer.Stop()
            '        Exit While
            '    End If
            'End If
            '    Select Case Console.ReadLine()
            '        Case "a"
            ' master.ScanAllObjects(group:=30, variation:=0, soeHandler:=soeHandler, config:=TaskConfig.Default) '.ContinueWith(Sub(result) Debug.WriteLine($"Result: {result}"))

            Thread.Sleep(5000)
            '        Case "c"
            '            master.SelectAndOperate(GetCommandHeaders(), TaskConfig.Default).ContinueWith(Sub(result) Debug.WriteLine($"Result: {result}"))
            '            task.ContinueWith(Function(result) Console.WriteLine("Result: " & result.Result))
            '        Case "o"
            '            Dim crob = New ControlRelayOutputBlock(ControlCode.PULSE_ON, 1, 100, 100)
            '            Dim single_ = master.SelectAndOperate(crob, 1, TaskConfig.Default).ContinueWith(Sub(result) Debug.WriteLine($"Result: {result}"))
            '            single_.ContinueWith(Function(result) Console.WriteLine("Result: " & result.Result))
            '        Case "l"
            '            Dim filters = channel.GetLogFilters()
            '            channel.SetLogFilters(filters.Add(LogFilters.TRANSPORT_TX Or LogFilters.TRANSPORT_RX))
            '        Case "i"
            '            integrityPoll.Demand()
            '        Case "r"
            ' rangePoll.Demand()

            '            Debug.WriteLine($"----------> Weird Polling Starts")
            ' weirdPoll.Demand()
            '            Debug.WriteLine($"----------> Weird Polling Ends")

            '        Case "e"
            '            classPoll.Demand()
            '        Case "x"
            '            Return 0
            '        Case Else
            '    End Select


        End While

        channel.Shutdown()

        Return 0
    End Function
End Class