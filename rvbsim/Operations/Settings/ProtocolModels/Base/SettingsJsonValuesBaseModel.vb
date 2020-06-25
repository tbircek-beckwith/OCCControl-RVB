Imports Newtonsoft.Json

Public Class SettingsJsonValuesBaseModel

    <JsonProperty("Id")>
    Public Property Id() As String

    <JsonProperty("Name")>
    Public Property Name() As String

    <JsonProperty("Regulator")>
    Public Property Regulator() As List(Of RegulatorJsonValueBaseModel)

End Class
