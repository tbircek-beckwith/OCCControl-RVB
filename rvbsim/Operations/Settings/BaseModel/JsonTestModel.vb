Imports Newtonsoft.Json

Public Class JsonTestModel

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("FwdRVBVoltage")>
    Public Property FwdRvbVoltage As List(Of JsonTestItemModel)

    <JsonProperty("FwdScaleFactor")>
    Public Property FwdScaleFactor As List(Of JsonTestItemModel)

    <JsonProperty("HeartbeatTimer")>
    Public Property HeartbeatTimer As List(Of JsonTestItemModel)

    <JsonProperty("RevRVBVoltage")>
    Public Property RevRvbVoltage As List(Of JsonTestItemModel)

    <JsonProperty("RevScaleFactor")>
    Public Property RevScaleFactor As List(Of JsonTestItemModel)

    <JsonProperty("RVBMax")>
    Public Property RvbMax As List(Of JsonTestItemModel)

    <JsonProperty("RVBMin")>
    Public Property RvbMin As List(Of JsonTestItemModel)

    <JsonProperty("UseRelative")>
    Public Property UseRelative As List(Of JsonTestItemModel)


End Class
