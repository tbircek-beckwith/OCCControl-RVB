Imports Newtonsoft.Json

Public Class DnpProtocolSettingsModel
    Inherits SettingsProtocolBaseModel

    <JsonProperty("Source")>
    Public Property Source() As Integer

    <JsonProperty("Destination")>
    Public Property Destination() As Integer

End Class
