Imports Newtonsoft.Json

Public Class NewJsonRange

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Max")>
    Public Property Max() As Integer

    <JsonProperty("Min")>
    Public Property Min() As Integer

End Class
