Imports System.IO
Imports Newtonsoft.Json

Public Class JsonFile

#Region "Public Methods"

    Public Sub New()

        Dim baseFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                          path2:="resources",
                                                          path3:="Settings.json")

        Dim dnpFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                       path2:="resources",
                                                           path3:="Settings-dnp.json")

        Dim iecFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                      path2:="resources",
                                                          path3:="Settings-iec.json")

        Dim modbusFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                      path2:="resources",
                                                          path3:="Settings-modbus.json")

        baseJsonSettings = GetSettings(Of JsonRootModel)(baseFileLocation)

        testSetting.Title = baseJsonSettings.Title
        testSetting.FileVersion = baseJsonSettings.FileVersion
        testSetting.Date = baseJsonSettings.Date
        testSetting.IsUiVisible = baseJsonSettings.UIVisible
        testSetting.IsSinglePhase = baseJsonSettings.SinglePhase
        testSetting.Protocol = baseJsonSettings.Protocol
        testSetting.readIpAddress = baseJsonSettings.Read
        testSetting.writeIpAddress = baseJsonSettings.Write
        testSetting.FwdRVBVoltage = baseJsonSettings.Test.Regulator(0).FwdRvbVoltage(0).Value
        testSetting.FwdRVBVoltageScale = baseJsonSettings.Test.Regulator(0).FwdScaleFactor(0).Value
        testSetting.HeartbeatTimer = baseJsonSettings.Test.Regulator(0).HeartbeatTimer(0).Value
        testSetting.RevRVBVoltage = baseJsonSettings.Test.Regulator(0).RevRvbVoltage(0).Value
        testSetting.RevRVBVoltageScale = baseJsonSettings.Test.Regulator(0).RevScaleFactor(0).Value
        testSetting.RVBMax = baseJsonSettings.Test.Regulator(0).RvbMax(0).Value
        testSetting.RVBMin = baseJsonSettings.Test.Regulator(0).RvbMin(0).Value

        Select Case testSetting.Protocol
            Case "dnp"
                testJsonValues = GetRegulator(Of DnpProtocolSettingsModel)(dnpFileLocation, testSetting.Protocol)
            Case "modbus"
                testJsonValues = GetRegulator(Of ModbusProtocolSettingsModel)(modbusFileLocation, testSetting.Protocol)
            Case "iec"
                testJsonValues = GetRegulator(Of IecProtocolSettingsModel)(iecFileLocation, testSetting.Protocol)
            Case Else
                MessageBox.Show(text:=$"Please check Settings.json file protocol value.{vbCrLf}Read value: {testSetting.Protocol}", caption:="Unsupported Protocol", buttons:=MessageBoxButtons.OK, icon:=MessageBoxIcon.Error)
        End Select



    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="fileLocation"></param>
    ''' <returns></returns>
    Public Function GetSettings(Of T)(fileLocation As String) As T

        Dim fileJson As T
        Using reader = New StreamReader(fileLocation)

            fileJson = JsonConvert.DeserializeObject(Of T)(reader.ReadToEnd())
        End Using

        Return fileJson
    End Function
#End Region

#Region "Private Methods"
    Private Function GetRegulator(Of T)(communicationObject As List(Of SettingsJsonValuesBaseModel)) As T

        Dim query As T
        For index = 0 To 2
            Dim regulatorId As Integer = index

            query = From setting In communicationObject
                    Select setting.Regulator(regulatorId).Value

        Next
        Return Nothing
    End Function

    Private Function GetRegulator(Of T)(fileLocation As String, protocol As String) As T

        Dim settingProtocol ' = New List(Of T().Enumerator)  ' As T ' = GetSettings(Of T)(fileLocation)
        Dim queryList = New List(Of SettingsJsonValuesBaseModel) 'As List(T)
        'Dim settingType As Type = settingProtocol.GetType()

        Select Case GetType(T) ' settingProtocol.GetType()
            Case GetType(DnpProtocolSettingsModel)
                settingProtocol = GetSettings(Of DnpProtocolSettingsModel)(fileLocation)
                queryList = CType(settingProtocol, DnpProtocolSettingsModel).Settings
            Case GetType(ModbusProtocolSettingsModel)
                settingProtocol = GetSettings(Of ModbusProtocolSettingsModel)(fileLocation)
                queryList = CType(settingProtocol, ModbusProtocolSettingsModel).Settings
            Case GetType(IecProtocolSettingsModel)
                settingProtocol = GetSettings(Of IecProtocolSettingsModel)(fileLocation)
                queryList = CType(settingProtocol, IecProtocolSettingsModel).Settings
            Case Else
                Return Nothing
        End Select

        testJsonValues = settingProtocol
        ' Dim settingType As Type = settingProtocol.GetType()

        'For index = 0 To 2
        '    Dim regulatorId As Integer = index

        '    Dim query = From setting In queryList
        '                Select setting  '.Regulator(regulatorId)

        '    Dim regulatorValues = From regulator In query
        '                          Select regulator.Regulator(regulatorId).Value


        '    ' regulatorValues.ForEach

        '    Dim modbusModel = New ModbusCommunicationModel With {
        '                                        .Name = settingProtocol.Id,
        '                                        .Id = regulatorId + 1,
        '                                        .Factory = settingProtocol.Factory,
        '                                        .Port = settingProtocol.Port}
        '    '.FRVBValue = query(0).Value
        '    '}

        '    Debug.WriteLine($"regulator: {regulatorId}")    ', item: {item}")

        '    'Next
        'Next
    End Function
#End Region
End Class
