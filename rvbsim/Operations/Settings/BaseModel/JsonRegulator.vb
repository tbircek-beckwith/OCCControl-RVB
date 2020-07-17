Imports Newtonsoft.Json

Public Class JsonRegulator

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Id")>
    Public Property Id() As Integer

    <JsonProperty("Name")>
    Public Property Name() As String

    <JsonProperty("Values")>
    Public Property Values() As List(Of JsonValue)

End Class
