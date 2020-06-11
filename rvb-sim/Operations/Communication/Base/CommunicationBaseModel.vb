''' <summary>
''' Holds common communication properties
''' </summary>
Public MustInherit Class CommunicationBaseModel(Of T)

#Region "Properties"

    ''' <summary>
    ''' Holds "Id" of the regulator for the model
    ''' </summary>
    ''' <returns></returns>
    Public Property Id() As UShort = 0

    ''' <summary>
    ''' Holds "Name" of the regulator for the model
    ''' </summary>
    ''' <returns></returns>
    Public Property Name() As String = String.Empty

    ''' <summary>
    ''' Holds "Port"
    ''' </summary>
    ''' <returns></returns>
    Public Property Port() As UShort = 0

    ''' <summary>
    ''' Holds "Local Voltage"
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property LocalVoltage() As UShort

    ''' <summary>
    ''' Holds "Source Voltage"
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property SourceVoltage() As UShort

    ''' <summary>
    ''' Holds "RVBEnable"
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RVBEnable() As UShort

    ''' <summary>
    ''' Holds "FRVBValue"
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property FRVBValue() As UShort

    ''' <summary>
    ''' Holds "FRVBScale" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property FRVBScale() As UShort

    ''' <summary>
    ''' Holds "RVBHeartBeatTimer" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RVBHeartBeatTimer() As UShort

    ''' <summary>
    ''' Holds "RRVBValue" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RRVBValue() As UShort

    ''' <summary>
    ''' Holds "RRVBScale" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RRVBScale() As UShort

    ''' <summary>
    ''' Holds "RVBMax" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RVBMax() As UShort

    ''' <summary>
    ''' Holds "RVBMin" 
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property RVBMin() As UShort

#End Region

End Class
