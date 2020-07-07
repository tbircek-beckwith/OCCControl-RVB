Imports Newtonsoft.Json

Public Class JsonRoot

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Date")>
    Public Property [Date]() As String

    <JsonProperty("Description")>
    Public Property Description() As String

    <JsonProperty("FileVersion")>
    Public Property FileVersion() As String

    <JsonProperty("Protocol")>
    Public Property Protocol() As String

    <JsonProperty("Read")>
    Public Property Read() As String

    <JsonProperty("SinglePhase")>
    Public Property SinglePhase() As Boolean

    <JsonProperty("Title")>
    Public Property Title() As String

    <JsonProperty("UIVisible")>
    Public Property UIVisible() As Boolean

    <JsonProperty("Write")>
    Public Property Write() As String

    <JsonProperty("Test")>
    Public Property Test() As JsonTest

    '
    ' Communication specific properties.
    '

    <JsonProperty("Port")>
    Public Property Port() As Integer

    <JsonProperty("Factory")>
    Public Property Factory() As Integer

    <JsonProperty("Source")>
    Public Property Source() As Integer

    <JsonProperty("Destination")>
    Public Property Destination() As Integer

    <JsonProperty("ReadIEDName")>
    Public Property ReadIEDName() As String

    <JsonProperty("WriteIEDName")>
    Public Property WriteIEDName() As String

End Class
