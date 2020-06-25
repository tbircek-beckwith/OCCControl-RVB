Imports Newtonsoft.Json

Public Class ModbusProtocolSettingsModel
    Inherits SettingsProtocolBaseModel

    <JsonProperty("Factory")>
    Public Property Factory() As Integer

End Class
