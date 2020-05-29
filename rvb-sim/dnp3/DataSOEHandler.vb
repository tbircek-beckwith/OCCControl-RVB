
Imports Automatak.DNP3.Interface

''' <summary>
''' Singleton that manipulates received dnp3.0 data
''' </summary>
Public Class DataSOEHandler
    Implements ISOEHandler

#Region "Public Properties"

    ''' <summary>
    ''' Default Instance
    ''' </summary>
    ''' <returns>Returns a new <see cref="DataSOEHandler"/></returns>
    Public Shared ReadOnly Property Instance As DataSOEHandler = New DataSOEHandler
#End Region

#Region "Default Constructor"

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    Public Sub New()

    End Sub
#End Region

#Region "Private Print Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="info"></param>
    Private Shared Sub PrintHeaderInfo(ByVal info As HeaderInfo)
        Debug.WriteLine($"---------> header.variation: {info.variation}, header.qualifier: {info.qualifier}, header.index: {info.headerIndex}")
    End Sub

    Private Shared Sub Print(ByVal value As Binary, ByVal index As UInt16)
        Debug.WriteLine($"----> Binary[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As DoubleBitBinary, ByVal index As UInt16)
        Debug.WriteLine($"----> DoubleBitBinary[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As Analog, ByVal index As UInt16)
        Debug.WriteLine($"----> Analog[{index}] value: {value.Value}, not sure about time: {value.Timestamp}")
    End Sub

    Private Shared Sub Print(ByVal value As Counter, ByVal index As UInt16)
        Debug.WriteLine($"----> Counter[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As FrozenCounter, ByVal index As UInt16)
        Debug.WriteLine($"----> FrozenCounter[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As BinaryOutputStatus, ByVal index As UInt16)
        Debug.WriteLine($"----> BinaryOutputStatus[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As AnalogOutputStatus, ByVal index As UInt16)
        Debug.WriteLine($"----> AnalogOutputStatus[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As OctetString, ByVal index As UInt16)
        Debug.WriteLine($"----> OctetString[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As TimeAndInterval, ByVal index As UInt16)
        Debug.WriteLine($"----> TimeAndInterval[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As BinaryCommandEvent, ByVal index As UInt16)
        Debug.WriteLine($"----> BinaryCommandEvent[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As AnalogCommandEvent, ByVal index As UInt16)
        Debug.WriteLine($"----> AnalogCommandEvent[{index}] {value}")
    End Sub

    Private Shared Sub Print(ByVal value As SecurityStat, ByVal index As UInt16)
        Debug.WriteLine($"----> SecurityStat[{index}] {value}")
    End Sub

#End Region

#Region "Implemented Public ISOEHandlers Methods"

    ''' <summary>
    ''' Processes <see cref="IndexedValue(Of Analog)"/>
    ''' </summary>
    ''' <param name="info"><seealso cref="HeaderInfo"/></param>
    ''' <param name="values">T = <seealso cref="Analog"/></param>
    Public Sub Process(ByVal info As HeaderInfo, ByVal values As IEnumerable(Of IndexedValue(Of Analog))) Implements ISOEHandler.Process
        For Each pair In values
            ' Debug.WriteLine($"----> index: {pair.Index}, value: {pair.Value.Value}, quality: {pair.Value.Quality}, time: {pair.Value.Timestamp}")
            Print(pair.Value, pair.Index)
        Next
    End Sub

#End Region

#Region "NotImplemented Public ISOEHandlers Methods"

    Public Sub Process(ByVal info As HeaderInfo, ByVal values As IEnumerable(Of IndexedValue(Of Binary))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(ByVal info As HeaderInfo, ByVal values As IEnumerable(Of IndexedValue(Of DoubleBitBinary))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(ByVal info As HeaderInfo, ByVal values As IEnumerable(Of IndexedValue(Of Counter))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of FrozenCounter))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of BinaryOutputStatus))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of AnalogOutputStatus))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of OctetString))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of TimeAndInterval))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of BinaryCommandEvent))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of AnalogCommandEvent))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub Process(info As HeaderInfo, values As IEnumerable(Of IndexedValue(Of SecurityStat))) Implements ISOEHandler.Process
        'For Each pair In values
        '    Print(pair.Value, pair.Index)
        'Next
    End Sub

    Public Sub BeginFragment(info As ResponseInfo) Implements ISOEHandler.BeginFragment
        Debug.WriteLine($"----> BeginFragement: {info}")
    End Sub

    Public Sub EndFragment(info As ResponseInfo) Implements ISOEHandler.EndFragment
        Debug.WriteLine($"----> EndFragement: {info}")
    End Sub

#End Region

End Class