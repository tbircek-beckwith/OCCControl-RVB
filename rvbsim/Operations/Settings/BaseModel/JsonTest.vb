Imports Newtonsoft.Json

Public Class JsonTest

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Regulator")>
    Public Property Regulator() As List(Of JsonRegulator)

End Class
