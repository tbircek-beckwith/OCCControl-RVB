Imports Newtonsoft.Json

Public Class IecProtocolSettingsModel
    Inherits SettingsProtocolBaseModel

    <JsonProperty("ReadIedName")>
    Public Property ReadIedName() As String

    <JsonProperty("WriteIedName")>
    Public Property WriteIedName() As String

End Class
