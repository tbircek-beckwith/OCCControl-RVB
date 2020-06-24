Imports Newtonsoft.Json

Public Class JsonRegulatorModel

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Id")>
    Public Property Id() As Integer

    <JsonProperty("Value")>
    Public Property Value() As Integer


End Class
