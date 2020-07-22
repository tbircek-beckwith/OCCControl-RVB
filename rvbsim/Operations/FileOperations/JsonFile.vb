Imports System.IO
Imports Newtonsoft.Json

Public Class JsonFile

#Region "Public Methods"

    Public Sub New()

        Dim baseFileLocation As String = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                      path2:="resources",
                                                      path3:=$"{SettingFileName}.json")

        ' load general setting file
        baseJsonSettings = GetSettings(Of JsonRoot)(baseFileLocation)


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

        Try

            Dim fileJson As T
            Using reader = New StreamReader(fileLocation)

                fileJson = JsonConvert.DeserializeObject(Of T)(reader.ReadToEnd())
            End Using

            Return fileJson

        Catch ex As Exception
            Dim message As String = $"{Now}: ({NameOf(GetSettings)}) {vbCrLf}{ex.Message}"  '{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)

            Return Nothing
        End Try

    End Function
#End Region
End Class
