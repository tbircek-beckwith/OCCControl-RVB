<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RVBSim
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RVBSim))
        Me.StartButton = New System.Windows.Forms.Button()
        Me.StopButton = New System.Windows.Forms.Button()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblLocalVoltageValue = New System.Windows.Forms.Label()
        Me.WriteIpAddr = New System.Windows.Forms.TextBox()
        Me.PortReg1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CommunicationDetails = New System.Windows.Forms.GroupBox()
        Me.DnpSettingsGroup = New System.Windows.Forms.GroupBox()
        Me.DnpReg3 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.DnpSourceVoltageReg3 = New System.Windows.Forms.NumericUpDown()
        Me.DnpLocalVoltageReg3 = New System.Windows.Forms.NumericUpDown()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.DnpRRVBValueReg3 = New System.Windows.Forms.NumericUpDown()
        Me.DnpFRVBValueReg3 = New System.Windows.Forms.NumericUpDown()
        Me.DnpReg2 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.DnpSourceVoltageReg2 = New System.Windows.Forms.NumericUpDown()
        Me.DnpLocalVoltageReg2 = New System.Windows.Forms.NumericUpDown()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DnpRRVBValueReg2 = New System.Windows.Forms.NumericUpDown()
        Me.DnpFRVBValueReg2 = New System.Windows.Forms.NumericUpDown()
        Me.DnpReg1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.DnpSourceVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.DnpLocalVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.DnpRRVBValueReg1 = New System.Windows.Forms.NumericUpDown()
        Me.DnpFRVBValueReg1 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusSettingsGroup = New System.Windows.Forms.GroupBox()
        Me.ModbusReg3 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ModbusSourceVoltageReg3 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusLocalVoltageReg3 = New System.Windows.Forms.NumericUpDown()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.ModbusRRVBValueReg3 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusFRVBValueReg3 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusReg2 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ModbusSourceVoltageReg2 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusLocalVoltageReg2 = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ModbusRRVBValueReg2 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusFRVBValueReg2 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusReg1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ModbusSourceVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusLocalVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ModbusRRVBValueReg1 = New System.Windows.Forms.NumericUpDown()
        Me.ModbusFRVBValueReg1 = New System.Windows.Forms.NumericUpDown()
        Me.RVBSettings = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.useDeltaVoltage = New System.Windows.Forms.RadioButton()
        Me.useFixedVoltage = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grpFwdSettings = New System.Windows.Forms.GroupBox()
        Me.FRVBScaleReg1 = New System.Windows.Forms.NumericUpDown()
        Me.F_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.FwdDeltaVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.Forward_Voltage_Label = New System.Windows.Forms.Label()
        Me.grpRevSettings = New System.Windows.Forms.GroupBox()
        Me.Reverse_Voltage_Label = New System.Windows.Forms.Label()
        Me.RevDeltaVoltageReg1 = New System.Windows.Forms.NumericUpDown()
        Me.R_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.RRVBScaleReg1 = New System.Windows.Forms.NumericUpDown()
        Me.RVBMax = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.heartbeattimer = New System.Windows.Forms.NumericUpDown()
        Me.RVBMin = New System.Windows.Forms.NumericUpDown()
        Me.IecSettingsGroup = New System.Windows.Forms.GroupBox()
        Me.IecReg3 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer8 = New System.Windows.Forms.SplitContainer()
        Me.IecSourceVoltageReg3 = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.IecLocalVoltageReg3 = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.IecRRVBValueReg3 = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.IecFRVBValueReg3 = New System.Windows.Forms.TextBox()
        Me.IecReg2 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer7 = New System.Windows.Forms.SplitContainer()
        Me.IecSourceVoltageReg2 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.IecLocalVoltageReg2 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.IecRRVBValueReg2 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.IecFRVBValueReg2 = New System.Windows.Forms.TextBox()
        Me.IecReg1 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer9 = New System.Windows.Forms.SplitContainer()
        Me.IecSourceVoltageReg1 = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.IecLocalVoltageReg1 = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.IecRRVBValueReg1 = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.IecFRVBValueReg1 = New System.Windows.Forms.TextBox()
        Me.lblwarning = New System.Windows.Forms.Label()
        Me.DNPDestinationReg1 = New System.Windows.Forms.NumericUpDown()
        Me.lbldestination = New System.Windows.Forms.Label()
        Me.DNPSourceReg1 = New System.Windows.Forms.NumericUpDown()
        Me.lblsource = New System.Windows.Forms.Label()
        Me.iec61850box = New System.Windows.Forms.RadioButton()
        Me.modbusbox = New System.Windows.Forms.RadioButton()
        Me.dnpbutton = New System.Windows.Forms.RadioButton()
        Me.ProtocolParameters = New System.Windows.Forms.GroupBox()
        Me.lblRevRVBValue = New System.Windows.Forms.Label()
        Me.lblMsgCenter = New System.Windows.Forms.Label()
        Me.lblFwdRVBValue = New System.Windows.Forms.Label()
        Me.ReadIpAddr = New System.Windows.Forms.TextBox()
        Me.SourceIPAddressLabel = New System.Windows.Forms.Label()
        Me.ProtocolBox = New System.Windows.Forms.GroupBox()
        Me.CommunicationDetails.SuspendLayout()
        Me.DnpSettingsGroup.SuspendLayout()
        Me.DnpReg3.SuspendLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.DnpSourceVoltageReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpLocalVoltageReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpRRVBValueReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpFRVBValueReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DnpReg2.SuspendLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        CType(Me.DnpSourceVoltageReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpLocalVoltageReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpRRVBValueReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpFRVBValueReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DnpReg1.SuspendLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer6.Panel1.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        CType(Me.DnpSourceVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpLocalVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpRRVBValueReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DnpFRVBValueReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ModbusSettingsGroup.SuspendLayout()
        Me.ModbusReg3.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.ModbusSourceVoltageReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusLocalVoltageReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusRRVBValueReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusFRVBValueReg3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ModbusReg2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.ModbusSourceVoltageReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusLocalVoltageReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusRRVBValueReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusFRVBValueReg2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ModbusReg1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.ModbusSourceVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusLocalVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusRRVBValueReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ModbusFRVBValueReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RVBSettings.SuspendLayout()
        Me.grpFwdSettings.SuspendLayout()
        CType(Me.FRVBScaleReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FwdDeltaVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRevSettings.SuspendLayout()
        CType(Me.RevDeltaVoltageReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RRVBScaleReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RVBMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RVBMin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.IecSettingsGroup.SuspendLayout()
        Me.IecReg3.SuspendLayout()
        CType(Me.SplitContainer8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer8.Panel1.SuspendLayout()
        Me.SplitContainer8.Panel2.SuspendLayout()
        Me.SplitContainer8.SuspendLayout()
        Me.IecReg2.SuspendLayout()
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer7.Panel1.SuspendLayout()
        Me.SplitContainer7.Panel2.SuspendLayout()
        Me.SplitContainer7.SuspendLayout()
        Me.IecReg1.SuspendLayout()
        CType(Me.SplitContainer9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer9.Panel1.SuspendLayout()
        Me.SplitContainer9.Panel2.SuspendLayout()
        Me.SplitContainer9.SuspendLayout()
        CType(Me.DNPDestinationReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DNPSourceReg1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProtocolParameters.SuspendLayout()
        Me.ProtocolBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(168, 19)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(75, 23)
        Me.StartButton.TabIndex = 3
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'StopButton
        '
        Me.StopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.StopButton.Enabled = False
        Me.StopButton.Location = New System.Drawing.Point(168, 46)
        Me.StopButton.Name = "StopButton"
        Me.StopButton.Size = New System.Drawing.Size(75, 23)
        Me.StopButton.TabIndex = 4
        Me.StopButton.Text = "Stop"
        Me.StopButton.UseVisualStyleBackColor = True
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripMenuItem1.Text = "IEC61850"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripMenuItem2.Text = "DNP3.0"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(121, 22)
        Me.ToolStripMenuItem3.Text = "Modbus"
        '
        'lblLocalVoltageValue
        '
        Me.lblLocalVoltageValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLocalVoltageValue.Location = New System.Drawing.Point(261, 16)
        Me.lblLocalVoltageValue.Name = "lblLocalVoltageValue"
        Me.lblLocalVoltageValue.Size = New System.Drawing.Size(282, 20)
        Me.lblLocalVoltageValue.TabIndex = 3
        Me.lblLocalVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WriteIpAddr
        '
        Me.WriteIpAddr.Location = New System.Drawing.Point(51, 47)
        Me.WriteIpAddr.Name = "WriteIpAddr"
        Me.WriteIpAddr.Size = New System.Drawing.Size(110, 20)
        Me.WriteIpAddr.TabIndex = 1
        '
        'PortReg1
        '
        Me.PortReg1.Location = New System.Drawing.Point(51, 70)
        Me.PortReg1.Name = "PortReg1"
        Me.PortReg1.Size = New System.Drawing.Size(110, 20)
        Me.PortReg1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Write:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Port:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CommunicationDetails
        '
        Me.CommunicationDetails.Controls.Add(Me.DnpSettingsGroup)
        Me.CommunicationDetails.Controls.Add(Me.ModbusSettingsGroup)
        Me.CommunicationDetails.Controls.Add(Me.RVBSettings)
        Me.CommunicationDetails.Controls.Add(Me.IecSettingsGroup)
        Me.CommunicationDetails.Location = New System.Drawing.Point(5, 116)
        Me.CommunicationDetails.Name = "CommunicationDetails"
        Me.CommunicationDetails.Size = New System.Drawing.Size(987, 493)
        Me.CommunicationDetails.TabIndex = 2
        Me.CommunicationDetails.TabStop = False
        '
        'DnpSettingsGroup
        '
        Me.DnpSettingsGroup.Controls.Add(Me.DnpReg3)
        Me.DnpSettingsGroup.Controls.Add(Me.DnpReg2)
        Me.DnpSettingsGroup.Controls.Add(Me.DnpReg1)
        Me.DnpSettingsGroup.Location = New System.Drawing.Point(6, 190)
        Me.DnpSettingsGroup.Name = "DnpSettingsGroup"
        Me.DnpSettingsGroup.Size = New System.Drawing.Size(652, 171)
        Me.DnpSettingsGroup.TabIndex = 37
        Me.DnpSettingsGroup.TabStop = False
        Me.DnpSettingsGroup.Text = "DNP Points"
        '
        'DnpReg3
        '
        Me.DnpReg3.Controls.Add(Me.SplitContainer4)
        Me.DnpReg3.Location = New System.Drawing.Point(455, 19)
        Me.DnpReg3.Name = "DnpReg3"
        Me.DnpReg3.Size = New System.Drawing.Size(190, 144)
        Me.DnpReg3.TabIndex = 36
        Me.DnpReg3.TabStop = False
        Me.DnpReg3.Text = "Regulator 3"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label18)
        Me.SplitContainer4.Panel1.Controls.Add(Me.Label19)
        Me.SplitContainer4.Panel1.Controls.Add(Me.DnpSourceVoltageReg3)
        Me.SplitContainer4.Panel1.Controls.Add(Me.DnpLocalVoltageReg3)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.Label20)
        Me.SplitContainer4.Panel2.Controls.Add(Me.Label21)
        Me.SplitContainer4.Panel2.Controls.Add(Me.DnpRRVBValueReg3)
        Me.SplitContainer4.Panel2.Controls.Add(Me.DnpFRVBValueReg3)
        Me.SplitContainer4.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer4.SplitterDistance = 61
        Me.SplitContainer4.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(3, 11)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(83, 13)
        Me.Label18.TabIndex = 23
        Me.Label18.Text = "Source Voltage:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 35)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(75, 13)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "Local Voltage:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpSourceVoltageReg3
        '
        Me.DnpSourceVoltageReg3.Location = New System.Drawing.Point(114, 7)
        Me.DnpSourceVoltageReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpSourceVoltageReg3.Name = "DnpSourceVoltageReg3"
        Me.DnpSourceVoltageReg3.Size = New System.Drawing.Size(61, 20)
        Me.DnpSourceVoltageReg3.TabIndex = 22
        Me.DnpSourceVoltageReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpSourceVoltageReg3.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'DnpLocalVoltageReg3
        '
        Me.DnpLocalVoltageReg3.Location = New System.Drawing.Point(114, 31)
        Me.DnpLocalVoltageReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpLocalVoltageReg3.Name = "DnpLocalVoltageReg3"
        Me.DnpLocalVoltageReg3.Size = New System.Drawing.Size(61, 20)
        Me.DnpLocalVoltageReg3.TabIndex = 2
        Me.DnpLocalVoltageReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpLocalVoltageReg3.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(3, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(105, 13)
        Me.Label20.TabIndex = 25
        Me.Label20.Text = "Source RVB Voltage"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(3, 32)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(91, 13)
        Me.Label21.TabIndex = 24
        Me.Label21.Text = "Fwd RVB Voltage"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpRRVBValueReg3
        '
        Me.DnpRRVBValueReg3.Location = New System.Drawing.Point(114, 6)
        Me.DnpRRVBValueReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpRRVBValueReg3.Name = "DnpRRVBValueReg3"
        Me.DnpRRVBValueReg3.Size = New System.Drawing.Size(61, 20)
        Me.DnpRRVBValueReg3.TabIndex = 14
        Me.DnpRRVBValueReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpRRVBValueReg3.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'DnpFRVBValueReg3
        '
        Me.DnpFRVBValueReg3.Location = New System.Drawing.Point(114, 28)
        Me.DnpFRVBValueReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpFRVBValueReg3.Name = "DnpFRVBValueReg3"
        Me.DnpFRVBValueReg3.Size = New System.Drawing.Size(61, 20)
        Me.DnpFRVBValueReg3.TabIndex = 3
        Me.DnpFRVBValueReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpFRVBValueReg3.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'DnpReg2
        '
        Me.DnpReg2.Controls.Add(Me.SplitContainer5)
        Me.DnpReg2.Location = New System.Drawing.Point(265, 19)
        Me.DnpReg2.Name = "DnpReg2"
        Me.DnpReg2.Size = New System.Drawing.Size(190, 144)
        Me.DnpReg2.TabIndex = 36
        Me.DnpReg2.TabStop = False
        Me.DnpReg2.Text = "Regulator 2"
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label22)
        Me.SplitContainer5.Panel1.Controls.Add(Me.Label23)
        Me.SplitContainer5.Panel1.Controls.Add(Me.DnpSourceVoltageReg2)
        Me.SplitContainer5.Panel1.Controls.Add(Me.DnpLocalVoltageReg2)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.Label24)
        Me.SplitContainer5.Panel2.Controls.Add(Me.Label25)
        Me.SplitContainer5.Panel2.Controls.Add(Me.DnpRRVBValueReg2)
        Me.SplitContainer5.Panel2.Controls.Add(Me.DnpFRVBValueReg2)
        Me.SplitContainer5.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer5.SplitterDistance = 61
        Me.SplitContainer5.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(3, 11)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 23
        Me.Label22.Text = "Source Voltage:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(3, 35)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(75, 13)
        Me.Label23.TabIndex = 21
        Me.Label23.Text = "Local Voltage:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpSourceVoltageReg2
        '
        Me.DnpSourceVoltageReg2.Location = New System.Drawing.Point(114, 7)
        Me.DnpSourceVoltageReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpSourceVoltageReg2.Name = "DnpSourceVoltageReg2"
        Me.DnpSourceVoltageReg2.Size = New System.Drawing.Size(61, 20)
        Me.DnpSourceVoltageReg2.TabIndex = 22
        Me.DnpSourceVoltageReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpSourceVoltageReg2.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'DnpLocalVoltageReg2
        '
        Me.DnpLocalVoltageReg2.Location = New System.Drawing.Point(114, 31)
        Me.DnpLocalVoltageReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpLocalVoltageReg2.Name = "DnpLocalVoltageReg2"
        Me.DnpLocalVoltageReg2.Size = New System.Drawing.Size(61, 20)
        Me.DnpLocalVoltageReg2.TabIndex = 2
        Me.DnpLocalVoltageReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpLocalVoltageReg2.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(3, 10)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(105, 13)
        Me.Label24.TabIndex = 25
        Me.Label24.Text = "Source RVB Voltage"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(3, 32)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(91, 13)
        Me.Label25.TabIndex = 24
        Me.Label25.Text = "Fwd RVB Voltage"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpRRVBValueReg2
        '
        Me.DnpRRVBValueReg2.Location = New System.Drawing.Point(114, 6)
        Me.DnpRRVBValueReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpRRVBValueReg2.Name = "DnpRRVBValueReg2"
        Me.DnpRRVBValueReg2.Size = New System.Drawing.Size(61, 20)
        Me.DnpRRVBValueReg2.TabIndex = 14
        Me.DnpRRVBValueReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpRRVBValueReg2.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'DnpFRVBValueReg2
        '
        Me.DnpFRVBValueReg2.Location = New System.Drawing.Point(114, 28)
        Me.DnpFRVBValueReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpFRVBValueReg2.Name = "DnpFRVBValueReg2"
        Me.DnpFRVBValueReg2.Size = New System.Drawing.Size(61, 20)
        Me.DnpFRVBValueReg2.TabIndex = 3
        Me.DnpFRVBValueReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpFRVBValueReg2.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'DnpReg1
        '
        Me.DnpReg1.Controls.Add(Me.SplitContainer6)
        Me.DnpReg1.Location = New System.Drawing.Point(75, 19)
        Me.DnpReg1.Name = "DnpReg1"
        Me.DnpReg1.Size = New System.Drawing.Size(190, 144)
        Me.DnpReg1.TabIndex = 35
        Me.DnpReg1.TabStop = False
        Me.DnpReg1.Text = "Regulator 1"
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer6.Name = "SplitContainer6"
        Me.SplitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer6.Panel1
        '
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label26)
        Me.SplitContainer6.Panel1.Controls.Add(Me.Label27)
        Me.SplitContainer6.Panel1.Controls.Add(Me.DnpSourceVoltageReg1)
        Me.SplitContainer6.Panel1.Controls.Add(Me.DnpLocalVoltageReg1)
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label28)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Label29)
        Me.SplitContainer6.Panel2.Controls.Add(Me.DnpRRVBValueReg1)
        Me.SplitContainer6.Panel2.Controls.Add(Me.DnpFRVBValueReg1)
        Me.SplitContainer6.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer6.SplitterDistance = 61
        Me.SplitContainer6.TabIndex = 0
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(3, 11)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(83, 13)
        Me.Label26.TabIndex = 23
        Me.Label26.Text = "Source Voltage:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(3, 35)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(75, 13)
        Me.Label27.TabIndex = 21
        Me.Label27.Text = "Local Voltage:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpSourceVoltageReg1
        '
        Me.DnpSourceVoltageReg1.Location = New System.Drawing.Point(114, 7)
        Me.DnpSourceVoltageReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpSourceVoltageReg1.Name = "DnpSourceVoltageReg1"
        Me.DnpSourceVoltageReg1.Size = New System.Drawing.Size(61, 20)
        Me.DnpSourceVoltageReg1.TabIndex = 22
        Me.DnpSourceVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpSourceVoltageReg1.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'DnpLocalVoltageReg1
        '
        Me.DnpLocalVoltageReg1.Location = New System.Drawing.Point(114, 31)
        Me.DnpLocalVoltageReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpLocalVoltageReg1.Name = "DnpLocalVoltageReg1"
        Me.DnpLocalVoltageReg1.Size = New System.Drawing.Size(61, 20)
        Me.DnpLocalVoltageReg1.TabIndex = 2
        Me.DnpLocalVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpLocalVoltageReg1.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(3, 10)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(105, 13)
        Me.Label28.TabIndex = 25
        Me.Label28.Text = "Source RVB Voltage"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(3, 32)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(91, 13)
        Me.Label29.TabIndex = 24
        Me.Label29.Text = "Fwd RVB Voltage"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DnpRRVBValueReg1
        '
        Me.DnpRRVBValueReg1.Location = New System.Drawing.Point(114, 6)
        Me.DnpRRVBValueReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpRRVBValueReg1.Name = "DnpRRVBValueReg1"
        Me.DnpRRVBValueReg1.Size = New System.Drawing.Size(61, 20)
        Me.DnpRRVBValueReg1.TabIndex = 14
        Me.DnpRRVBValueReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpRRVBValueReg1.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'DnpFRVBValueReg1
        '
        Me.DnpFRVBValueReg1.Location = New System.Drawing.Point(114, 28)
        Me.DnpFRVBValueReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DnpFRVBValueReg1.Name = "DnpFRVBValueReg1"
        Me.DnpFRVBValueReg1.Size = New System.Drawing.Size(61, 20)
        Me.DnpFRVBValueReg1.TabIndex = 3
        Me.DnpFRVBValueReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DnpFRVBValueReg1.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'ModbusSettingsGroup
        '
        Me.ModbusSettingsGroup.Controls.Add(Me.ModbusReg3)
        Me.ModbusSettingsGroup.Controls.Add(Me.ModbusReg2)
        Me.ModbusSettingsGroup.Controls.Add(Me.ModbusReg1)
        Me.ModbusSettingsGroup.Location = New System.Drawing.Point(6, 19)
        Me.ModbusSettingsGroup.Name = "ModbusSettingsGroup"
        Me.ModbusSettingsGroup.Size = New System.Drawing.Size(652, 171)
        Me.ModbusSettingsGroup.TabIndex = 33
        Me.ModbusSettingsGroup.TabStop = False
        Me.ModbusSettingsGroup.Text = "Modbus Registers"
        '
        'ModbusReg3
        '
        Me.ModbusReg3.Controls.Add(Me.SplitContainer3)
        Me.ModbusReg3.Location = New System.Drawing.Point(455, 19)
        Me.ModbusReg3.Name = "ModbusReg3"
        Me.ModbusReg3.Size = New System.Drawing.Size(190, 144)
        Me.ModbusReg3.TabIndex = 36
        Me.ModbusReg3.TabStop = False
        Me.ModbusReg3.Text = "Regulator 3"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label14)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label15)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ModbusSourceVoltageReg3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ModbusLocalVoltageReg3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label16)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Label17)
        Me.SplitContainer3.Panel2.Controls.Add(Me.ModbusRRVBValueReg3)
        Me.SplitContainer3.Panel2.Controls.Add(Me.ModbusFRVBValueReg3)
        Me.SplitContainer3.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer3.SplitterDistance = 61
        Me.SplitContainer3.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 13)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Source Voltage:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 35)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "Local Voltage:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusSourceVoltageReg3
        '
        Me.ModbusSourceVoltageReg3.Location = New System.Drawing.Point(114, 7)
        Me.ModbusSourceVoltageReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusSourceVoltageReg3.Name = "ModbusSourceVoltageReg3"
        Me.ModbusSourceVoltageReg3.Size = New System.Drawing.Size(61, 20)
        Me.ModbusSourceVoltageReg3.TabIndex = 22
        Me.ModbusSourceVoltageReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusSourceVoltageReg3.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'ModbusLocalVoltageReg3
        '
        Me.ModbusLocalVoltageReg3.Location = New System.Drawing.Point(114, 31)
        Me.ModbusLocalVoltageReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusLocalVoltageReg3.Name = "ModbusLocalVoltageReg3"
        Me.ModbusLocalVoltageReg3.Size = New System.Drawing.Size(61, 20)
        Me.ModbusLocalVoltageReg3.TabIndex = 2
        Me.ModbusLocalVoltageReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusLocalVoltageReg3.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(105, 13)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Source RVB Voltage"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 32)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(91, 13)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "Fwd RVB Voltage"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusRRVBValueReg3
        '
        Me.ModbusRRVBValueReg3.Location = New System.Drawing.Point(114, 6)
        Me.ModbusRRVBValueReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusRRVBValueReg3.Name = "ModbusRRVBValueReg3"
        Me.ModbusRRVBValueReg3.Size = New System.Drawing.Size(61, 20)
        Me.ModbusRRVBValueReg3.TabIndex = 14
        Me.ModbusRRVBValueReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusRRVBValueReg3.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'ModbusFRVBValueReg3
        '
        Me.ModbusFRVBValueReg3.Location = New System.Drawing.Point(114, 28)
        Me.ModbusFRVBValueReg3.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusFRVBValueReg3.Name = "ModbusFRVBValueReg3"
        Me.ModbusFRVBValueReg3.Size = New System.Drawing.Size(61, 20)
        Me.ModbusFRVBValueReg3.TabIndex = 3
        Me.ModbusFRVBValueReg3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusFRVBValueReg3.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'ModbusReg2
        '
        Me.ModbusReg2.Controls.Add(Me.SplitContainer2)
        Me.ModbusReg2.Location = New System.Drawing.Point(265, 19)
        Me.ModbusReg2.Name = "ModbusReg2"
        Me.ModbusReg2.Size = New System.Drawing.Size(190, 144)
        Me.ModbusReg2.TabIndex = 36
        Me.ModbusReg2.TabStop = False
        Me.ModbusReg2.Text = "Regulator 2"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ModbusSourceVoltageReg2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ModbusLocalVoltageReg2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label12)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label13)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ModbusRRVBValueReg2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.ModbusFRVBValueReg2)
        Me.SplitContainer2.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer2.SplitterDistance = 61
        Me.SplitContainer2.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Source Voltage:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Local Voltage:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusSourceVoltageReg2
        '
        Me.ModbusSourceVoltageReg2.Location = New System.Drawing.Point(114, 7)
        Me.ModbusSourceVoltageReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusSourceVoltageReg2.Name = "ModbusSourceVoltageReg2"
        Me.ModbusSourceVoltageReg2.Size = New System.Drawing.Size(61, 20)
        Me.ModbusSourceVoltageReg2.TabIndex = 22
        Me.ModbusSourceVoltageReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusSourceVoltageReg2.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'ModbusLocalVoltageReg2
        '
        Me.ModbusLocalVoltageReg2.Location = New System.Drawing.Point(114, 31)
        Me.ModbusLocalVoltageReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusLocalVoltageReg2.Name = "ModbusLocalVoltageReg2"
        Me.ModbusLocalVoltageReg2.Size = New System.Drawing.Size(61, 20)
        Me.ModbusLocalVoltageReg2.TabIndex = 2
        Me.ModbusLocalVoltageReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusLocalVoltageReg2.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(3, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(105, 13)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Source RVB Voltage"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 32)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Fwd RVB Voltage"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusRRVBValueReg2
        '
        Me.ModbusRRVBValueReg2.Location = New System.Drawing.Point(114, 6)
        Me.ModbusRRVBValueReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusRRVBValueReg2.Name = "ModbusRRVBValueReg2"
        Me.ModbusRRVBValueReg2.Size = New System.Drawing.Size(61, 20)
        Me.ModbusRRVBValueReg2.TabIndex = 14
        Me.ModbusRRVBValueReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusRRVBValueReg2.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'ModbusFRVBValueReg2
        '
        Me.ModbusFRVBValueReg2.Location = New System.Drawing.Point(114, 28)
        Me.ModbusFRVBValueReg2.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusFRVBValueReg2.Name = "ModbusFRVBValueReg2"
        Me.ModbusFRVBValueReg2.Size = New System.Drawing.Size(61, 20)
        Me.ModbusFRVBValueReg2.TabIndex = 3
        Me.ModbusFRVBValueReg2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusFRVBValueReg2.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'ModbusReg1
        '
        Me.ModbusReg1.Controls.Add(Me.SplitContainer1)
        Me.ModbusReg1.Location = New System.Drawing.Point(75, 19)
        Me.ModbusReg1.Name = "ModbusReg1"
        Me.ModbusReg1.Size = New System.Drawing.Size(190, 144)
        Me.ModbusReg1.TabIndex = 35
        Me.ModbusReg1.TabStop = False
        Me.ModbusReg1.Text = "Regulator 1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ModbusSourceVoltageReg1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ModbusLocalVoltageReg1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ModbusRRVBValueReg1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ModbusFRVBValueReg1)
        Me.SplitContainer1.Size = New System.Drawing.Size(184, 125)
        Me.SplitContainer1.SplitterDistance = 61
        Me.SplitContainer1.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Source Voltage:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Local Voltage:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusSourceVoltageReg1
        '
        Me.ModbusSourceVoltageReg1.Location = New System.Drawing.Point(114, 7)
        Me.ModbusSourceVoltageReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusSourceVoltageReg1.Name = "ModbusSourceVoltageReg1"
        Me.ModbusSourceVoltageReg1.Size = New System.Drawing.Size(61, 20)
        Me.ModbusSourceVoltageReg1.TabIndex = 22
        Me.ModbusSourceVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusSourceVoltageReg1.Value = New Decimal(New Integer() {1700, 0, 0, 0})
        '
        'ModbusLocalVoltageReg1
        '
        Me.ModbusLocalVoltageReg1.Location = New System.Drawing.Point(114, 31)
        Me.ModbusLocalVoltageReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusLocalVoltageReg1.Name = "ModbusLocalVoltageReg1"
        Me.ModbusLocalVoltageReg1.Size = New System.Drawing.Size(61, 20)
        Me.ModbusLocalVoltageReg1.TabIndex = 2
        Me.ModbusLocalVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusLocalVoltageReg1.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Source RVB Voltage"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Fwd RVB Voltage"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ModbusRRVBValueReg1
        '
        Me.ModbusRRVBValueReg1.Location = New System.Drawing.Point(114, 6)
        Me.ModbusRRVBValueReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusRRVBValueReg1.Name = "ModbusRRVBValueReg1"
        Me.ModbusRRVBValueReg1.Size = New System.Drawing.Size(61, 20)
        Me.ModbusRRVBValueReg1.TabIndex = 14
        Me.ModbusRRVBValueReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusRRVBValueReg1.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        '
        'ModbusFRVBValueReg1
        '
        Me.ModbusFRVBValueReg1.Location = New System.Drawing.Point(114, 28)
        Me.ModbusFRVBValueReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.ModbusFRVBValueReg1.Name = "ModbusFRVBValueReg1"
        Me.ModbusFRVBValueReg1.Size = New System.Drawing.Size(61, 20)
        Me.ModbusFRVBValueReg1.TabIndex = 3
        Me.ModbusFRVBValueReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ModbusFRVBValueReg1.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        '
        'RVBSettings
        '
        Me.RVBSettings.Controls.Add(Me.Label7)
        Me.RVBSettings.Controls.Add(Me.useDeltaVoltage)
        Me.RVBSettings.Controls.Add(Me.useFixedVoltage)
        Me.RVBSettings.Controls.Add(Me.Label5)
        Me.RVBSettings.Controls.Add(Me.grpFwdSettings)
        Me.RVBSettings.Controls.Add(Me.grpRevSettings)
        Me.RVBSettings.Controls.Add(Me.RVBMax)
        Me.RVBSettings.Controls.Add(Me.Label4)
        Me.RVBSettings.Controls.Add(Me.heartbeattimer)
        Me.RVBSettings.Controls.Add(Me.RVBMin)
        Me.RVBSettings.Location = New System.Drawing.Point(7, 361)
        Me.RVBSettings.Name = "RVBSettings"
        Me.RVBSettings.Size = New System.Drawing.Size(651, 132)
        Me.RVBSettings.TabIndex = 34
        Me.RVBSettings.TabStop = False
        Me.RVBSettings.Text = "RVB Settings"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(540, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "RVB Min"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'useDeltaVoltage
        '
        Me.useDeltaVoltage.AutoSize = True
        Me.useDeltaVoltage.Checked = True
        Me.useDeltaVoltage.Location = New System.Drawing.Point(362, 28)
        Me.useDeltaVoltage.Name = "useDeltaVoltage"
        Me.useDeltaVoltage.Size = New System.Drawing.Size(86, 17)
        Me.useDeltaVoltage.TabIndex = 8
        Me.useDeltaVoltage.TabStop = True
        Me.useDeltaVoltage.Text = "Use Relative"
        Me.useDeltaVoltage.UseVisualStyleBackColor = True
        '
        'useFixedVoltage
        '
        Me.useFixedVoltage.AutoSize = True
        Me.useFixedVoltage.Location = New System.Drawing.Point(362, 54)
        Me.useFixedVoltage.Name = "useFixedVoltage"
        Me.useFixedVoltage.Size = New System.Drawing.Size(88, 17)
        Me.useFixedVoltage.TabIndex = 9
        Me.useFixedVoltage.Text = "Use Absolute"
        Me.useFixedVoltage.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(536, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "RVB Max"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpFwdSettings
        '
        Me.grpFwdSettings.Controls.Add(Me.FRVBScaleReg1)
        Me.grpFwdSettings.Controls.Add(Me.F_RVBScaleFactor_Label)
        Me.grpFwdSettings.Controls.Add(Me.FwdDeltaVoltageReg1)
        Me.grpFwdSettings.Controls.Add(Me.Forward_Voltage_Label)
        Me.grpFwdSettings.Location = New System.Drawing.Point(4, 24)
        Me.grpFwdSettings.Name = "grpFwdSettings"
        Me.grpFwdSettings.Size = New System.Drawing.Size(168, 101)
        Me.grpFwdSettings.TabIndex = 26
        Me.grpFwdSettings.TabStop = False
        Me.grpFwdSettings.Text = "Fwd RVB Settings"
        '
        'FRVBScaleReg1
        '
        Me.FRVBScaleReg1.DecimalPlaces = 1
        Me.FRVBScaleReg1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FRVBScaleReg1.Location = New System.Drawing.Point(102, 25)
        Me.FRVBScaleReg1.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FRVBScaleReg1.Name = "FRVBScaleReg1"
        Me.FRVBScaleReg1.Size = New System.Drawing.Size(52, 20)
        Me.FRVBScaleReg1.TabIndex = 6
        Me.FRVBScaleReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FRVBScaleReg1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'F_RVBScaleFactor_Label
        '
        Me.F_RVBScaleFactor_Label.Location = New System.Drawing.Point(6, 20)
        Me.F_RVBScaleFactor_Label.Name = "F_RVBScaleFactor_Label"
        Me.F_RVBScaleFactor_Label.Size = New System.Drawing.Size(95, 30)
        Me.F_RVBScaleFactor_Label.TabIndex = 13
        Me.F_RVBScaleFactor_Label.Text = "Forward RVB Scale Factor"
        Me.F_RVBScaleFactor_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FwdDeltaVoltageReg1
        '
        Me.FwdDeltaVoltageReg1.DecimalPlaces = 1
        Me.FwdDeltaVoltageReg1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FwdDeltaVoltageReg1.Location = New System.Drawing.Point(102, 57)
        Me.FwdDeltaVoltageReg1.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.FwdDeltaVoltageReg1.Name = "FwdDeltaVoltageReg1"
        Me.FwdDeltaVoltageReg1.Size = New System.Drawing.Size(52, 20)
        Me.FwdDeltaVoltageReg1.TabIndex = 7
        Me.FwdDeltaVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Forward_Voltage_Label
        '
        Me.Forward_Voltage_Label.AutoSize = True
        Me.Forward_Voltage_Label.Location = New System.Drawing.Point(11, 61)
        Me.Forward_Voltage_Label.Name = "Forward_Voltage_Label"
        Me.Forward_Voltage_Label.Size = New System.Drawing.Size(84, 13)
        Me.Forward_Voltage_Label.TabIndex = 17
        Me.Forward_Voltage_Label.Text = "Local Voltage + "
        Me.Forward_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpRevSettings
        '
        Me.grpRevSettings.Controls.Add(Me.Reverse_Voltage_Label)
        Me.grpRevSettings.Controls.Add(Me.RevDeltaVoltageReg1)
        Me.grpRevSettings.Controls.Add(Me.R_RVBScaleFactor_Label)
        Me.grpRevSettings.Controls.Add(Me.RRVBScaleReg1)
        Me.grpRevSettings.Location = New System.Drawing.Point(172, 24)
        Me.grpRevSettings.Name = "grpRevSettings"
        Me.grpRevSettings.Size = New System.Drawing.Size(168, 101)
        Me.grpRevSettings.TabIndex = 34
        Me.grpRevSettings.TabStop = False
        Me.grpRevSettings.Text = "Rev RVB Settings"
        '
        'Reverse_Voltage_Label
        '
        Me.Reverse_Voltage_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Reverse_Voltage_Label.Location = New System.Drawing.Point(10, 52)
        Me.Reverse_Voltage_Label.Name = "Reverse_Voltage_Label"
        Me.Reverse_Voltage_Label.Size = New System.Drawing.Size(92, 30)
        Me.Reverse_Voltage_Label.TabIndex = 30
        Me.Reverse_Voltage_Label.Text = "Src RVB Voltage is ="
        Me.Reverse_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RevDeltaVoltageReg1
        '
        Me.RevDeltaVoltageReg1.DecimalPlaces = 1
        Me.RevDeltaVoltageReg1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RevDeltaVoltageReg1.Location = New System.Drawing.Point(102, 57)
        Me.RevDeltaVoltageReg1.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.RevDeltaVoltageReg1.Name = "RevDeltaVoltageReg1"
        Me.RevDeltaVoltageReg1.Size = New System.Drawing.Size(52, 20)
        Me.RevDeltaVoltageReg1.TabIndex = 11
        Me.RevDeltaVoltageReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'R_RVBScaleFactor_Label
        '
        Me.R_RVBScaleFactor_Label.Location = New System.Drawing.Point(7, 20)
        Me.R_RVBScaleFactor_Label.Name = "R_RVBScaleFactor_Label"
        Me.R_RVBScaleFactor_Label.Size = New System.Drawing.Size(95, 30)
        Me.R_RVBScaleFactor_Label.TabIndex = 28
        Me.R_RVBScaleFactor_Label.Text = "Reverse RVB Scale Factor"
        Me.R_RVBScaleFactor_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RRVBScaleReg1
        '
        Me.RRVBScaleReg1.DecimalPlaces = 1
        Me.RRVBScaleReg1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RRVBScaleReg1.Location = New System.Drawing.Point(102, 25)
        Me.RRVBScaleReg1.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RRVBScaleReg1.Name = "RRVBScaleReg1"
        Me.RRVBScaleReg1.Size = New System.Drawing.Size(52, 20)
        Me.RRVBScaleReg1.TabIndex = 10
        Me.RRVBScaleReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RRVBScaleReg1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'RVBMax
        '
        Me.RVBMax.DecimalPlaces = 1
        Me.RVBMax.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RVBMax.Location = New System.Drawing.Point(589, 44)
        Me.RVBMax.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.RVBMax.Minimum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.RVBMax.Name = "RVBMax"
        Me.RVBMax.Size = New System.Drawing.Size(52, 20)
        Me.RVBMax.TabIndex = 31
        Me.RVBMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RVBMax.Value = New Decimal(New Integer() {150, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(451, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Heartbeat (sec)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'heartbeattimer
        '
        Me.heartbeattimer.Location = New System.Drawing.Point(456, 98)
        Me.heartbeattimer.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.heartbeattimer.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.heartbeattimer.Name = "heartbeattimer"
        Me.heartbeattimer.Size = New System.Drawing.Size(52, 20)
        Me.heartbeattimer.TabIndex = 5
        Me.heartbeattimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.heartbeattimer.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'RVBMin
        '
        Me.RVBMin.DecimalPlaces = 1
        Me.RVBMin.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RVBMin.Location = New System.Drawing.Point(589, 21)
        Me.RVBMin.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.RVBMin.Minimum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.RVBMin.Name = "RVBMin"
        Me.RVBMin.Size = New System.Drawing.Size(52, 20)
        Me.RVBMin.TabIndex = 32
        Me.RVBMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RVBMin.Value = New Decimal(New Integer() {90, 0, 0, 0})
        '
        'IecSettingsGroup
        '
        Me.IecSettingsGroup.Controls.Add(Me.IecReg3)
        Me.IecSettingsGroup.Controls.Add(Me.IecReg2)
        Me.IecSettingsGroup.Controls.Add(Me.IecReg1)
        Me.IecSettingsGroup.Location = New System.Drawing.Point(664, 19)
        Me.IecSettingsGroup.Name = "IecSettingsGroup"
        Me.IecSettingsGroup.Size = New System.Drawing.Size(313, 467)
        Me.IecSettingsGroup.TabIndex = 38
        Me.IecSettingsGroup.TabStop = False
        Me.IecSettingsGroup.Text = "IEC 61850 Data Points"
        '
        'IecReg3
        '
        Me.IecReg3.Controls.Add(Me.SplitContainer8)
        Me.IecReg3.Location = New System.Drawing.Point(6, 319)
        Me.IecReg3.Name = "IecReg3"
        Me.IecReg3.Size = New System.Drawing.Size(298, 144)
        Me.IecReg3.TabIndex = 37
        Me.IecReg3.TabStop = False
        Me.IecReg3.Text = "Regulator 3"
        '
        'SplitContainer8
        '
        Me.SplitContainer8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer8.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer8.Name = "SplitContainer8"
        Me.SplitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer8.Panel1
        '
        Me.SplitContainer8.Panel1.Controls.Add(Me.IecSourceVoltageReg3)
        Me.SplitContainer8.Panel1.Controls.Add(Me.Label34)
        Me.SplitContainer8.Panel1.Controls.Add(Me.Label35)
        Me.SplitContainer8.Panel1.Controls.Add(Me.IecLocalVoltageReg3)
        '
        'SplitContainer8.Panel2
        '
        Me.SplitContainer8.Panel2.Controls.Add(Me.Label36)
        Me.SplitContainer8.Panel2.Controls.Add(Me.IecRRVBValueReg3)
        Me.SplitContainer8.Panel2.Controls.Add(Me.Label37)
        Me.SplitContainer8.Panel2.Controls.Add(Me.IecFRVBValueReg3)
        Me.SplitContainer8.Size = New System.Drawing.Size(292, 125)
        Me.SplitContainer8.SplitterDistance = 61
        Me.SplitContainer8.TabIndex = 0
        '
        'IecSourceVoltageReg3
        '
        Me.IecSourceVoltageReg3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecSourceVoltageReg3.Location = New System.Drawing.Point(108, 7)
        Me.IecSourceVoltageReg3.Name = "IecSourceVoltageReg3"
        Me.IecSourceVoltageReg3.Size = New System.Drawing.Size(180, 20)
        Me.IecSourceVoltageReg3.TabIndex = 24
        Me.IecSourceVoltageReg3.Text = "ATCC0$MX$SrcCtrV$mag$i"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(3, 11)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(83, 13)
        Me.Label34.TabIndex = 23
        Me.Label34.Text = "Source Voltage:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(3, 35)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(75, 13)
        Me.Label35.TabIndex = 21
        Me.Label35.Text = "Local Voltage:"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecLocalVoltageReg3
        '
        Me.IecLocalVoltageReg3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecLocalVoltageReg3.Location = New System.Drawing.Point(108, 31)
        Me.IecLocalVoltageReg3.Name = "IecLocalVoltageReg3"
        Me.IecLocalVoltageReg3.Size = New System.Drawing.Size(180, 20)
        Me.IecLocalVoltageReg3.TabIndex = 4
        Me.IecLocalVoltageReg3.Text = "ATCC0$MX$LodCtrV$mag$i"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(3, 10)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(105, 13)
        Me.Label36.TabIndex = 25
        Me.Label36.Text = "Source RVB Voltage"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecRRVBValueReg3
        '
        Me.IecRRVBValueReg3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecRRVBValueReg3.Location = New System.Drawing.Point(108, 6)
        Me.IecRRVBValueReg3.Name = "IecRRVBValueReg3"
        Me.IecRRVBValueReg3.Size = New System.Drawing.Size(180, 20)
        Me.IecRRVBValueReg3.TabIndex = 15
        Me.IecRRVBValueReg3.Text = "ATCC0$SP$RRemVVal$setMag$i"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(3, 32)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(91, 13)
        Me.Label37.TabIndex = 24
        Me.Label37.Text = "Fwd RVB Voltage"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecFRVBValueReg3
        '
        Me.IecFRVBValueReg3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecFRVBValueReg3.Location = New System.Drawing.Point(108, 28)
        Me.IecFRVBValueReg3.Name = "IecFRVBValueReg3"
        Me.IecFRVBValueReg3.Size = New System.Drawing.Size(180, 20)
        Me.IecFRVBValueReg3.TabIndex = 5
        Me.IecFRVBValueReg3.Text = "ATCC0$SP$FRemVVal$setMag$i"
        '
        'IecReg2
        '
        Me.IecReg2.Controls.Add(Me.SplitContainer7)
        Me.IecReg2.Location = New System.Drawing.Point(6, 169)
        Me.IecReg2.Name = "IecReg2"
        Me.IecReg2.Size = New System.Drawing.Size(298, 144)
        Me.IecReg2.TabIndex = 36
        Me.IecReg2.TabStop = False
        Me.IecReg2.Text = "Regulator 2"
        '
        'SplitContainer7
        '
        Me.SplitContainer7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer7.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer7.Name = "SplitContainer7"
        Me.SplitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer7.Panel1
        '
        Me.SplitContainer7.Panel1.Controls.Add(Me.IecSourceVoltageReg2)
        Me.SplitContainer7.Panel1.Controls.Add(Me.Label30)
        Me.SplitContainer7.Panel1.Controls.Add(Me.Label31)
        Me.SplitContainer7.Panel1.Controls.Add(Me.IecLocalVoltageReg2)
        '
        'SplitContainer7.Panel2
        '
        Me.SplitContainer7.Panel2.Controls.Add(Me.Label32)
        Me.SplitContainer7.Panel2.Controls.Add(Me.IecRRVBValueReg2)
        Me.SplitContainer7.Panel2.Controls.Add(Me.Label33)
        Me.SplitContainer7.Panel2.Controls.Add(Me.IecFRVBValueReg2)
        Me.SplitContainer7.Size = New System.Drawing.Size(292, 125)
        Me.SplitContainer7.SplitterDistance = 61
        Me.SplitContainer7.TabIndex = 0
        '
        'IecSourceVoltageReg2
        '
        Me.IecSourceVoltageReg2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecSourceVoltageReg2.Location = New System.Drawing.Point(108, 7)
        Me.IecSourceVoltageReg2.Name = "IecSourceVoltageReg2"
        Me.IecSourceVoltageReg2.Size = New System.Drawing.Size(180, 20)
        Me.IecSourceVoltageReg2.TabIndex = 24
        Me.IecSourceVoltageReg2.Text = "ATCC0$MX$SrcCtrV$mag$i"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(3, 11)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(83, 13)
        Me.Label30.TabIndex = 23
        Me.Label30.Text = "Source Voltage:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(3, 35)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(75, 13)
        Me.Label31.TabIndex = 21
        Me.Label31.Text = "Local Voltage:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecLocalVoltageReg2
        '
        Me.IecLocalVoltageReg2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecLocalVoltageReg2.Location = New System.Drawing.Point(108, 31)
        Me.IecLocalVoltageReg2.Name = "IecLocalVoltageReg2"
        Me.IecLocalVoltageReg2.Size = New System.Drawing.Size(180, 20)
        Me.IecLocalVoltageReg2.TabIndex = 4
        Me.IecLocalVoltageReg2.Text = "ATCC0$MX$LodCtrV$mag$i"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(3, 10)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(105, 13)
        Me.Label32.TabIndex = 25
        Me.Label32.Text = "Source RVB Voltage"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecRRVBValueReg2
        '
        Me.IecRRVBValueReg2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecRRVBValueReg2.Location = New System.Drawing.Point(108, 6)
        Me.IecRRVBValueReg2.Name = "IecRRVBValueReg2"
        Me.IecRRVBValueReg2.Size = New System.Drawing.Size(180, 20)
        Me.IecRRVBValueReg2.TabIndex = 15
        Me.IecRRVBValueReg2.Text = "ATCC0$SP$RRemVVal$setMag$i"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(3, 32)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(91, 13)
        Me.Label33.TabIndex = 24
        Me.Label33.Text = "Fwd RVB Voltage"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecFRVBValueReg2
        '
        Me.IecFRVBValueReg2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecFRVBValueReg2.Location = New System.Drawing.Point(108, 28)
        Me.IecFRVBValueReg2.Name = "IecFRVBValueReg2"
        Me.IecFRVBValueReg2.Size = New System.Drawing.Size(180, 20)
        Me.IecFRVBValueReg2.TabIndex = 5
        Me.IecFRVBValueReg2.Text = "ATCC0$SP$FRemVVal$setMag$i"
        '
        'IecReg1
        '
        Me.IecReg1.Controls.Add(Me.SplitContainer9)
        Me.IecReg1.Location = New System.Drawing.Point(6, 19)
        Me.IecReg1.Name = "IecReg1"
        Me.IecReg1.Size = New System.Drawing.Size(298, 144)
        Me.IecReg1.TabIndex = 35
        Me.IecReg1.TabStop = False
        Me.IecReg1.Text = "Regulator 1"
        '
        'SplitContainer9
        '
        Me.SplitContainer9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer9.Location = New System.Drawing.Point(3, 16)
        Me.SplitContainer9.Name = "SplitContainer9"
        Me.SplitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer9.Panel1
        '
        Me.SplitContainer9.Panel1.Controls.Add(Me.IecSourceVoltageReg1)
        Me.SplitContainer9.Panel1.Controls.Add(Me.Label38)
        Me.SplitContainer9.Panel1.Controls.Add(Me.Label39)
        Me.SplitContainer9.Panel1.Controls.Add(Me.IecLocalVoltageReg1)
        '
        'SplitContainer9.Panel2
        '
        Me.SplitContainer9.Panel2.Controls.Add(Me.Label40)
        Me.SplitContainer9.Panel2.Controls.Add(Me.IecRRVBValueReg1)
        Me.SplitContainer9.Panel2.Controls.Add(Me.Label41)
        Me.SplitContainer9.Panel2.Controls.Add(Me.IecFRVBValueReg1)
        Me.SplitContainer9.Size = New System.Drawing.Size(292, 125)
        Me.SplitContainer9.SplitterDistance = 61
        Me.SplitContainer9.TabIndex = 0
        '
        'IecSourceVoltageReg1
        '
        Me.IecSourceVoltageReg1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecSourceVoltageReg1.Location = New System.Drawing.Point(108, 7)
        Me.IecSourceVoltageReg1.Name = "IecSourceVoltageReg1"
        Me.IecSourceVoltageReg1.Size = New System.Drawing.Size(180, 20)
        Me.IecSourceVoltageReg1.TabIndex = 24
        Me.IecSourceVoltageReg1.Text = "ATCC0$MX$SrcCtrV$mag$i"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(3, 11)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(83, 13)
        Me.Label38.TabIndex = 23
        Me.Label38.Text = "Source Voltage:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(3, 35)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(75, 13)
        Me.Label39.TabIndex = 21
        Me.Label39.Text = "Local Voltage:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecLocalVoltageReg1
        '
        Me.IecLocalVoltageReg1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecLocalVoltageReg1.Location = New System.Drawing.Point(108, 31)
        Me.IecLocalVoltageReg1.Name = "IecLocalVoltageReg1"
        Me.IecLocalVoltageReg1.Size = New System.Drawing.Size(180, 20)
        Me.IecLocalVoltageReg1.TabIndex = 4
        Me.IecLocalVoltageReg1.Text = "ATCC0$MX$LodCtrV$mag$i"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(3, 10)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(105, 13)
        Me.Label40.TabIndex = 25
        Me.Label40.Text = "Source RVB Voltage"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecRRVBValueReg1
        '
        Me.IecRRVBValueReg1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecRRVBValueReg1.Location = New System.Drawing.Point(108, 6)
        Me.IecRRVBValueReg1.Name = "IecRRVBValueReg1"
        Me.IecRRVBValueReg1.Size = New System.Drawing.Size(180, 20)
        Me.IecRRVBValueReg1.TabIndex = 15
        Me.IecRRVBValueReg1.Text = "ATCC0$SP$RRemVVal$setMag$i"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(3, 32)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(91, 13)
        Me.Label41.TabIndex = 24
        Me.Label41.Text = "Fwd RVB Voltage"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IecFRVBValueReg1
        '
        Me.IecFRVBValueReg1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IecFRVBValueReg1.Location = New System.Drawing.Point(108, 28)
        Me.IecFRVBValueReg1.Name = "IecFRVBValueReg1"
        Me.IecFRVBValueReg1.Size = New System.Drawing.Size(180, 20)
        Me.IecFRVBValueReg1.TabIndex = 5
        Me.IecFRVBValueReg1.Text = "ATCC0$SP$FRemVVal$setMag$i"
        '
        'lblwarning
        '
        Me.lblwarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblwarning.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblwarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwarning.ForeColor = System.Drawing.Color.Red
        Me.lblwarning.Location = New System.Drawing.Point(6, 86)
        Me.lblwarning.Name = "lblwarning"
        Me.lblwarning.Size = New System.Drawing.Size(247, 23)
        Me.lblwarning.TabIndex = 7
        Me.lblwarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DNPDestinationReg1
        '
        Me.DNPDestinationReg1.Location = New System.Drawing.Point(158, 50)
        Me.DNPDestinationReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DNPDestinationReg1.Name = "DNPDestinationReg1"
        Me.DNPDestinationReg1.Size = New System.Drawing.Size(61, 20)
        Me.DNPDestinationReg1.TabIndex = 1
        Me.DNPDestinationReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DNPDestinationReg1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lbldestination
        '
        Me.lbldestination.Location = New System.Drawing.Point(90, 54)
        Me.lbldestination.Name = "lbldestination"
        Me.lbldestination.Size = New System.Drawing.Size(67, 13)
        Me.lbldestination.TabIndex = 5
        Me.lbldestination.Text = "Destination:  "
        Me.lbldestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DNPSourceReg1
        '
        Me.DNPSourceReg1.Location = New System.Drawing.Point(158, 25)
        Me.DNPSourceReg1.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.DNPSourceReg1.Name = "DNPSourceReg1"
        Me.DNPSourceReg1.Size = New System.Drawing.Size(61, 20)
        Me.DNPSourceReg1.TabIndex = 0
        Me.DNPSourceReg1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.DNPSourceReg1.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'lblsource
        '
        Me.lblsource.AutoSize = True
        Me.lblsource.Location = New System.Drawing.Point(90, 29)
        Me.lblsource.Name = "lblsource"
        Me.lblsource.Size = New System.Drawing.Size(47, 13)
        Me.lblsource.TabIndex = 3
        Me.lblsource.Text = "Source: "
        Me.lblsource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'iec61850box
        '
        Me.iec61850box.AutoSize = True
        Me.iec61850box.Location = New System.Drawing.Point(7, 60)
        Me.iec61850box.Name = "iec61850box"
        Me.iec61850box.Size = New System.Drawing.Size(72, 17)
        Me.iec61850box.TabIndex = 2
        Me.iec61850box.TabStop = True
        Me.iec61850box.Text = "IEC61850"
        Me.iec61850box.UseVisualStyleBackColor = True
        '
        'modbusbox
        '
        Me.modbusbox.AutoSize = True
        Me.modbusbox.Location = New System.Drawing.Point(7, 20)
        Me.modbusbox.Name = "modbusbox"
        Me.modbusbox.Size = New System.Drawing.Size(63, 17)
        Me.modbusbox.TabIndex = 1
        Me.modbusbox.TabStop = True
        Me.modbusbox.Text = "Modbus"
        Me.modbusbox.UseVisualStyleBackColor = True
        '
        'dnpbutton
        '
        Me.dnpbutton.AutoSize = True
        Me.dnpbutton.Checked = True
        Me.dnpbutton.Location = New System.Drawing.Point(7, 40)
        Me.dnpbutton.Name = "dnpbutton"
        Me.dnpbutton.Size = New System.Drawing.Size(63, 17)
        Me.dnpbutton.TabIndex = 0
        Me.dnpbutton.TabStop = True
        Me.dnpbutton.Text = "DNP3.0"
        Me.dnpbutton.UseVisualStyleBackColor = True
        '
        'ProtocolParameters
        '
        Me.ProtocolParameters.Controls.Add(Me.lblRevRVBValue)
        Me.ProtocolParameters.Controls.Add(Me.lblMsgCenter)
        Me.ProtocolParameters.Controls.Add(Me.lblFwdRVBValue)
        Me.ProtocolParameters.Controls.Add(Me.ReadIpAddr)
        Me.ProtocolParameters.Controls.Add(Me.SourceIPAddressLabel)
        Me.ProtocolParameters.Controls.Add(Me.WriteIpAddr)
        Me.ProtocolParameters.Controls.Add(Me.StartButton)
        Me.ProtocolParameters.Controls.Add(Me.lblLocalVoltageValue)
        Me.ProtocolParameters.Controls.Add(Me.Label3)
        Me.ProtocolParameters.Controls.Add(Me.StopButton)
        Me.ProtocolParameters.Controls.Add(Me.Label2)
        Me.ProtocolParameters.Controls.Add(Me.PortReg1)
        Me.ProtocolParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ProtocolParameters.Location = New System.Drawing.Point(270, 5)
        Me.ProtocolParameters.Name = "ProtocolParameters"
        Me.ProtocolParameters.Size = New System.Drawing.Size(722, 111)
        Me.ProtocolParameters.TabIndex = 0
        Me.ProtocolParameters.TabStop = False
        Me.ProtocolParameters.Text = "Protocol Parameters"
        '
        'lblRevRVBValue
        '
        Me.lblRevRVBValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRevRVBValue.Location = New System.Drawing.Point(261, 56)
        Me.lblRevRVBValue.Name = "lblRevRVBValue"
        Me.lblRevRVBValue.Size = New System.Drawing.Size(282, 20)
        Me.lblRevRVBValue.TabIndex = 37
        Me.lblRevRVBValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMsgCenter
        '
        Me.lblMsgCenter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMsgCenter.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMsgCenter.Location = New System.Drawing.Point(261, 78)
        Me.lblMsgCenter.Name = "lblMsgCenter"
        Me.lblMsgCenter.Size = New System.Drawing.Size(451, 30)
        Me.lblMsgCenter.TabIndex = 36
        Me.lblMsgCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFwdRVBValue
        '
        Me.lblFwdRVBValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFwdRVBValue.Location = New System.Drawing.Point(261, 36)
        Me.lblFwdRVBValue.Name = "lblFwdRVBValue"
        Me.lblFwdRVBValue.Size = New System.Drawing.Size(282, 20)
        Me.lblFwdRVBValue.TabIndex = 35
        Me.lblFwdRVBValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ReadIpAddr
        '
        Me.ReadIpAddr.Location = New System.Drawing.Point(51, 20)
        Me.ReadIpAddr.Name = "ReadIpAddr"
        Me.ReadIpAddr.Size = New System.Drawing.Size(110, 20)
        Me.ReadIpAddr.TabIndex = 0
        '
        'SourceIPAddressLabel
        '
        Me.SourceIPAddressLabel.AutoSize = True
        Me.SourceIPAddressLabel.Location = New System.Drawing.Point(9, 24)
        Me.SourceIPAddressLabel.Name = "SourceIPAddressLabel"
        Me.SourceIPAddressLabel.Size = New System.Drawing.Size(36, 13)
        Me.SourceIPAddressLabel.TabIndex = 33
        Me.SourceIPAddressLabel.Text = "Read:"
        Me.SourceIPAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProtocolBox
        '
        Me.ProtocolBox.Controls.Add(Me.dnpbutton)
        Me.ProtocolBox.Controls.Add(Me.modbusbox)
        Me.ProtocolBox.Controls.Add(Me.iec61850box)
        Me.ProtocolBox.Controls.Add(Me.lblsource)
        Me.ProtocolBox.Controls.Add(Me.DNPSourceReg1)
        Me.ProtocolBox.Controls.Add(Me.lbldestination)
        Me.ProtocolBox.Controls.Add(Me.DNPDestinationReg1)
        Me.ProtocolBox.Controls.Add(Me.lblwarning)
        Me.ProtocolBox.Location = New System.Drawing.Point(5, 4)
        Me.ProtocolBox.Name = "ProtocolBox"
        Me.ProtocolBox.Size = New System.Drawing.Size(259, 112)
        Me.ProtocolBox.TabIndex = 1
        Me.ProtocolBox.TabStop = False
        Me.ProtocolBox.Text = "Supported TCP/IP Protocols"
        '
        'RVBSim
        '
        Me.AcceptButton = Me.StartButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 613)
        Me.Controls.Add(Me.ProtocolParameters)
        Me.Controls.Add(Me.ProtocolBox)
        Me.Controls.Add(Me.CommunicationDetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "RVBSim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "RVB Simulator"
        Me.CommunicationDetails.ResumeLayout(False)
        Me.DnpSettingsGroup.ResumeLayout(False)
        Me.DnpReg3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.Panel2.PerformLayout()
        CType(Me.SplitContainer4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.DnpSourceVoltageReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpLocalVoltageReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpRRVBValueReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpFRVBValueReg3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DnpReg2.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel1.PerformLayout()
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.Panel2.PerformLayout()
        CType(Me.SplitContainer5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer5.ResumeLayout(False)
        CType(Me.DnpSourceVoltageReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpLocalVoltageReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpRRVBValueReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpFRVBValueReg2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DnpReg1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.ResumeLayout(False)
        Me.SplitContainer6.Panel1.PerformLayout()
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.Panel2.PerformLayout()
        CType(Me.SplitContainer6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer6.ResumeLayout(False)
        CType(Me.DnpSourceVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpLocalVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpRRVBValueReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DnpFRVBValueReg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ModbusSettingsGroup.ResumeLayout(False)
        Me.ModbusReg3.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.ModbusSourceVoltageReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusLocalVoltageReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusRRVBValueReg3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusFRVBValueReg3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ModbusReg2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.ModbusSourceVoltageReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusLocalVoltageReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusRRVBValueReg2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusFRVBValueReg2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ModbusReg1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.ModbusSourceVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusLocalVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusRRVBValueReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ModbusFRVBValueReg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RVBSettings.ResumeLayout(False)
        Me.RVBSettings.PerformLayout()
        Me.grpFwdSettings.ResumeLayout(False)
        Me.grpFwdSettings.PerformLayout()
        CType(Me.FRVBScaleReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FwdDeltaVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRevSettings.ResumeLayout(False)
        CType(Me.RevDeltaVoltageReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RRVBScaleReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RVBMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RVBMin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.IecSettingsGroup.ResumeLayout(False)
        Me.IecReg3.ResumeLayout(False)
        Me.SplitContainer8.Panel1.ResumeLayout(False)
        Me.SplitContainer8.Panel1.PerformLayout()
        Me.SplitContainer8.Panel2.ResumeLayout(False)
        Me.SplitContainer8.Panel2.PerformLayout()
        CType(Me.SplitContainer8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer8.ResumeLayout(False)
        Me.IecReg2.ResumeLayout(False)
        Me.SplitContainer7.Panel1.ResumeLayout(False)
        Me.SplitContainer7.Panel1.PerformLayout()
        Me.SplitContainer7.Panel2.ResumeLayout(False)
        Me.SplitContainer7.Panel2.PerformLayout()
        CType(Me.SplitContainer7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer7.ResumeLayout(False)
        Me.IecReg1.ResumeLayout(False)
        Me.SplitContainer9.Panel1.ResumeLayout(False)
        Me.SplitContainer9.Panel1.PerformLayout()
        Me.SplitContainer9.Panel2.ResumeLayout(False)
        Me.SplitContainer9.Panel2.PerformLayout()
        CType(Me.SplitContainer9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer9.ResumeLayout(False)
        CType(Me.DNPDestinationReg1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DNPSourceReg1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProtocolParameters.ResumeLayout(False)
        Me.ProtocolParameters.PerformLayout()
        Me.ProtocolBox.ResumeLayout(False)
        Me.ProtocolBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents StopButton As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblLocalVoltageValue As System.Windows.Forms.Label
    Friend WithEvents WriteIpAddr As System.Windows.Forms.TextBox
    Friend WithEvents PortReg1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CommunicationDetails As System.Windows.Forms.GroupBox
    Friend WithEvents iec61850box As System.Windows.Forms.RadioButton
    Friend WithEvents modbusbox As System.Windows.Forms.RadioButton
    Friend WithEvents dnpbutton As System.Windows.Forms.RadioButton
    Friend WithEvents DNPDestinationReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbldestination As System.Windows.Forms.Label
    Friend WithEvents DNPSourceReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblsource As System.Windows.Forms.Label
    Friend WithEvents lblwarning As System.Windows.Forms.Label
    Friend WithEvents ModbusFRVBValueReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ModbusLocalVoltageReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ProtocolParameters As System.Windows.Forms.GroupBox
    Friend WithEvents ProtocolBox As System.Windows.Forms.GroupBox
    Friend WithEvents heartbeattimer As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents F_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents FRVBScaleReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents IecLocalVoltageReg1 As System.Windows.Forms.TextBox
    Friend WithEvents IecFRVBValueReg1 As System.Windows.Forms.TextBox
    Friend WithEvents Forward_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents FwdDeltaVoltageReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents useDeltaVoltage As System.Windows.Forms.RadioButton
    Friend WithEvents useFixedVoltage As System.Windows.Forms.RadioButton
    Friend WithEvents grpFwdSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents R_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents RRVBScaleReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents IecRRVBValueReg1 As System.Windows.Forms.TextBox
    Friend WithEvents ModbusRRVBValueReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Reverse_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents RevDeltaVoltageReg1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ReadIpAddr As System.Windows.Forms.TextBox
    Friend WithEvents SourceIPAddressLabel As System.Windows.Forms.Label
    Friend WithEvents grpRevSettings As System.Windows.Forms.GroupBox
    Friend WithEvents RVBMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents RVBMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents RVBSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lblRevRVBValue As System.Windows.Forms.Label
    Friend WithEvents lblMsgCenter As System.Windows.Forms.Label
    Friend WithEvents lblFwdRVBValue As System.Windows.Forms.Label
    Friend WithEvents ModbusSettingsGroup As GroupBox
    Friend WithEvents ModbusReg1 As GroupBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ModbusSourceVoltageReg1 As NumericUpDown
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ModbusReg3 As GroupBox
    Friend WithEvents SplitContainer3 As SplitContainer
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents ModbusSourceVoltageReg3 As NumericUpDown
    Friend WithEvents ModbusLocalVoltageReg3 As NumericUpDown
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents ModbusRRVBValueReg3 As NumericUpDown
    Friend WithEvents ModbusFRVBValueReg3 As NumericUpDown
    Friend WithEvents ModbusReg2 As GroupBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents ModbusSourceVoltageReg2 As NumericUpDown
    Friend WithEvents ModbusLocalVoltageReg2 As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents ModbusRRVBValueReg2 As NumericUpDown
    Friend WithEvents ModbusFRVBValueReg2 As NumericUpDown
    Friend WithEvents DnpSettingsGroup As GroupBox
    Friend WithEvents DnpReg3 As GroupBox
    Friend WithEvents SplitContainer4 As SplitContainer
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents DnpSourceVoltageReg3 As NumericUpDown
    Friend WithEvents DnpLocalVoltageReg3 As NumericUpDown
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents DnpRRVBValueReg3 As NumericUpDown
    Friend WithEvents DnpFRVBValueReg3 As NumericUpDown
    Friend WithEvents DnpReg2 As GroupBox
    Friend WithEvents SplitContainer5 As SplitContainer
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents DnpSourceVoltageReg2 As NumericUpDown
    Friend WithEvents DnpLocalVoltageReg2 As NumericUpDown
    Friend WithEvents Label24 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents DnpRRVBValueReg2 As NumericUpDown
    Friend WithEvents DnpFRVBValueReg2 As NumericUpDown
    Friend WithEvents DnpReg1 As GroupBox
    Friend WithEvents SplitContainer6 As SplitContainer
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents DnpSourceVoltageReg1 As NumericUpDown
    Friend WithEvents DnpLocalVoltageReg1 As NumericUpDown
    Friend WithEvents Label28 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents DnpRRVBValueReg1 As NumericUpDown
    Friend WithEvents DnpFRVBValueReg1 As NumericUpDown
    Friend WithEvents IecSettingsGroup As GroupBox
    Friend WithEvents IecReg1 As GroupBox
    Friend WithEvents SplitContainer9 As SplitContainer
    Friend WithEvents IecSourceVoltageReg1 As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents IecReg2 As GroupBox
    Friend WithEvents SplitContainer7 As SplitContainer
    Friend WithEvents IecSourceVoltageReg2 As TextBox
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents IecLocalVoltageReg2 As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents IecRRVBValueReg2 As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents IecFRVBValueReg2 As TextBox
    Friend WithEvents IecReg3 As GroupBox
    Friend WithEvents SplitContainer8 As SplitContainer
    Friend WithEvents IecSourceVoltageReg3 As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents IecLocalVoltageReg3 As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents IecRRVBValueReg3 As TextBox
    Friend WithEvents Label37 As Label
    Friend WithEvents IecFRVBValueReg3 As TextBox
End Class