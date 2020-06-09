''' <summary>
''' Holds iec61850 communication specific properties
''' </summary>
Public Class IECCommunicationModel '(Of T)
    ' Inherits CommunicationBaseModel(Of String)

#Region "Properties"

    ''' <summary>
    ''' Holds "Id" of the regulator for the model
    ''' </summary>
    ''' <returns></returns>
    Public Property Id() As UShort = 0

    ''' <summary>
    ''' Holds "Id" of the regulator for the model
    ''' </summary>
    ''' <returns></returns>
    Public Property Name() As String = String.Empty

    ''' <summary>
    ''' Holds "Port"
    ''' </summary>
    ''' <returns></returns>
    Public Property Port() As UShort = 0

    ''' <summary>
    ''' IEC61850 IED name to read
    ''' </summary>
    ''' <returns></returns>
    Public Property ReadIEDName() As String = String.Empty

    ''' <summary>
    ''' IEC61850 IED name to write 
    ''' </summary>
    ''' <returns></returns>
    Public Property WriteIEDName() As String = String.Empty

    ''' <summary>
    ''' IEC61850 IED class to use in communications
    ''' </summary>
    ''' <returns></returns>
    Public Property IEDClass() As String = String.Empty

    ''' <summary>
    ''' Holds "Local Voltage"
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property LocalVoltage() As String = String.Empty

    ''' <summary>
    ''' Holds "Source Voltage"
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property SourceVoltage() As String = String.Empty

    ''' <summary>
    ''' Holds "RVBEnable"
    ''' </summary>
    ''' <returns></returns>
    Public Shadows Property RVBEnable() As String = String.Empty

    ''' <summary>
    ''' Holds "FRVBValue"
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property FRVBValue() As String = String.Empty

    ''' <summary>
    ''' Holds "FRVBScale" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property FRVBScale() As String = String.Empty

    ''' <summary>
    ''' Holds "RVBHeartBeatTimer" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property RVBHeartBeatTimer() As String = String.Empty

    ''' <summary>
    ''' Holds "RRVBValue" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property RRVBValue() As String = String.Empty

    ''' <summary>
    ''' Holds "RRVBScale" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property RRVBScale() As String = String.Empty

    ''' <summary>
    ''' Holds "RVBMax" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property RVBMax() As String = String.Empty

    ''' <summary>
    ''' Holds "RVBMin" 
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Property RVBMin() As String = String.Empty

#End Region

End Class
