Imports System.IO
Imports System.Threading

Module FormOperations

    Friend Sub CloseForm()    'sender As Object, e As System.Windows.Forms.FormClosingEventArgs)
        Try
            'due to IEC61850 protocol we need to close communication certain way
            If Not RVBSim.btnStart.Enabled Then pause.Pause()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            With My.Settings
                .location = RVBSim.DesktopLocation
                If RVBSim.dnpbutton.Checked Then
                    .dnphost = RVBSim.txtWrite.Text
                    .dnpport = CUShort(RVBSim.txtPort.Text)
                    .protocol = "dnp"
                    .source = CUShort(RVBSim.NumericUpDownDNPSourceAddress.Value)
                    .destination = CUShort(RVBSim.NumericUpDownDNPDestinationAddress.Value)
                ElseIf RVBSim.modbusbox.Checked Then
                    .mdhost = RVBSim.txtWrite.Text
                    .mdport = CUShort(RVBSim.txtPort.Text)
                    .protocol = "modbus"
                    .mdLocalvoltage = CUShort(RVBSim.NumericUpDownModbusLocalVoltageRegister.Value)
                    .mdFRVBvoltage = CUShort(RVBSim.NumericUpDownModbusFwdRVBVoltageRegister.Value)
                    .mdRRVBvoltage = CUShort(RVBSim.NumericUpDownModbusRevRVBVoltageRegister.Value)
                ElseIf RVBSim.iec61850box.Checked Then
                    .iechost = RVBSim.txtWrite.Text
                    .iecport = CUShort(RVBSim.txtPort.Text)
                    .protocol = "iec"
                    .IECLocalVoltage = RVBSim.txtIECLocalVoltage.Text
                    .IECFwdRVBVoltage = RVBSim.txtIECFwdRVBVoltage.Text
                    .IECRevRVBVoltage = RVBSim.txtIECRevRVBVoltage.Text
                End If
                .heartbeat = CUShort(RVBSim.heartbeattimer.Value)
                .Fdeltavoltage = CDbl(RVBSim.FwdDeltaVoltage.Value)
                .Fmultiplier = CDbl(RVBSim.FwdRVBScaleFactor.Value)
                .Rdeltavoltage = CDbl(RVBSim.RevDeltaVoltage.Value)
                .Rmultiplier = CDbl(RVBSim.RevRVBScaleFactor.Value)
                .IPAddressToRead = RVBSim.txtWrite.Text
            End With

        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally

            Dim logFilePath = Path.Combine(path1:=My.Application.Info.DirectoryPath, path2:="Log.txt")

            My.Computer.FileSystem.WriteAllText(logFilePath, sb.ToString, False)
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(CloseForm)}")
        End Try

    End Sub

End Module
