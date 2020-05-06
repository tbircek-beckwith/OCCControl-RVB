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
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblLocalVoltageValue = New System.Windows.Forms.Label()
        Me.txtWrite = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddressBox = New System.Windows.Forms.GroupBox()
        Me.txtIECRevRVBVoltage = New System.Windows.Forms.TextBox()
        Me.IEC_R_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.NumericUpDownModbusRevRVBVoltageRegister = New System.Windows.Forms.NumericUpDown()
        Me.Modbus_R_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.txtIECLocalVoltage = New System.Windows.Forms.TextBox()
        Me.txtIECFwdRVBVoltage = New System.Windows.Forms.TextBox()
        Me.IEC_F_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.IEC_LocalVoltage_Label = New System.Windows.Forms.Label()
        Me.NumericUpDownModbusFwdRVBVoltageRegister = New System.Windows.Forms.NumericUpDown()
        Me.Modbus_F_RVBVoltage_Label = New System.Windows.Forms.Label()
        Me.NumericUpDownModbusLocalVoltageRegister = New System.Windows.Forms.NumericUpDown()
        Me.lbllocalvoltage = New System.Windows.Forms.Label()
        Me.lblwarning = New System.Windows.Forms.Label()
        Me.NumericUpDownDNPDestinationAddress = New System.Windows.Forms.NumericUpDown()
        Me.lbldestination = New System.Windows.Forms.Label()
        Me.NumericUpDownDNPSourceAddress = New System.Windows.Forms.NumericUpDown()
        Me.lblsource = New System.Windows.Forms.Label()
        Me.iec61850box = New System.Windows.Forms.RadioButton()
        Me.modbusbox = New System.Windows.Forms.RadioButton()
        Me.dnpbutton = New System.Windows.Forms.RadioButton()
        Me.ProtocolParameters = New System.Windows.Forms.GroupBox()
        Me.lblRevRVBValue = New System.Windows.Forms.Label()
        Me.lblMsgCenter = New System.Windows.Forms.Label()
        Me.lblFwdRVBValue = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radUseDeltaVoltage = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.heartbeattimer = New System.Windows.Forms.NumericUpDown()
        Me.radUseFixedVoltage = New System.Windows.Forms.RadioButton()
        Me.grpFwdSettings = New System.Windows.Forms.GroupBox()
        Me.FwdRVBScaleFactor = New System.Windows.Forms.NumericUpDown()
        Me.F_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.FwdDeltaVoltage = New System.Windows.Forms.NumericUpDown()
        Me.Forward_Voltage_Label = New System.Windows.Forms.Label()
        Me.grpRevSettings = New System.Windows.Forms.GroupBox()
        Me.Reverse_Voltage_Label = New System.Windows.Forms.Label()
        Me.RevDeltaVoltage = New System.Windows.Forms.NumericUpDown()
        Me.R_RVBScaleFactor_Label = New System.Windows.Forms.Label()
        Me.RevRVBScaleFactor = New System.Windows.Forms.NumericUpDown()
        Me.txtRead = New System.Windows.Forms.TextBox()
        Me.SourceIPAddressLabel = New System.Windows.Forms.Label()
        Me.RVBMin = New System.Windows.Forms.NumericUpDown()
        Me.RVBMax = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ProtocolBox = New System.Windows.Forms.GroupBox()
        Me.AddressBox.SuspendLayout()
        CType(Me.NumericUpDownModbusRevRVBVoltageRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownModbusFwdRVBVoltageRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownModbusLocalVoltageRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownDNPDestinationAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownDNPSourceAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProtocolParameters.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFwdSettings.SuspendLayout()
        CType(Me.FwdRVBScaleFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FwdDeltaVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRevSettings.SuspendLayout()
        CType(Me.RevDeltaVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RevRVBScaleFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RVBMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RVBMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProtocolBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(168, 19)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 3
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnStop.Enabled = False
        Me.btnStop.Location = New System.Drawing.Point(168, 46)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 23)
        Me.btnStop.TabIndex = 4
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
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
        Me.lblLocalVoltageValue.Location = New System.Drawing.Point(9, 94)
        Me.lblLocalVoltageValue.Name = "lblLocalVoltageValue"
        Me.lblLocalVoltageValue.Size = New System.Drawing.Size(234, 20)
        Me.lblLocalVoltageValue.TabIndex = 3
        Me.lblLocalVoltageValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWrite
        '
        Me.txtWrite.Location = New System.Drawing.Point(51, 47)
        Me.txtWrite.Name = "txtWrite"
        Me.txtWrite.Size = New System.Drawing.Size(110, 20)
        Me.txtWrite.TabIndex = 1
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(51, 70)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(110, 20)
        Me.txtPort.TabIndex = 2
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
        Me.AddressBox.Controls.Add(Me.txtIECRevRVBVoltage)
        Me.AddressBox.Controls.Add(Me.IEC_R_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.NumericUpDownModbusRevRVBVoltageRegister)
        Me.AddressBox.Controls.Add(Me.Modbus_R_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.txtIECLocalVoltage)
        Me.AddressBox.Controls.Add(Me.txtIECFwdRVBVoltage)
        Me.AddressBox.Controls.Add(Me.IEC_F_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.IEC_LocalVoltage_Label)
        Me.AddressBox.Controls.Add(Me.NumericUpDownModbusFwdRVBVoltageRegister)
        Me.AddressBox.Controls.Add(Me.Modbus_F_RVBVoltage_Label)
        Me.AddressBox.Controls.Add(Me.NumericUpDownModbusLocalVoltageRegister)
        Me.AddressBox.Controls.Add(Me.lbllocalvoltage)
        Me.AddressBox.Controls.Add(Me.lblwarning)
        Me.AddressBox.Controls.Add(Me.NumericUpDownDNPDestinationAddress)
        Me.AddressBox.Controls.Add(Me.lbldestination)
        Me.AddressBox.Controls.Add(Me.NumericUpDownDNPSourceAddress)
        Me.AddressBox.Controls.Add(Me.lblsource)
        Me.AddressBox.Location = New System.Drawing.Point(5, 49)
        Me.AddressBox.Name = "AddressBox"
        Me.AddressBox.Size = New System.Drawing.Size(610, 138)
        Me.AddressBox.TabIndex = 2
        Me.AddressBox.TabStop = False
        '
        'txtIECRevRVBVoltage
        '
        Me.txtIECRevRVBVoltage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIECRevRVBVoltage.Location = New System.Drawing.Point(440, 76)
        Me.txtIECRevRVBVoltage.Name = "txtIECRevRVBVoltage"
        Me.txtIECRevRVBVoltage.Size = New System.Drawing.Size(166, 20)
        Me.txtIECRevRVBVoltage.TabIndex = 15
        Me.txtIECRevRVBVoltage.Visible = False
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
        'NumericUpDownModbusRevRVBVoltageRegister
        '
        Me.NumericUpDownModbusRevRVBVoltageRegister.Location = New System.Drawing.Point(253, 76)
        Me.NumericUpDownModbusRevRVBVoltageRegister.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDownModbusRevRVBVoltageRegister.Name = "NumericUpDownModbusRevRVBVoltageRegister"
        Me.NumericUpDownModbusRevRVBVoltageRegister.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownModbusRevRVBVoltageRegister.TabIndex = 14
        Me.NumericUpDownModbusRevRVBVoltageRegister.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownModbusRevRVBVoltageRegister.Value = New Decimal(New Integer() {1996, 0, 0, 0})
        Me.NumericUpDownModbusRevRVBVoltageRegister.Visible = False
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
        'txtIECLocalVoltage
        '
        Me.txtIECLocalVoltage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIECLocalVoltage.Location = New System.Drawing.Point(440, 14)
        Me.txtIECLocalVoltage.Name = "txtIECLocalVoltage"
        Me.txtIECLocalVoltage.Size = New System.Drawing.Size(166, 20)
        Me.txtIECLocalVoltage.TabIndex = 4
        Me.txtIECLocalVoltage.Visible = False
        '
        'txtIECFwdRVBVoltage
        '
        Me.txtIECFwdRVBVoltage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIECFwdRVBVoltage.Location = New System.Drawing.Point(440, 43)
        Me.txtIECFwdRVBVoltage.Name = "txtIECFwdRVBVoltage"
        Me.txtIECFwdRVBVoltage.Size = New System.Drawing.Size(166, 20)
        Me.txtIECFwdRVBVoltage.TabIndex = 5
        Me.txtIECFwdRVBVoltage.Visible = False
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
        'NumericUpDownModbusFwdRVBVoltageRegister
        '
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Location = New System.Drawing.Point(253, 43)
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Name = "NumericUpDownModbusFwdRVBVoltageRegister"
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownModbusFwdRVBVoltageRegister.TabIndex = 3
        Me.NumericUpDownModbusFwdRVBVoltageRegister.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Value = New Decimal(New Integer() {1992, 0, 0, 0})
        Me.NumericUpDownModbusFwdRVBVoltageRegister.Visible = False
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
        'NumericUpDownModbusLocalVoltageRegister
        '
        Me.NumericUpDownModbusLocalVoltageRegister.Location = New System.Drawing.Point(253, 14)
        Me.NumericUpDownModbusLocalVoltageRegister.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDownModbusLocalVoltageRegister.Name = "NumericUpDownModbusLocalVoltageRegister"
        Me.NumericUpDownModbusLocalVoltageRegister.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownModbusLocalVoltageRegister.TabIndex = 2
        Me.NumericUpDownModbusLocalVoltageRegister.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownModbusLocalVoltageRegister.Value = New Decimal(New Integer() {1701, 0, 0, 0})
        Me.NumericUpDownModbusLocalVoltageRegister.Visible = False
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
        Me.lblwarning.Location = New System.Drawing.Point(6, 107)
        Me.lblwarning.Name = "lblwarning"
        Me.lblwarning.Size = New System.Drawing.Size(598, 23)
        Me.lblwarning.TabIndex = 7
        Me.lblwarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblwarning.Visible = False
        '
        'NumericUpDownDNPDestinationAddress
        '
        Me.NumericUpDownDNPDestinationAddress.Location = New System.Drawing.Point(79, 41)
        Me.NumericUpDownDNPDestinationAddress.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDownDNPDestinationAddress.Name = "NumericUpDownDNPDestinationAddress"
        Me.NumericUpDownDNPDestinationAddress.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownDNPDestinationAddress.TabIndex = 1
        Me.NumericUpDownDNPDestinationAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownDNPDestinationAddress.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDownDNPDestinationAddress.Visible = False
        '
        'lbldestination
        '
        Me.lbldestination.Location = New System.Drawing.Point(11, 45)
        Me.lbldestination.Name = "lbldestination"
        Me.lbldestination.Size = New System.Drawing.Size(67, 13)
        Me.lbldestination.TabIndex = 5
        Me.lbldestination.Text = "Destination:  "
        Me.lbldestination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbldestination.Visible = False
        '
        'NumericUpDownDNPSourceAddress
        '
        Me.NumericUpDownDNPSourceAddress.Location = New System.Drawing.Point(79, 16)
        Me.NumericUpDownDNPSourceAddress.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.NumericUpDownDNPSourceAddress.Name = "NumericUpDownDNPSourceAddress"
        Me.NumericUpDownDNPSourceAddress.Size = New System.Drawing.Size(61, 20)
        Me.NumericUpDownDNPSourceAddress.TabIndex = 0
        Me.NumericUpDownDNPSourceAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDownDNPSourceAddress.Value = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NumericUpDownDNPSourceAddress.Visible = False
        '
        'lblsource
        '
        Me.lblsource.AutoSize = True
        Me.lblsource.Location = New System.Drawing.Point(11, 20)
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
        Me.ProtocolParameters.Controls.Add(Me.lblRevRVBValue)
        Me.ProtocolParameters.Controls.Add(Me.lblMsgCenter)
        Me.ProtocolParameters.Controls.Add(Me.lblFwdRVBValue)
        Me.ProtocolParameters.Controls.Add(Me.GroupBox1)
        Me.ProtocolParameters.Controls.Add(Me.txtRead)
        Me.ProtocolParameters.Controls.Add(Me.SourceIPAddressLabel)
        Me.ProtocolParameters.Controls.Add(Me.txtWrite)
        Me.ProtocolParameters.Controls.Add(Me.btnStart)
        Me.ProtocolParameters.Controls.Add(Me.lblLocalVoltageValue)
        Me.ProtocolParameters.Controls.Add(Me.Label3)
        Me.ProtocolParameters.Controls.Add(Me.btnStop)
        Me.ProtocolParameters.Controls.Add(Me.Label2)
        Me.ProtocolParameters.Controls.Add(Me.txtPort)
        Me.ProtocolParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ProtocolParameters.Location = New System.Drawing.Point(5, 189)
        Me.ProtocolParameters.Name = "ProtocolParameters"
        Me.ProtocolParameters.Size = New System.Drawing.Size(610, 198)
        Me.ProtocolParameters.TabIndex = 0
        Me.ProtocolParameters.TabStop = False
        Me.ProtocolParameters.Text = "Protocol Parameters"
        '
        'lblRevRVBValue
        '
        Me.lblRevRVBValue.Location = New System.Drawing.Point(9, 134)
        Me.lblRevRVBValue.Name = "lblRevRVBValue"
        Me.lblRevRVBValue.Size = New System.Drawing.Size(234, 20)
        Me.lblRevRVBValue.TabIndex = 37
        Me.lblRevRVBValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMsgCenter
        '
        Me.lblMsgCenter.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMsgCenter.Location = New System.Drawing.Point(9, 156)
        Me.lblMsgCenter.Name = "lblMsgCenter"
        Me.lblMsgCenter.Size = New System.Drawing.Size(594, 36)
        Me.lblMsgCenter.TabIndex = 36
        Me.lblMsgCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFwdRVBValue
        '
        Me.lblFwdRVBValue.Location = New System.Drawing.Point(9, 114)
        Me.lblFwdRVBValue.Name = "lblFwdRVBValue"
        Me.lblFwdRVBValue.Size = New System.Drawing.Size(234, 20)
        Me.lblFwdRVBValue.TabIndex = 35
        Me.lblFwdRVBValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radUseDeltaVoltage)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.heartbeattimer)
        Me.GroupBox1.Controls.Add(Me.radUseFixedVoltage)
        Me.GroupBox1.Controls.Add(Me.grpFwdSettings)
        Me.GroupBox1.Controls.Add(Me.grpRevSettings)
        Me.GroupBox1.Location = New System.Drawing.Point(261, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 131)
        Me.GroupBox1.TabIndex = 34
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RVB Settings"
        '
        'radUseDeltaVoltage
        '
        Me.radUseDeltaVoltage.AutoSize = True
        Me.radUseDeltaVoltage.Checked = True
        Me.radUseDeltaVoltage.Location = New System.Drawing.Point(161, 17)
        Me.radUseDeltaVoltage.Name = "radUseDeltaVoltage"
        Me.radUseDeltaVoltage.Size = New System.Drawing.Size(86, 17)
        Me.radUseDeltaVoltage.TabIndex = 8
        Me.radUseDeltaVoltage.TabStop = True
        Me.radUseDeltaVoltage.Text = "Use Relative"
        Me.radUseDeltaVoltage.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Write Interval (sec)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'heartbeattimer
        '
        Me.heartbeattimer.Location = New System.Drawing.Point(102, 15)
        Me.heartbeattimer.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.heartbeattimer.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.heartbeattimer.Name = "heartbeattimer"
        Me.heartbeattimer.Size = New System.Drawing.Size(52, 20)
        Me.heartbeattimer.TabIndex = 5
        Me.heartbeattimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.heartbeattimer.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'radUseFixedVoltage
        '
        Me.radUseFixedVoltage.AutoSize = True
        Me.radUseFixedVoltage.Location = New System.Drawing.Point(249, 17)
        Me.radUseFixedVoltage.Name = "radUseFixedVoltage"
        Me.radUseFixedVoltage.Size = New System.Drawing.Size(88, 17)
        Me.radUseFixedVoltage.TabIndex = 9
        Me.radUseFixedVoltage.Text = "Use Absolute"
        Me.radUseFixedVoltage.UseVisualStyleBackColor = True
        '
        'grpFwdSettings
        '
        Me.grpFwdSettings.Controls.Add(Me.FwdRVBScaleFactor)
        Me.grpFwdSettings.Controls.Add(Me.F_RVBScaleFactor_Label)
        Me.grpFwdSettings.Controls.Add(Me.FwdDeltaVoltage)
        Me.grpFwdSettings.Controls.Add(Me.Forward_Voltage_Label)
        Me.grpFwdSettings.Location = New System.Drawing.Point(4, 40)
        Me.grpFwdSettings.Name = "grpFwdSettings"
        Me.grpFwdSettings.Size = New System.Drawing.Size(168, 85)
        Me.grpFwdSettings.TabIndex = 26
        Me.grpFwdSettings.TabStop = False
        Me.grpFwdSettings.Text = "Fwd RVB Settings"
        '
        'FwdRVBScaleFactor
        '
        Me.FwdRVBScaleFactor.DecimalPlaces = 1
        Me.FwdRVBScaleFactor.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FwdRVBScaleFactor.Location = New System.Drawing.Point(102, 25)
        Me.FwdRVBScaleFactor.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FwdRVBScaleFactor.Name = "FwdRVBScaleFactor"
        Me.FwdRVBScaleFactor.Size = New System.Drawing.Size(52, 20)
        Me.FwdRVBScaleFactor.TabIndex = 6
        Me.FwdRVBScaleFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FwdRVBScaleFactor.Value = New Decimal(New Integer() {1, 0, 0, 0})
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
        'FwdDeltaVoltage
        '
        Me.FwdDeltaVoltage.DecimalPlaces = 1
        Me.FwdDeltaVoltage.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.FwdDeltaVoltage.Location = New System.Drawing.Point(102, 54)
        Me.FwdDeltaVoltage.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.FwdDeltaVoltage.Name = "FwdDeltaVoltage"
        Me.FwdDeltaVoltage.Size = New System.Drawing.Size(52, 20)
        Me.FwdDeltaVoltage.TabIndex = 7
        Me.FwdDeltaVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Forward_Voltage_Label
        '
        Me.Forward_Voltage_Label.AutoSize = True
        Me.Forward_Voltage_Label.Location = New System.Drawing.Point(11, 58)
        Me.Forward_Voltage_Label.Name = "Forward_Voltage_Label"
        Me.Forward_Voltage_Label.Size = New System.Drawing.Size(84, 13)
        Me.Forward_Voltage_Label.TabIndex = 17
        Me.Forward_Voltage_Label.Text = "Local Voltage + "
        Me.Forward_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpRevSettings
        '
        Me.grpRevSettings.Controls.Add(Me.Reverse_Voltage_Label)
        Me.grpRevSettings.Controls.Add(Me.RevDeltaVoltage)
        Me.grpRevSettings.Controls.Add(Me.R_RVBScaleFactor_Label)
        Me.grpRevSettings.Controls.Add(Me.RevRVBScaleFactor)
        Me.grpRevSettings.Location = New System.Drawing.Point(172, 40)
        Me.grpRevSettings.Name = "grpRevSettings"
        Me.grpRevSettings.Size = New System.Drawing.Size(168, 85)
        Me.grpRevSettings.TabIndex = 34
        Me.grpRevSettings.TabStop = False
        Me.grpRevSettings.Text = "Rev RVB Settings"
        '
        'Reverse_Voltage_Label
        '
        Me.Reverse_Voltage_Label.AutoSize = True
        Me.Reverse_Voltage_Label.Location = New System.Drawing.Point(12, 58)
        Me.Reverse_Voltage_Label.Name = "Reverse_Voltage_Label"
        Me.Reverse_Voltage_Label.Size = New System.Drawing.Size(84, 13)
        Me.Reverse_Voltage_Label.TabIndex = 30
        Me.Reverse_Voltage_Label.Text = "Local Voltage + "
        Me.Reverse_Voltage_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RevDeltaVoltage
        '
        Me.RevDeltaVoltage.DecimalPlaces = 1
        Me.RevDeltaVoltage.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RevDeltaVoltage.Location = New System.Drawing.Point(102, 54)
        Me.RevDeltaVoltage.Minimum = New Decimal(New Integer() {100, 0, 0, -2147483648})
        Me.RevDeltaVoltage.Name = "RevDeltaVoltage"
        Me.RevDeltaVoltage.Size = New System.Drawing.Size(52, 20)
        Me.RevDeltaVoltage.TabIndex = 11
        Me.RevDeltaVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'RevRVBScaleFactor
        '
        Me.RevRVBScaleFactor.DecimalPlaces = 1
        Me.RevRVBScaleFactor.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RevRVBScaleFactor.Location = New System.Drawing.Point(102, 25)
        Me.RevRVBScaleFactor.Minimum = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RevRVBScaleFactor.Name = "RevRVBScaleFactor"
        Me.RevRVBScaleFactor.Size = New System.Drawing.Size(52, 20)
        Me.RevRVBScaleFactor.TabIndex = 10
        Me.RevRVBScaleFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RevRVBScaleFactor.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtRead
        '
        Me.txtRead.Location = New System.Drawing.Point(51, 20)
        Me.txtRead.Name = "txtRead"
        Me.txtRead.Size = New System.Drawing.Size(110, 20)
        Me.txtRead.TabIndex = 0
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
        'RVBMin
        '
        Me.RVBMin.DecimalPlaces = 1
        Me.RVBMin.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RVBMin.Location = New System.Drawing.Point(845, 290)
        Me.RVBMin.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.RVBMin.Minimum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.RVBMin.Name = "RVBMin"
        Me.RVBMin.Size = New System.Drawing.Size(52, 20)
        Me.RVBMin.TabIndex = 32
        Me.RVBMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RVBMin.Value = New Decimal(New Integer() {90, 0, 0, 0})
        Me.RVBMin.Visible = False
        '
        'RVBMax
        '
        Me.RVBMax.DecimalPlaces = 1
        Me.RVBMax.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.RVBMax.Location = New System.Drawing.Point(758, 290)
        Me.RVBMax.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.RVBMax.Minimum = New Decimal(New Integer() {90, 0, 0, 0})
        Me.RVBMax.Name = "RVBMax"
        Me.RVBMax.Size = New System.Drawing.Size(52, 20)
        Me.RVBMax.TabIndex = 31
        Me.RVBMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.RVBMax.Value = New Decimal(New Integer() {150, 0, 0, 0})
        Me.RVBMax.Visible = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(830, 247)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 33)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Remote Voltage Min"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.Visible = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(745, 247)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 33)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Remote Voltage Max"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'ProtocolBox
        '
        Me.ProtocolBox.Controls.Add(Me.dnpbutton)
        Me.ProtocolBox.Controls.Add(Me.modbusbox)
        Me.ProtocolBox.Controls.Add(Me.iec61850box)
        Me.ProtocolBox.Location = New System.Drawing.Point(5, 4)
        Me.ProtocolBox.Name = "ProtocolBox"
        Me.ProtocolBox.Size = New System.Drawing.Size(610, 45)
        Me.ProtocolBox.TabIndex = 1
        Me.ProtocolBox.TabStop = False
        Me.ProtocolBox.Text = "Supported TCP/IP Protocols"
        '
        'RVBSim
        '
        Me.AcceptButton = Me.btnStart
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 388)
        Me.Controls.Add(Me.RVBMin)
        Me.Controls.Add(Me.ProtocolBox)
        Me.Controls.Add(Me.ProtocolParameters)
        Me.Controls.Add(Me.RVBMax)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.AddressBox)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "RVBSim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "RVB Simulator"
        Me.AddressBox.ResumeLayout(False)
        Me.AddressBox.PerformLayout()
        CType(Me.NumericUpDownModbusRevRVBVoltageRegister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownModbusFwdRVBVoltageRegister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownModbusLocalVoltageRegister, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownDNPDestinationAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownDNPSourceAddress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProtocolParameters.ResumeLayout(False)
        Me.ProtocolParameters.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.heartbeattimer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFwdSettings.ResumeLayout(False)
        Me.grpFwdSettings.PerformLayout()
        CType(Me.FwdRVBScaleFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FwdDeltaVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRevSettings.ResumeLayout(False)
        Me.grpRevSettings.PerformLayout()
        CType(Me.RevDeltaVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RevRVBScaleFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RVBMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RVBMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProtocolBox.ResumeLayout(False)
        Me.ProtocolBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblLocalVoltageValue As System.Windows.Forms.Label
    Friend WithEvents txtWrite As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AddressBox As System.Windows.Forms.GroupBox
    Friend WithEvents iec61850box As System.Windows.Forms.RadioButton
    Friend WithEvents modbusbox As System.Windows.Forms.RadioButton
    Friend WithEvents dnpbutton As System.Windows.Forms.RadioButton
    Friend WithEvents NumericUpDownDNPDestinationAddress As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbldestination As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownDNPSourceAddress As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblsource As System.Windows.Forms.Label
    Friend WithEvents lblwarning As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownModbusFwdRVBVoltageRegister As System.Windows.Forms.NumericUpDown
    Friend WithEvents Modbus_F_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownModbusLocalVoltageRegister As System.Windows.Forms.NumericUpDown
    Friend WithEvents lbllocalvoltage As System.Windows.Forms.Label
    Friend WithEvents ProtocolParameters As System.Windows.Forms.GroupBox
    Friend WithEvents ProtocolBox As System.Windows.Forms.GroupBox
    Friend WithEvents heartbeattimer As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents F_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents FwdRVBScaleFactor As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtIECLocalVoltage As System.Windows.Forms.TextBox
    Friend WithEvents txtIECFwdRVBVoltage As System.Windows.Forms.TextBox
    Friend WithEvents IEC_F_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents IEC_LocalVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents Forward_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents FwdDeltaVoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents radUseDeltaVoltage As System.Windows.Forms.RadioButton
    Friend WithEvents radUseFixedVoltage As System.Windows.Forms.RadioButton
    Friend WithEvents grpFwdSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents R_RVBScaleFactor_Label As System.Windows.Forms.Label
    Friend WithEvents RevRVBScaleFactor As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtIECRevRVBVoltage As System.Windows.Forms.TextBox
    Friend WithEvents IEC_R_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownModbusRevRVBVoltageRegister As System.Windows.Forms.NumericUpDown
    Friend WithEvents Modbus_R_RVBVoltage_Label As System.Windows.Forms.Label
    Friend WithEvents Reverse_Voltage_Label As System.Windows.Forms.Label
    Friend WithEvents RevDeltaVoltage As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtRead As System.Windows.Forms.TextBox
    Friend WithEvents SourceIPAddressLabel As System.Windows.Forms.Label
    Friend WithEvents grpRevSettings As System.Windows.Forms.GroupBox
    Friend WithEvents RVBMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents RVBMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRevRVBValue As System.Windows.Forms.Label
    Friend WithEvents lblMsgCenter As System.Windows.Forms.Label
    Friend WithEvents lblFwdRVBValue As System.Windows.Forms.Label

End Class
