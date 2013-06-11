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
    Dim delayforOmicronPowerUp As Integer = 0
    Dim Heart_Beat_Timer As Double = 0.0
    Dim Heart_Beat_Set As Double = 0.0
    Dim Forward_RVBVoltage2Write As Double
    Dim dnp As New tcpdnp.dnp
    Dim modbus As New tcpmodbus.modbus
    Dim iec As New iec.iec
    Dim readresult As UShort = 0
    Dim processID As Integer = 0
    Dim visibility As Boolean = True
    Dim RVBVisibilityTime As Double = 0.0
    'Rev 15 changes 
    Dim support As Boolean      'True rev 15 or greater False rev 8
    Dim Reverse_RVBVoltage2Write As Double

    Private Delegate Sub SetTextDelegate(ByVal myControl As Control, ByVal itsText As String)
    Private Delegate Sub ClearTextDelegate(ByVal myControl As TextBox)
    'Private Delegate Sub SetProgressBarValueDelegate(ByVal itsValue As Integer)

    'Private Sub SetProgressBarValue(ByVal itsValue As Integer)
    '    If ProgressBar1.InvokeRequired Then
    '        Dim del As New SetProgressBarValueDelegate(AddressOf SetProgressBarValue)
    '        ProgressBar1.Invoke(del, New Object() {itsValue})
    '    Else
    '        ProgressBar1.Value = itsValue
    '    End If
    'End Sub

    Private Sub ClearText(ByVal myControl As TextBox)
        If myControl.InvokeRequired Then
            Dim del As New ClearTextDelegate(AddressOf ClearText)
            myControl.Invoke(del, New Object() {myControl})
        Else
            myControl.Clear()
        End If
    End Sub

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

        Select Case radLocal.Checked
            Case False
                Dim ActualLocalVoltage As Double = readresult / M2001D_Comm_Scale
                Forward_RVBVoltage2OperateWith = (ActualLocalVoltage + Forward_DeltaVoltage.Value) ' * multiplier.Value   'using delta voltage
            Case True
                Forward_RVBVoltage2OperateWith = Forward_DeltaVoltage.Value ' * multiplier.Value      'using fixed voltage
        End Select
        Forward_RVBVoltage2OperateWith *= F_RVBScaleFactor_Value.Value
        Forward_RVBVoltage2Write = Forward_RVBVoltage2OperateWith
    End Sub

    Private Sub ticker(ByVal mip As IPAddress, ByVal m_port As UShort)

        GenerateRVBVoltage2Transfer()

        If dnpbutton.Checked Then
            dnp.tcpdnp(mip, m_port, CUShort(destnum.Value), CUShort(sourcenum.Value), _dnpfunc.directnoack,
                       _dnpobj.AnalogOutput, _dnpvar.var2, _dnpindex.write, 1, 0, CUShort(Forward_RVBVoltage2Write), 0)
        End If

        If modbusbox.Checked Then
            modbus.CommunicateSingleUnit(mip, m_port, CUShort(RVBVoltage.Value), _modbusfunc.write, CUShort(Forward_RVBVoltage2Write)) 'CUShort(writeresult))
        End If
        If iec61850box.Checked Then
            iec.iec(mip, m_port, txtRVBVoltage.Text, "Write", CUShort(Forward_RVBVoltage2Write))
            'iec.iec(mip, m_port, txtRVBVoltage.Text, "Write", 1200)
        End If
        RVBVisibilityTime = RVBVisibilityDelay
    End Sub

    Dim m_ip, mip As IPAddress
    Dim m_port As UShort
    Dim canvas As Graphics '= New Graphics '(Me.ProgressBar1.CreateGraphics)

    Private Sub ReadLocalVoltage()
        'Reading Local voltage and transmitting back
        'done by apporiate protocol selected by the user
        Try
            mip = Net.IPAddress.Parse(txthost.Text)
            If Not IPAddressToReadTextbox.Text = Nothing Then
                m_ip = Net.IPAddress.Parse(IPAddressToReadTextbox.Text)
            Else
                m_ip = mip
                IPAddressToReadTextbox.Text = mip.ToString
            End If
            m_port = CUShort(txtport.Text)

            'Local Voltage will read every 100ms but writing back will be done in 
            ' the user speficied time frame
            If dnpbutton.Checked Then
                readresult = (dnp.tcpdnp(m_ip, m_port, CUShort(destnum.Value), CUShort(sourcenum.Value), _dnpfunc.read,
                                            _dnpobj.AnalogInput, _dnpvar.varall, _dnpindex.read)) '/ M2001D_Comm_Scale
            ElseIf modbusbox.Checked Then
                readresult = (modbus.CommunicateSingleUnit(m_ip, m_port, CUShort(locvoltage.Value), _modbusfunc.read, 1)) '/ Comm_Scale
            ElseIf iec61850box.Checked Then
                readresult = (iec.iec(m_ip, m_port, txtLocalVoltage.Text, "Connect")) '/ M2001D_Comm_Scale
            End If

            If Not (readresult > FwdRVBMax.Value Or readresult < FwdRVBMin.Value) Then
                delayforOmicronPowerUp += 1
                ''SetProgressBarValue(Heart_Beat_Timer)
                Heart_Beat_Timer += 109
                If (Heart_Beat_Timer >= Heart_Beat_Set) OrElse (delayforOmicronPowerUp = delayforOmicronPowerUpMax) Then
                    'write to the unit using customer selected protocol
                    'ticker(m_ip, m_port)
                    ticker(mip, m_port)
                    Heart_Beat_Timer = 0
                    'SetProgressBarValue(Heart_Beat_Timer)
                    delayforOmicronPowerUp = 7
                End If
                If delayforOmicronPowerUp > delayforOmicronPowerUpMax Then
                    SetText(Label1, "Reads: " & FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1))
                    If Not RVBVisibilityTime = 0 Then
                        SetText(Label1, Label1.Text & " Writes: " & FormatNumber(Forward_RVBVoltage2Write, 1))
                        RVBVisibilityTime -= 1
                    ElseIf RVBVisibilityTime = 0 Then
                        SetText(Label1, "Reads: " & FormatNumber(CDbl(readresult / M2001D_Comm_Scale), 1) & " Writes: ----")
                        '& vbCrLf & (CInt(Heart_Beat_Timer / 1000)).ToString & " secs of " & (CInt(Heart_Beat_Set / 1000).ToString & " secs"))
                    End If
                End If

            Else
                delayforOmicronPowerUp = 0
                Heart_Beat_Timer = 0
                'SetProgressBarValue(Heart_Beat_Timer)
                SetText(Label1, "Read value is out of limits...")
                'SetText(Label1, readresult)
            End If

        Catch ex As Exception
            SetText(Label1, ex.ToString)
        End Try
    End Sub

    Private Sub Disenable()
        locvoltage.Enabled = Button1.Enabled
        RVBVoltage.Enabled = Button1.Enabled
        sourcenum.Enabled = Button1.Enabled
        txtLocalVoltage.Enabled = Button1.Enabled
        txtRVBVoltage.Enabled = Button1.Enabled
        destnum.Enabled = Button1.Enabled
        txthost.Enabled = Button1.Enabled
        txtport.Enabled = Button1.Enabled
        dnpbutton.Enabled = Button1.Enabled
        modbusbox.Enabled = Button1.Enabled
        iec61850box.Enabled = Button1.Enabled
        F_RVBScaleFactor_Value.Enabled = Button1.Enabled
        heartbeattimer.Enabled = Button1.Enabled
        radDelta.Enabled = Button1.Enabled
        radLocal.Enabled = Button1.Enabled
        'deltavoltage.Enabled = Button1.Enabled
        IPAddressToReadTextbox.Enabled = Button1.Enabled
    End Sub

#Region " Timers "
    Private Sub Start()
        Button2.Enabled = True
        Button1.Enabled = False
        Disenable()
        ReadLocalVoltageTimer = New System.Timers.Timer
        With ReadLocalVoltageTimer
            AddHandler .Elapsed, AddressOf ReadLocalVoltage
            .Interval = 100
            .Enabled = True
        End With
        Heart_Beat_Set = (heartbeattimer.Value * 1000) - TDelay
        'ProgressBar1.Maximum = (heartbeattimer.Value * 1000) - 100
        ReadLocalVoltage()
    End Sub
#End Region

    Private Sub Pause()
        SetText(Label1, "Comm stopped ...")
        Button2.Enabled = False
        Button1.Enabled = True
        Disenable()
        delayforOmicronPowerUp = 0
        Heart_Beat_Timer = 0
        'SetProgressBarValue(Heart_Beat_Timer)
        ReadLocalVoltageTimer.Stop()
        ReadLocalVoltageTimer.Dispose()
    End Sub

    'Private Sub _close()
    '    Me.Close()
    'End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Start()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Pause()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            With My.Settings
                ._location = Me.DesktopLocation
                If dnpbutton.Checked Then
                    ._dnphost = txthost.Text
                    ._dnpport = CUShort(txtport.Text)
                    ._protocol = "dnp"
                    ._source = CUShort(sourcenum.Value)
                    ._destination = CUShort(destnum.Value)
                ElseIf modbusbox.Checked Then
                    ._md_host = txthost.Text
                    ._md_port = CUShort(txtport.Text)
                    ._protocol = "modbus"
                    ._md_localvoltage = CUShort(locvoltage.Value)
                    ._md_FRVBvoltage = CUShort(RVBVoltage.Value)
                    ._md_RRVBvoltage = CUShort(Modbus_R_RVBVoltage_Value.Value)
                ElseIf iec61850box.Checked Then
                    .iechost = txthost.Text
                    .iecport = CUShort(txtport.Text)
                    ._protocol = "iec"
                    .IEC61850LocalVoltage = txtLocalVoltage.Text
                    .IEC61850FRVBVoltage = txtRVBVoltage.Text
                End If
                ._heartbeat = CUShort(heartbeattimer.Value)
                ._Fdeltavoltage = CDbl(Forward_DeltaVoltage.Value)
                ._Fmultiplier = CDbl(F_RVBScaleFactor_Value.Value)
                If Not IPAddressToReadTextbox.Text = Nothing Then
                    ._IPAddressToRead = IPAddressToReadTextbox.Text
                Else
                    ._IPAddressToRead = txthost.Text
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
            'txthost.Focus()
            IPAddressToReadTextbox.Focus()
        End If
    End Sub

    Private Sub checkHandler(sender As System.Object)
        Select Case sender.text
            Case "DNP3.0"
                AddressBox.Text = "DNP3.0 Addresses"
                lblwarning.Text = "Don't forget to download DNP config file (RVBTest.xml)"
                If My.Settings._dnpport = Nothing Then
                    txtport.Text = "20000"
                Else
                    txtport.Text = My.Settings._dnpport.ToString
                End If
            Case "Modbus"
                AddressBox.Text = "Modbus registers"
                If My.Settings._md_port = Nothing Then
                    txtport.Text = "502"
                Else
                    txtport.Text = My.Settings._md_port.ToString
                End If
            Case "IEC61850"
                AddressBox.Text = "IEC61850 Datasets"
                lblwarning.Text = "Don't forget to purchase IEC61850"
                If My.Settings.IEC61850LocalVoltage = Nothing Then
                    txtLocalVoltage.Text = "ATCC0$MX$LodCtrV$mag$i"
                Else
                    txtLocalVoltage.Text = My.Settings.IEC61850LocalVoltage
                End If
                If My.Settings.IEC61850FRVBVoltage = Nothing Then
                    txtRVBVoltage.Text = "ATCC0$SP$FRemVVal$setMag$i"
                Else
                    txtRVBVoltage.Text = My.Settings.IEC61850FRVBVoltage
                End If
                If My.Settings.iecport = Nothing Then
                    txtport.Text = "102"
                Else
                    txtport.Text = My.Settings.iecport.ToString
                End If
        End Select

        IPAddressToReadTextbox.Enabled = modbusbox.Checked      'dnp & iec61850 library is not supporting 2 different ip addresses yet
        'blocking 2nd address
        lbldestination.Visible = dnpbutton.Checked
        lblsource.Visible = dnpbutton.Checked
        sourcenum.Visible = dnpbutton.Checked
        destnum.Visible = dnpbutton.Checked
        lblwarning.Visible = dnpbutton.Checked Or iec61850box.Checked

        lbllocalvoltage.Visible = modbusbox.Checked
        Modbus_F_RVBVoltage_Label.Visible = modbusbox.Checked
        Modbus_R_RVBVoltage_Label.Visible = modbusbox.Checked And support
        locvoltage.Visible = modbusbox.Checked
        RVBVoltage.Visible = modbusbox.Checked
        Modbus_R_RVBVoltage_Value.Visible = modbusbox.Checked And support

        IEC_LocalVoltage_Label.Visible = iec61850box.Checked
        IEC_F_RVBVoltage_Label.Visible = iec61850box.Checked
        IEC_R_RVBVoltage_Label.Visible = iec61850box.Checked And support
        txtLocalVoltage.Visible = iec61850box.Checked
        txtRVBVoltage.Visible = iec61850box.Checked
        IEC_R_RVBVoltage_Value.Visible = iec61850box.Checked And support

        'txthost.Select()
        IPAddressToReadTextbox.Select()
        'rev 15 items
        R_RVBScaleFactor_Label.Visible = support
        R_RVBScaleFactor_Value.Visible = support
        Reverse_Voltage_Label.Visible = support
        Reverse_DeltaVoltage.Visible = support

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
            Dim cmdlines As String() = New String(My.Application.CommandLineArgs.Count) {}
            For Each item As String In My.Application.CommandLineArgs
                cmdlines.SetValue(item.ToLower, i)
                i += 1
            Next
            i = 0
            With My.Settings
                For Each item In cmdlines
                    Select Case item.ToLower
                        Case "-p"
                            ._protocol = cmdlines.GetValue(i + 1)
                        Case "-i"
                            Select Case ._protocol
                                Case "dnp"
                                    ._dnphost = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    ._md_host = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .iechost = cmdlines.GetValue(i + 1)
                            End Select
                        Case "-o"
                            Select Case ._protocol
                                Case "dnp"
                                    ._dnpport = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    ._md_port = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .iecport = cmdlines.GetValue(i + 1)
                            End Select
                        Case "-m"
                            Select Case ._protocol
                                Case "dnp"
                                    ._source = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    ._md_localvoltage = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .IEC61850LocalVoltage = UCase(cmdlines.GetValue(i + 1))
                            End Select
                        Case "-d"
                            Select Case ._protocol
                                Case "dnp"
                                    ._destination = cmdlines.GetValue(i + 1)
                                Case "modbus"
                                    ._md_FRVBvoltage = cmdlines.GetValue(i + 1)
                                Case "iec"
                                    .IEC61850FRVBVoltage = UCase(cmdlines.GetValue(i + 1))
                            End Select
                        Case "-h"
                            ._heartbeat = cmdlines.GetValue(i + 1)
                        Case "-v"
                            ._Fdeltavoltage = cmdlines.GetValue(i + 1)
                        Case "-x"
                            ._Fmultiplier = cmdlines.GetValue(i + 1)
                        Case "-g"
                            If cmdlines.GetValue(i + 1) = "off" Then
                                visibility = False
                            End If
                            populatetheform()
                        Case "-r"
                            ._IPAddressToRead = cmdlines.GetValue(i + 1)
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
        'Me.Text = "RVBTest V-" & My.Application.Info.Version.ToString & " " & My.Application.Info.Copyright ' & " Version "
        'Dim copyright As String = My.Application.Info.Copyright
        'Dim versionFormat As String = "(v-{0}.{1}.{2}.{3})"
        'Dim version As String = System.String.Format(versionFormat,My.Application.Info.Version.Major,My.Application.Info.Version.Minor,My.Application.Info.Version.Build,        My.Application.Info.Version.Revision)
        'Me.Text = My.Application.Info.Title & " " & copyright & " " & version & " RVB Revision: " & SupportedRVBRevision
        'Me.Text = "RVB Simulator By " + copyright + " " + version + " Supported RVB Revision: " + SupportedRVBRevision
        Me.Text = String.Format("RVB Simulator v-{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)

        If CInt(SupportedRVBRevision) >= 15 Then
            support = True
            grpRevSettings.Visible = True
        Else
            support = False
            grpRevSettings.Visible = False
        End If
        SetText(txtFwdRVBMax, FormatNumber(CDbl(FwdRVBMax.Value / 10), 1))
        SetText(txtFwdRVBMin, FormatNumber(CDbl(FwdRVBMin.Value / 10), 1))
        SetText(txtRevRVBMax, FormatNumber(CDbl(RevRVBMax.Value / 10), 1))
        SetText(txtRevRVBMin, FormatNumber(CDbl(RevRVBMin.Value / 10), 1))

        With My.Settings
            sourcenum.Value = ._source
            destnum.Value = ._destination
            locvoltage.Value = ._md_localvoltage
            RVBVoltage.Value = ._md_FRVBvoltage
            Modbus_R_RVBVoltage_Value.Value = ._md_RRVBvoltage
            heartbeattimer.Value = ._heartbeat
            IPAddressToReadTextbox.Text = ._IPAddressToRead
            If ._Fdeltavoltage < MinDeltaVoltage Or ._Fdeltavoltage > MaxDeltaVoltage Then
                Forward_DeltaVoltage.Value = 0.0
            Else
                Forward_DeltaVoltage.Value = ._Fdeltavoltage
            End If
            F_RVBScaleFactor_Value.Value = ._Fmultiplier
            txtLocalVoltage.Text = .IEC61850LocalVoltage
            txtRVBVoltage.Text = .IEC61850FRVBVoltage
            '************************************************************
            '*  if visibility set to true by the user                   *
            '*  Verify the form is in visible area                      *
            '*  If NOT reset location to 0,0                            *
            '************************************************************
            If visibility Then
                Dim x As Integer = My.Computer.Screen.WorkingArea.Left
                Dim y As Integer = My.Computer.Screen.WorkingArea.Right
                If ._location.X < x Or ._location.X > y Then
                    Me.DesktopLocation = New System.Drawing.Point(20, 20)
                Else
                    Me.DesktopLocation = ._location
                End If
            Else
                Me.DesktopLocation = New System.Drawing.Point(32000, 32000)
            End If
            '************************************************************
            Select Case ._protocol
                Case "dnp"
                    dnpbutton.Checked = True
                    'modbusbox.Checked = False
                    txthost.Text = ._dnphost
                    txtport.Text = ._dnpport
                    checkHandler(dnpbutton)
                Case "modbus"
                    'dnpbutton.Checked = False
                    modbusbox.Checked = True
                    txthost.Text = ._md_host
                    txtport.Text = ._md_port
                    checkHandler(modbusbox)
                Case "iec"
                    iec61850box.Checked = True
                    txthost.Text = .iechost
                    txtport.Text = .iecport
                    checkHandler(iec61850box)
            End Select
        End With
    End Sub

    Private Sub Radio_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles radDelta.CheckedChanged, radLocal.CheckedChanged

        Select Case sender.name
            Case "radDelta"
                Forward_Voltage_Label.Text = DeltaMessage
                Reverse_Voltage_Label.Text = DeltaMessage
                Forward_DeltaVoltage.Minimum = MinDeltaVoltage
                Forward_DeltaVoltage.Maximum = MaxDeltaVoltage
                Reverse_DeltaVoltage.Minimum = MinDeltaVoltage
                Reverse_DeltaVoltage.Maximum = MaxDeltaVoltage
            Case "radLocal"
                Forward_Voltage_Label.Text = DirectMessage
                Reverse_Voltage_Label.Text = DirectMessage
                Forward_DeltaVoltage.Minimum = FwdRVBMin.Value / M2001D_Comm_Scale   'MinSpecValue
                Forward_DeltaVoltage.Maximum = FwdRVBMax.Value / M2001D_Comm_Scale   'MaxSpecValue
                Reverse_DeltaVoltage.Minimum = FwdRVBMin.Value / M2001D_Comm_Scale
                Reverse_DeltaVoltage.Maximum = FwdRVBMax.Value / M2001D_Comm_Scale
        End Select
    End Sub

    Private Sub RVBHscrollLimits(sender As System.Object, e As System.EventArgs) Handles FwdRVBMin.ValueChanged, FwdRVBMax.ValueChanged, RevRVBMax.ValueChanged, RevRVBMin.ValueChanged
        Select Case sender.name
            Case "FwdRVBMax"
                SetText(txtFwdRVBMax, FormatNumber(CDbl(sender.value / 10), 1))
            Case "FwdRVBMin"
                SetText(txtFwdRVBMin, FormatNumber(CDbl(sender.value / 10), 1))
            Case "RevRVBMax"
                SetText(txtRevRVBMax, FormatNumber(CDbl(sender.value / 10), 1))
            Case "RevRVBMin"
                SetText(txtRevRVBMin, FormatNumber(CDbl(sender.value / 10), 1))
        End Select
    End Sub

    Private Sub RVBTextLimits(sender As System.Object, e As System.EventArgs) Handles txtFwdRVBMax.TextChanged, txtFwdRVBMin.TextChanged, txtRevRVBMax.TextChanged, txtRevRVBMin.TextChanged
        Dim senderValue As Double = FormatNumber(CDbl(sender.text) * 10, 1)
        Console.WriteLine("Value is {0}", senderValue)
    End Sub
End Class
