Imports System.Threading

Module DisEnableCheckBoxes

    ''' Enables or disables controls per Start button
    Friend Sub Disenable()

        Try
            With RVBSim.StartButton
                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(Disenable)}")

                SetValues(.Enabled)

                ''dnp settings dis/enable
                'SetEnable(RVBSim.DNPSourceReg1, .Enabled)
                'SetEnable(RVBSim.DNPDestinationReg1, .Enabled)

                ''modbus settings dis/enable
                'SetEnable(RVBSim.ModbusLocalVoltageReg1, .Enabled)
                'SetEnable(RVBSim.ModbusFRVBValueReg1, .Enabled)
                'SetEnable(RVBSim.ModbusRRVBValueReg1, .Enabled)

                ''iec61850 settings dis/enable
                'SetEnable(RVBSim.IecLocalVoltageReg1, .Enabled)
                'SetEnable(RVBSim.IecFRVBValueReg1, .Enabled)
                'SetEnable(RVBSim.IecRRVBValueReg1, .Enabled)

                'communication settings dis/enable
                SetEnable(RVBSim.WriteIpAddr, .Enabled)
                SetEnable(RVBSim.ReadIpAddr, .Enabled)
                SetEnable(RVBSim.PortReg1, .Enabled)

                'protocol options dis/enable
                SetEnable(RVBSim.dnpbutton, .Enabled)
                SetEnable(RVBSim.modbusbox, .Enabled)
                SetEnable(RVBSim.iec61850box, .Enabled)

                'general rvb settings dis/enable
                SetEnable(RVBSim.heartBeatTimer, .Enabled)
                SetEnable(RVBSim.useDeltaVoltage, .Enabled)
                SetEnable(RVBSim.useFixedVoltage, .Enabled)
                SetEnable(RVBSim.FRVBScaleReg1, .Enabled)
                SetEnable(RVBSim.RVBMax, .Enabled)
                SetEnable(RVBSim.RVBMin, .Enabled)
                SetEnable(RVBSim.RevRVBScaleFactorReg1, .Enabled)

            End With

        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        End Try
    End Sub

End Module
