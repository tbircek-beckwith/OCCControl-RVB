Imports System.IO
Imports System.Threading

Module FormOperations

    Friend Sub CloseForm()    'sender As Object, e As System.Windows.Forms.FormClosingEventArgs)
        Try
            'due to IEC61850 protocol we need to close communication certain way
            If Not RVBSim.StartButton.Enabled Then pause.Pause()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            With My.Settings
                .location = RVBSim.DesktopLocation
                If RVBSim.dnpbutton.Checked Then
                    .dnphost = RVBSim.WriteIpAddr.Text
                    .dnpport = CUShort(RVBSim.PortReg1.Text)
                    .protocol = "dnp"
                    .source = CUShort(RVBSim.DNPSourceReg1.Value)
                    .destination = CUShort(RVBSim.DNPDestinationReg1.Value)
                ElseIf RVBSim.modbusbox.Checked Then
                    .mdhost = RVBSim.WriteIpAddr.Text
                    .mdport = CUShort(RVBSim.PortReg1.Text)
                    .protocol = "modbus"
                    .mdLocalvoltage = CUShort(RVBSim.ModbusLocalVoltageReg1.Value)
                    .mdSrcVoltage = CUShort(RVBSim.ModbusSourceVoltageReg1.Value)
                    .mdFRVBvoltage = CUShort(RVBSim.ModbusFRVBValueReg1.Value)
                    .mdRRVBvoltage = CUShort(RVBSim.ModbusRRVBValueReg1.Value)
                ElseIf RVBSim.iec61850box.Checked Then
                    .iechost = RVBSim.WriteIpAddr.Text
                    .iecport = CUShort(RVBSim.PortReg1.Text)
                    .protocol = "iec"
                    .IECLocalVoltage = RVBSim.IecLocalVoltageReg1.Text
                    .IECFwdRVBVoltage = RVBSim.IecFRVBValueReg1.Text
                    .IECRevRVBVoltage = RVBSim.IecRRVBValueReg1.Text
                End If
                .heartbeat = CUShort(RVBSim.SettingsHeartbeatTimerReg1.Value)
                .Fdeltavoltage = CDbl(RVBSim.SettingsFwdRVBVoltageReg1.Value)
                .Fmultiplier = CDbl(RVBSim.SettingsFwdScaleFactorReg1.Value)
                .Rdeltavoltage = CDbl(RVBSim.SettingsRevRVBVoltageReg1.Value)
                .Rmultiplier = CDbl(RVBSim.SettingsRevScaleFactorReg1.Value)
                .IPAddressToRead = RVBSim.WriteIpAddr.Text
            End With

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)
        Finally

            Dim logFilePath = Path.Combine(path1:=My.Application.Info.DirectoryPath, path2:="Log.txt")

            My.Computer.FileSystem.WriteAllText(file:=logFilePath, text:=sb.ToString, append:=False)

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(CloseForm)}")
        End Try

    End Sub

End Module
