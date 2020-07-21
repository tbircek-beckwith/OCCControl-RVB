
Imports System.Threading

''' <summary>
''' updates metering values in the specified form
''' </summary>
Public Class UpdateMeteringValues

    ''' <summary>
    ''' default constructor
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' updates metering values.
    ''' </summary>
    ''' <param name="rvbForm">the form where the labels are located.</param>
    ''' <param name="registerBox">the active register.</param>
    ''' <param name="regulatorId">the active regulator.</param>
    Public Sub New(rvbForm As RVBSim, registerBox As NumericUpDown, regulatorId As Integer)

        Dim labelDirection As String = String.Empty
        Dim opType As String = String.Empty
        Dim value As Double = 0.0

        If registerBox.Name.Contains("FRVB") Then
            labelDirection = "Fwd"
            opType = "Writing"
            value = Interlocked.Read(FwdRVBVoltages2Write(regulatorId)) / BecoCommunicationScaleFactor

        ElseIf registerBox.Name.Contains("RRVB") Then
            labelDirection = "Rev"
            opType = "Writing"
            value = Interlocked.Read(RevRVBVoltages2Write(regulatorId)) / BecoCommunicationScaleFactor

        ElseIf registerBox.Name.Contains("Source") Then
            labelDirection = "Source"
            opType = "Reading"
            value = Interlocked.Read(SourceVoltageReadings.Item(regulatorId)) / BecoCommunicationScaleFactor

        ElseIf registerBox.Name.Contains("Local") Then
            labelDirection = "Local"
            opType = "Reading"
            value = Interlocked.Read(LocalVoltageReadings.Item(regulatorId)) / BecoCommunicationScaleFactor

        End If

        Dim labelControlName As String = $"{labelDirection}Voltage{opType}Regulator{regulatorId + 1}"

        Dim l() As Control = rvbForm.Controls.Find(labelControlName, True)

        If l.Length > 0 Then

            If l(0).Visible Then

                SetText(l(0), $"{labelDirection}: {FormatNumber(value, 1)}V")

            End If
        End If
    End Sub

End Class
