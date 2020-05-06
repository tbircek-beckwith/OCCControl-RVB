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

            If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, "Settings.xml")) Then
                xmlReader = Xml.XmlReader.Create(My.Computer.FileSystem.CombinePath(My.Application.Info.DirectoryPath, "Settings.xml"), settings)

                While xmlReader.Read
                    If xmlReader.HasAttributes Then
                        ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)
                        If xmlReader.Depth = 2 Then myAttributeName = xmlReader.Name

test:                   If myAttributeName = "test" Then
                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                Select Case xmlReader.Name
                                    Case "protocol"
                                        testSetting.Protocol = xmlReader.Value
                                    Case "read"
                                        testSetting.readIpAddress = xmlReader.Value
                                    Case "write"
                                        testSetting.writeIpAddress = xmlReader.Value
                                    Case "heartbeattimer"
                                        testSetting.HeartbeatTimer = CUShort(xmlReader.Value)
                                    Case "userelative"
                                        Dim userelative As Boolean = CType(xmlReader.Value, Boolean)
                                        RVBSim.radUseDeltaVoltage.Checked = userelative
                                        RVBSim.radUseFixedVoltage.Checked = Not userelative
                                        RVBSim.Radio_CheckedChanged(RVBSim.radUseDeltaVoltage, Nothing)
                                    Case "fwdrvbvoltage"
                                        testSetting.FwdRVBVoltage = CDbl(xmlReader.Value)
                                    Case "revrvbvoltage"
                                        testSetting.RevRVBVoltage = CDbl(xmlReader.Value)
                                    Case "fwdscalefactor"
                                        testSetting.FwdRVBVoltageScale = CDbl(xmlReader.Value)
                                    Case "revscalefactor"
                                        testSetting.RevRVBVoltageScale = CDbl(xmlReader.Value)
                                    Case "rvbmax"
                                        testSetting.RVBMax = CDbl(xmlReader.Value)
                                    Case "rvbmin"
                                        testSetting.RVBMin = CDbl(xmlReader.Value)
                                    Case "visible"
                                        visibility = CType(xmlReader.Value, Boolean)
                                End Select
                            End While
                            ' Move the reader back to the element node.
                            xmlReader.MoveToElement()

dnp:                    ElseIf myAttributeName = "dnp" Then

                            'If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        dnpSetting.Port = xmlReader.Value
                                    Case "source"
                                        dnpSetting.source = CUShort(xmlReader.Value)
                                    Case "dest"
                                        dnpSetting.destination = CUShort(xmlReader.Value)
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "point"
                                        Select Case id
                                            Case "LocalVoltage"
                                                dnpSetting.LocalVoltage = CUShort(xmlReader.Value)
                                            Case "RVBEnable"
                                                dnpSetting.RVBEnable = CUShort(xmlReader.Value)
                                            Case "FRVBValue"
                                                dnpSetting.FRVBValue = CUShort(xmlReader.Value)
                                            Case "FRVBScale"
                                                dnpSetting.FRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBHeartbeat"
                                                dnpSetting.RVBHeartBeatTimer = CUShort(xmlReader.Value)
                                            Case "RRVBValue"
                                                dnpSetting.RRVBValue = CUShort(xmlReader.Value)
                                            Case "RRVBScale"
                                                dnpSetting.RRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBMax"
                                                dnpSetting.RVBMax = CUShort(xmlReader.Value)
                                            Case "RVBMin"
                                                dnpSetting.RVBMin = CUShort(xmlReader.Value)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()

modbus:                 ElseIf myAttributeName = "modbus" Then

                            'If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        modbusRegister.Port = xmlReader.Value
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "reg"
                                        Select Case id
                                            Case "LocalVoltage"
                                                modbusRegister.LocalVoltage = CUShort(xmlReader.Value)
                                            Case "RVBEnable"
                                                modbusRegister.RVBEnable = CUShort(xmlReader.Value)
                                            Case "FRVBValue"
                                                modbusRegister.FRVBValue = CUShort(xmlReader.Value)
                                            Case "FRVBScale"
                                                modbusRegister.FRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBHeartbeat"
                                                modbusRegister.RVBHeartBeatTimer = CUShort(xmlReader.Value)
                                            Case "RVBActive"
                                                modbusRegister.RVBActive = CUShort(xmlReader.Value)
                                            Case "RRVBValue"
                                                modbusRegister.RRVBValue = CUShort(xmlReader.Value)
                                            Case "RRVBScale"
                                                modbusRegister.RRVBScale = CUShort(xmlReader.Value)
                                            Case "RVBMax"
                                                modbusRegister.RVBMax = CUShort(xmlReader.Value)
                                            Case "RVBMin"
                                                modbusRegister.RVBMin = CUShort(xmlReader.Value)
                                            Case "Factory"
                                                modbusRegister.Factory = CUShort(xmlReader.Value)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()

iec61850:               ElseIf myAttributeName = "iec" Then

                            ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + xmlReader.Name + "> Depth {0}", xmlReader.Depth)

                            While xmlReader.MoveToNextAttribute()
                                ' If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", xmlReader.Name, xmlReader.Value, myAttributeName)
                                ' Move the reader back to the element node.
                                Select Case xmlReader.Name
                                    Case "port"
                                        iecSetting.Port = xmlReader.Value
                                    Case "readiedname"
                                        iedName = xmlReader.Value
                                        iecSetting.ReadIEDName = iedName
                                    Case "writeiedname"
                                        iedName = xmlReader.Value
                                        iecSetting.WriteIEDName = iedName
                                    Case "class"
                                        iedClass = xmlReader.Value
                                        iecSetting.iedClass = iedClass
                                    Case "id"
                                        id = xmlReader.Value
                                    Case "name"
                                        iecName = xmlReader.Value
                                    Case "fc"
                                        iecFC = xmlReader.Value
                                    Case "sdi"
                                        iecSdi = xmlReader.Value
                                        Select Case id
                                            Case "RVBEnable"
                                                'RVBSim.iecSetting.RVBEnable = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                iecSetting.RVBEnable = String.Format("{0}${1}${2}${3}", iedClass, iecFC, iecName, iecSdi) 'see iec library
                                            Case "RVBHeartbeat"
                                                'RVBSim.iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}", iedClass, iecFC, iecName, iecSdi) 'see iec library
                                        End Select
                                    Case "dai"
                                        iecDai = xmlReader.Value
                                        Select Case id
                                            Case "LocalVoltage"
                                                'RVBSim.iecSetting.LocalVoltage = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.LocalVoltage = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "FRVBValue"
                                                'RVBSim.iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "FRVBScale"
                                                ' RVBSim.iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RRVBValue"
                                                'RVBSim.iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RRVBScale"
                                                'RVBSim.iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RVBMax"
                                                ' RVBSim.iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                            Case "RVBMin"
                                                ' RVBSim.iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}", iedClass, iecFC, iecName, iecSdi, iecDai)
                                        End Select
                                End Select
                            End While
                            xmlReader.MoveToElement()
                        End If
                    End If
                End While
            End If
        Catch ex As Exception
            SetText(RVBSim.lblMsgCenter, ex.Message)
            sb.AppendLine(String.Format("{0} {1}", Now, ex.Message))
        Finally
            If ConsoleWriteEnable Then Console.WriteLine("Current thread is # {0} --- XML_read", Thread.CurrentThread.GetHashCode)
        End Try
    End Sub
End Class
