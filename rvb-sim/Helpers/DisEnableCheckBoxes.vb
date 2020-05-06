Imports System.Threading

Module DisEnableCheckBoxes

    ''' Enables or disables controls per Start button
    Friend Sub Disenable()

        Try
            With RVBSim.btnStart
                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} Disenable", Thread.CurrentThread.GetHashCode)

                'dnp settings dis/enable
                SetEnable(RVBSim.NumericUpDownDNPSourceAddress, .Enabled)
                SetEnable(RVBSim.NumericUpDownDNPDestinationAddress, .Enabled)

                'modbus settings dis/enable
                SetEnable(RVBSim.NumericUpDownModbusLocalVoltageRegister, .Enabled)
                SetEnable(RVBSim.NumericUpDownModbusFwdRVBVoltageRegister, .Enabled)
                SetEnable(RVBSim.NumericUpDownModbusRevRVBVoltageRegister, .Enabled)

                'iec61850 settings dis/enable
                SetEnable(RVBSim.txtIECLocalVoltage, .Enabled)
                SetEnable(RVBSim.txtIECFwdRVBVoltage, .Enabled)
                SetEnable(RVBSim.txtIECRevRVBVoltage, .Enabled)

                'communication settings dis/enable
                SetEnable(RVBSim.txtWrite, .Enabled)
                SetEnable(RVBSim.txtRead, .Enabled)
                SetEnable(RVBSim.txtPort, .Enabled)

                'protocol options dis/enable
                SetEnable(RVBSim.dnpbutton, .Enabled)
                SetEnable(RVBSim.modbusbox, .Enabled)
                SetEnable(RVBSim.iec61850box, .Enabled)

                'general rvb settings dis/enable
                SetEnable(RVBSim.heartbeattimer, .Enabled)
                SetEnable(RVBSim.radUseDeltaVoltage, .Enabled)
                SetEnable(RVBSim.radUseFixedVoltage, .Enabled)
                SetEnable(RVBSim.FwdRVBScaleFactor, .Enabled)
                SetEnable(RVBSim.RVBMax, .Enabled)
                SetEnable(RVBSim.RVBMin, .Enabled)
                SetEnable(RVBSim.RevRVBScaleFactor, .Enabled)

                If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- Disenable", Thread.CurrentThread.GetHashCode)

            End With

        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        End Try
    End Sub

End Module
