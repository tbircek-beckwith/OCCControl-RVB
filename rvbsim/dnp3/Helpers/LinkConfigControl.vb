'Imports Automatak.DNP3.Interface

'Public Class LinkConfigControl
'    'Private ReadOnly IsMaster As Boolean
'    'Private ReadOnly LocalAddress As UShort
'    'Private ReadOnly RemoteAddress As UShort
'    'Private ReadOnly NumRetry As UInteger
'    'Private ReadOnly ResponseTimeout As TimeSpan
'    'Private ReadOnly KeepAliveTimeout As TimeSpan
'    'Private newPropertyValue As LinkConfig

'    '''' <summary>
'    '''' 
'    '''' </summary>
'    '''' <param name="isMaster"></param>
'    '''' <param name="localAddress"></param>
'    '''' <param name="remoteAddress"></param>
'    '''' <param name="numRetry"></param>
'    '''' <param name="responseTimeout"></param>
'    '''' <param name="keepAliveTimeout"></param>
'    'Public Sub New(ByVal isMaster As Boolean, ByVal localAddress As UShort, ByVal remoteAddress As UShort, ByVal numRetry As UInteger, ByVal responseTimeout As TimeSpan, ByVal keepAliveTimeout As TimeSpan)
'    '    Me.IsMaster = isMaster
'    '    Me.LocalAddress = localAddress
'    '    Me.RemoteAddress = remoteAddress
'    '    Me.NumRetry = numRetry
'    '    Me.ResponseTimeout = responseTimeout
'    '    Me.KeepAliveTimeout = keepAliveTimeout
'    'End Sub


'    'Public Property Configuration() As LinkConfig
'    '    Get
'    '        Dim config = New LinkConfig With {
'    '            .isMaster = IsMaster,
'    '            .localAddr = LocalAddress,
'    '            .remoteAddr = RemoteAddress,
'    '            .responseTimeout = ResponseTimeout,
'    '            .keepAliveTimeout = KeepAliveTimeout
'    '        }

'    '        Return config
'    '    End Get
'    '    Set(ByVal value As LinkConfig)
'    '        Me.Configuration value
'    '    End Set
'    'End Property

'    Public Property Configuration As LinkConfig
'        Get
'            Dim config = New LinkConfig(isMaster, Me.checkBoxConfirmed.Checked)
'            config.localAddr = Decimal.ToUInt16(Me.numericUpDownSource.Value)
'            config.remoteAddr = Decimal.ToUInt16(Me.numericUpDownDest.Value)
'            'config.numRetry = Decimal.ToUInt32(Me.numericUpDownRetries.Value)
'            config.responseTimeout = TimeSpan.FromMilliseconds(Decimal.ToUInt32(Me.numericUpDownTimeout.Value))
'            config.keepAliveTimeout = TimeSpan.FromMilliseconds(Decimal.ToUInt32(Me.numericUpDownKeepAliveTimeout.Value * 1000))
'            Return config
'        End Get
'        Set(ByVal value As LinkConfig)
'            Configure(value)
'        End Set
'    End Property

'    Private Sub SetState()
'        Me.groupBoxConfirmed.Enabled = Me.checkBoxConfirmed.Checked
'    End Sub

'    Private Sub Configure(ByVal config As LinkConfig)
'        isMaster = config.isMaster
'        RVBSim..Value = config.localAddr
'        Me.numericUpDownDest.Value = config.remoteAddr
'        Me.numericUpDownTimeout.Value = Convert.ToDecimal(config.responseTimeout.TotalMilliseconds)
'        Me.numericUpDownRetries.Value = config.numRetry
'        Me.checkBoxConfirmed.Checked = config.useConfirms
'        Me.numericUpDownKeepAliveTimeout.Value = Convert.ToDecimal(config.keepAliveTimeout.TotalMilliseconds)
'        SetState()
'    End Sub

'    Private isMaster As Boolean = False

'    Private Sub checkBoxConfirmed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
'        SetState()
'    End Sub
'End Class
