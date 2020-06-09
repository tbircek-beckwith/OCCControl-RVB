
Imports System.Runtime.CompilerServices

Public Module ExtensionMethods
    <Extension()>
    Public Function GetChildControls(Of T As Control)(ByVal parent As Control) As List(Of T)
        Dim result As New List(Of Control)

        For Each control As Control In parent.Controls
            If TypeOf control Is T Then result.Add(control)
            result.AddRange(control.GetChildControls(Of T)())
        Next
        Return result.ToArray().Select(Of T)(Function(arg1) CType(arg1, T)).ToList()
    End Function
End Module
