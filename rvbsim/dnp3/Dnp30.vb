
Imports Automatak.DNP3.Adapter
Imports Automatak.DNP3.Interface

Public Class Dnp30

    'Private Shared Function GetCommandHeaders() As ICommandHeaders
    '    Dim crob = New ControlRelayOutputBlock(opType:=OperationType.PULSE_ON, tcc:=TripCloseCode.NUL, clear:=False, count:=1, onTime:=100, offTime:=100)
    '    Dim ao = New AnalogOutputDouble64(1.37)

    '    Return CommandSet.From(CommandHeader.From(IndexedValue.From(crob, 0)), CommandHeader.From(IndexedValue.From(ao, 1)))

    'End Function

    ''' <summary>
    ''' Open DNP communication as a Master
    ''' </summary>
    ''' <param name="ipaddress"></param>
    ''' <param name="port"></param>
    Protected Friend Sub OpenConnection(ByVal ipaddress As String, ByVal port As UShort)

        Dim logger = New PrintingLogAdapter()
        Dim channelPrinter As IChannelListener = ChannelListener.Print()

        ' generate
        Dim mgr As IDNP3Manager = DNP3ManagerFactory.CreateManager(concurrency:=1, logHandler:=logger)

        ' generate the channel
        Dim channel = mgr.AddTCPClient(id:="client",
                                       filters:=LogLevels.NORMAL Or LogLevels.APP_COMMS,
                                       retry:=New ChannelRetry(minRetryDelay:=TimeSpan.FromSeconds(1), maxRetryDelay:=TimeSpan.FromSeconds(5), reconnectDelay:=TimeSpan.FromSeconds(5)),
                                       remotes:=New List(Of IPEndpoint) From {New IPEndpoint(ipaddress, port)},
                                       listener:=channelPrinter)

        ' configure master and link
        Dim config = New MasterStackConfig()
        config.link.localAddr = RVBSim.NumericUpDownDNPSourceAddress.Value
        config.link.remoteAddr = RVBSim.NumericUpDownDNPDestinationAddress.Value
        config.master.disableUnsolOnStartup = False

        Dim soeHandler = DataSOEHandler.Instance ' PrintingSOEHandler.Instance
        Dim application = DefaultMasterApplication.Instance

        Dim master = channel.AddMaster(id:="master",               'id for logging
                                       publisher:=soeHandler,      'callback for data processing
                                       application:=application,   ' master application instance
                                       config:=config              ' stack configuration
                                       )

        ' establish Local & Source Voltage scan.
        Dim headers = New Header() {Header.Range8(group:=Objects.AnalogInput,
                                                  variation:=Variations.AnaInput16bitVar4,
                                                  startIndex:=dnpSetting.LocalVoltage,
                                                  stopIndex:=dnpSetting.LocalVoltage + 1)}

        ' open communication
        master.Enable()

    End Sub
End Class