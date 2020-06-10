Imports System.Reflection
Imports System.Threading

Module Populate

    Friend Sub Populatetheform()
        Try
            Dim xmlRead As New ReadXmlFile
            xmlRead.Read()
            RVBSim.Text = $"RVB Simulator({Assembly.GetEntryAssembly().GetName().Version.ToString(3)})"

            ' Dim iecTextboxes = RVBSim.AddressBox.GetChildControls(Of TextBox)().Where(Function(tb) tb.Name.StartsWith("Iec"))
            ' Dim dnpNumericUpDown = RVBSim.AddressBox.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.StartsWith("Dnp"))
            ' Dim modbusNumericUpDown = RVBSim.AddressBox.GetChildControls(Of NumericUpDown)().Where(Function(tb) tb.Name.StartsWith("Modbus"))

            '' returns every control
            'Dim somethingSuper = RVBSim.AddressBox.GetChildControls(Of Control)()

            'For Each control As Control In somethingSuper
            '    Debug.WriteLine($"control.name ={control.Name}, control.text = {control.Text}, control.type = {control.GetType()}")
            'Next

            ' Dim counter As Integer = 1
            'Dim someList As List(Of Object) = New List(Of Object)
            'For Each something As Regulator In Regulators
            '    someList.AddRange(something.DnpCommunication)
            '    someList.AddRange(something.ModbusCommunication)
            '    someList.AddRange(something.IECCommunication)
            'Next
            'For Each model In someList

            SetValues(True)
            'Dim item As Type = model.GetType()
            'Dim props() As PropertyInfo = item.GetProperties()

            'Debug.WriteLine($"Properties (N = {props.Length})")
            'For Each prop In props
            '    If prop.GetIndexParameters().Length = 0 Then

            '        Dim t() As Control = RVBSim.Controls.Find($"{model.Name}{prop.Name}Reg{model.Id}", True)

            '        If t.Length > 0 Then
            '            Debug.Write($"control: {model.Name}{prop.Name}Reg{model.Id}: old text: {t(0).Text} -- ")
            '            t(0).Text = prop.GetValue(model)
            '            Debug.WriteLine($"new text: {t(0).Text}")
            '        End If
            '    End If
            'Next
            'Next
            Debug.WriteLine("Stop here")

            'For Each communicationModels As Regulator In Regulators
            '    Dim i As Integer = 1
            '    ' fill up dnp items in the ui
            '    For Each model In communicationModels.DnpCommunication

            '        Debug.WriteLine($"model: {model}")

            '        Dim item As Type = model.GetType()
            '        Dim props() As PropertyInfo = item.GetProperties()

            '        Debug.WriteLine($"Properties (N = {props.Length})")
            '        For Each prop In props
            '            If prop.GetIndexParameters().Length = 0 Then

            '                Dim t() As Control = RVBSim.Controls.Find($"{model.Name}{prop.Name}Reg{i}", True)

            '                If t.Length > 0 Then
            '                    Debug.Write($"control: DNP{prop.Name}Reg{i}: old text: {t(0).Text} -- ")
            '                    t(0).Text = prop.GetValue(model)
            '                    Debug.WriteLine($"new text: {t(0).Text}")
            '                End If
            '            End If
            '        Next

            '        i += 1
            '    Next
            '    i = 1
            '    For Each model In communicationModels.ModbusCommunication
            '        Debug.WriteLine($"model: {model}")
            '        ' Debug.WriteLine($"control: {RVBSim.ControlCollection(0)}")

            '        Dim item As Type = model.GetType()
            '        Dim props() As PropertyInfo = item.GetProperties()

            '        Debug.WriteLine($"Properties (N = {props.Length})")
            '        For Each prop In props
            '            If prop.GetIndexParameters().Length = 0 Then

            '                Dim t() As Control = RVBSim.Controls.Find($"{model.Name}{prop.Name}Reg{i}", True)

            '                If t.Length > 0 Then
            '                    Debug.Write($"control: Modbus{prop.Name}Reg{i}: old text: {t(0).Text} -- ")
            '                    t(0).Text = prop.GetValue(model)
            '                    Debug.WriteLine($"new text: {t(0).Text}")
            '                End If
            '            End If
            '        Next

            '        i += 1
            '    Next
            '    i = 1
            '    For Each model In communicationModels.IECCommunication
            '        Debug.WriteLine($"model: {model}")
            '        ' Debug.WriteLine($"control: {RVBSim.ControlCollection(0)}")

            '        Dim item As Type = model.GetType()
            '        Dim props() As PropertyInfo = item.GetProperties()

            '        Debug.WriteLine($"Properties (N = {props.Length})")
            '        For Each prop In props
            '            If prop.GetIndexParameters().Length = 0 Then
            '                'If prop.PropertyType.Name = "String" Then
            '                Dim t() As Control = RVBSim.Controls.Find($"{model.Name}{prop.Name}Reg{i}", True)

            '                    If t.Length > 0 Then
            '                        Debug.Write($"control: IEC{prop.Name}Reg{i}: old text: {t(0).Text} -- ")
            '                        t(0).Text = prop.GetValue(model)
            '                        Debug.WriteLine($"new text: {t(0).Text}")
            '                    End If
            '                'End If
            '            End If
            '        Next


            '        i += 1
            '    Next
            'Next


            ''If CInt(SupportedRVBRevision) >= 15 Then
            ''    support = True
            ''    RVBSim.grpRevSettings.Visible = True
            ''Else
            ''    support = False
            ''    RVBSim.grpRevSettings.Visible = False
            ''End If

            '' With My.Settings
            'RVBSim.DNPSourceAddress.Value = dnpSetting.source              '.source
            'RVBSim.DNPDestinationAddress.Value = dnpSetting.destination   '.destination

            'RVBSim.ModbusMeteringLocalVoltage.Value = modbusRegister.LocalVoltage  '.mdLocalvoltage
            'RVBSim.ModbusFwdRVBVoltageReg1.Value = modbusRegister.FRVBValue  '.mdFRVBvoltage
            'RVBSim.ModbusRevRVBVoltageReg1.Value = modbusRegister.RRVBValue  '.mdRRVBvoltage

            'RVBSim.heartBeatTimer.Value = testSetting.HeartbeatTimer                  '.heartbeat
            'If testSetting.FwdRVBVoltage < MinDeltaVoltage Or testSetting.FwdRVBVoltage > MaxDeltaVoltage Then RVBSim.FwdDeltaVoltageReg1.Value = 0.0 Else RVBSim.FwdDeltaVoltageReg1.Value = testSetting.FwdRVBVoltage
            'If testSetting.RevRVBVoltage < MinDeltaVoltage Or testSetting.RevRVBVoltage > MaxDeltaVoltage Then RVBSim.RevDeltaVoltageReg1.Value = 0.0 Else RVBSim.RevDeltaVoltageReg1.Value = testSetting.RevRVBVoltage

            'RVBSim.FwdRVBScaleFactorReg1.Value = testSetting.FwdRVBVoltageScale  '.Fmultiplier
            'RVBSim.RevRVBScaleFactorReg1.Value = testSetting.RevRVBVoltageScale   '.Rmultiplier
            'RVBSim.IecMeteringLocalVoltage.Text = iecSetting.LocalVoltage       '.IECLocalVoltage
            'RVBSim.IecFwdRVBVoltageReg1.Text = iecSetting.FRVBValue         ' .IECFwdRVBVoltage
            'RVBSim.IecRevRVBVoltageReg1.Text = iecSetting.RRVBValue         ' .IECRevRVBVoltage

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
                        RVBSim.DesktopLocation = New Point(20, 20)
                    Else
                        RVBSim.DesktopLocation = .location
                    End If
                Else
                    RVBSim.DesktopLocation = New Point(32000, 32000)
                    ' Me.DesktopLocation = New System.Drawing.Point(20, 20)
                End If
                '************************************************************
            End With
            Select Case testSetting.Protocol    '.protocol
                Case "dnp"
                    RVBSim.dnpbutton.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).DnpCommunication(0).Port ' dnpSetting.Port  '.dnpport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.dnpbutton)
                Case "modbus"
                    RVBSim.modbusbox.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).ModbusCommunication(0).Port ' modbusRegister.Port  '.mdport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.modbusbox)
                Case "iec"
                    RVBSim.iec61850box.Checked = True
                    RVBSim.PortReg1.Text = Regulators(0).IECCommunication(0).Port ' iecSetting.Port '.iecport
                    Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")
                    RVBSim.CheckHandler(RVBSim.iec61850box)
            End Select
            RVBSim.ReadIpAddr.Text = testSetting.readIpAddress  '.IPAddressToRead
            RVBSim.WriteIpAddr.Text = testSetting.writeIpAddress
            '
        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(Populatetheform)}")
        End Try
    End Sub

End Module
