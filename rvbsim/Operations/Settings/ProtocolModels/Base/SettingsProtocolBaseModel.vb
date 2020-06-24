Imports Newtonsoft.Json

Public Class SettingsProtocolBaseModel

    <JsonProperty("Id")>
    Public Property Id() As String

    <JsonProperty("Title")>
    Public Property Title() As String

    <JsonProperty("Version")>
    Public Property Version() As String

    <JsonProperty("Date")>
    Public Property [Date]() As String

    <JsonProperty("Port")>
    Public Property Port() As Integer

    <JsonProperty("Settings")>
    Public Property Settings() As List(Of SettingsJsonValuesBaseModel)

End Class
