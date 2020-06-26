Imports System.IO
Imports Newtonsoft.Json

Public Class JsonFile

#Region "Public Methods"

    Public Sub New()

        Dim baseFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                          path2:="resources",
                                                          path3:="Settings.json")

        ' Dim dnpFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
        '                                               path2:="resources",
        '                                                   path3:="Settings-dnp.json")

        'Dim iecFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
        '                                              path2:="resources",
        '                                                  path3:="Settings-iec.json")

        'Dim modbusFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
        '                                              path2:="resources",
        '                                                  path3:="Settings-modbus.json")

        baseJsonSettings = GetSettings(Of JsonRootModel)(baseFileLocation)
        ' dnpJson = GetSettings(Of DnpProtocolSettingsModel)(dnpFileLocation)
        'Dim iecJson = GetSettings(Of IecProtocolSettingsModel)(iecFileLocation)
        'Dim modbusJson = GetSettings(Of ModbusProtocolSettingsModel)(modbusFileLocation)

        Debug.WriteLine("something")

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

End Class
