Imports iec.iec
Imports tcpmodbus.modbus
Imports tcpdnp.dnp
Imports System.Net
Imports System.Timers

Public Class RVBSim
    Const SupportedRVBRevision As String = "15"         'Supported RVB feature document revision
    Const OperatingVoltage As Integer = 900
    Const M2001D_Comm_Scale As Integer = 10
    Const MaxDeltaVoltage As Integer = 100
    Const MinDeltaVoltage As Integer = -100
    Const DeltaMessage As String = "Local Voltage + "
    Const DirectMessage As String = "RVB Voltage is ="
    Const RVBVisibilityDelay As Integer = 25            '* 100msec RVB voltage stays on the display
    'Const delayforOmicronPowerUpMax As Integer = 6
    Const TDelay As Integer = 500                       'Heartbeat refresh time is off so it can refresh
    Const Interval As Integer = 50                      'timer interval

    Dim ReadLocalVoltageTimer As System.Timers.Timer
    Dim m_ip, mip As IPAddress
    Dim m_port As UShort
    Dim delayforOmicronPowerUp As Integer = 0
    Dim Heart_Beat_Timer As Double = 0.0
    Dim Heart_Beat_Set As Double = 0.0
    Dim Forward_RVBVoltage2Write As Double = 0.0
    Dim dnp As New tcpdnp.dnp
    Dim modbus As New tcpmodbus.modbus
    Dim iec As New iec.iec
    Dim readresult As UShort = 0
    Dim ActualLocalVoltage As Double = 0.0
    Dim processID As Integer = 0
    Dim RVBVisibilityTime As Double = 0.0
    Dim support As Boolean      'True rev 15 or greater False rev 8
    Dim Reverse_RVBVoltage2Write As Double = 0.0
    Dim powerDirection As UInt16 = 0
    Dim OmicronBootupCompleted As Boolean = False
    
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

    Private Delegate Sub SetTextDelegate(ByVal myControl As Control, ByVal itsText As String)

    Public Sub SetText(ByVal myControl As Control, ByVal itsText As String)
        Try
            If myControl.InvokeRequired Then
                Dim del As New SetTextDelegate(AddressOf SetText)
                myControl.Invoke(del, New Object() {myControl, itsText})
            Else
                myControl.Text = itsText
            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

    Private Sub GenerateRVBVoltage2Transfer()
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
        End Try
    End Sub

    Private Sub ticker(ByVal mip As IPAddress, ByVal m_port As UShort)
        Try
            ' ReadLocalVoltageTimer.Stop()
            GenerateRVBVoltage2Transfer()

            ' If Not ActualLocalVoltage <= OperatingVoltage Then
dnp:        If dnpbutton.Checked Then
                'transmit Forward RVB Voltage
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _dnpobj.AnalogOutput, _dnpvar.var2, _
                                    _dnpindex.write, 1, dnpSetting.FRVBValue, CUShort(Forward_RVBVoltage2Write), 0)
                'transmit Reverse RVB Voltage
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _dnpobj.AnalogOutput, _dnpvar.var2, _
                                    _dnpindex.write, 1, dnpSetting.RRVBValue, CUShort(Reverse_RVBVoltage2Write), 0)

modbus:     ElseIf modbusbox.Checked Then
                'transmit Forward RVB Voltage
                modbus.CommunicateSingleUnit(mip, m_port, NumericUpDownModbusFwdRVBVoltageRegister.Value, _modbusfunc.write, CUShort(Forward_RVBVoltage2Write))
                'transmit Reverse RVB Voltage
                modbus.CommunicateSingleUnit(mip, m_port, NumericUpDownModbusRevRVBVoltageRegister.Value, _modbusfunc.write, CUShort(Reverse_RVBVoltage2Write))

iec61850:   ElseIf iec61850box.Checked Then
                'transmit Forward RVB Voltage
                iec.iec(mip, m_port, txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write), iecSetting.iedName)
                'transmit Reverse RVB Voltage
                iec.iec(mip, m_port, txtIECRevRVBVoltage.Text, "Write", CUShort(Reverse_RVBVoltage2Write), iecSetting.iedName)
            End If
            RVBVisibilityTime = RVBVisibilityDelay
            Console.WriteLine("Writing Fwd voltage: {0}", Forward_RVBVoltage2Write) ' / M2001D_Comm_Scale)
            Console.WriteLine("Writing Rev voltage: {0}", Reverse_RVBVoltage2Write) ' / M2001D_Comm_Scale)
            'End If
            ' ReadLocalVoltageTimer.Start()
        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

    Private Sub ReadLocalVoltage()
        'Reading Local voltage and transmitting back
        'done by apporiate protocol selected by the user
        Try
            'Local Voltage will read every 100ms but writing back will be done in 
            ' the user speficied time frame
            If dnpbutton.Checked Then
                readresult = (dnp.tcpdnp(m_ip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.read, _dnpobj.AnalogInput, _dnpvar.varall, _dnpindex.read, 1, dnpSetting.LocalVoltage))

            ElseIf modbusbox.Checked Then
                readresult = (modbus.CommunicateSingleUnit(m_ip, m_port, NumericUpDownModbusLocalVoltageRegister.Value, _modbusfunc.read, 1))
            ElseIf iec61850box.Checked Then
                readresult = (iec.iec(m_ip, m_port, txtIECLocalVoltage.Text, "Connect"))
            End If

            'If readresult / M2001D_Comm_Scale >= OperatingVoltage Then
            'If Not OmicronBootupCompleted Then
            '    delayforOmicronPowerUp += 1
            '    Heart_Beat_Timer = 0
            'End If
            Heart_Beat_Timer += Interval   ' 100

            If (Heart_Beat_Timer >= Heart_Beat_Set) Then   ' OrElse (delayforOmicronPowerUp = delayforOmicronPowerUpMax) Then
                'write to the unit using customer selected protocol
                Console.WriteLine("Heart_Beat_Timer: {0} msec", Heart_Beat_Timer)   'Omicron boot up completed: {1}", OmicronBootupCompleted)
                ticker(mip, m_port)
                Heart_Beat_Timer = 0
                'delayforOmicronPowerUp = delayforOmicronPowerUpMax + 1 '7
                'OmicronBootupCompleted = True
            End If

            If readresult < OperatingVoltage Then
                Console.WriteLine("Operating Voltage is {0}", OperatingVoltage)
                ticker(mip, m_port)
                Heart_Beat_Timer = 0
            End If
            SetText(Label1, String.Format("     Reads: {0}" + vbCrLf + "Fwd RVB: {1}" + vbCrLf + "Rev RVB: {2}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1), FormatNumber(Forward_RVBVoltage2Write, 1), FormatNumber(Reverse_RVBVoltage2Write, 1)))

            'Else
            'Heart_Beat_Timer = 0
            'OmicronBootupCompleted = False
            'delayforOmicronPowerUp = 0
            'SetText(Label1, String.Format("     Reads: {0}" + vbCrLf + "Fwd RVB: {1}" + vbCrLf + "Rev RVB: {1}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1), "Out of operating range"))
            'End If

            Console.WriteLine("Actual voltage is: {0}", readresult) '/ M2001D_Comm_Scale)

        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
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
                txtPort.Enabled = .Enabled
                If modbusbox.Checked Then txtRead.Enabled = .Enabled Else txtRead.Enabled = False
                'protocol options dis/enable
                dnpbutton.Enabled = .Enabled
                modbusbox.Enabled = .Enabled
                iec61850box.Enabled = .Enabled
                'general rvb settings dis/enable
                heartbeattimer.Enabled = .Enabled
                radUseDeltaVoltage.Enabled = .Enabled
                radUseFixedVoltage.Enabled = .Enabled
                'forward rvb settings dis/enable
                FwdRVBScaleFactor.Enabled = .Enabled
                RVBMax.Enabled = .Enabled
                RVBMin.Enabled = .Enabled
                'reverse rvb settings dis/enable
                RevRVBScaleFactor.Enabled = .Enabled
            End With
        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

    Private Sub SendSettings()
        Try
            If testSetting.Protocol = "dnp" Then
                'Enable RVB using dnp
dnp:            dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.RVBEnable, 1, 0)
                'set RVB heartbeat timer
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.RVBHeartBeatTimer, heartbeattimer.Value, 0)
                'set RVB Max
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.RVBMax, RVBMax.Value * M2001D_Comm_Scale, 0)
                'set RVB Min
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.RVBMin, RVBMin.Value * M2001D_Comm_Scale, 0)
                'set Fwd RVB Scale Factor
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.FRVBScale, FwdRVBScaleFactor.Value * M2001D_Comm_Scale, 0)
                'set Rev RVB Scale Factor 
                dnp.tcpdnp(mip, m_port, NumericUpDownDNPDestinationAddress.Value, NumericUpDownDNPSourceAddress.Value, _dnpfunc.directnoack, _
                                        _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, dnpSetting.RRVBScale, RevRVBScaleFactor.Value * M2001D_Comm_Scale, 0)

            ElseIf testSetting.Protocol = "modbus" Then
                'Enable RVB using modbus
modbus:         modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.RVBEnable, _modbusfunc.write, 1)
                'set RVB heartbeat timer
                modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.RVBHeartBeatTimer, _modbusfunc.write, heartbeattimer.Value)
                'set RVB Max
                modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.RVBMax, _modbusfunc.write, RVBMax.Value * M2001D_Comm_Scale)
                'set RVB Min
                modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.RVBMin, _modbusfunc.write, RVBMin.Value * M2001D_Comm_Scale)
                'set Fwd RVB Scale Factor
                modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.FRVBScale, _modbusfunc.write, FwdRVBScaleFactor.Value * M2001D_Comm_Scale)
                'set Rev RVB Scale Factor 
                modbus.CommunicateSingleUnit(m_ip, m_port, modbusRegister.RRVBScale, _modbusfunc.write, RevRVBScaleFactor.Value * M2001D_Comm_Scale)

            ElseIf testSetting.Protocol = "iec" Then
                'enable RVB using IEC61850
iec61850:       iec.iec(mip, m_port, iecSetting.RVBEnable, "Write", 1, iecSetting.iedName, DataType.bool)
                'set RVB heartbeat timer
                iec.iec(mip, m_port, iecSetting.RVBHeartBeatTimer, "Write", heartbeattimer.Value, iecSetting.iedName)
                'set RVB Max
                iec.iec(mip, m_port, iecSetting.RVBMax, "Write", RVBMax.Value * M2001D_Comm_Scale, iecSetting.iedName)
                'set RVB Min
                iec.iec(mip, m_port, iecSetting.RVBMin, "Write", RVBMin.Value * M2001D_Comm_Scale, iecSetting.iedName)
                'set Fwd RVB Scale Factor
                iec.iec(mip, m_port, iecSetting.FRVBScale, "Write", FwdRVBScaleFactor.Value * M2001D_Comm_Scale, iecSetting.iedName)
                'set Rev RVB Scale Factor
                iec.iec(mip, m_port, iecSetting.RRVBScale, "Write", RevRVBScaleFactor.Value * M2001D_Comm_Scale, iecSetting.iedName)
            Else
                MsgBox("Unsupported communication protocol")
                Pause()
            End If

        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

#Region " Timers "
    Private Sub Start()
        Try
            btnStop.Enabled = True
            btnStart.Enabled = False
            Disenable()

            mip = Net.IPAddress.Parse(txtWrite.Text)
            If Not txtRead.Text = Nothing Then
                m_ip = Net.IPAddress.Parse(txtRead.Text)
            Else
                m_ip = mip
                txtRead.Text = mip.ToString
            End If
            m_port = CUShort(txtPort.Text)

            'NO LONGER NEEDED
            'turn on rvb option in the factory if it is not enabled already
            'Dim factoryOptions As UInt16 = modbus.CommunicateSingleUnit(m_ip, modbusRegister.Port, modbusRegister.Factory, _modbusfunc.read, 1)
            'If Not (factoryOptions = (factoryOptions Or &H20)) Then   'RVB is bit 5
            '    factoryOptions = (factoryOptions Or &H20)
            '    modbus.CommunicateSingleUnit(m_ip, modbusRegister.Port, modbusRegister.Factory, _modbusfunc.write, factoryOptions)
            'End If
            '''''''''''''''''''''''''''''''''''''

            'Send RVB settings everytime start pressed
            SendSettings()
            '''''''''''''''''''''''''''''''''''''

            ReadLocalVoltageTimer = New System.Timers.Timer
            With ReadLocalVoltageTimer
                AddHandler .Elapsed, AddressOf ReadLocalVoltage
                .Interval = Interval
                .Enabled = True
            End With
            Heart_Beat_Set = (heartbeattimer.Value * 1000) - TDelay
            OmicronBootupCompleted = False
            ReadLocalVoltage()
        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub
#End Region

    Private Sub Pause()
        Try
            SetText(Label1, "Comm stopped ...")
            btnStop.Enabled = False
            btnStart.Enabled = True
            Disenable()
            delayforOmicronPowerUp = 0
            Heart_Beat_Timer = 0
            ReadLocalVoltageTimer.Stop()
            ReadLocalVoltageTimer.Dispose()
        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

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
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Try
        AddHandler My.Application.UnhandledException, AddressOf MyApplication_UnhandledException
        Dim i As Integer = 1
        i /= 0
        '    'AddHandler My.Application.UnhandledException, AddressOf MyHandler
        Main()
        ' Catch ex As Exception
        'MsgBox(ex.ToString + vbCrLf + ex.InnerException.ToString)
        ' End Try

    End Sub

    Public Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs)

        MsgBox(e.Exception.ToString) '+ vbCrLf + e.ToString, , "Unhandled exception")
    End Sub

    Public Sub Main()
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
            Next
            populatetheform()
            If My.Application.CommandLineArgs.Count > 1 Then
                checkcommandline()
            Else
                txtRead.Focus()
            End If
        Catch ex As Exception
            SetText(Label1, ex.ToString)
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

            txtRead.Enabled = modbusbox.Checked      'dnp & iec61850 library is not supporting 2 different ip addresses
            'blocking 2nd address
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
        End Try
    End Sub
End Class

