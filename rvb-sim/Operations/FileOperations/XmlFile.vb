Imports System.IO
Imports System.Threading

Friend Class ReadXmlFile
    Public Sub Read()
        Try
            Dim myAttributeName As String = ""
            Dim iedName As String = ""
            Dim iedClass As String = ""
            Dim iecName As String = ""
            Dim iecFC As String = ""
            Dim iecSdi As String = ""
            Dim iecDai As String = ""
            Dim id As String = ""
            Dim xmlReader As Xml.XmlReader = Nothing
            Dim settings = New Xml.XmlReaderSettings With {
                .IgnoreComments = True,
                .IgnoreWhitespace = True
            }

            Dim settingFileLocation = Path.Combine(path1:=My.Application.Info.DirectoryPath,
                                                   path2:="resources",
                                                   path3:="Settings.xml")

            If My.Computer.FileSystem.FileExists(settingFileLocation) Then
                'reader = Xml.XmlReader.Create(settingFileLocation, settings)

                Using reader As Xml.XmlReader = Xml.XmlReader.Create(settingFileLocation, settings)

                    While reader.Read
                        If reader.HasAttributes Then
                            ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)
                            If reader.Depth = 2 Then myAttributeName = reader.Name

test:                       If myAttributeName = "test" Then
                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    Select Case reader.Name
                                        Case "protocol"
                                            testSetting.Protocol = reader.Value
                                        Case "read"
                                            testSetting.readIpAddress = reader.Value
                                        Case "write"
                                            testSetting.writeIpAddress = reader.Value
                                        Case "heartbeattimer"
                                            testSetting.HeartbeatTimer = CUShort(reader.Value)
                                        Case "userelative"
                                            Dim userelative As Boolean = CType(reader.Value, Boolean)
                                            RVBSim.radUseDeltaVoltage.Checked = userelative
                                            RVBSim.radUseFixedVoltage.Checked = Not userelative
                                            RVBSim.Radio_CheckedChanged(RVBSim.radUseDeltaVoltage, Nothing)
                                        Case "fwdrvbvoltage"
                                            testSetting.FwdRVBVoltage = CDbl(reader.Value)
                                        Case "revrvbvoltage"
                                            testSetting.RevRVBVoltage = CDbl(reader.Value)
                                        Case "fwdscalefactor"
                                            testSetting.FwdRVBVoltageScale = CDbl(reader.Value)
                                        Case "revscalefactor"
                                            testSetting.RevRVBVoltageScale = CDbl(reader.Value)
                                        Case "rvbmax"
                                            testSetting.RVBMax = CDbl(reader.Value)
                                        Case "rvbmin"
                                            testSetting.RVBMin = CDbl(reader.Value)
                                        Case "visible"
                                            visibility = CType(reader.Value, Boolean)
                                    End Select
                                End While
                                ' Move the reader back to the element node.
                                reader.MoveToElement()

dnp:                        ElseIf myAttributeName = "dnp" Then

                                'If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
                                    Select Case reader.Name
                                        Case "port"
                                            dnpSetting.Port = reader.Value
                                        Case "source"
                                            dnpSetting.source = CUShort(reader.Value)
                                        Case "dest"
                                            dnpSetting.destination = CUShort(reader.Value)
                                        Case "id"
                                            id = reader.Value
                                        Case "point"
                                            Select Case id
                                                Case "LocalVoltage"
                                                    dnpSetting.LocalVoltage = CUShort(reader.Value)
                                                Case "RVBEnable"
                                                    dnpSetting.RVBEnable = CUShort(reader.Value)
                                                Case "FRVBValue"
                                                    dnpSetting.FRVBValue = CUShort(reader.Value)
                                                Case "FRVBScale"
                                                    dnpSetting.FRVBScale = CUShort(reader.Value)
                                                Case "RVBHeartbeat"
                                                    dnpSetting.RVBHeartBeatTimer = CUShort(reader.Value)
                                                Case "RRVBValue"
                                                    dnpSetting.RRVBValue = CUShort(reader.Value)
                                                Case "RRVBScale"
                                                    dnpSetting.RRVBScale = CUShort(reader.Value)
                                                Case "RVBMax"
                                                    dnpSetting.RVBMax = CUShort(reader.Value)
                                                Case "RVBMin"
                                                    dnpSetting.RVBMin = CUShort(reader.Value)
                                            End Select
                                    End Select
                                End While
                                reader.MoveToElement()

modbus:                     ElseIf myAttributeName = "modbus" Then

                                'If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
                                    Select Case reader.Name
                                        Case "port"
                                            modbusRegister.Port = reader.Value
                                        Case "id"
                                            id = reader.Value
                                        Case "reg"
                                            Select Case id
                                                Case "LocalVoltage"
                                                    modbusRegister.LocalVoltage = CUShort(reader.Value)
                                                Case "RVBEnable"
                                                    modbusRegister.RVBEnable = CUShort(reader.Value)
                                                Case "FRVBValue"
                                                    modbusRegister.FRVBValue = CUShort(reader.Value)
                                                Case "FRVBScale"
                                                    modbusRegister.FRVBScale = CUShort(reader.Value)
                                                Case "RVBHeartbeat"
                                                    modbusRegister.RVBHeartBeatTimer = CUShort(reader.Value)
                                                Case "RVBActive"
                                                    modbusRegister.RVBActive = CUShort(reader.Value)
                                                Case "RRVBValue"
                                                    modbusRegister.RRVBValue = CUShort(reader.Value)
                                                Case "RRVBScale"
                                                    modbusRegister.RRVBScale = CUShort(reader.Value)
                                                Case "RVBMax"
                                                    modbusRegister.RVBMax = CUShort(reader.Value)
                                                Case "RVBMin"
                                                    modbusRegister.RVBMin = CUShort(reader.Value)
                                                Case "Factory"
                                                    modbusRegister.Factory = CUShort(reader.Value)
                                            End Select
                                    End Select
                                End While
                                reader.MoveToElement()

iec61850:                   ElseIf myAttributeName = "iec" Then

                                ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    ' If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
                                    Select Case reader.Name
                                        Case "port"
                                            iecSetting.Port = reader.Value
                                        Case "readiedname"
                                            iedName = reader.Value
                                            iecSetting.ReadIEDName = iedName
                                        Case "writeiedname"
                                            iedName = reader.Value
                                            iecSetting.WriteIEDName = iedName
                                        Case "class"
                                            iedClass = reader.Value
                                            iecSetting.iedClass = iedClass
                                        Case "id"
                                            id = reader.Value
                                        Case "name"
                                            iecName = reader.Value
                                        Case "fc"
                                            iecFC = reader.Value
                                        Case "sdi"
                                            iecSdi = reader.Value
                                            Select Case id
                                                Case "RVBEnable"
                                                    'RVBSim.iecSetting.RVBEnable = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                    iecSetting.RVBEnable = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                                Case "RVBHeartbeat"
                                                    'RVBSim.iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                    iecSetting.RVBHeartBeatTimer = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                            End Select
                                        Case "dai"
                                            iecDai = reader.Value
                                            Select Case id
                                                Case "LocalVoltage"
                                                    'RVBSim.iecSetting.LocalVoltage = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.LocalVoltage = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "FRVBValue"
                                                    'RVBSim.iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.FRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "FRVBScale"
                                                    ' RVBSim.iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.FRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RRVBValue"
                                                    'RVBSim.iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.RRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RRVBScale"
                                                    'RVBSim.iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.RRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RVBMax"
                                                    ' RVBSim.iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.RVBMax = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RVBMin"
                                                    ' RVBSim.iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecSetting.RVBMin = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                            End Select
                                    End Select
                                End While
                                reader.MoveToElement()
                            End If
                        End If
                    End While

                End Using

            End If
        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine($"{Now} {ex.Message}")
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(ReadXmlFile)}")
        End Try
    End Sub
End Class
