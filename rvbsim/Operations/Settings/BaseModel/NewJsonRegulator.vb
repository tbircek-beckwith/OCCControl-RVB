Imports Newtonsoft.Json

Public Class NewJsonRegulator

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Id")>
    Public Property Id() As String

    <JsonProperty("Name")>
    Public Property Name() As String

    <JsonProperty("Values")>
    Public Property Values() As List(Of NewJsonValue)

End Class
