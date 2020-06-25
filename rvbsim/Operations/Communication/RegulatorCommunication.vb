Imports System.Collections.ObjectModel
''' <summary>
''' Holds all information about the regulator(s)
''' </summary>
Public Class RegulatorCommunication
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

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    ' Throw New NotImplementedException()
    '    ' Dim combine As IEnumerator
    '    'Return New RegulatorEnumerator()

    '    Dim output As List(Of Object) = New List(Of Object)

    '    output.AddRange(DnpCommunication)
    '    output.AddRange(ModbusCommunication)
    '    output.AddRange(IECCommunication)

    '    Return output
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Private Class RegulatorEnumerator
        Implements IEnumerator

        ' Private _regulator As List(Of T)
        Private ReadOnly _dnpCommunication As ObservableCollection(Of DnpCommunicationModel)
        Private ReadOnly _modbusCommunication() As ObservableCollection(Of ModbusCommunicationModel)
        Private ReadOnly _iecCommunication As ObservableCollection(Of IECCommunicationModel)
        ' Private ReadOnly _position As Integer = -1

        Public Sub New(ByVal modbusCommunication() As ObservableCollection(Of ModbusCommunicationModel), ByVal iecCommunication As ObservableCollection(Of IECCommunicationModel), ByVal dnpCommunication As ObservableCollection(Of DnpCommunicationModel))
            _modbusCommunication = modbusCommunication
            _iecCommunication = iecCommunication
            _dnpCommunication = dnpCommunication
        End Sub

        Public ReadOnly Property Current As Object Implements IEnumerator.Current
            Get
                Throw New NotImplementedException()
                'Return
            End Get
        End Property

        Public Sub Reset() Implements IEnumerator.Reset
            Throw New NotImplementedException()
        End Sub

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Throw New NotImplementedException()
        End Function
    End Class

End Class

