
Module MeteringLabels

    Friend Sub ResetMeteringLabels()

        ' try to get every metering Label in the form
        ' this is under Finally to prevent labels to maintain some values in case of Exceptions
        Dim labels = RVBSim.GetChildControls(Of Label)().Where(Function(x) x.Name.Contains("Writing") Or x.Name.Contains("Reading"))

        For Each label In labels
            SetText(label:=label, text:=String.Empty)
        Next

    End Sub

End Module