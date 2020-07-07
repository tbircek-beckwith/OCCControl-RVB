Imports Newtonsoft.Json

Public Class NewJsonTest

    ' JsonProperty names need to match the .json file and are case insensitive

    <JsonProperty("Regulator")>
    Public Property Regulator() As List(Of NewJsonRegulator)

End Class
