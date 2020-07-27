''' <summary>
''' Holds modbus communication specific properties
''' </summary>
Public Class ModbusCommunicationModel
    Inherits CommunicationBaseModel(Of UShort)

#Region "Properties"

    '''' <summary>
    '''' Holds "Port" information.
    '''' </summary>
    '''' <returns></returns>
    'Public Property Port() As UShort

    ''' <summary>
    ''' Holds "Factory setting" information.
    ''' </summary>
    ''' <returns></returns>
    Public Property Factory() As UShort

    ''' <summary>
    ''' Holds "RVB Active" information.
    ''' </summary>
    ''' <returns></returns>
    Public Property RVBActive() As UShort

#End Region

End Class
