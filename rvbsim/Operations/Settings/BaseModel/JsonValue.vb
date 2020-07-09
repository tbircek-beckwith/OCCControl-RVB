Imports Newtonsoft.Json

Public Class JsonValue

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Id")>
    Public Property Id() As String

    <JsonProperty("Name")>
    Public Property Name() As String

    <JsonProperty("Description")>
    Public Property Description() As String

    <JsonProperty("Fc")>
    Public Property Fc() As String

    <JsonProperty("Sdi")>
    Public Property Sdi() As String

    <JsonProperty("Dai")>
    Public Property Dai() As String

    <JsonProperty("Max")>
    Public Property Max() As Integer

    <JsonProperty("Min")>
    Public Property Min() As Integer

    <JsonProperty("ScaleFactor")>
    Public Property ScaleFactor() As Integer

    <JsonProperty("Value")>
    Public Property Value() As String

    <JsonProperty("Unit")>
    Public Property Unit() As String

End Class
