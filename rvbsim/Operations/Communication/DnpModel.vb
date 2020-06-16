''' <summary>
''' Holds dnp communication specific properties
''' </summary>
Public Class DnpCommunicationModel
    Inherits CommunicationBaseModel(Of UShort)

#Region "Properties"

    '''' <summary>
    '''' Holds "Port" information.
    '''' </summary>
    '''' <returns></returns>
    'Public Property Port() As UShort

    ''' <summary>
    ''' Holds "Source Address" information.
    ''' </summary>
    ''' <returns></returns>
    Public Property SourceAddress() As UShort

    ''' <summary>
    ''' Holds "Remote Address" information.
    ''' </summary>
    ''' <returns></returns>
    Public Property DestinationAddress() As UShort

#End Region

End Class
