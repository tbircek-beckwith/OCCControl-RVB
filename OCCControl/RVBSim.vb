Imports iec.iec
Imports tcpmodbus.modbus
Imports tcpdnp.dnp
Imports System.Net
Imports System.Timers

Public Class RVBSim
    Const SupportedRVBRevision As String = "15"       'Supported RVB feature document revision
    Const M2001D_Comm_Scale As Integer = 10
    Const MaxDeltaVoltage As Integer = 100
    Const MinDeltaVoltage As Integer = -100
    Const DeltaMessage As String = "Local Voltage + "
    Const DirectMessage As String = "RVB Voltage is ="
    Const RVBVisibilityDelay As Integer = 25         '* 100msec RVB voltage stays on the display
    Const delayforOmicronPowerUpMax As Integer = 6
    Const TDelay As Integer = 500                    'Heartbeat refresh time is off so it can refresh

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
    Dim processID As Integer = 0
    Dim visibility As Boolean = True
    Dim RVBVisibilityTime As Double = 0.0
    ''Rev 15 changes 
    Dim support As Boolean      'True rev 15 or greater False rev 8
    Dim Reverse_RVBVoltage2Write As Double = 0.0
    Dim powerDirection As UInt16 = 0

    Enum Register
        None = 0
        RVBEnable = 1991
        'FwdRVBVoltage = 1992
        FwdRVBScaleFactor = 1993
        RVBHeartbeatTimer = 1994
        'RVBBiasActive = 1995
        'RevRVBVoltage = 1996
        RevRVBScaleFactor = 1997
        RVBMaximum = 1998
        RVBMinimum = 1999
        FactoryOptions = 4787
    End Enum

    Private Delegate Sub SetTextDelegate(ByVal myControl As Control, ByVal itsText As String)

    Private Sub SetText(ByVal myControl As Control, ByVal itsText As String)
        If myControl.InvokeRequired Then
            Dim del As New SetTextDelegate(AddressOf SetText)
            myControl.Invoke(del, New Object() {myControl, itsText})
        Else
            myControl.Text = itsText
        End If
    End Sub

    Private Sub GenerateRVBVoltage2Transfer()
        Dim Forward_RVBVoltage2OperateWith As Double = 0.0
        Dim Reverse_RVBVoltage2OperateWith As Double = 0.0

        Select Case radUseFixedVoltage.Checked
            Case False
                Dim ActualLocalVoltage As Double = readresult / M2001D_Comm_Scale
                Console.WriteLine("Actual voltage is: {0}", ActualLocalVoltage)
                If Not ActualLocalVoltage = 0.0 Then
                    Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(FwdDeltaVoltage.Value)) ' Else Forward_RVBVoltage2OperateWith = 0 ' * multiplier.Value   'using delta voltage
                    Reverse_RVBVoltage2OperateWith = (ActualLocalVoltage + CDbl(RevDeltaVoltage.Value))
                Else
                    Forward_RVBVoltage2OperateWith = 0.0
                    Reverse_RVBVoltage2OperateWith = 0.0
                End If
            Case True
                Forward_RVBVoltage2OperateWith = CDbl(FwdDeltaVoltage.Value) ' * multiplier.Value      'using fixed voltage
                Reverse_RVBVoltage2OperateWith = CDbl(RevDeltaVoltage.Value)
        End Select
        Forward_RVBVoltage2OperateWith *= CDbl(FwdRVBScaleFactor.Value)
        Reverse_RVBVoltage2OperateWith *= CDbl(RevRVBScaleFactor.Value)

        Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
        Reverse_RVBVoltage2Write = Reverse_RVBVoltage2OperateWith
    End Sub

    Private Sub ticker(ByVal mip As IPAddress, ByVal m_port As UShort)
        Try
            GenerateRVBVoltage2Transfer()

            If dnpbutton.Checked Then
                dnp.tcpdnp(mip, m_port, CUShort(NumericUpDownDNPDestinationAddress.Value), CUShort(NumericUpDownDNPSourceAddress.Value), _dnpfunc.directnoack,
                           _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, 0, CUShort(Forward_RVBVoltage2Write), 0)
            End If

            If modbusbox.Checked Then
                modbus.CommunicateSingleUnit(mip, m_port, CUShort(NumericUpDownModbusFwdRVBVoltageRegister.Value), _modbusfunc.write, CUShort(Forward_RVBVoltage2Write))
                modbus.CommunicateSingleUnit(mip, m_port, CUShort(NumericUpDownModbusRevRVBVoltageRegister.Value), _modbusfunc.write, CUShort(Reverse_RVBVoltage2Write))
            End If
            If iec61850box.Checked Then
                iec.iec(mip, m_port, txtIECFwdRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write))
            End If
            RVBVisibilityTime = RVBVisibilityDelay
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
                readresult = (dnp.tcpdnp(m_ip, m_port, CUShort(NumericUpDownDNPDestinationAddress.Value), CUShort(NumericUpDownDNPSourceAddress.Value), _dnpfunc.read,
                                            _dnpobj.AnalogInput, _dnpvar.varall, _dnpindex.read))
            ElseIf modbusbox.Checked Then
                readresult = (modbus.CommunicateSingleUnit(m_ip, m_port, CUShort(NumericUpDownModbusLocalVoltageRegister.Value), _modbusfunc.read, 1))
            ElseIf iec61850box.Checked Then
                readresult = (iec.iec(m_ip, m_port, txtIECLocalVoltage.Text, "Connect"))
            End If

            delayforOmicronPowerUp += 1
            Heart_Beat_Timer += 109
            If (Heart_Beat_Timer >= Heart_Beat_Set) OrElse (delayforOmicronPowerUp = delayforOmicronPowerUpMax) Then
                'write to the unit using customer selected protocol
                ticker(mip, m_port)
                Heart_Beat_Timer = 0
                delayforOmicronPowerUp = 7
            End If
            If delayforOmicronPowerUp > delayforOmicronPowerUpMax Then
                If Not RVBVisibilityTime = 0 Then
                   RVBVisibilityTime -= 1
                    ' ElseIf RVBVisibilityTime = 0 Then

                End If
                SetText(Label1, String.Format("     Reads: {0}" + vbCrLf + "Fwd RVB: {1}" + vbCrLf + "Rev RVB: {2}", FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1), FormatNumber(Forward_RVBVoltage2Write, 1), FormatNumber(Reverse_RVBVoltage2Write, 1)))
            End If

        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

    Private Sub Disenable()
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
            txtRead.Enabled = .Enabled
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
    End Sub

    Private Sub SendSettings()
        'Enable RVB
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.RVBEnable, _modbusfunc.write, 1)
        'set RVB heartbeat timer
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.RVBHeartbeatTimer, _modbusfunc.write, heartbeattimer.Value)
        'set RVB Max
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.RVBMaximum, _modbusfunc.write, RVBMax.Value * M2001D_Comm_Scale)
        'set RVB Min
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.RVBMinimum, _modbusfunc.write, RVBMin.Value * M2001D_Comm_Scale)
        'set Fwd RVB Scale Factor
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.FwdRVBScaleFactor, _modbusfunc.write, FwdRVBScaleFactor.Value * M2001D_Comm_Scale)
        'set Rev RVB Scale Factor 
        modbus.CommunicateSingleUnit(m_ip, m_port, Register.RevRVBScaleFactor, _modbusfunc.write, RevRVBScaleFactor.Value * M2001D_Comm_Scale)
    End Sub

#Region " Timers "
    Private Sub Start()
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

        'turn on rvb option in the factory if it is not enabled already
        Dim factoryOptions As UInt16 = modbus.CommunicateSingleUnit(m_ip, m_port, Register.FactoryOptions, _modbusfunc.read, 1)
        If Not (factoryOptions = (factoryOptions Or &H20)) Then   'RVB is bit 5
            factoryOptions = (factoryOptions Or &H20)
            modbus.CommunicateSingleUnit(m_ip, m_port, Register.FactoryOptions, _modbusfunc.write, factoryOptions)
        End If
        '''''''''''''''''''''''''''''''''''''

        'Send RVB settings everytime start pressed
        SendSettings()
        '''''''''''''''''''''''''''''''''''''

        ReadLocalVoltageTimer = New System.Timers.Timer
        With ReadLocalVoltageTimer
            AddHandler .Elapsed, AddressOf ReadLocalVoltage
            .Interval = 100
            .Enabled = True
        End With
        Heart_Beat_Set = (heartbeattimer.Value * 1000) - TDelay

        ReadLocalVoltage()
    End Sub
#End Region

    Private Sub Pause()
        SetText(Label1, "Comm stopped ...")
        btnStop.Enabled = False
        btnStart.Enabled = True
        Disenable()
        delayforOmicronPowerUp = 0
        Heart_Beat_Timer = 0
        ReadLocalVoltageTimer.Stop()
        ReadLocalVoltageTimer.Dispose()
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        Start()
    End Sub

    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        Pause()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
                If Not txtRead.Text = Nothing Then
                    .IPAddressToRead = txtRead.Text
                Else
                    .IPAddressToRead = txtWrite.Text
                End If

            End With
        Catch ex As Exception
            'do nothing
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Main()
    End Sub

    Public Sub Main()
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
        If My.Application.CommandLineArgs.Count > 1 Then
            checkcommandline()
        Else
            populatetheform()
            txtRead.Focus()
        End If

    End Sub

    Private Sub checkHandler(sender As System.Object)
        Select Case sender.text
            Case "DNP3.0"
                AddressBox.Text = "DNP3.0 Addresses"
                lblwarning.Text = "Don't forget to download DNP config file (RVBTest.xml)"
                If My.Settings.dnpport = Nothing Then
                    txtPort.Text = "20000"
                Else
                    txtPort.Text = My.Settings.dnpport.ToString
                End If
            Case "Modbus"
                AddressBox.Text = "Modbus registers"
                If My.Settings.mdport = Nothing Then
                    txtPort.Text = "502"
                Else
                    txtPort.Text = My.Settings.mdport.ToString
                End If
            Case "IEC61850"
                AddressBox.Text = "IEC61850 Datasets"
                lblwarning.Text = "Don't forget to purchase IEC61850"
                If My.Settings.IECLocalVoltage = Nothing Then
                    txtIECLocalVoltage.Text = "ATCC0$MX$LodCtrV$mag$i"
                Else
                    txtIECLocalVoltage.Text = My.Settings.IECLocalVoltage
                End If
                If My.Settings.IECFwdRVBVoltage = Nothing Then
                    txtIECFwdRVBVoltage.Text = "ATCC0$SP$FRemVVal$setMag$i"
                Else
                    txtIECFwdRVBVoltage.Text = My.Settings.IECFwdRVBVoltage
                End If
                If My.Settings.iecport = Nothing Then
                    txtPort.Text = "102"
                Else
                    txtPort.Text = My.Settings.iecport.ToString
                End If
        End Select

        txtRead.Enabled = modbusbox.Checked      'dnp & iec61850 library is not supporting 2 different ip addresses yet
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

        'txthost.Select()
        txtRead.Select()
        'rev 15 items
        R_RVBScaleFactor_Label.Visible = support
        RevRVBScaleFactor.Visible = support
        Reverse_Voltage_Label.Visible = support
        RevDeltaVoltage.Visible = support

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

        Dim i As Integer = 0

        Try
            Dim cmdlines As String() = New String(My.Application.CommandLineArgs.Count - 1) {}
            For Each item As String In My.Application.CommandLineArgs
                cmdlines.SetValue(item.ToLower, i)
                Console.WriteLine("Command Line is: {0}", item.ToLower)
                i += 1
            Next
            i = 0
            With My.Settings
                For Each item In cmdlines
                    Select Case item.ToLower
                        Case "-p"
                            .protocol = cmdlines.GetValue(i + 1)
                        Case "-i"
                            Select Case .protocol
                                Case "dnp"
                                    .dnphost = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    .mdhost = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .iechost = cmdlines.GetValue(i + 1)
                            End Select
                        Case "-o"
                            Select Case .protocol
                                Case "dnp"
                                    .dnpport = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    .mdport = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .iecport = cmdlines.GetValue(i + 1)
                            End Select
                        Case "-m"
                            Select Case .protocol
                                Case "dnp"
                                    .source = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    .mdLocalvoltage = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .IECLocalVoltage = UCase(cmdlines.GetValue(i + 1))
                            End Select
                        Case "-d"
                            Select Case .protocol
                                Case "dnp"
                                    .destination = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    .mdFRVBvoltage = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .IECFwdRVBVoltage = UCase(cmdlines.GetValue(i + 1))
                            End Select
                        Case "-h"
                            .heartbeat = cmdlines.GetValue(i + 1)
                        Case "-v"
                            .Fdeltavoltage = cmdlines.GetValue(i + 1)
                        Case "-x"
                            .Fmultiplier = cmdlines.GetValue(i + 1)
                        Case "-g"
                            If cmdlines.GetValue(i + 1) = "off" Then
                                visibility = False
                            End If
                        Case "-r"
                            .IPAddressToRead = cmdlines.GetValue(i + 1)
                        Case "-s"
                            Select Case cmdlines.GetValue(i + 1)
                                Case "start"
                                    populatetheform()
                                    Start()
                                Case "pause"
                                    Pause()
                                Case "end"
                                    Me.Close()
                            End Select
                    End Select
                    i += 1
                Next
            End With
            populatetheform()
        Catch ex As Exception
            'no feedback to user
        End Try
    End Sub

    'Private Sub consolehelp()
    '    Console.Clear()
    '    Console.WriteLine(" ")
    '    Console.WriteLine("     RVB Voltage Test Usage")
    '    Console.WriteLine(" ")
    '    Console.WriteLine("-p   is either dnp or modbus")
    '    Console.WriteLine("-i   ipaddress")
    '    Console.WriteLine("-o   port number")
    '    Console.WriteLine("-m   if protocol is dnp than master address,")
    '    Console.WriteLine("     if protocol is modbus than Local Voltage register")
    '    Console.WriteLine("-d   if protocol is dnp than destination address,")
    '    Console.WriteLine("     if protocol is modbus than RVB Voltage register")
    '    Console.WriteLine("-h   heartbeat refresh timer in seconds range 2-120")
    '    Console.WriteLine("-v   delta voltage value add to the local voltage range")
    ''    Console.WriteLine("-x  RVB scale factor")
    ''    Console.WriteLine("-g  on or off. shows windows interface")
    '    Console.Writeline ("-r  is remote ip address to read voltage from")
    '    Console.WriteLine("-s   start: starts broadcasting, ")
    '    Console.WriteLine("     end: end this program,")
    '    Console.WriteLine("     pause: waits until off or on")
    '    Console.WriteLine(" ")
    '    Console.WriteLine("Example:")
    '    Console.WriteLine("rvbtest -p dnp -i 10.10.2.147 -o 20000 -m 3")
    ''    Console.WriteLine("        -d 1 -h 2 -v 6 -x 1.0 -g off -s start")
    '    Console.WriteLine("        -d 1 -h 2 -v 6 -s start")
    '    Console.WriteLine(" ")
    '    _close()
    'End Sub

    Private Sub populatetheform()

        Me.Text = String.Format("RVB Simulator v-{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        If CInt(SupportedRVBRevision) >= 15 Then
            support = True
            grpRevSettings.Visible = True
        Else
            support = False
            grpRevSettings.Visible = False
        End If

        With My.Settings
            NumericUpDownDNPSourceAddress.Value = .source
            NumericUpDownDNPDestinationAddress.Value = .destination

            NumericUpDownModbusLocalVoltageRegister.Value = .mdLocalvoltage
            NumericUpDownModbusFwdRVBVoltageRegister.Value = .mdFRVBvoltage
            NumericUpDownModbusRevRVBVoltageRegister.Value = .mdRRVBvoltage

            heartbeattimer.Value = .heartbeat
            txtRead.Text = .IPAddressToRead
            If .Fdeltavoltage < MinDeltaVoltage Or .Fdeltavoltage > MaxDeltaVoltage Then FwdDeltaVoltage.Value = 0.0 Else FwdDeltaVoltage.Value = .Fdeltavoltage
            If .Rdeltavoltage < MinDeltaVoltage Or .Rdeltavoltage > MaxDeltaVoltage Then RevDeltaVoltage.Value = 0.0 Else RevDeltaVoltage.Value = .Rdeltavoltage

            FwdRVBScaleFactor.Value = .Fmultiplier
            RevRVBScaleFactor.Value = .Rmultiplier
            txtIECLocalVoltage.Text = .IECLocalVoltage
            txtIECFwdRVBVoltage.Text = .IECFwdRVBVoltage
            txtIECRevRVBVoltage.Text = .IECRevRVBVoltage

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
            Select Case .protocol
                Case "dnp"
                    dnpbutton.Checked = True
                    txtRead.Text = .dnphost
                    txtWrite.Text = .dnphost
                    txtPort.Text = .dnpport
                    checkHandler(dnpbutton)
                Case "modbus"
                    modbusbox.Checked = True
                    txtRead.Text = .mdhost
                    txtWrite.Text = .mdhost
                    txtPort.Text = .mdport
                    checkHandler(modbusbox)
                Case "iec"
                    iec61850box.Checked = True
                    txtRead.Text = .iechost
                    txtWrite.Text = .iechost
                    txtPort.Text = .iecport
                    checkHandler(iec61850box)
            End Select
        End With
    End Sub

    Private Sub Radio_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radUseDeltaVoltage.CheckedChanged, radUseFixedVoltage.CheckedChanged

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
                If readresult / M2001D_Comm_Scale >= FwdDeltaVoltage.Minimum Then FwdDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else FwdDeltaVoltage.Value = 120.0
                If readresult / M2001D_Comm_Scale >= RevDeltaVoltage.Minimum Then RevDeltaVoltage.Value = readresult / M2001D_Comm_Scale Else RevDeltaVoltage.Value = 120.0
        End Select
    End Sub
End Class

Public Class ReadXmlFile

    Public Sub read()
        Dim xmlReader As Xml.XmlReader = Nothing
        Dim i As Integer = 0
        Try
            Dim settings = New Xml.XmlReaderSettings
            settings.IgnoreComments = True
            settings.IgnoreWhitespace = True

            xmlReader = Xml.XmlReader.Create(My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, "TestPlan.xml"))

            While xmlReader.Read
                Select Case xmlReader.Name
                    'Case "tapRange"
                    '    Dim lowerTap As Integer = CInt(xmlReader.GetAttribute("lowestTap"))
                    '    LowestTaps.Add(lowerTap)
                    '    Dim higherTap As Integer = CInt(xmlReader.GetAttribute("highestTap"))
                    '    HighestTaps.Add(higherTap)
                    'Case "neutral"
                    '    Dim neutralTap As Integer = CInt(xmlReader.GetAttribute("pos"))
                    '    NeutralPositions.Add(neutralTap)
                    'Case "neutralTapNumber"
                    '    minNeutralTapNumber = CInt(xmlReader.GetAttribute("min"))
                    '    maxNeutralTapNumber = CInt(xmlReader.GetAttribute("max"))
                    'Case "restingTapNumber"
                    '    minRestingTapNumber = CInt(xmlReader.GetAttribute("min"))
                    '    maxRestingTapNumber = CInt(xmlReader.GetAttribute("max"))
                    'Case "restingTapLocations"
                    '    minRestingTapLocation = CInt(xmlReader.GetAttribute("min"))
                    '    maxRestingTapLocation = CInt(xmlReader.GetAttribute("max"))
                    'Case "neutralContacts"
                    '    minNeutralContact = CInt(xmlReader.GetAttribute("min"))
                    '    maxNeutralContact = CInt(xmlReader.GetAttribute("max"))
                    'Case "operationsPerTest"
                    '    OperationsPerTest = CInt(xmlReader.GetAttribute("min"))
                End Select
            End While
        Catch ex As Exception
            MsgBox(ex.ToString, , "XML Reader")
        End Try
    End Sub
End Class

