Imports System.Net
Imports System.Threading
Imports System.Text
Imports System.IO

'custom libraries
Imports tcpmodbus.AsyncModbus
Imports tcpdnp.AsyncDNP3_0
Imports iec.AsyncIEC61850

Public Class RVBSim

    Friend Const ConsoleWriteEnable As Boolean = False          ' True       '
    Private Const SupportedRVBRevision As String = "15"         'Supported RVB feature document revision
    Private Const OperatingVoltage As Integer = 900
    Private Const M2001D_Comm_Scale As Integer = 10
    Private Const MaxDeltaVoltage As Integer = 100
    Private Const MinDeltaVoltage As Integer = -100
    Private Const DeltaMessage As String = "Local Voltage + "
    Private Const DirectMessage As String = "RVB Voltage is ="
    Private Const DNP_BufferSize As Integer = 29
    Private Const Modbus_BufferSize As Integer = 12
    Private Const IEC_BufferSize As Integer = 200

    Protected Friend sb As New StringBuilder
    Protected Friend IPs As String() = New String(1) {}

    Private dnp As tcpdnp.AsyncDNP3_0
    Private modbus As tcpmodbus.AsyncModbus
    Private iec61850 As iec.AsyncIEC61850

    Private processID As Integer = 0
    Private support As Boolean      'True rev 15 or greater False rev 8
    Private errorCounter As Integer = 0

    Private WriteTickerDone As New ManualResetEvent(False)
    Private ReadTickerDone As New ManualResetEvent(False)
    Private TimersEvent As New ManualResetEvent(False)

    Private WriteRegisterWait As RegisteredWaitHandle
    Private ReadRegisterWait As RegisteredWaitHandle

    Protected Friend visibility As Boolean = True
    Protected Friend testSetting As TestSettings
    Protected Friend modbusRegister As ModbusSettings
    Protected Friend dnpSetting As DnpSettings
    Protected Friend iecSetting As IECSettings

    Protected Friend Structure TestSettings
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

    Protected Friend Structure ModbusSettings
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

    Protected Friend Structure DnpSettings
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

    Protected Friend Structure IECSettings
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

    Private _WriteInterval As Integer
    Private Property WriteInterval() As Integer
        Get
            Return _WriteInterval
        End Get
        Set(ByVal value As Integer)
            _WriteInterval = value
        End Set
    End Property

    Private _ReadInterval As Integer
    Private Property ReadInterval() As Integer
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
    Private Property Heart_Beat_Timer() As Double
        Get
            Return _Heart_Beat_Timer
        End Get
        Set(ByVal value As Double)
            _Heart_Beat_Timer = value
        End Set
    End Property

    Private _ActualLocalVoltage As Double = 0.0
    Private Property ActualLocalVoltage() As Double
        Get
            Return _ActualLocalVoltage
        End Get
        Set(ByVal value As Double)
            _ActualLocalVoltage = value
        End Set
    End Property

    Private _Forward_RVBVoltage2Write As Double = 0.0
    Private Property Forward_RVBVoltage2Write() As Double
        Get
            Return _Forward_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Forward_RVBVoltage2Write = value
        End Set
    End Property

    Private _Reverse_RVBVoltage2Write As Double = 0.0
    Private Property Reverse_RVBVoltage2Write() As Double
        Get
            Return _Reverse_RVBVoltage2Write
        End Get
        Set(ByVal value As Double)
            _Reverse_RVBVoltage2Write = value
        End Set
    End Property

    Private _readresult As UShort = 0
    Private Property readresult() As UShort
        Get
            Return _readresult
        End Get
        Set(ByVal value As UShort)
            _readresult = value
        End Set
    End Property

    Private _errorMsg As String = ""
    Private Property ReceivedErrorMsg() As String
        Get
            Return _errorMsg
        End Get
        Set(ByVal value As String)
            _errorMsg = value
        End Set
    End Property

    Private Delegate Sub SetTextDelegate(ByVal [label] As Label, ByVal [text] As String)
    Private Delegate Sub SetEnableDelegate(ByVal [control] As Control, ByVal [enable] As Boolean)

    Protected Friend Sub SetText(ByVal [label] As Label, ByVal [text] As String)

        Try
            If [label].InvokeRequired Then
                Dim del As New SetTextDelegate(AddressOf SetText)
                [label].Invoke(del, New Object() {[label], [text]})
            Else
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} SetText --- Text is {1}", Thread.CurrentThread.GetHashCode, [text])
                [label].Text = [text]
            End If
        Catch ex As Exception
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

    Protected Friend Sub SetEnable(ByVal [control] As Control, ByVal [enable] As Boolean)

        Try
            If [control].InvokeRequired Then
                Dim del As New SetEnableDelegate(AddressOf SetEnable)
                [control].Invoke(del, New Object() {[control], [enable]})
            Else
                [control].Enabled = [enable]
            End If
        Catch ex As Exception
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

    Private Sub GenerateRVBVoltage2Transfer()
        If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} GenerateRVBVoltage2Transfer", Thread.CurrentThread.GetHashCode)

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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

    Private Sub CheckErrors()
        If Not Interlocked.Read(errorCounter) >= 10 Then
            ResetTimers()
        End If
    End Sub

    Private Sub ResetTimers()

        Try
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} ResetTimers{1} ---------------------------- errorCounter: {2}", Thread.CurrentThread.GetHashCode, vbCrLf, Interlocked.Read(errorCounter))

            If Not Interlocked.Read(errorCounter) >= 10 Then

                ReadRegisterWait.Unregister(Nothing)
                WriteRegisterWait.Unregister(Nothing)

                ReadRegisterWait = ThreadPool.RegisterWaitForSingleObject(ReadTickerDone, New WaitOrTimerCallback(AddressOf PeriodicReadEvent), Nothing, ReadInterval, False)

                WriteRegisterWait = ThreadPool.RegisterWaitForSingleObject(WriteTickerDone, New WaitOrTimerCallback(AddressOf PeriodicWriteEvent), Nothing, WriteInterval, False)

            Else

                Pause()
                Throw New CustomExceptions("Too many errors")

            End If

        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

    Private Sub PeriodicWriteEvent(ByVal state As Object, ByVal timeOut As Boolean) '(ByVal mip As IPAddress, ByVal m_port As UShort)

        If timeOut Then
            If ConsoleWriteEnable Then
                Console.WriteLine("Current thread is # {0} PeriodicWriteEvent", Thread.CurrentThread.GetHashCode)
                Heart_Beat_Timer = 0
            End If

            ReadRegisterWait.Unregister(Nothing)
            GenerateRVBVoltage2Transfer()

            Try
                Dim WriteEvent As New ManualResetEvent(False)
                If ProtocolInUse() = "dnp" Then
                    'transmit Forward RVB Voltage
                    dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    dnp.Send(WriteEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.DirectOp, Objects.AnalogOutput, Variations.AnaOutBlockShort, QualifierField.AnaOutBlock16bitIndex, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then
                    'transmit Forward RVB Voltage
                    modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, NumericUpDownModbusFwdRVBVoltageRegister.Value, CUShort(Forward_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, NumericUpDownModbusRevRVBVoltageRegister.Value, CUShort(Reverse_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

                ElseIf ProtocolInUse() = "iec" Then
                    'transmit Forward RVB Voltage
                    iec61850.Send(WriteEvent, txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                    'transmit Reverse RVB Voltage
                    WriteEvent.Reset()
                    iec61850.Send(WriteEvent, txtIECRevRVBVoltage.Text, "Write", CUShort(Reverse_RVBVoltage2Write))
                    WriteEvent.WaitOne()
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                End If

                SetText(lblFwdRVBValue, String.Format("Fwd RVB: {0}", FormatNumber((Forward_RVBVoltage2Write / M2001D_Comm_Scale), 1)))
                SetText(lblRevRVBValue, String.Format("Rev RVB: {0}", FormatNumber((Reverse_RVBVoltage2Write / M2001D_Comm_Scale), 1)))

                WriteEvent.SafeWaitHandle.Close()

                If ConsoleWriteEnable Then
                    Console.WriteLine(" ---------------------- Writing Fwd voltage: {0}", Forward_RVBVoltage2Write)
                    Console.WriteLine(" ---------------------- Writing Rev voltage: {0}", Reverse_RVBVoltage2Write)
                End If

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))

                WriteRegisterWait.Unregister(Nothing)
                ResetTimers()

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                CheckErrors()
                SetText(lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            End Try
        Else
            WriteRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Private Sub PeriodicReadEvent(ByVal state As Object, ByVal timeOut As Boolean)

        If timeOut Then
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} PeriodicReadEvent", Thread.CurrentThread.GetHashCode)

            Try
                Dim ReadEvent As New ManualResetEvent(False)

                If ProtocolInUse() = "dnp" Then
                    dnp.Send(ReadEvent, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, Mode.Read, Objects.AnalogInput, Variations.AnaInput16bitVar4, QualifierField.AnaInput16bitStartStop, dnpSetting.LocalVoltage)
                    ReadEvent.WaitOne()
                    readresult = tcpdnp.AsyncDNP3_0.result
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse() = "modbus" Then
                    modbus.Send(ReadEvent, tcpmodbus.AsyncModbus.Functions.Read, NumericUpDownModbusLocalVoltageRegister.Value, 1)
                    ReadEvent.WaitOne()
                    readresult = tcpmodbus.AsyncModbus.result
                    ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

                ElseIf ProtocolInUse() = "iec" Then
                    If ConsoleWriteEnable Then Console.WriteLine("{0}------------------- Reading Local Voltage -------------------", vbCrLf)
                    iec61850.Send(ReadEvent, txtIECLocalVoltage.Text, "Read")
                    ReadEvent.WaitOne()
                    readresult = iec.AsyncIEC61850.result
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived
                    If ConsoleWriteEnable Then Console.WriteLine("{0}------------------- Reading Local Voltage Done -------------------", vbCrLf)
                End If

                SetText(lblLocalVoltageValue, String.Format("Remote Voltage: {0}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1)))
                SetText(lblMsgCenter, String.Format("Error: {0}", ReceivedErrorMsg))

                If ConsoleWriteEnable Then
                    Heart_Beat_Timer += ReadInterval
                    Console.WriteLine("Reading local voltage: {0} - {1}", readresult, Heart_Beat_Timer)
                    Console.WriteLine("Current thread is # {0} --- PeriodicReadEvent", Thread.CurrentThread.GetHashCode)
                End If

                If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))

                ReadEvent.SafeWaitHandle.Close()

            Catch ex As Exception
                Interlocked.Increment(errorCounter)
                SetText(lblMsgCenter, ex.Message)
                sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            End Try
        Else
            ReadRegisterWait.Unregister(Nothing)
        End If
    End Sub

    Private Sub Disenable()

        Try
            With btnStart
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} Disenable", Thread.CurrentThread.GetHashCode)

                'dnp settings dis/enable
                SetEnable(NumericUpDownDNPSourceAddress, .Enabled)
                SetEnable(NumericUpDownDNPDestinationAddress, .Enabled)

                'modbus settings dis/enable
                SetEnable(NumericUpDownModbusLocalVoltageRegister, .Enabled)
                SetEnable(NumericUpDownModbusFwdRVBVoltageRegister, .Enabled)
                SetEnable(NumericUpDownModbusRevRVBVoltageRegister, .Enabled)

                'iec61850 settings dis/enable
                SetEnable(txtIECLocalVoltage, .Enabled)
                SetEnable(txtIECFwdRVBVoltage, .Enabled)
                SetEnable(txtIECRevRVBVoltage, .Enabled)

                'communication settings dis/enable
                SetEnable(txtWrite, .Enabled)
                SetEnable(txtRead, .Enabled)
                SetEnable(txtPort, .Enabled)

                'protocol options dis/enable
                SetEnable(dnpbutton, .Enabled)
                SetEnable(modbusbox, .Enabled)
                SetEnable(iec61850box, .Enabled)

                'general rvb settings dis/enable
                SetEnable(heartbeattimer, .Enabled)
                SetEnable(radUseDeltaVoltage, .Enabled)
                SetEnable(radUseFixedVoltage, .Enabled)
                SetEnable(FwdRVBScaleFactor, .Enabled)
                SetEnable(RVBMax, .Enabled)
                SetEnable(RVBMin, .Enabled)
                SetEnable(RevRVBScaleFactor, .Enabled)

                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Disenable", Thread.CurrentThread.GetHashCode)

            End With

        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
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
        If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- UpdateProtocol", Thread.CurrentThread.GetHashCode)
    End Sub

    Private Sub SendSettings()

        Try
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- SendSettings --- START", Thread.CurrentThread.GetHashCode)
            Dim WriteEvent As New ManualResetEvent(False)
            SetText(lblMsgCenter, "Sending settings to the units ...")

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

                ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

            ElseIf ProtocolInUse = "modbus" Then
                'Enable RVB using modbus
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.RVBEnable, 1)
                WriteEvent.WaitOne()

                'set RVB heartbeat timer
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.RVBHeartBeatTimer, heartbeattimer.Value)
                WriteEvent.WaitOne()

                'set RVB Max
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.RVBMax, RVBMax.Value * M2001D_Comm_Scale)
                WriteEvent.WaitOne()

                'set RVB Min
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.RVBMin, RVBMin.Value * M2001D_Comm_Scale)
                WriteEvent.WaitOne()

                'set Fwd RVB Scale Factor
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.FRVBScale, FwdRVBScaleFactor.Value * M2001D_Comm_Scale)
                WriteEvent.WaitOne()

                'set Rev RVB Scale Factor 
                WriteEvent.Reset()
                modbus.Send(WriteEvent, tcpmodbus.AsyncModbus.Functions.Write, modbusRegister.RRVBScale, RevRVBScaleFactor.Value * M2001D_Comm_Scale)
                WriteEvent.WaitOne()

                ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

            ElseIf ProtocolInUse = "iec" Then
                'enable RVB using IEC61850
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.RVBEnable, "Write", 1, DataType.bool)
                WriteEvent.WaitOne()

                'set RVB heartbeat timer
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.RVBHeartBeatTimer, "Write", heartbeattimer.Value, DataType.int)
                WriteEvent.WaitOne()

                'set RVB Max
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.RVBMax, "Write", RVBMax.Value * M2001D_Comm_Scale, DataType.int)
                WriteEvent.WaitOne()

                'set RVB Min
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.RVBMin, "Write", RVBMin.Value * M2001D_Comm_Scale, DataType.int)
                WriteEvent.WaitOne()

                'set Fwd RVB Scale Factor
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.FRVBScale, "Write", FwdRVBScaleFactor.Value * M2001D_Comm_Scale, DataType.int)
                WriteEvent.WaitOne()

                'set Rev RVB Scale Factor
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, iecSetting.RRVBScale, "Write", RevRVBScaleFactor.Value * M2001D_Comm_Scale, DataType.int)
                WriteEvent.WaitOne()

            Else
                MsgBox("Unsupported communication protocol")
                Pause()
            End If

            SetText(lblMsgCenter, "Sending completed ... reading Local Voltage")
            If Not ReceivedErrorMsg = "None" Then sb.AppendLine(String.Format("{0} Received {1} error", Now, ReceivedErrorMsg))
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- SendSettings --- END", Thread.CurrentThread.GetHashCode)

        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

    Private Sub Start()

        Try
            Dim Connection As New ManualResetEvent(False)
            TimersEvent = New ManualResetEvent(False)

            errorCounter = 0
            ReceivedErrorMsg = "None"

            'Update protocol per user selection
            UpdateProtocol()

            'set texts and buttons
            SetText(lblMsgCenter, "Establishing communication ...")
            SetEnable(btnStop, True)
            SetEnable(btnStart, False)
            Disenable()

            IPs = {txtRead.Text, txtWrite.Text}
            SetText(lblMsgCenter, "Connecting to the units ...")

            Dim success As Boolean = False

            If PingIPAddresses(IPs) Then

                If ProtocolInUse = "modbus" Then
                    tcpmodbus.AsyncModbus.ConsoleWriteEnable = ConsoleWriteEnable
                    modbus = New tcpmodbus.AsyncModbus(IPs.Length, Modbus_BufferSize)
                    modbus.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                    ReceivedErrorMsg = tcpmodbus.AsyncModbus.ErrorReceived

                ElseIf ProtocolInUse = "dnp" Then
                    tcpdnp.AsyncDNP3_0.ConsoleWriteEnable = ConsoleWriteEnable
                    dnp = New tcpdnp.AsyncDNP3_0(IPs.Length, DNP_BufferSize)
                    dnp.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                    ReceivedErrorMsg = tcpdnp.AsyncDNP3_0.ErrorReceived

                ElseIf ProtocolInUse = "iec" Then
                    iec.AsyncIEC61850.ConsoleWriteEnable = ConsoleWriteEnable
                    iec61850 = New iec.AsyncIEC61850(iecSetting.ReadIEDName, iecSetting.WriteIEDName, IPs.Length, IEC_BufferSize)
                    iec61850.AsyncConnectTo(IPs, CUShort(txtPort.Text), Connection)
                    ReceivedErrorMsg = iec.AsyncIEC61850.ErrorReceived

                End If

                success = Connection.WaitOne(1000)
                Thread.CurrentThread.Join(100)

                If success Then
                    SetText(lblMsgCenter, "Connection successful ...")
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
                    Throw New Sockets.SocketException(CInt(Sockets.SocketError.HostUnreachable))
                End If

                Connection.SafeWaitHandle.Close()
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Start", Thread.CurrentThread.GetHashCode)

            Else
                Throw New CustomExceptions("Cannot connect to the unit(s)")
            End If
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
            SetEnable(btnStart, True)
            SetEnable(btnStop, False)
            Disenable()
        End Try
    End Sub

    Private Sub Pause()
        Dim Disconnect As New ManualResetEvent(False)

        Try
            WriteRegisterWait.Unregister(Nothing)
            ReadRegisterWait.Unregister(Nothing)

            If ProtocolInUse = "modbus" Then
                modbus.Disconnect(Disconnect)
                Disconnect.WaitOne()
                modbus.Dispose()
            ElseIf ProtocolInUse = "dnp" Then
                dnp.Disconnect(Disconnect)
                Disconnect.WaitOne()
                dnp.Dispose()
            ElseIf ProtocolInUse = "iec" Then
                iec61850.Disconnect(Disconnect)
                Disconnect.WaitOne()
                iec61850.Dispose()
            End If

            TimersEvent.Dispose()
            Disconnect.Dispose()

            Heart_Beat_Timer = 0
            readresult = 0
            Forward_RVBVoltage2Write = 0.0
            Reverse_RVBVoltage2Write = 0.0
            SetEnable(btnStop, False)
            SetEnable(btnStart, True)

            Disenable()

            'if no errors show comm stop msg
            If ReceivedErrorMsg = "None" Then
                SetText(lblMsgCenter, "Comm stopped ...")
                sb.AppendLine(String.Format("{0} Successfully disconnected", Now))
            Else
                sb.AppendLine(String.Format("{0} Disconnect failed {1}", Now, ReceivedErrorMsg))
            End If
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Pause", Thread.CurrentThread.GetHashCode)
            SetText(lblLocalVoltageValue, String.Format(""))
            SetText(lblFwdRVBValue, String.Format(""))
            SetText(lblRevRVBValue, String.Format(""))

        Catch ex As Exception

        End Try
    End Sub

    Private Function PingIPAddresses(ByVal IPs As String()) As Boolean
        PingIPAddresses = False
        SetText(lblMsgCenter, ReceivedErrorMsg)

        Dim siteResponds As Boolean = My.Computer.Network.Ping(IPs(0), 5000)
        PingIPAddresses = My.Computer.Network.Ping(IPs(1), 5000) And siteResponds

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
            'due to IEC61850 protocol we need to close communication certain way
            If Not btnStart.Enabled Then btnStop_Click(sender, e)
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            My.Computer.FileSystem.WriteAllText(My.Computer.FileSystem.CombinePath(My.Computer.FileSystem.CurrentDirectory, "Log.txt"), sb.ToString, False)
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- FormClosing", Thread.CurrentThread.GetHashCode)
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Main()
    End Sub

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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Main", Thread.CurrentThread.GetHashCode)
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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- checkHandler", Thread.CurrentThread.GetHashCode)
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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- checkcommandline", Thread.CurrentThread.GetHashCode)
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
                    txtPort.Text = dnpSetting.Port  '.dnpport
                    checkHandler(dnpbutton)
                Case "modbus"
                    modbusbox.Checked = True
                    txtPort.Text = modbusRegister.Port  '.mdport
                    checkHandler(modbusbox)
                Case "iec"
                    iec61850box.Checked = True
                    txtPort.Text = iecSetting.Port '.iecport
                    checkHandler(iec61850box)
            End Select
            txtRead.Text = testSetting.readIpAddress  '.IPAddressToRead
            txtWrite.Text = testSetting.writeIpAddress
            '
        Catch ex As Exception
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- populatetheform", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub

    Protected Friend Sub Radio_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radUseDeltaVoltage.CheckedChanged, radUseFixedVoltage.CheckedChanged
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
            SetText(lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Radio_checkedchanged", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub
End Class

Friend Class ReadXmlFile
    Protected Friend Sub read()
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
                        ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)
                        If xmlReader.Depth = 2 Then myAttributeName = xmlReader.Name

test:                   If myAttributeName = "test" Then
                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
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

                            'If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
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

                            'If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
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

                            ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                ' If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        RVBSim.iecSetting.Port = xmlReader.Value
                                    Case "readiedname"
                                        iedName = xmlReader.Value
                                        RVBSim.iecSetting.ReadIEDName = iedName
                                    Case "writeiedname"
                                        iedName = xmlReader.Value
                                        RVBSim.iecSetting.WriteIEDName = iedName
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
            RVBSim.SetText(RVBSim.lblMsgCenter, ex.Message)
            RVBSim.sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If RVBSim.ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- XML_read", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub
End Class

Friend Class CustomExceptions
    Inherits Exception

    Protected Friend Sub New()

    End Sub

    Protected Friend Sub New(message As String)
        MyBase.New(message)
    End Sub

    Protected Friend Sub New(message As String, inner As Exception)
        MyBase.New(message, inner)
    End Sub
End Class