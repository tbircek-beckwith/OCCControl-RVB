Imports iec.iec
Imports tcpmodbus.modbus
Imports tcpdnp.AsyncTcp4RVB
Imports System.Net
Imports System.Timers
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class RVBSim
    Const SupportedRVBRevision As String = "15"         'Supported RVB feature document revision
    Const OperatingVoltage As Integer = 900
    Const M2001D_Comm_Scale As Integer = 10
    Const MaxDeltaVoltage As Integer = 100
    Const MinDeltaVoltage As Integer = -100
    Const DeltaMessage As String = "Local Voltage + "
    Const DirectMessage As String = "RVB Voltage is ="

    Public sb As New StringBuilder

    'Private ReadLocalVoltageTimer As New System.Timers.Timer
    'Private WriteLocalVoltageTimer As New System.Timers.Timer

    'Private m_ip, mip As IPAddress
    'Private m_port As UShort

    'Private delayforOmicronPowerUp As Integer = 0
    Private dnp As New tcpdnp.AsyncTcp4RVB
    Private modbus As New tcpmodbus.AsyncTcp4RVB
    Private iec As New iec.iec
    ' Private iec2 As New iec.AsyncTcp4RVB

    Private processID As Integer = 0
    Private support As Boolean      'True rev 15 or greater False rev 8

    Private WriteTickerDone As New ManualResetEvent(False)
    Private ReadTickerDone As New ManualResetEvent(False)
    Private TimersEvent As New ManualResetEvent(False)
    'Private CalculateRVBVoltageEvent As New ManualResetEvent(False)
    Private WriteRegisterWait As RegisteredWaitHandle
    Private ReadRegisterWait As RegisteredWaitHandle

    Public visibility As Boolean = True
    Public testSetting As TestSettings
    Public modbusRegister As ModbusSettings
    Public dnpSetting As DnpSettings
    Public iecSetting As IECSettings

    Public Structure TestSettings
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

    Public Structure ModbusSettings
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

    Public Structure DnpSettings
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

    Public Structure IECSettings
        Dim Port As String
        Dim iedName As String
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

    Private _WriteInterval As Integer
    Public Property WriteInterval() As Integer
        Get
            Return _WriteInterval
        End Get
        Set(ByVal value As Integer)
            _WriteInterval = value
        End Set
    End Property

    Private _ReadInterval As Integer
    Public Property ReadInterval() As Integer
        Get
            Return _ReadInterval
        End Get
        Set(ByVal value As Integer)
            _ReadInterval = value
        End Set
    End Property

    Private _protocolinuse As String
    Public Property ProtocolInUse() As String
        Get
            Return _protocolinuse
        End Get
        Set(ByVal value As String)
            _protocolinuse = value
        End Set
    End Property

    Private _Heart_Beat_Timer As Double = 0.0
    Public Property Heart_Beat_Timer() As Double
        Get
            Return _Heart_Beat_Timer
        End Get
        Set(ByVal value As Double)
            _Heart_Beat_Timer = value
        End Set
    End Property

    Private _ActualLocalVoltage As Double = 0.0
    Public Property ActualLocalVoltage() As Double
        Get
            Return _ActualLocalVoltage
        End Get
        Set(ByVal value As Double)
            _ActualLocalVoltage = value
        End Set
    End Property

    Private _Forward_RVBVoltage2Write As Double = 0.0
    Public Property Forward_RVBVoltage2Write() As Double
        Get
            Return _Forward_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Forward_RVBVoltage2Write = value
        End Set
    End Property

    Private _Reverse_RVBVoltage2Write As Double = 0.0
    Public Property Reverse_RVBVoltage2Write() As Double
        Get
            Return _Reverse_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Reverse_RVBVoltage2Write = value
        End Set
    End Property

    Private _readresult As UShort = 0
    Public Property readresult() As UShort
        Get
            Return _readresult
        End Get
        Set(ByVal value As UShort)
            _readresult = value
        End Set
    End Property

    Private _errorMsg As String = ""
    Public Property ReceivedErrorMsg() As String
        Get
            Return _errorMsg
        End Get
        Set(ByVal value As String)
            _errorMsg = value
        End Set
    End Property

    Private Delegate Sub SetTextDelegate(ByVal Label As Label, ByVal itsText As String)

    Public Sub SetText(ByVal Label As Label, ByVal itsText As String)
        Try
            If Label1.InvokeRequired Then
                Dim del As New SetTextDelegate(AddressOf SetText)
                Label1.Invoke(del, New Object() {New Label, itsText})
            Else
                Label1.Text = itsText
            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- SetText --- {1}", Thread.CurrentThread.ManagedThreadId, itsText)
            GC.Collect()
        End Try
    End Sub

    Private Sub GenerateRVBVoltage2Transfer() '(ByVal ManualEvent As ManualResetEvent)
        Try
            Dim Forward_RVBVoltage2OperateWith As Double = 0.0
            Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

            Select Case radUseFixedVoltage.Checked
                Case False
                    ActualLocalVoltage = readresult / M2001D_Comm_Scale
                    If Not ActualLocalVoltage = 0.0 Then
                        Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(FwdDeltaVoltage.Value))
                        Reverse_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(RevDeltaVoltage.Value))
                    Else
                        Forward_RVBVoltage2OperateWith = 0.0
                        Reverse_RVBVoltage2OperateWith = 0.0
                    End If
                Case True
                    Forward_RVBVoltage2OperateWith = CDbl(FwdDeltaVoltage.Value)
                    Reverse_RVBVoltage2OperateWith = CDbl(RevDeltaVoltage.Value)
            End Select
            Forward_RVBVoltage2OperateWith *= CDbl(FwdRVBScaleFactor.Value)
            Reverse_RVBVoltage2OperateWith *= CDbl(RevRVBScaleFactor.Value)

            Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
            Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            'ManualEvent.Set()
            Console.WriteLine("Current thread is # {0} --- GenerateRVBVoltage2Transfer", Thread.CurrentThread.ManagedThreadId)
            GC.Collect()
        End Try
    End Sub

    Private Sub ResetTimers()
        Try
            'WriteRegisterWait.Unregister(Nothing)
            Thread.CurrentThread.Join(50)
            ReadTickerDone.Reset()
            ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf PeriodicReadEvent), Nothing, ReadInterval, False)
            WriteTickerDone.Reset()
            WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf PeriodicWriteEvent), Nothing, WriteInterval, False)
            Console.WriteLine("{0}Resetting timers{0}", vbCrLf)
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- ResetTimers", Thread.CurrentThread.ManagedThreadId)
            GC.Collect()
        End Try

    End Sub

    Private Sub PeriodicWriteEvent(ByVal state As Object, ByVal timeOut As Boolean) '(ByVal mip As IPAddress, ByVal m_port As UShort)

        If timeOut Then
            'Console.WriteLine("----------------------------------- Start to write -----------------------------------")
            Heart_Beat_Timer = 0
            ReadRegisterWait.Unregister(Nothing)
            Dim WriteEvent As New ManualResetEvent(False)
            Try
                'Console.WriteLine("ticker{1} ----------------------------------- Start to write :{0} -----------------------------------", Now.Ticks, vbCrLf)

                If ProtocolInUse() = "dnp" Then         ' dnpbutton.Checked Then
                    'transmit Forward RVB Voltage
                    dnp = New tcpdnp.AsyncTcp4RVB
                    dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    'Console.WriteLine("--------------------------- Writing RVB Voltage(DNP) ------------------------------")
                    ReceivedErrorMsg = tcpdnp.StateObject.ErrorReceived
                    dnp = Nothing

                ElseIf ProtocolInUse() = "modbus" Then          ' modbusbox.Checked Then
                    'transmit Forward RVB Voltage
                    modbus = New tcpmodbus.AsyncTcp4RVB
                    modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, NumericUpDownModbusFwdRVBVoltageRegister.Value, CUShort(Forward_RVBVoltage2Write), WriteEvent)
                    WriteEvent.WaitOne()

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, NumericUpDownModbusRevRVBVoltageRegister.Value, CUShort(Reverse_RVBVoltage2Write), WriteEvent)
                    WriteEvent.WaitOne()
                    'Console.WriteLine("--------------------------- Writing RVB Voltage(Modbus) ------------------------------")
                    ReceivedErrorMsg = modbus.ErrorReceived

                    modbus = Nothing

                    'ElseIf ProtocolInUse() = "iec" Then         ' iec61850box.Checked Then
                    '    'transmit Forward RVB Voltage
                    '    iec.iec(mip, m_port, txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write), iecSetting.iedName)
                    '    'WriteEvent.Reset()
                    '    'iec2.Send(txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write), iecSetting.iedName, WriteEvent)
                    '    'WriteEvent.WaitOne()
                    '    'transmit Reverse RVB Voltage
                    '    iec.iec(mip, m_port, txtIECRevRVBVoltage.Text, "Write", CUShort(Reverse_RVBVoltage2Write), iecSetting.iedName)
                    '    'WriteEvent.Reset()
                    '    'iec2.Send(txtIECRevRVBVoltage.Text, "Write", CUShort(Reverse_RVBVoltage2Write), iecSetting.iedName, WriteEvent)
                    '    'WriteEvent.WaitOne()
                End If

                WriteRegisterWait.Unregister(Nothing)

                Console.WriteLine("{0}------------- Fwd RVB Voltage: {1} -------------{0}------------- RevRVB Voltage: {2} -------------{0}------------- Error: {3}{0}", vbCrLf, FormatNumber((Forward_RVBVoltage2Write / M2001D_Comm_Scale), 1), FormatNumber((Reverse_RVBVoltage2Write / M2001D_Comm_Scale), 1), ReceivedErrorMsg)

                SetText(Label1, String.Format("     Reads: {0}{3}Fwd RVB: {1}{3}Rev RVB: {2}{3}Error: {4}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1), FormatNumber((Forward_RVBVoltage2Write / M2001D_Comm_Scale), 1), FormatNumber((Reverse_RVBVoltage2Write / M2001D_Comm_Scale), 1), vbCrLf, ReceivedErrorMsg))

                ' WriteEvent.Close()

            Catch ex As Exception
                SetText(Label1, ex.ToString)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            Finally
                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))
                WriteEvent.SafeWaitHandle.Close()
                ResetTimers()
                Console.WriteLine("Current thread is # {0} --- PeriodicWriteEvent", Thread.CurrentThread.ManagedThreadId)
                GC.Collect()
            End Try
        Else
            WriteRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Private Sub PeriodicReadEvent(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut Then
            ' Console.WriteLine("----------------------------------- Start to read -----------------------------------")
            Dim ReadEvent As New ManualResetEvent(False)
            'Console.WriteLine("----------------------------------- Start to read :{0} -----------------------------------", Now.Ticks)
            Try

                If ProtocolInUse() = "dnp" Then
                    'ReadEvent.Reset()
                    dnp = New tcpdnp.AsyncTcp4RVB
                    tcpdnp.AsyncTcp4RVB.result = 0
                    dnp.Send(ReadEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, dnpSetting.LocalVoltage)
                    ReadEvent.WaitOne()
                    readresult = tcpdnp.AsyncTcp4RVB.result
                    ReceivedErrorMsg = tcpdnp.StateObject.ErrorReceived
                    dnp = Nothing
                ElseIf ProtocolInUse() = "modbus" Then
                    modbus = New tcpmodbus.AsyncTcp4RVB
                    modbus.Send(tcpmodbus.AsyncTcp4RVB.f.read, NumericUpDownModbusLocalVoltageRegister.Value, 1, ReadEvent)
                    ReadEvent.WaitOne()
                    readresult = modbus.result
                    modbus = Nothing
                    'ElseIf ProtocolInUse() = "iec" Then
                    '    readresult = (iec.iec(m_ip, m_port, txtIECLocalVoltage.Text, "Connect", , iecSetting.iedName))
                    '    '    ReadEvent.Reset()
                    '    '    iec2.Send(txtIECLocalVoltage.Text, ReadEvent)
                    '    '    ReadEvent.WaitOne()
                    '    '    readresult = iec2.result
                End If

                Heart_Beat_Timer += ReadInterval

                ' Dim CalculateRVBVoltageEvent As New ManualResetEvent(False)
                GenerateRVBVoltage2Transfer() '(CalculateRVBVoltageEvent)
                ' CalculateRVBVoltageEvent.WaitOne()
               
                Console.WriteLine("Reading local voltage: {0} - {1}", readresult, Heart_Beat_Timer)
              
            Catch ex As Exception
                SetText(Label1, ex.ToString)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            Finally
                ReadEvent.SafeWaitHandle.Close()
                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))
                'Console.WriteLine("----------------------------------- End of read -----------------------------------")
                GC.Collect()
            End Try
        Else
            ReadRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Private Sub Disenable()
        Try
            With btnStart
                'dnp settings dis/enable
                NumericUpDownDNPSourceAddress.Enabled = .Enabled
                NumericUpDownDNPDestinationAddress.Enabled = .Enabled

                'modbus settings dis/enable
                NumericUpDownModbusLocalVoltageRegister.Enabled = .Enabled
                NumericUpDownModbusFwdRVBVoltageRegister.Enabled = .Enabled
                NumericUpDownModbusRevRVBVoltageRegister.Enabled = .Enabled

                'iec61850 settings dis/enable
                txtIECLocalVoltage.Enabled = .Enabled
                txtIECFwdRVBVoltage.Enabled = .Enabled
                txtIECRevRVBVoltage.Enabled = .Enabled

                'communication settings dis/enable
                txtWrite.Enabled = .Enabled
                txtRead.Enabled = .Enabled
                txtPort.Enabled = .Enabled

                'protocol options dis/enable
                dnpbutton.Enabled = .Enabled
                modbusbox.Enabled = .Enabled
                iec61850box.Enabled = .Enabled

                'general rvb settings dis/enable
                heartbeattimer.Enabled = .Enabled
                radUseDeltaVoltage.Enabled = .Enabled
                radUseFixedVoltage.Enabled = .Enabled
                FwdRVBScaleFactor.Enabled = .Enabled
                RVBMax.Enabled = .Enabled
                RVBMin.Enabled = .Enabled
                RevRVBScaleFactor.Enabled = .Enabled

            End With
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- Disenable", Thread.CurrentThread.GetHashCode)
            ' Thread.CurrentThread.Join(100)
            GC.Collect()
        End Try
    End Sub

    Private Sub UpdateProtocol()
        If modbusbox.Checked Then
            ProtocolInUse = "modbus"
        ElseIf dnpbutton.Checked Then
            ProtocolInUse = "dnp"
        ElseIf iec61850box.Checked Then
            ProtocolInUse = "iec"
        End If
        Console.WriteLine("Current thread is # {0} --- UpdateProtocol", Thread.CurrentThread.ManagedThreadId)
    End Sub

    Private Sub SendSettings()

        Dim WriteEvent As New ManualResetEvent(False)
        Try
            'ReadLocalVoltageTimer.Stop()
            SetText(Label1, "Sending settings to the units ...")

            If ProtocolInUse = "dnp" Then
                'Enable RVB using dnp
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBEnable, 1, 0)
                WriteEvent.WaitOne()

                'set RVB heartbeat timer
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBHeartBeatTimer, heartbeattimer.Value, 0)
                WriteEvent.WaitOne()

                'set RVB Max
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMax, RVBMax.Value * M2001D_Comm_Scale, 0)
                WriteEvent.WaitOne()

                'set RVB Min
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RVBMin, RVBMin.Value * M2001D_Comm_Scale, 0)
                WriteEvent.WaitOne()

                'set Fwd RVB Scale Factor
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBScale, FwdRVBScaleFactor.Value * M2001D_Comm_Scale, 0)
                WriteEvent.WaitOne()

                'set Rev RVB Scale Factor 
                WriteEvent.Reset()
                dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBScale, RevRVBScaleFactor.Value * M2001D_Comm_Scale, 0)
                WriteEvent.WaitOne()

            ElseIf ProtocolInUse = "modbus" Then
                'Enable RVB using modbus
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RVBEnable, 1, WriteEvent)
                WriteEvent.WaitOne()

                'set RVB heartbeat timer
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RVBHeartBeatTimer, heartbeattimer.Value, WriteEvent)
                WriteEvent.WaitOne()

                'set RVB Max
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RVBMax, RVBMax.Value * M2001D_Comm_Scale, WriteEvent)
                WriteEvent.WaitOne()

                'set RVB Min
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RVBMin, RVBMin.Value * M2001D_Comm_Scale, WriteEvent)
                WriteEvent.WaitOne()

                'set Fwd RVB Scale Factor
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RVBMin, RVBMin.Value * M2001D_Comm_Scale, WriteEvent)
                WriteEvent.WaitOne()

                'set Rev RVB Scale Factor 
                WriteEvent.Reset()
                modbus.Send(tcpmodbus.AsyncTcp4RVB.f.write, modbusRegister.RRVBScale, RevRVBScaleFactor.Value * M2001D_Comm_Scale, WriteEvent)
                WriteEvent.WaitOne()

                'ElseIf ProtocolInUse = "iec" Then
                '    'enable RVB using IEC61850
                '    iec.iec(mip, m_port, iecSetting.RVBEnable, "Write", 1, iecSetting.iedName, DataType.bool)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
                '    'set RVB heartbeat timer
                '    iec.iec(mip, m_port, iecSetting.RVBHeartBeatTimer, "Write", heartbeattimer.Value, iecSetting.iedName)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
                '    ''set RVB Max
                '    iec.iec(mip, m_port, iecSetting.RVBMax, "Write", RVBMax.Value * M2001D_Comm_Scale, iecSetting.iedName)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
                '    ''set RVB Min
                '    iec.iec(mip, m_port, iecSetting.RVBMin, "Write", RVBMin.Value * M2001D_Comm_Scale, iecSetting.iedName)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
                '    ''set Fwd RVB Scale Factor
                '    iec.iec(mip, m_port, iecSetting.FRVBScale, "Write", FwdRVBScaleFactor.Value * M2001D_Comm_Scale, iecSetting.iedName)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
                '    ''set Rev RVB Scale Factor
                '    iec.iec(mip, m_port, iecSetting.RRVBScale, "Write", RevRVBScaleFactor.Value * M2001D_Comm_Scale, iecSetting.iedName)
                '    'WriteEvent.Reset()
                '    'iec2.Send(WriteEvent)
                '    'WriteEvent.WaitOne()
            Else
                MsgBox("Unsupported communication protocol")
                Pause()
            End If
            SetText(Label1, "Sending completed ... reading Local Voltage")
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            WriteEvent.SafeWaitHandle.Close()
            Console.WriteLine("Current thread is # {0} --- SendSettings", Thread.CurrentThread.ManagedThreadId)
            GC.Collect()
        End Try
    End Sub

    Private Sub Start()
        Dim Connection As New ManualResetEvent(False)
        TimersEvent = New ManualResetEvent(False)
        Try
            ReceivedErrorMsg = "None"
            'Update protocol per user selection
            UpdateProtocol()
            SetText(Label1, "Establishing communication ...")

            btnStop.Enabled = True
            btnStart.Enabled = False

            Disenable()

            Dim IPs As String() = {txtRead.Text, txtWrite.Text}
            SetText(Label1, "Connecting to the units ...")

            Dim success As Boolean = False

            If PingIPAddresses(IPs) Then

                If ProtocolInUse = "modbus" Then
                    modbus = New tcpmodbus.AsyncTcp4RVB
                    modbus.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                ElseIf ProtocolInUse = "dnp" Then
                    dnp = New tcpdnp.AsyncTcp4RVB
                    dnp.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                ElseIf ProtocolInUse = "iec" Then
                    'iec2.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                    'SendSettings()
                    Connection.Set()
                End If

                success = Connection.WaitOne(30)

                If success Then
                    SetText(Label1, "Connection successful ...")
                    sb.AppendLine(String.Format("{0} Successfully connected to read {1}", Now, IPs(0)))
                    sb.AppendLine(String.Format("{0} Successfully connected to write {1}", Now, IPs(1)))
                    SendSettings()

                    TimersEvent.Set()

                    ReadInterval = heartbeattimer.Value * 250
                    WriteInterval = heartbeattimer.Value * 900
                    'initial read of local voltage
                    ReadTickerDone.Reset()
                    ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf PeriodicReadEvent), Nothing, ReadInterval, True)

                    WriteTickerDone.Reset()
                    WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf PeriodicWriteEvent), Nothing, WriteInterval, False)

                Else
                    SetText(Label1, "Connection failed ...")
                    sb.AppendLine(String.Format("{0} Connection failed to {1}", Now, IPs(0)))
                    sb.AppendLine(String.Format("{0} Connection failed to {1}", Now, IPs(1)))
                    Pause()
                End If
            Else
                SetText(Label1, "Please check IP addresses")
                Pause()
            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Connection.SafeWaitHandle.Close()
            'Thread.CurrentThread.Join(100)
            Console.WriteLine("Current thread is # {0} --- Start", Thread.CurrentThread.ManagedThreadId)
            GC.Collect()
            'Me.WindowState = FormWindowState.Minimized
            'Me.WindowState = FormWindowState.Normal
        End Try
    End Sub

    Private Sub Pause()
        Dim Disconnect As New ManualResetEvent(False)

        Try
            WriteRegisterWait.Unregister(Nothing)
            ReadRegisterWait.Unregister(Nothing)

            If ProtocolInUse = "modbus" Then
                modbus.AsyncDisconnect()
            ElseIf ProtocolInUse = "dnp" Then
                dnp = New tcpdnp.AsyncTcp4RVB
                dnp.AsyncDisconnect(Disconnect)
                Disconnect.WaitOne(10)
                dnp = Nothing
            ElseIf ProtocolInUse = "iec" Then

            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            TimersEvent.Reset()
            Disconnect.SafeWaitHandle.Close()
            'ReadLocalVoltageTimer.Dispose()
            'WriteLocalVoltageTimer.Dispose()
            Heart_Beat_Timer = 0
            readresult = 0
            Forward_RVBVoltage2Write = 0.0
            Reverse_RVBVoltage2Write = 0.0

            btnStop.Enabled = False
            btnStart.Enabled = True

            Disenable()
            'if no errors show comm stop msg
            If ReceivedErrorMsg = "None" Then
                SetText(Label1, "Comm stopped ...")
                sb.AppendLine(String.Format("{0} Successfully disconnected", Now))
            Else
                sb.AppendLine(String.Format("{0} Disconnect failed {1}", Now, ReceivedErrorMsg))
            End If
            Console.WriteLine("Current thread is # {0} --- Pause", Thread.CurrentThread.ManagedThreadId)
            GC.Collect()
        End Try
    End Sub

    Private Function PingIPAddresses(ByVal IPs As String()) As Boolean
        PingIPAddresses = False
        Try
            Dim siteResponds = My.Computer.Network.Ping(IPs(0), 5000)
            PingIPAddresses = My.Computer.Network.Ping(IPs(1), 5000) And siteResponds
        Catch ex As Exception
            Console.WriteLine("Error: {0}", ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- PingIPAddress", Thread.CurrentThread.ManagedThreadId)
        End Try
        Return PingIPAddresses
    End Function

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        Start()
    End Sub

    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        Pause()
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            With My.Settings
                .location = Me.DesktopLocation
                If dnpbutton.Checked Then
                    .dnphost = txtWrite.Text
                    .dnpport = CUShort(txtPort.Text)
                    .protocol = "dnp"
                    .source = CUShort(NumericUpDownDNPSourceAddress.Value)
                    .destination = CUShort(NumericUpDownDNPDestinationAddress.Value)
                ElseIf modbusbox.Checked Then
                    .mdhost = txtWrite.Text
                    .mdport = CUShort(txtPort.Text)
                    .protocol = "modbus"
                    .mdLocalvoltage = CUShort(NumericUpDownModbusLocalVoltageRegister.Value)
                    .mdFRVBvoltage = CUShort(NumericUpDownModbusFwdRVBVoltageRegister.Value)
                    .mdRRVBvoltage = CUShort(NumericUpDownModbusRevRVBVoltageRegister.Value)
                ElseIf iec61850box.Checked Then
                    .iechost = txtWrite.Text
                    .iecport = CUShort(txtPort.Text)
                    .protocol = "iec"
                    .IECLocalVoltage = txtIECLocalVoltage.Text
                    .IECFwdRVBVoltage = txtIECFwdRVBVoltage.Text
                    .IECRevRVBVoltage = txtIECRevRVBVoltage.Text
                End If
                .heartbeat = CUShort(heartbeattimer.Value)
                .Fdeltavoltage = CDbl(FwdDeltaVoltage.Value)
                .Fmultiplier = CDbl(FwdRVBScaleFactor.Value)
                .Rdeltavoltage = CDbl(RevDeltaVoltage.Value)
                .Rmultiplier = CDbl(RevRVBScaleFactor.Value)
                .IPAddressToRead = txtWrite.Text
            End With

        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.CombinePath(My.Computer.FileSystem.CurrentDirectory, "Log.txt"), sb.ToString, False)
            Console.WriteLine("Current thread is # {0} --- FormClosing", Thread.CurrentThread.ManagedThreadId)
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'AddHandler My.Application.UnhandledException, AddressOf MyApplication_UnhandledException
        Main()
    End Sub

    'Public Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs)

    '    MsgBox(e.Exception.ToString) '+ vbCrLf + e.ToString, , "Unhandled exception")
    'End Sub

    Public Sub Main()
        Dim proc As Process
        Try
            processID = 0
            For Each proc In Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName)
                processID += 1
                If processID = 0 Then

                Else 'processID = 2 Then
                    For i = 1 To processID
                        proc.CloseMainWindow()
                    Next i
                End If
                proc = Nothing
            Next
            populatetheform()
            If My.Application.CommandLineArgs.Count > 1 Then
                checkcommandline()
            Else
                txtRead.Focus()
            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- Main", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub

    Private Sub checkHandler(sender As System.Object)
        Try
            Select Case sender.text
                Case "DNP3.0"
                    AddressBox.Text = "DNP3.0 Addresses"
                    lblwarning.Text = "Don't forget to download DNP default file"
                    txtPort.Text = dnpSetting.Port
                Case "Modbus"
                    AddressBox.Text = "Modbus registers"
                    txtPort.Text = modbusRegister.Port
                Case "IEC61850"
                    AddressBox.Text = "IEC61850 Datasets"
                    lblwarning.Text = "Don't forget to purchase IEC61850"
                    txtPort.Text = iecSetting.Port
            End Select

            lbldestination.Visible = dnpbutton.Checked
            lblsource.Visible = dnpbutton.Checked
            NumericUpDownDNPSourceAddress.Visible = dnpbutton.Checked
            NumericUpDownDNPDestinationAddress.Visible = dnpbutton.Checked
            lblwarning.Visible = dnpbutton.Checked Or iec61850box.Checked

            lbllocalvoltage.Visible = modbusbox.Checked
            Modbus_F_RVBVoltage_Label.Visible = modbusbox.Checked
            Modbus_R_RVBVoltage_Label.Visible = modbusbox.Checked And support
            NumericUpDownModbusLocalVoltageRegister.Visible = modbusbox.Checked
            NumericUpDownModbusFwdRVBVoltageRegister.Visible = modbusbox.Checked
            NumericUpDownModbusRevRVBVoltageRegister.Visible = modbusbox.Checked And support

            IEC_LocalVoltage_Label.Visible = iec61850box.Checked
            IEC_F_RVBVoltage_Label.Visible = iec61850box.Checked
            IEC_R_RVBVoltage_Label.Visible = iec61850box.Checked And support
            txtIECLocalVoltage.Visible = iec61850box.Checked
            txtIECFwdRVBVoltage.Visible = iec61850box.Checked
            txtIECRevRVBVoltage.Visible = iec61850box.Checked And support
            txtRead.Select()
            'rev 15 items
            R_RVBScaleFactor_Label.Visible = support
            RevRVBScaleFactor.Visible = support
            Reverse_Voltage_Label.Visible = support
            RevDeltaVoltage.Visible = support

        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- checkHandler", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub

    Private Sub dnpbutton_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles dnpbutton.CheckedChanged
        checkHandler(sender)
    End Sub

    Private Sub modbusbox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles modbusbox.CheckedChanged
        checkHandler(sender)
    End Sub

    Private Sub iec61850box_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles iec61850box.CheckedChanged
        checkHandler(sender)
    End Sub

    Private Sub checkcommandline()
        Try
            Dim i As Integer = 0
            Dim cmdlines As String() = New String(My.Application.CommandLineArgs.Count - 1) {}
            For Each item As String In My.Application.CommandLineArgs
                Select Case item.ToLower
                    Case "-vf"  'fwd voltage offset value
                        testSetting.FwdRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-vr"  'rev voltage offset value
                        testSetting.RevRVBVoltage = My.Application.CommandLineArgs.Item(i + 1)
                    Case "-s"
                        Select Case My.Application.CommandLineArgs.Item(i + 1)
                            Case "start"
                                Start()
                            Case "pause"
                                Pause()
                            Case "end"
                                Pause()
                                Me.Close()
                        End Select
                End Select
                i += 1
            Next
            If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then FwdDeltaVoltage.Value = 0.0 Else FwdDeltaVoltage.Value = testSetting.FwdRVBVoltage
            If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RevDeltaVoltage.Value = 0.0 Else RevDeltaVoltage.Value = testSetting.RevRVBVoltage
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- checkcommandline", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub

    Private Sub populatetheform()
        Try
            Dim xmlRead As New ReadXmlFile
            xmlRead.read()
            Me.Text = String.Format("RVB Simulator v-{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

            If CInt(SupportedRVBRevision) >= 15 Then
                support = True
                grpRevSettings.Visible = True
            Else
                support = False
                grpRevSettings.Visible = False
            End If

            ' With My.Settings
            NumericUpDownDNPSourceAddress.Value = dnpSetting.source              '.source
            NumericUpDownDNPDestinationAddress.Value = dnpSetting.destination   '.destination

            NumericUpDownModbusLocalVoltageRegister.Value = modbusRegister.LocalVoltage  '.mdLocalvoltage
            NumericUpDownModbusFwdRVBVoltageRegister.Value = modbusRegister.FRVBValue  '.mdFRVBvoltage
            NumericUpDownModbusRevRVBVoltageRegister.Value = modbusRegister.RRVBValue  '.mdRRVBvoltage

            heartbeattimer.Value = testSetting.HeartbeatTimer                  '.heartbeat
            If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then FwdDeltaVoltage.Value = 0.0 Else FwdDeltaVoltage.Value = testSetting.FwdRVBVoltage
            If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RevDeltaVoltage.Value = 0.0 Else RevDeltaVoltage.Value = testSetting.RevRVBVoltage

            FwdRVBScaleFactor.Value = testSetting.FwdRVBVoltageScale  '.Fmultiplier
            RevRVBScaleFactor.Value = testSetting.RevRVBVoltageScale   '.Rmultiplier
            txtIECLocalVoltage.Text = iecSetting.LocalVoltage       '.IECLocalVoltage
            txtIECFwdRVBVoltage.Text = iecSetting.FRVBValue         ' .IECFwdRVBVoltage
            txtIECRevRVBVoltage.Text = iecSetting.RRVBValue         ' .IECRevRVBVoltage

            With My.Settings
                '************************************************************
                '*  if visibility set to true by the user                   *
                '*  Verify the form is in visible area                      *
                '*  If NOT reset location to 0,0                            *
                '************************************************************
                If visibility Then
                    Dim x As Integer = My.Computer.Screen.WorkingArea.Left
                    Dim y As Integer = My.Computer.Screen.WorkingArea.Right
                    If .location.X < x Or .location.X > y Then
                        Me.DesktopLocation = New System.Drawing.Point(20, 20)
                    Else
                        Me.DesktopLocation = .location
                    End If
                Else
                    Me.DesktopLocation = New System.Drawing.Point(32000, 32000)
                    ' Me.DesktopLocation = New System.Drawing.Point(20, 20)
                End If
                '************************************************************
            End With
            Select Case testSetting.Protocol    '.protocol
                Case "dnp"
                    dnpbutton.Checked = True
                    'txtRead.Text = .dnphost
                    'txtWrite.Text = '.dnphost
                    txtPort.Text = dnpSetting.Port  '.dnpport
                    checkHandler(dnpbutton)
                Case "modbus"
                    modbusbox.Checked = True
                    'txtRead.Text = .mdhost
                    'txtWrite.Text = .mdhost
                    txtPort.Text = modbusRegister.Port  '.mdport
                    checkHandler(modbusbox)
                Case "iec"
                    iec61850box.Checked = True
                    'txtRead.Text = .iechost
                    'txtWrite.Text = .iechost
                    txtPort.Text = iecSetting.Port '.iecport
                    checkHandler(iec61850box)
            End Select
            txtRead.Text = testSetting.readIpAddress  '.IPAddressToRead
            txtWrite.Text = testSetting.writeIpAddress
            '
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- populatetheform", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub

    Public Sub Radio_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radUseDeltaVoltage.CheckedChanged, radUseFixedVoltage.CheckedChanged
        Try
            Select Case sender.name
                Case "radUseDeltaVoltage"
                    Forward_Voltage_Label.Text = DeltaMessage
                    Reverse_Voltage_Label.Text = DeltaMessage
                    FwdDeltaVoltage.Minimum = MinDeltaVoltage
                    FwdDeltaVoltage.Maximum = MaxDeltaVoltage
                    RevDeltaVoltage.Minimum = MinDeltaVoltage
                    RevDeltaVoltage.Maximum = MaxDeltaVoltage
                    FwdDeltaVoltage.Value = 0.0
                    RevDeltaVoltage.Value = 0.0
                Case "radUseFixedVoltage"
                    Forward_Voltage_Label.Text = DirectMessage
                    Reverse_Voltage_Label.Text = DirectMessage
                    FwdDeltaVoltage.Minimum = RVBMin.Value 'MinSpecValue
                    FwdDeltaVoltage.Maximum = RVBMax.Value 'MaxSpecValue
                    RevDeltaVoltage.Minimum = RVBMin.Value 'MinSpecValue
                    RevDeltaVoltage.Maximum = RVBMax.Value 'MaxSpecValue
                    If readresult / M2001D_Comm_Scale >= FwdDeltaVoltage.Minimum Then FwdDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else FwdDeltaVoltage.Value = FwdDeltaVoltage.Maximum
                    If readresult / M2001D_Comm_Scale >= RevDeltaVoltage.Minimum Then RevDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else RevDeltaVoltage.Value = RevDeltaVoltage.Minimum
            End Select
        Catch ex As Exception
            SetText(Label1, ex.ToString)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- Radio_checkedchanged", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub
End Class

Public Class ReadXmlFile
    Public Sub read()
        Try
            Dim myAttributeName As String = ""
            Dim iedName As String = ""
            Dim iedClass As String = ""
            Dim iecName As String = ""
            Dim iecFC As String = ""
            Dim iecSdi As String = ""
            Dim iecDai As String = ""
            Dim id As String = ""
            Dim xmlReader As Xml.XmlReader = Nothing
            Dim settings = New Xml.XmlReaderSettings
            settings.IgnoreComments = True
            settings.IgnoreWhitespace = True

            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, "Settings.xml")) Then
                xmlReader = Xml.XmlReader.Create(My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, "Settings.xml"), settings)

                While xmlReader.Read
                    If xmlReader.HasAttributes Then
                        ' Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)
                        If xmlReader.Depth = 2 Then myAttributeName = xmlReader.Name

test:                   If myAttributeName = "test" Then
                            While xmlReader.MoveToNextAttribute()
                                'Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                Select Case xmlReader.Name
                                    Case "protocol"
                                        RVBSim.testSetting.Protocol = xmlReader.Value
                                    Case "read"
                                        RVBSim.testSetting.readIpAddress = xmlReader.Value
                                    Case "write"
                                        RVBSim.testSetting.writeIpAddress = xmlReader.Value
                                    Case "heartbeattimer"
                                        RVBSim.testSetting.HeartbeatTimer = CUShort(xmlReader.Value)
                                    Case "userelative"
                                        Dim userelative As Boolean = CType(xmlReader.Value, Boolean)
                                        RVBSim.radUseDeltaVoltage.Checked = userelative
                                        RVBSim.radUseFixedVoltage.Checked = Not userelative
                                        RVBSim.Radio_CheckedChanged(RVBSim.radUseDeltaVoltage, Nothing)
                                    Case "fwdrvbvoltage"
                                        RVBSim.testSetting.FwdRVBVoltage = CDbl(xmlReader.Value)
                                    Case "revrvbvoltage"
                                        RVBSim.testSetting.RevRVBVoltage = CDbl(xmlReader.Value)
                                    Case "fwdscalefactor"
                                        RVBSim.testSetting.FwdRVBVoltageScale = CDbl(xmlReader.Value)
                                    Case "revscalefactor"
                                        RVBSim.testSetting.RevRVBVoltageScale = CDbl(xmlReader.Value)
                                    Case "rvbmax"
                                        RVBSim.testSetting.RVBMax = CDbl(xmlReader.Value)
                                    Case "rvbmin"
                                        RVBSim.testSetting.RVBMin = CDbl(xmlReader.Value)
                                    Case "visible"
                                        RVBSim.visibility = CType(xmlReader.Value, Boolean)
                                End Select
                            End While
                            ' Move the reader back to the element node.
                            xmlReader.MoveToElement()

dnp:                    ElseIf myAttributeName = "dnp" Then

                            'Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        RVBSim.dnpSetting.Port = xmlReader.Value
                                    Case "source"
                                        RVBSim.dnpSetting.source = CUShort(xmlReader.Value)
                                    Case "dest"
                                        RVBSim.dnpSetting.destination = CUShort(xmlReader.Value)
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "point"
                                        Select Case id
                                            Case "LocalVoltage"
                                                RVBSim.dnpSetting.LocalVoltage = CUShort(xmlReader.Value)
                                            Case "RVBEnable"
                                                RVBSim.dnpSetting.RVBEnable = CUShort(xmlReader.Value)
                                            Case "FRVBValue"
                                                RVBSim.dnpSetting.FRVBValue = CUShort(xmlReader.Value)
                                            Case "FRVBScale"
                                                RVBSim.dnpSetting.FRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBHeartbeat"
                                                RVBSim.dnpSetting.RVBHeartBeatTimer = CUShort(xmlReader.Value)
                                            Case "RRVBValue"
                                                RVBSim.dnpSetting.RRVBValue = CUShort(xmlReader.Value)
                                            Case "RRVBScale"
                                                RVBSim.dnpSetting.RRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBMax"
                                                RVBSim.dnpSetting.RVBMax = CUShort(xmlReader.Value)
                                            Case "RVBMin"
                                                RVBSim.dnpSetting.RVBMin = CUShort(xmlReader.Value)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()

modbus:                 ElseIf myAttributeName = "modbus" Then

                            'Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        RVBSim.modbusRegister.Port = xmlReader.Value
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "reg"
                                        Select Case id
                                            Case "LocalVoltage"
                                                RVBSim.modbusRegister.LocalVoltage = CUShort(xmlReader.Value)
                                            Case "RVBEnable"
                                                RVBSim.modbusRegister.RVBEnable = CUShort(xmlReader.Value)
                                            Case "FRVBValue"
                                                RVBSim.modbusRegister.FRVBValue = CUShort(xmlReader.Value)
                                            Case "FRVBScale"
                                                RVBSim.modbusRegister.FRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBHeartbeat"
                                                RVBSim.modbusRegister.RVBHeartBeatTimer = CUShort(xmlReader.Value)
                                            Case "RVBActive"
                                                RVBSim.modbusRegister.RVBActive = CUShort(xmlReader.Value)
                                            Case "RRVBValue"
                                                RVBSim.modbusRegister.RRVBValue = CUShort(xmlReader.Value)
                                            Case "RRVBScale"
                                                RVBSim.modbusRegister.RRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBMax"
                                                RVBSim.modbusRegister.RVBMax = CUShort(xmlReader.Value)
                                            Case "RVBMin"
                                                RVBSim.modbusRegister.RVBMin = CUShort(xmlReader.Value)
                                            Case "Factory"
                                                RVBSim.modbusRegister.Factory = CUShort(xmlReader.Value)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()

iec61850:               ElseIf myAttributeName = "iec" Then

                            ' Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                ' Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        RVBSim.iecSetting.Port = xmlReader.Value
                                    Case "iedname"
                                        iedName = xmlReader.Value
                                        RVBSim.iecSetting.iedName = iedName
                                    Case "class"
                                        iedClass = xmlReader.Value
                                        RVBSim.iecSetting.iedClass = iedClass
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "name"
                                        iecName = xmlReader.Value
                                    Case "fc"
                                        iecFC = xmlReader.Value
                                    Case "sdi"
                                        iecSdi = xmlReader.Value
                                        Select Case id
                                            Case "RVBEnable"
                                                'RVBSim.iecSetting.RVBEnable = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                RVBSim.iecSetting.RVBEnable = String.Format("{0}${1}${2}${3}", iedClass, iecFC, iecName, iecSdi) 'see iec library
                                            Case "RVBHeartbeat"
                                                'RVBSim.iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                RVBSim.iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}", iedClass, iecFC, iecName, iecSdi) 'see iec library
                                        End Select
                                    Case "dai"
                                        iecDai = xmlReader.Value
                                        Select Case id
                                            Case "LocalVoltage"
                                                'RVBSim.iecSetting.LocalVoltage = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.LocalVoltage = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "FRVBValue"
                                                'RVBSim.iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "FRVBScale"
                                                ' RVBSim.iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RRVBValue"
                                                'RVBSim.iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RRVBScale"
                                                'RVBSim.iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RVBMax"
                                                ' RVBSim.iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RVBMin"
                                                ' RVBSim.iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                RVBSim.iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()
                        End If
                    End If
                End While
            End If
        Catch ex As Exception
            RVBSim.SetText(RVBSim.Label1, ex.ToString)
            RVBSim.sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            Console.WriteLine("Current thread is # {0} --- XML_read", Thread.CurrentThread.ManagedThreadId)
        End Try
    End Sub
End Class

