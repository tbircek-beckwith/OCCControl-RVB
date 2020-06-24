Imports Newtonsoft.Json

Public Class JsonSettingsModel

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Title")>
    Public Property Title() As String

    <JsonProperty("FileVersion")>
    Public Property FileVersion() As String

    <JsonProperty("Date")>
    Public Property [Date]() As String

    <JsonProperty("Protocol")>
    Public Property Protocol() As String

    <JsonProperty("UIVisible")>
    Public Property UIVisible() As Boolean

    <JsonProperty("SinglePhase")>
    Public Property SinglePhase() As Boolean

    <JsonProperty("Read")>
    Public Property Read() As String

    <JsonProperty("Write")>
    Public Property Write() As String

    <JsonProperty("Test")>
    Public Property Test() As JsonTestModel


End Class
