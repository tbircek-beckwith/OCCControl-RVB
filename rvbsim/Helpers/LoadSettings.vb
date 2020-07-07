Imports System.IO

Public Class LoadSettings

    Public Sub New()

    End Sub

    Public Sub New(Optional ByVal protocol As String = "") ' As Boolean

        'jsonRead = New JsonFile()

        Dim isProtocolValid As Boolean = True

        'If String.IsNullOrWhiteSpace(protocol) Then
        '    ProtocolInUse = baseJsonSettings.Protocol
        'End If

        testJsonSettings = jsonRead.GetSettings(Of NewJsonRoot)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{ProtocolInUse} - Copy.json"))

        testJsonSettingsRegulators = testJsonSettings.Test

        Select Case ProtocolInUse
            Case "dnp"
                'testJsonValues = New DnpProtocolSettingsModel()
                ' testJsonValues = jsonRead.GetSettings(Of NewJsonRoot)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{ProtocolInUse} - Copy.json")).Test
                ' RVBSim.dnpbutton.Checked = True
                ' RVBSim.PortReg1.Text = CType(testJsonValues, DnpProtocolSettingsModel).Port
                ' ProtocolInUse = "dnp"
                RVBSim.CheckHandler(RVBSim.dnpbutton)
            Case "modbus"
                ' testJsonValues = New ModbusProtocolSettingsModel()
                'testJsonValues = jsonRead.GetSettings(Of NewJsonRoot)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{ProtocolInUse} - Copy.json"))
                ' RVBSim.modbusbox.Checked = True
                ' RVBSim.PortReg1.Text = CType(testJsonValues, ModbusProtocolSettingsModel).Port
                'ProtocolInUse = "modbus"
                RVBSim.CheckHandler(RVBSim.modbusbox)
            Case "iec"
                ' testJsonValues = New IecProtocolSettingsModel()
                ' testJsonValues = jsonRead.GetSettings(Of IecProtocolSettingsModel)(Path.Combine(path1:=BaseJsonSettingsFileLocation, path2:=$"Settings-{ProtocolInUse}.json"))
                ' RVBSim.iec61850box.Checked = True
                'RVBSim.PortReg1.Text = CType(testJsonValues, IecProtocolSettingsModel).Port
                'ProtocolInUse = "iec"
                RVBSim.CheckHandler(RVBSim.iec61850box)
            Case Else
                MessageBox.Show(text:=$"Please check Settings.json file protocol value.{vbCrLf}Read value: {testSetting.Protocol} <- not accurate", caption:="Unsupported Protocol", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
                isProtocolValid = False
                Exit Select
        End Select

        RVBSim.UpdateProtocol()

        Debug.WriteLine($"port value: {RVBSim.PortReg1.Text}")

        ' Return isProtocolValid
    End Sub

End Class
