Imports System.Text
Imports System.Threading
Imports Automatak.DNP3.Interface
Imports RVBSim.Communication.Operations
Imports RVBSim.PeriodicOperations

Module Declarations

    Friend Const ConsoleWriteEnable As Boolean = True       'False          ' 
    Friend Const SupportedRVBRevision As String = "15"         'Supported RVB feature document revision
    Friend Const OperatingVoltage As Integer = 900
    Friend Const M2001D_Comm_Scale As Integer = 10
    Friend Const MaxDeltaVoltage As Integer = 100
    Friend Const MinDeltaVoltage As Integer = -100
    Friend Const DeltaMessage As String = "Local Voltage + "
    Friend Const DirectMessage As String = "RVB Voltage is ="
    Friend Const DNP_BufferSize As Integer = 29
    Friend Const Modbus_BufferSize As Integer = 12
    Friend Const IEC_BufferSize As Integer = 200

    Friend sb As New StringBuilder
    Friend IPs As String() = New String(1) {}

    '''<summary>new modbus communication libraries</summary>
    Friend modbusRead As EasyModbus.ModbusClient
    Friend modbusWrite As EasyModbus.ModbusClient

    Friend dnpReadManager As IDNP3Manager
    Friend dnpPollingData As IEnumerable(Of IndexedValue(Of Analog))

    Friend start As New StartOperation
    Friend pause As New PauseOperation

    Friend periodicRead As New ReadEvents
    Friend periodicWrite As New WriteEvents
    Friend periodicReset As New ResetEvents

    Friend dnp As tcpdnp.AsyncDNP3_0
    'Friend modbus As tcpmodbus.AsyncModbus
    Friend iec61850 As iec.AsyncIEC61850

    Friend processID As Integer = 0
    Friend support As Boolean      'True rev 15 or greater False rev 8
    Friend errorCounter As Integer = 0

    Friend WriteTickerDone As New ManualResetEvent(False)
    Friend ReadTickerDone As New ManualResetEvent(False)
    Friend TimersEvent As New ManualResetEvent(False)

    Friend WriteRegisterWait As RegisteredWaitHandle
    Friend ReadRegisterWait As RegisteredWaitHandle

    Friend visibility As Boolean = True
    Friend testSetting As TestSettings
    Friend modbusRegister As ModbusSettings
    Friend dnpSetting As DnpSettings
    Friend iecSetting As IECSettings

    Friend Structure TestSettings
        Dim Protocol As String
        Dim readIpAddress As String
        Dim writeIpAddress As String
        Dim HeartbeatTimer As UShort
        Dim FwdRVBVoltage As Double
        Dim RevRVBVoltage As Double
        Dim FwdRVBVoltageScale As Double
        Dim RevRVBVoltageScale As Double
        Dim RVBMax As Double
        Dim RVBMin As Double
    End Structure

    Friend Structure ModbusSettings
        Dim Port As String
        Dim LocalVoltage As UShort
        Dim RVBEnable As UShort
        Dim FRVBValue As UShort
        Dim FRVBScale As UShort
        Dim RVBHeartBeatTimer As UShort
        Dim RVBActive As UShort
        Dim RRVBValue As UShort
        Dim RRVBScale As UShort
        Dim RVBMax As UShort
        Dim RVBMin As UShort
        Dim Factory As UShort
    End Structure

    Friend Structure DnpSettings
        Dim Port As String
        Dim source As UShort
        Dim destination As UShort
        'just get value of point, DNP only cares about the point values
        Dim LocalVoltage As UShort
        Dim RVBEnable As UShort
        Dim FRVBValue As UShort
        Dim FRVBScale As UShort
        Dim RVBHeartBeatTimer As UShort
        Dim RRVBValue As UShort
        Dim RRVBScale As UShort
        Dim RVBMax As UShort
        Dim RVBMin As UShort
    End Structure

    Friend Structure IECSettings
        Dim Port As String
        Dim ReadIEDName As String
        Dim WriteIEDName As String
        Dim iedClass As String
        Dim LocalVoltage As String
        Dim RVBEnable As String
        Dim FRVBValue As String
        Dim FRVBScale As String
        Dim RVBHeartBeatTimer As String
        Dim RRVBValue As String
        Dim RRVBScale As String
        Dim RVBMax As String
        Dim RVBMin As String
    End Structure

    ''' <summary>
    ''' Holds supported dnp3.0 data variations
    ''' </summary>
    Friend Enum Variations As Byte
        AnaOutBlockShort = 2
        AnaInput16bitVar4 = 4
    End Enum

    ''' <summary>
    ''' Holds supported dnp3.0 qualifier fields
    ''' </summary>
    Friend Enum QualifierField As Byte
        AnaInput8bitStartStop = 0
        AnaInput16bitStartStop = 1
        AnaOutBlock8bitIndex = 23
        AnaOutBlock16bitIndex = 40
    End Enum

    ''' <summary>
    ''' Holds supported dnp3.0 objects types
    ''' </summary>
    Friend Enum Objects As Byte
        AnalogInput = 30
        AnalogOutputStatus = 40
        AnalogOutput = 41
    End Enum

    Private _WriteInterval As Integer
    Friend Property WriteInterval() As Integer
        Get
            Return _WriteInterval
        End Get
        Set(ByVal value As Integer)
            _WriteInterval = value
        End Set
    End Property

    Private _ReadInterval As Integer
    Friend Property ReadInterval() As Integer
        Get
            Return _ReadInterval
        End Get
        Set(ByVal value As Integer)
            _ReadInterval = value
        End Set
    End Property

    Private _protocolinuse As String
    Friend Property ProtocolInUse() As String
        Get
            Return _protocolinuse
        End Get
        Set(ByVal value As String)
            _protocolinuse = value
        End Set
    End Property

    Private _Heart_Beat_Timer As Double = 0.0
    Friend Property Heart_Beat_Timer() As Double
        Get
            Return _Heart_Beat_Timer
        End Get
        Set(ByVal value As Double)
            _Heart_Beat_Timer = value
        End Set
    End Property

    Private _ActualLocalVoltage As Double = 0.0
    Friend Property ActualLocalVoltage() As Double
        Get
            Return _ActualLocalVoltage
        End Get
        Set(ByVal value As Double)
            _ActualLocalVoltage = value
        End Set
    End Property

    Private _Forward_RVBVoltage2Write As Double = 0.0
    Friend Property Forward_RVBVoltage2Write() As Double
        Get
            Return _Forward_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Forward_RVBVoltage2Write = value
        End Set
    End Property

    Private _Reverse_RVBVoltage2Write As Double = 0.0
    Friend Property Reverse_RVBVoltage2Write() As Double
        Get
            Return _Reverse_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Reverse_RVBVoltage2Write = value
        End Set
    End Property

    Private _readresult As UShort = 0
    Friend Property readresult() As UShort
        Get
            Return _readresult
        End Get
        Set(ByVal value As UShort)
            _readresult = value
        End Set
    End Property

    Friend Property ReceivedErrorMsg() As String = ""

End Module
