Imports System.Collections.ObjectModel
''' <summary>
''' Holds all information about the regulator(s)
''' </summary>
Public Class Regulator

    ''' <summary>
    ''' Collection of dnp3.0 communication details
    ''' </summary>
    ''' <returns></returns>
    Public Property DnpCommunication As ObservableCollection(Of DnpCommunicationModel) = New ObservableCollection(Of DnpCommunicationModel)

    ''' <summary>
    ''' Collection of modbus communication details
    ''' </summary>
    ''' <returns></returns>
    Public Property ModbusCommunication As ObservableCollection(Of ModbusCommunicationModel) = New ObservableCollection(Of ModbusCommunicationModel)

    ''' <summary>
    ''' Collection of iec61850 communication details
    ''' </summary>
    ''' <returns></returns>
    Public Property IECCommunication As ObservableCollection(Of IECCommunicationModel) = New ObservableCollection(Of IECCommunicationModel)


End Class
