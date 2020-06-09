Imports System.Threading.Tasks
Imports Automatak.DNP3.Interface

Public Class AnalogOutputControl
    Enum AOType
        Integer16
        Integer32
        Float32
        Double64
    End Enum

    Public ReadOnly Property Type As AOType
    Public ReadOnly Property Value As Integer
    Public ReadOnly Property Index As Integer

    Public Sub New(ByVal type As AOType, ByVal value As Integer, ByVal index As Integer)
        Me.Type = type
        Me.Value = value
        Me.Index = index
    End Sub

    Public ReadOnly Property DirectOperateAction As Func(Of ICommandProcessor, Task(Of CommandTaskResult))
        Get
            Dim index = Me.Index
            Dim value = Me.Value
            ' Dim type = Me.Type

            Select Case Type
                Case AOType.Integer16
                    Return Function(ByVal cp) cp.DirectOperate(New AnalogOutputInt16(Convert.ToInt16(value)), index, TaskConfig.[Default])
                Case AOType.Integer32
                    Return Function(ByVal cp) cp.DirectOperate(New AnalogOutputInt32(Convert.ToInt32(value)), index, TaskConfig.[Default])
                Case AOType.Float32
                    Return Function(ByVal cp) cp.DirectOperate(New AnalogOutputFloat32(Convert.ToSingle(value)), index, TaskConfig.[Default])
                Case Else
                    Return Function(ByVal cp) cp.DirectOperate(New AnalogOutputDouble64(Convert.ToDouble(value)), index, TaskConfig.[Default])
            End Select
        End Get
    End Property

    Public ReadOnly Property SelectAndOperateAction As Func(Of ICommandProcessor, Task(Of CommandTaskResult))
        Get
            Dim index = Me.Index
            Dim value = Me.Value

            Select Case Type
                Case AOType.Integer16
                    Return Function(ByVal cp) cp.SelectAndOperate(New AnalogOutputInt16(Convert.ToInt16(value)), index, TaskConfig.[Default])
                Case AOType.Integer32
                    Return Function(ByVal cp) cp.SelectAndOperate(New AnalogOutputInt32(Convert.ToInt32(value)), index, TaskConfig.[Default])
                Case AOType.Float32
                    Return Function(ByVal cp) cp.SelectAndOperate(New AnalogOutputFloat32(Convert.ToSingle(value)), index, TaskConfig.[Default])
                Case Else
                    Return Function(ByVal cp) cp.SelectAndOperate(New AnalogOutputDouble64(Convert.ToDouble(value)), index, TaskConfig.[Default])
            End Select
        End Get
    End Property
End Class
