<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RVBSim
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RVBSim))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txthost = New System.Windows.Forms.TextBox()
        Me.txtport = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddressBox = New System.Windows.Forms.GroupBox()
        Me.IEC_R_RVBVoltage_Value = New System.Windows.Forms.TextBox()
        Me.IEC_R_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.Modbus_R_RVBVoltage_Value = New System.Windows.Forms.NumericUpDown()
        Me.Modbus_R_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.txtLocalVoltage = New System.Windows.Forms.TextBox()
        Me.txtRVBVoltage = New System.Windows.Forms.TextBox()
        Me.IEC_F_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.IEC_LocalVoltage_Label = New System.Windows.Forms.Label()
        Me.RVBVoltage = New System.Windows.Forms.NumericUpDown()
        Me.Modbus_F_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.locvoltage = New System.Windows.Forms.NumericUpDown()
        Me.lbllocalvoltage = New System.Windows.Forms.Label()
        Me.lblwarning = New System.Windows.Forms.Label()
        Me.destnum = New System.Windows.Forms.NumericUpDown()
        Me.lbldestination = New System.Windows.Forms.Label()
        Me.sourcenum = New System.Windows.Forms.NumericUpDown()
        Me.lblsource = New System.Windows.Forms.Label()
        Me.iec61850box = New System.Windows.Forms.RadioButton()
        Me.modbusbox = New System.Windows.Forms.RadioButton()
        Me.dnpbutton = New System.Windows.Forms.RadioButton()
        Me.ProtocolParameters = New System.Windows.Forms.GroupBox()
        Me.IPAddressToReadTextbox = New System.Windows.Forms.TextBox()
        Me.SourceIPAddressLabel = New System.Windows.Forms.Label()
        Me.Reverse_Voltage_Label = New System.Windows.Forms.Label()
        Me.Reverse_DeltaVoltage = New System.Windows.Forms.NumericUpDown()
        Me.R_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.R_RVBScaleFactor_Value = New System.Windows.Forms.NumericUpDown()
        Me.grpRVBLimits = New System.Windows.Forms.GroupBox()
        Me.txtRVBMin = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RVBMin = New System.Windows.Forms.HScrollBar()
        Me.txtRVBMax = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RVBMax = New System.Windows.Forms.HScrollBar()
        Me.radDelta = New System.Windows.Forms.RadioButton()
        Me.radLocal = New System.Windows.Forms.RadioButton()
        Me.Forward_Voltage_Label = New System.Windows.Forms.Label()
        Me.Forward_DeltaVoltage = New System.Windows.Forms.NumericUpDown()
        Me.F_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.F_RVBScaleFactor_Value = New System.Windows.Forms.NumericUpDown()
        Me.heartbeattimer = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProtocolBox = New System.Windows.Forms.GroupBox()
        Me.AddressBox.SuspendLayout()
        CType(Me.Modbus_R_RVBVoltage_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RVBVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.locvoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.destnum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sourcenum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProtocolParameters.SuspendLayout()
        CType(Me.Reverse_DeltaVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.R_RVBScaleFactor_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRVBLimits.SuspendLayout()
        CType(Me.Forward_DeltaVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.F_RVBScaleFactor_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProtocolBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(128, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Start"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(128, 69)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Stop"
        Me.Button2.UseVisualStyleBackColor = True
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
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(9, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(194, 51)
        Me.Label1.TabIndex = 3
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txthost
        '
        Me.txthost.Location = New System.Drawing.Point(47, 47)
        Me.txthost.Name = "txthost"
        Me.txthost.Size = New System.Drawing.Size(75, 20)
        Me.txthost.TabIndex = 1
        '
        'txtport
        '
        Me.txtport.Location = New System.Drawing.Point(47, 70)
        Me.txtport.Name = "txtport"
        Me.txtport.Size = New System.Drawing.Size(75, 20)
        Me.txtport.TabIndex = 2
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
        'AddressBox
        '
        Me.AddressBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddressBox.Controls.Add(Me.IEC_R_RVBVoltage_Value)
        Me.AddressBox.Controls.Add(Me.IEC_R_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.Modbus_R_RVBVoltage_Value)
        Me.AddressBox.Controls.Add(Me.Modbus_R_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.txtLocalVoltage)
        Me.AddressBox.Controls.Add(Me.txtRVBVoltage)
        Me.AddressBox.Controls.Add(Me.IEC_F_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.IEC_LocalVoltage_Label)
        Me.AddressBox.Controls.Add(Me.RVBVoltage)
        Me.AddressBox.Controls.Add(Me.Modbus_F_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.locvoltage)
        Me.AddressBox.Controls.Add(Me.lbllocalvoltage)
        Me.AddressBox.Controls.Add(Me.lblwarning)
        Me.AddressBox.Controls.Add(Me.destnum)
        Me.AddressBox.Controls.Add(Me.lbldestination)
        Me.AddressBox.Controls.Add(Me.sourcenum)
        Me.AddressBox.Controls.Add(Me.lblsource)
        Me.AddressBox.Location = New System.Drawing.Point(5, 49)
        Me.AddressBox.Name = "AddressBox"
        Me.AddressBox.Size = New System.Drawing.Size(603, 139)
        Me.AddressBox.TabIndex = 2
        Me.AddressBox.TabStop = False
        '
        'IEC_R_RVBVoltage_Value
        '
        Me.IEC_R_RVBVoltage_Value.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IEC_R_RVBVoltage_Value.Location = New System.Drawing.Point(433, 76)
        Me.IEC_R_RVBVoltage_Value.Name = "IEC_R_RVBVoltage_Value"
        Me.IEC_R_RVBVoltage_Value.Size = New System.Drawing.Size(166, 20)
        Me.IEC_R_RVBVoltage_Value.TabIndex = 15
        Me.IEC_R_RVBVoltage_Value.Visible = False
        '
        'IEC_R_RVBVoltage_Label
        '
        Me.IEC_R_RVBVoltage_Label.Location = New System.Drawing.Point(351, 71)
        Me.IEC_R_RVBVoltage_Label.Name = "IEC_R_RVBVoltage_Label"
        Me.IEC_R_RVBVoltage_Label.Size = New System.Drawing.Size(74, 30)
        Me.IEC_R_RVBVoltage_Label.TabIndex = 17
        Me.IEC_R_RVBVoltage_Label.Text = "Reverse RVB Voltage: "
        Me.IEC_R_RVBVoltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IEC_R_RVBVoltage_Label.Visible = False
        '
        'Modbus_R_RVBVoltage_Value
        '
        Me.Modbus_R_RVBVoltage_Value.Location = New System.Drawing.Point(253, 76)
        Me.Modbus_R_RVBVoltage_Value.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.Modbus_R_RVBVoltage_Value.Name = "Modbus_R_RVBVoltage_Value"
        Me.Modbus_R_RVBVoltage_Value.Size = New System.Drawing.Size(61, 20)
        Me.Modbus_R_RVBVoltage_Value.TabIndex = 14
        Me.Modbus_R_RVBVoltage_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Modbus_R_RVBVoltage_Value.Visible = False
        '
        'Modbus_R_RVBVoltage_Label
        '
        Me.Modbus_R_RVBVoltage_Label.Location = New System.Drawing.Point(175, 71)
        Me.Modbus_R_RVBVoltage_Label.Name = "Modbus_R_RVBVoltage_Label"
        Me.Modbus_R_RVBVoltage_Label.Size = New System.Drawing.Size(74, 30)
        Me.Modbus_R_RVBVoltage_Label.TabIndex = 16
        Me.Modbus_R_RVBVoltage_Label.Text = "Reverse RVB Voltage: "
        Me.Modbus_R_RVBVoltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Modbus_R_RVBVoltage_Label.Visible = False
        '
        'txtLocalVoltage
        '
        Me.txtLocalVoltage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocalVoltage.Location = New System.Drawing.Point(433, 14)
        Me.txtLocalVoltage.Name = "txtLocalVoltage"
        Me.txtLocalVoltage.Size = New System.Drawing.Size(166, 20)
        Me.txtLocalVoltage.TabIndex = 4
        Me.txtLocalVoltage.Visible = False
        '
        'txtRVBVoltage
        '
        Me.txtRVBVoltage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRVBVoltage.Location = New System.Drawing.Point(433, 43)
        Me.txtRVBVoltage.Name = "txtRVBVoltage"
        Me.txtRVBVoltage.Size = New System.Drawing.Size(166, 20)
        Me.txtRVBVoltage.TabIndex = 5
        Me.txtRVBVoltage.Visible = False
        '
        'IEC_F_RVBVoltage_Label
        '
        Me.IEC_F_RVBVoltage_Label.Location = New System.Drawing.Point(351, 38)
        Me.IEC_F_RVBVoltage_Label.Name = "IEC_F_RVBVoltage_Label"
        Me.IEC_F_RVBVoltage_Label.Size = New System.Drawing.Size(74, 30)
        Me.IEC_F_RVBVoltage_Label.TabIndex = 13
        Me.IEC_F_RVBVoltage_Label.Text = "Forward RVB Voltage: "
        Me.IEC_F_RVBVoltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IEC_F_RVBVoltage_Label.Visible = False
        '
        'IEC_LocalVoltage_Label
        '
        Me.IEC_LocalVoltage_Label.AutoSize = True
        Me.IEC_LocalVoltage_Label.Location = New System.Drawing.Point(351, 18)
        Me.IEC_LocalVoltage_Label.Name = "IEC_LocalVoltage_Label"
        Me.IEC_LocalVoltage_Label.Size = New System.Drawing.Size(78, 13)
        Me.IEC_LocalVoltage_Label.TabIndex = 12
        Me.IEC_LocalVoltage_Label.Text = "Local Voltage: "
        Me.IEC_LocalVoltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IEC_LocalVoltage_Label.Visible = False
        '
        'RVBVoltage
        '
        Me.RVBVoltage.Location = New System.Drawing.Point(253, 43)
        Me.RVBVoltage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.RVBVoltage.Name = "RVBVoltage"
        Me.RVBVoltage.Size = New System.Drawing.Size(61, 20)
        Me.RVBVoltage.TabIndex = 3
        Me.RVBVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RVBVoltage.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        Me.RVBVoltage.Visible = False
        '
        'Modbus_F_RVBVoltage_Label
        '
        Me.Modbus_F_RVBVoltage_Label.Location = New System.Drawing.Point(175, 38)
        Me.Modbus_F_RVBVoltage_Label.Name = "Modbus_F_RVBVoltage_Label"
        Me.Modbus_F_RVBVoltage_Label.Size = New System.Drawing.Size(74, 30)
        Me.Modbus_F_RVBVoltage_Label.TabIndex = 10
        Me.Modbus_F_RVBVoltage_Label.Text = "Forward RVB Voltage: "
        Me.Modbus_F_RVBVoltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Modbus_F_RVBVoltage_Label.Visible = False
        '
        'locvoltage
        '
        Me.locvoltage.Location = New System.Drawing.Point(253, 14)
        Me.locvoltage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.locvoltage.Name = "locvoltage"
        Me.locvoltage.Size = New System.Drawing.Size(61, 20)
        Me.locvoltage.TabIndex = 2
        Me.locvoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.locvoltage.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        Me.locvoltage.Visible = False
        '
        'lbllocalvoltage
        '
        Me.lbllocalvoltage.AutoSize = True
        Me.lbllocalvoltage.Location = New System.Drawing.Point(175, 18)
        Me.lbllocalvoltage.Name = "lbllocalvoltage"
        Me.lbllocalvoltage.Size = New System.Drawing.Size(78, 13)
        Me.lbllocalvoltage.TabIndex = 8
        Me.lbllocalvoltage.Text = "Local Voltage: "
        Me.lbllocalvoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbllocalvoltage.Visible = False
        '
        'lblwarning
        '
        Me.lblwarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblwarning.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwarning.ForeColor = System.Drawing.Color.Red
        Me.lblwarning.Location = New System.Drawing.Point(6, 110)
        Me.lblwarning.Name = "lblwarning"
        Me.lblwarning.Size = New System.Drawing.Size(591, 23)
        Me.lblwarning.TabIndex = 7
        Me.lblwarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblwarning.Visible = False
        '
        'destnum
        '
        Me.destnum.Location = New System.Drawing.Point(79, 39)
        Me.destnum.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.destnum.Name = "destnum"
        Me.destnum.Size = New System.Drawing.Size(61, 20)
        Me.destnum.TabIndex = 1
        Me.destnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.destnum.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.destnum.Visible = False
        '
        'lbldestination
        '
        Me.lbldestination.Location = New System.Drawing.Point(11, 43)
        Me.lbldestination.Name = "lbldestination"
        Me.lbldestination.Size = New System.Drawing.Size(67, 13)
        Me.lbldestination.TabIndex = 5
        Me.lbldestination.Text = "Destination:  "
        Me.lbldestination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbldestination.Visible = False
        '
        'sourcenum
        '
        Me.sourcenum.Location = New System.Drawing.Point(79, 14)
        Me.sourcenum.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.sourcenum.Name = "sourcenum"
        Me.sourcenum.Size = New System.Drawing.Size(61, 20)
        Me.sourcenum.TabIndex = 0
        Me.sourcenum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.sourcenum.Value = New Decimal(New Integer() {3, 0, 0, 0})
        Me.sourcenum.Visible = False
        '
        'lblsource
        '
        Me.lblsource.AutoSize = True
        Me.lblsource.Location = New System.Drawing.Point(11, 18)
        Me.lblsource.Name = "lblsource"
        Me.lblsource.Size = New System.Drawing.Size(47, 13)
        Me.lblsource.TabIndex = 3
        Me.lblsource.Text = "Source: "
        Me.lblsource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblsource.Visible = False
        '
        'iec61850box
        '
        Me.iec61850box.AutoSize = True
        Me.iec61850box.Location = New System.Drawing.Point(440, 19)
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
        Me.modbusbox.Location = New System.Drawing.Point(228, 19)
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
        Me.dnpbutton.Location = New System.Drawing.Point(51, 19)
        Me.dnpbutton.Name = "dnpbutton"
        Me.dnpbutton.Size = New System.Drawing.Size(63, 17)
        Me.dnpbutton.TabIndex = 0
        Me.dnpbutton.TabStop = True
        Me.dnpbutton.Text = "DNP3.0"
        Me.dnpbutton.UseVisualStyleBackColor = True
        '
        'ProtocolParameters
        '
        Me.ProtocolParameters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProtocolParameters.Controls.Add(Me.IPAddressToReadTextbox)
        Me.ProtocolParameters.Controls.Add(Me.SourceIPAddressLabel)
        Me.ProtocolParameters.Controls.Add(Me.Reverse_Voltage_Label)
        Me.ProtocolParameters.Controls.Add(Me.Reverse_DeltaVoltage)
        Me.ProtocolParameters.Controls.Add(Me.R_RVBScaleFactor_Label)
        Me.ProtocolParameters.Controls.Add(Me.R_RVBScaleFactor_Value)
        Me.ProtocolParameters.Controls.Add(Me.grpRVBLimits)
        Me.ProtocolParameters.Controls.Add(Me.radDelta)
        Me.ProtocolParameters.Controls.Add(Me.radLocal)
        Me.ProtocolParameters.Controls.Add(Me.Forward_Voltage_Label)
        Me.ProtocolParameters.Controls.Add(Me.Forward_DeltaVoltage)
        Me.ProtocolParameters.Controls.Add(Me.F_RVBScaleFactor_Label)
        Me.ProtocolParameters.Controls.Add(Me.F_RVBScaleFactor_Value)
        Me.ProtocolParameters.Controls.Add(Me.heartbeattimer)
        Me.ProtocolParameters.Controls.Add(Me.Label4)
        Me.ProtocolParameters.Controls.Add(Me.txthost)
        Me.ProtocolParameters.Controls.Add(Me.Button1)
        Me.ProtocolParameters.Controls.Add(Me.Label1)
        Me.ProtocolParameters.Controls.Add(Me.Label3)
        Me.ProtocolParameters.Controls.Add(Me.Button2)
        Me.ProtocolParameters.Controls.Add(Me.Label2)
        Me.ProtocolParameters.Controls.Add(Me.txtport)
        Me.ProtocolParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ProtocolParameters.Location = New System.Drawing.Point(5, 193)
        Me.ProtocolParameters.Name = "ProtocolParameters"
        Me.ProtocolParameters.Size = New System.Drawing.Size(603, 173)
        Me.ProtocolParameters.TabIndex = 0
        Me.ProtocolParameters.TabStop = False
        Me.ProtocolParameters.Text = "Protocol Parameters"
        '
        'IPAddressToReadTextbox
        '
        Me.IPAddressToReadTextbox.Location = New System.Drawing.Point(47, 20)
        Me.IPAddressToReadTextbox.Name = "IPAddressToReadTextbox"
        Me.IPAddressToReadTextbox.Size = New System.Drawing.Size(75, 20)
        Me.IPAddressToReadTextbox.TabIndex = 0
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
        'Reverse_Voltage_Label
        '
        Me.Reverse_Voltage_Label.AutoSize = True
        Me.Reverse_Voltage_Label.Location = New System.Drawing.Point(394, 83)
        Me.Reverse_Voltage_Label.Name = "Reverse_Voltage_Label"
        Me.Reverse_Voltage_Label.Size = New System.Drawing.Size(84, 13)
        Me.Reverse_Voltage_Label.TabIndex = 30
        Me.Reverse_Voltage_Label.Text = "Local Voltage + "
        Me.Reverse_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Reverse_DeltaVoltage
        '
        Me.Reverse_DeltaVoltage.DecimalPlaces = 1
        Me.Reverse_DeltaVoltage.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Reverse_DeltaVoltage.Location = New System.Drawing.Point(490, 79)
        Me.Reverse_DeltaVoltage.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.Reverse_DeltaVoltage.Name = "Reverse_DeltaVoltage"
        Me.Reverse_DeltaVoltage.Size = New System.Drawing.Size(52, 20)
        Me.Reverse_DeltaVoltage.TabIndex = 11
        Me.Reverse_DeltaVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'R_RVBScaleFactor_Label
        '
        Me.R_RVBScaleFactor_Label.Location = New System.Drawing.Point(389, 45)
        Me.R_RVBScaleFactor_Label.Name = "R_RVBScaleFactor_Label"
        Me.R_RVBScaleFactor_Label.Size = New System.Drawing.Size(95, 30)
        Me.R_RVBScaleFactor_Label.TabIndex = 28
        Me.R_RVBScaleFactor_Label.Text = "Reverse RVB Scale Factor:"
        Me.R_RVBScaleFactor_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'R_RVBScaleFactor_Value
        '
        Me.R_RVBScaleFactor_Value.DecimalPlaces = 1
        Me.R_RVBScaleFactor_Value.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.R_RVBScaleFactor_Value.Location = New System.Drawing.Point(490, 51)
        Me.R_RVBScaleFactor_Value.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.R_RVBScaleFactor_Value.Name = "R_RVBScaleFactor_Value"
        Me.R_RVBScaleFactor_Value.Size = New System.Drawing.Size(52, 20)
        Me.R_RVBScaleFactor_Value.TabIndex = 10
        Me.R_RVBScaleFactor_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.R_RVBScaleFactor_Value.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'grpRVBLimits
        '
        Me.grpRVBLimits.Controls.Add(Me.txtRVBMin)
        Me.grpRVBLimits.Controls.Add(Me.Label7)
        Me.grpRVBLimits.Controls.Add(Me.RVBMin)
        Me.grpRVBLimits.Controls.Add(Me.txtRVBMax)
        Me.grpRVBLimits.Controls.Add(Me.Label5)
        Me.grpRVBLimits.Controls.Add(Me.RVBMax)
        Me.grpRVBLimits.Location = New System.Drawing.Point(251, 105)
        Me.grpRVBLimits.Name = "grpRVBLimits"
        Me.grpRVBLimits.Size = New System.Drawing.Size(311, 58)
        Me.grpRVBLimits.TabIndex = 26
        Me.grpRVBLimits.TabStop = False
        Me.grpRVBLimits.Visible = False
        '
        'txtRVBMin
        '
        Me.txtRVBMin.Location = New System.Drawing.Point(243, 34)
        Me.txtRVBMin.Name = "txtRVBMin"
        Me.txtRVBMin.Size = New System.Drawing.Size(38, 20)
        Me.txtRVBMin.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(163, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Remote Voltage Min"
        '
        'RVBMin
        '
        Me.RVBMin.LargeChange = 1
        Me.RVBMin.Location = New System.Drawing.Point(163, 34)
        Me.RVBMin.Maximum = 1500
        Me.RVBMin.Minimum = 900
        Me.RVBMin.Name = "RVBMin"
        Me.RVBMin.Size = New System.Drawing.Size(80, 17)
        Me.RVBMin.TabIndex = 2
        Me.RVBMin.Value = 900
        '
        'txtRVBMax
        '
        Me.txtRVBMax.Location = New System.Drawing.Point(88, 34)
        Me.txtRVBMax.Name = "txtRVBMax"
        Me.txtRVBMax.Size = New System.Drawing.Size(38, 20)
        Me.txtRVBMax.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Remote Voltage Max"
        '
        'RVBMax
        '
        Me.RVBMax.LargeChange = 1
        Me.RVBMax.Location = New System.Drawing.Point(8, 34)
        Me.RVBMax.Maximum = 1500
        Me.RVBMax.Minimum = 900
        Me.RVBMax.Name = "RVBMax"
        Me.RVBMax.Size = New System.Drawing.Size(80, 17)
        Me.RVBMax.TabIndex = 0
        Me.RVBMax.Value = 1500
        '
        'radDelta
        '
        Me.radDelta.AutoSize = True
        Me.radDelta.Checked = True
        Me.radDelta.Location = New System.Drawing.Point(396, 20)
        Me.radDelta.Name = "radDelta"
        Me.radDelta.Size = New System.Drawing.Size(72, 17)
        Me.radDelta.TabIndex = 8
        Me.radDelta.TabStop = True
        Me.radDelta.Text = "Use Delta"
        Me.radDelta.UseVisualStyleBackColor = True
        '
        'radLocal
        '
        Me.radLocal.AutoSize = True
        Me.radLocal.Location = New System.Drawing.Point(490, 20)
        Me.radLocal.Name = "radLocal"
        Me.radLocal.Size = New System.Drawing.Size(72, 17)
        Me.radLocal.TabIndex = 9
        Me.radLocal.Text = "Use Fixed"
        Me.radLocal.UseVisualStyleBackColor = True
        '
        'Forward_Voltage_Label
        '
        Me.Forward_Voltage_Label.AutoSize = True
        Me.Forward_Voltage_Label.Location = New System.Drawing.Point(216, 83)
        Me.Forward_Voltage_Label.Name = "Forward_Voltage_Label"
        Me.Forward_Voltage_Label.Size = New System.Drawing.Size(84, 13)
        Me.Forward_Voltage_Label.TabIndex = 17
        Me.Forward_Voltage_Label.Text = "Local Voltage + "
        Me.Forward_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Forward_DeltaVoltage
        '
        Me.Forward_DeltaVoltage.DecimalPlaces = 1
        Me.Forward_DeltaVoltage.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.Forward_DeltaVoltage.Location = New System.Drawing.Point(312, 79)
        Me.Forward_DeltaVoltage.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.Forward_DeltaVoltage.Name = "Forward_DeltaVoltage"
        Me.Forward_DeltaVoltage.Size = New System.Drawing.Size(52, 20)
        Me.Forward_DeltaVoltage.TabIndex = 7
        Me.Forward_DeltaVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'F_RVBScaleFactor_Label
        '
        Me.F_RVBScaleFactor_Label.Location = New System.Drawing.Point(216, 45)
        Me.F_RVBScaleFactor_Label.Name = "F_RVBScaleFactor_Label"
        Me.F_RVBScaleFactor_Label.Size = New System.Drawing.Size(95, 30)
        Me.F_RVBScaleFactor_Label.TabIndex = 13
        Me.F_RVBScaleFactor_Label.Text = "Forward RVB Scale Factor:"
        Me.F_RVBScaleFactor_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'F_RVBScaleFactor_Value
        '
        Me.F_RVBScaleFactor_Value.DecimalPlaces = 1
        Me.F_RVBScaleFactor_Value.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.F_RVBScaleFactor_Value.Location = New System.Drawing.Point(312, 51)
        Me.F_RVBScaleFactor_Value.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.F_RVBScaleFactor_Value.Name = "F_RVBScaleFactor_Value"
        Me.F_RVBScaleFactor_Value.Size = New System.Drawing.Size(52, 20)
        Me.F_RVBScaleFactor_Value.TabIndex = 6
        Me.F_RVBScaleFactor_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.F_RVBScaleFactor_Value.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'heartbeattimer
        '
        Me.heartbeattimer.Location = New System.Drawing.Point(312, 20)
        Me.heartbeattimer.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.heartbeattimer.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.heartbeattimer.Name = "heartbeattimer"
        Me.heartbeattimer.Size = New System.Drawing.Size(52, 20)
        Me.heartbeattimer.TabIndex = 5
        Me.heartbeattimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.heartbeattimer.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(216, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Write Interval (sec)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ProtocolBox
        '
        Me.ProtocolBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProtocolBox.Controls.Add(Me.dnpbutton)
        Me.ProtocolBox.Controls.Add(Me.modbusbox)
        Me.ProtocolBox.Controls.Add(Me.iec61850box)
        Me.ProtocolBox.Location = New System.Drawing.Point(5, 4)
        Me.ProtocolBox.Name = "ProtocolBox"
        Me.ProtocolBox.Size = New System.Drawing.Size(603, 45)
        Me.ProtocolBox.TabIndex = 1
        Me.ProtocolBox.TabStop = False
        Me.ProtocolBox.Text = "Supported TCP/IP Protocols"
        '
        'Form1
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 368)
        Me.Controls.Add(Me.ProtocolBox)
        Me.Controls.Add(Me.ProtocolParameters)
        Me.Controls.Add(Me.AddressBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "RVB Simulator"
        Me.AddressBox.ResumeLayout(False)
        Me.AddressBox.PerformLayout()
        CType(Me.Modbus_R_RVBVoltage_Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RVBVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.locvoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.destnum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sourcenum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProtocolParameters.ResumeLayout(False)
        Me.ProtocolParameters.PerformLayout()
        CType(Me.Reverse_DeltaVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.R_RVBScaleFactor_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRVBLimits.ResumeLayout(False)
        Me.grpRVBLimits.PerformLayout()
        CType(Me.Forward_DeltaVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.F_RVBScaleFactor_Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProtocolBox.ResumeLayout(False)
        Me.ProtocolBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txthost As System.Windows.Forms.TextBox
    Friend WithEvents txtport As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AddressBox As System.Windows.Forms.GroupBox
    Friend WithEvents iec61850box As System.Windows.Forms.RadioButton
    Friend WithEvents modbusbox As System.Windows.Forms.RadioButton
    Friend WithEvents dnpbutton As System.Windows.Forms.RadioButton
    Friend WithEvents destnum As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbldestination As System.Windows.Forms.Label
    Friend WithEvents sourcenum As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblsource As System.Windows.Forms.Label
    Friend WithEvents lblwarning As System.Windows.Forms.Label
    Friend WithEvents RVBVoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents Modbus_F_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents locvoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbllocalvoltage As System.Windows.Forms.Label
    Friend WithEvents ProtocolParameters As System.Windows.Forms.GroupBox
    Friend WithEvents ProtocolBox As System.Windows.Forms.GroupBox
    Friend WithEvents heartbeattimer As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents F_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents F_RVBScaleFactor_Value As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtLocalVoltage As System.Windows.Forms.TextBox
    Friend WithEvents txtRVBVoltage As System.Windows.Forms.TextBox
    Friend WithEvents IEC_F_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents IEC_LocalVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents Forward_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents Forward_DeltaVoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents radDelta As System.Windows.Forms.RadioButton
    Friend WithEvents radLocal As System.Windows.Forms.RadioButton
    Friend WithEvents grpRVBLimits As System.Windows.Forms.GroupBox
    Friend WithEvents txtRVBMin As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents RVBMin As System.Windows.Forms.HScrollBar
    Friend WithEvents txtRVBMax As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents RVBMax As System.Windows.Forms.HScrollBar
    Friend WithEvents R_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents R_RVBScaleFactor_Value As System.Windows.Forms.NumericUpDown
    Friend WithEvents IEC_R_RVBVoltage_Value As System.Windows.Forms.TextBox
    Friend WithEvents IEC_R_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents Modbus_R_RVBVoltage_Value As System.Windows.Forms.NumericUpDown
    Friend WithEvents Modbus_R_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents Reverse_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents Reverse_DeltaVoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents IPAddressToReadTextbox As System.Windows.Forms.TextBox
    Friend WithEvents SourceIPAddressLabel As System.Windows.Forms.Label

End Class
