﻿Imports System.IO
Imports System.Threading

''' <summary>
''' 
''' </summary>
Friend Class ReadXmlFile

    ''' <summary>
    ''' 
    ''' </summary>
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
            Dim regulatorNumber = 0
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

                Dim newRegulator As RegulatorCommunication = New RegulatorCommunication()
                Dim dnpModel As DnpCommunicationModel = New DnpCommunicationModel()
                Dim modbusModel As ModbusCommunicationModel = New ModbusCommunicationModel()
                Dim iecModel As IECCommunicationModel = New IECCommunicationModel()

                Using reader As Xml.XmlReader = Xml.XmlReader.Create(settingFileLocation, settings)

                    While reader.Read
                        If reader.HasAttributes Then
                            ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)
                            If reader.Depth = 2 Then myAttributeName = reader.Name


                            If myAttributeName = "test" Then
                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
test:                               Select Case reader.Name
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
                                            RVBSim.SettingsUserelativeReg1.Checked = userelative
                                            RVBSim.SettingsUsefixedReg1.Checked = Not userelative
                                            RVBSim.Radio_CheckedChanged(RVBSim.SettingsUserelativeReg1, Nothing)
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

                            ElseIf myAttributeName = "dnp" Then

                                'If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
dnp:                                Select Case reader.Name
                                        Case "regulator"
                                            dnpModel = New DnpCommunicationModel With {
                                                .Name = "Dnp",
                                                .Id = CUShort(reader.Value)
                                            }
                                        Case "port"
                                            dnpModel.Port = reader.Value
                                            ' dnpSetting.Port = reader.Value
                                        Case "source"
                                            dnpModel.SourceAddress = CUShort(reader.Value)
                                            dnpSetting.source = CUShort(reader.Value)
                                        Case "destination"
                                            dnpModel.DestinationAddress = CUShort(reader.Value)
                                            dnpSetting.destination = CUShort(reader.Value)
                                        Case "id"
                                            id = reader.Value
                                        Case "point"
                                            Select Case id
                                                Case "LocalVoltage"
                                                    dnpModel.LocalVoltage = CUShort(reader.Value)
                                                    dnpSetting.LocalVoltage = CUShort(reader.Value)
                                                Case "SourceVoltage"
                                                    dnpModel.SourceVoltage = CUShort(reader.Value)
                                                    dnpSetting.LocalVoltage = CUShort(reader.Value)
                                                Case "RVBEnable"
                                                    dnpModel.RVBEnable = CUShort(reader.Value)
                                                    dnpSetting.RVBEnable = CUShort(reader.Value)
                                                Case "FRVBValue"
                                                    dnpModel.FRVBValue = CUShort(reader.Value)
                                                    dnpSetting.FRVBValue = CUShort(reader.Value)
                                                Case "FRVBScale"
                                                    dnpModel.FRVBScale = CUShort(reader.Value)
                                                    dnpSetting.FRVBScale = CUShort(reader.Value)
                                                Case "RVBHeartbeat"
                                                    dnpModel.RVBHeartBeatTimer = CUShort(reader.Value)
                                                    dnpSetting.RVBHeartBeatTimer = CUShort(reader.Value)
                                                Case "RRVBValue"
                                                    dnpModel.RRVBValue = CUShort(reader.Value)
                                                    dnpSetting.RRVBValue = CUShort(reader.Value)
                                                Case "RRVBScale"
                                                    dnpModel.RRVBScale = CUShort(reader.Value)
                                                    dnpSetting.RRVBScale = CUShort(reader.Value)
                                                Case "RVBMax"
                                                    dnpModel.RVBMax = CUShort(reader.Value)
                                                    dnpSetting.RVBMax = CUShort(reader.Value)
                                                Case "RVBMin"
                                                    dnpModel.RVBMin = CUShort(reader.Value)
                                                    dnpSetting.RVBMin = CUShort(reader.Value)
                                                    newRegulator.DnpCommunication.Add(dnpModel)
                                            End Select

                                    End Select

                                End While

                                reader.MoveToElement()

                                '' add new communication model
                                'newRegulator.Communication.Add(newCommunicationModel)
                                '' add this Regulator before moving to next Element
                                'Regulators.Add(newRegulator)

                            ElseIf myAttributeName = "modbus" Then

                                'If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    'If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
modbus:                             Select Case reader.Name
                                        Case "regulator"
                                            modbusModel = New ModbusCommunicationModel With {
                                                .Name = "Modbus",
                                                .Id = reader.Value
                                                }
                                        Case "port"
                                            modbusModel.Port = CUShort(reader.Value)
                                            ' modbusRegister.Port = CUShort(reader.Value)
                                        Case "factory"
                                            modbusModel.Factory = CUShort(reader.Value)
                                            'modbusRegister.Factory = CUShort(reader.Value)
                                        Case "id"
                                            id = reader.Value
                                        Case "reg"
                                            Select Case id
                                                Case "LocalVoltage"
                                                    modbusModel.LocalVoltage = CUShort(reader.Value)
                                                   ' modbusRegister.LocalVoltage = CUShort(reader.Value)
                                                Case "SourceVoltage"
                                                    modbusModel.SourceVoltage = CUShort(reader.Value)
                                                    'modbusRegister.LocalVoltage = CUShort(reader.Value)
                                                Case "RVBEnable"
                                                    modbusModel.RVBEnable = CUShort(reader.Value)
                                                   ' modbusRegister.RVBEnable = CUShort(reader.Value)
                                                Case "FRVBValue"
                                                    modbusModel.FRVBValue = CUShort(reader.Value)
                                                   ' modbusRegister.FRVBValue = CUShort(reader.Value)
                                                Case "FRVBScale"
                                                    modbusModel.FRVBScale = CUShort(reader.Value)
                                                    'modbusRegister.FRVBScale = CUShort(reader.Value)
                                                Case "RVBHeartbeat"
                                                    modbusModel.RVBHeartBeatTimer = CUShort(reader.Value)
                                                   ' modbusRegister.RVBHeartBeatTimer = CUShort(reader.Value)
                                                Case "RVBActive"
                                                    modbusModel.RVBActive = CUShort(reader.Value)
                                                   ' modbusRegister.RVBActive = CUShort(reader.Value)
                                                Case "RRVBValue"
                                                    modbusModel.RRVBValue = CUShort(reader.Value)
                                                   ' modbusRegister.RRVBValue = CUShort(reader.Value)
                                                Case "RRVBScale"
                                                    modbusModel.RRVBScale = CUShort(reader.Value)
                                                   ' modbusRegister.RRVBScale = CUShort(reader.Value)
                                                Case "RVBMax"
                                                    modbusModel.RVBMax = CUShort(reader.Value)
                                                   ' modbusRegister.RVBMax = CUShort(reader.Value)
                                                Case "RVBMin"
                                                    modbusModel.RVBMin = CUShort(reader.Value)
                                                    'modbusRegister.RVBMin = CUShort(reader.Value)
                                                    newRegulator.ModbusCommunication.Add(modbusModel)
                                            End Select
                                    End Select
                                End While
                                reader.MoveToElement()

                            ElseIf myAttributeName = "iec" Then

                                ' If ConsoleWrite Then Console.WriteLine("Attributes of <" + reader.Name + "> Depth {0}", reader.Depth)

                                While reader.MoveToNextAttribute()
                                    ' If ConsoleWrite Then Console.WriteLine("<{0}>  ""{1}"" add this to <{2}>", reader.Name, reader.Value, myAttributeName)
                                    ' Move the reader back to the element node.
iec61850:                           Select Case reader.Name
                                        Case "regulator"
                                            ' regulatorNumber = reader.Value
                                            iecModel = New IECCommunicationModel With {
                                                .Name = "Iec",
                                                .Id = reader.Value
                                                }
                                        Case "port"
                                            iecModel.Port = reader.Value 'iecDai
                                            ' iecSetting.Port = reader.Value
                                        Case "readiedname"
                                            iecModel.ReadIEDName = reader.Value
                                            iedName = reader.Value
                                            iecSetting.ReadIEDName = iedName
                                        Case "writeiedname"
                                            iecModel.WriteIEDName = reader.Value
                                            iedName = reader.Value
                                            iecSetting.WriteIEDName = iedName
                                        Case "class"
                                            iecModel.IEDClass = reader.Value
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
                                                    ' iecModel.RVBEnable = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                                    iecModel.RVBEnable = String.Format($"{iecModel.IEDClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                                    iecSetting.RVBEnable = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                                Case "RVBHeartbeat"
                                                    'RVBSim.iecSetting.RVBHeartBeatTimer = String.Format("{0}${1}${2}${3}${4}", iedName, iedClass, iecFC, iecName, iecSdi)
                                                    iecModel.RVBHeartBeatTimer = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                                    iecSetting.RVBHeartBeatTimer = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}") 'see iec library
                                            End Select
                                        Case "dai"
                                            iecDai = reader.Value
                                            Select Case id
                                                Case "LocalVoltage"
                                                    iecModel.LocalVoltage = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.LocalVoltage = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "SourceVoltage"
                                                    iecModel.SourceVoltage = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.LocalVoltage = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "FRVBValue"
                                                    'RVBSim.iecSetting.FRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.FRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.FRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "FRVBScale"
                                                    ' RVBSim.iecSetting.FRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.FRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.FRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RRVBValue"
                                                    'RVBSim.iecSetting.RRVBValue = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.RRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.RRVBValue = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RRVBScale"
                                                    'RVBSim.iecSetting.RRVBScale = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.RRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.RRVBScale = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RVBMax"
                                                    ' RVBSim.iecSetting.RVBMax = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.RVBMax = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.RVBMax = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                Case "RVBMin"
                                                    ' RVBSim.iecSetting.RVBMin = String.Format("{0}${1}${2}${3}${4}${5}", iedName, iedClass, iecFC, iecName, iecSdi, iecDai)
                                                    iecModel.RVBMin = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    iecSetting.RVBMin = String.Format($"{iedClass}${iecFC}${iecName}${iecSdi}${iecDai}")
                                                    newRegulator.IECCommunication.Add(iecModel)
                                            End Select
                                    End Select
                                End While
                                reader.MoveToElement()
                            End If
                        End If
                    End While
                    Regulators.Add(newRegulator)
                End Using

            End If
        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetText(RVBSim.lblMsgCenter, message)
            sb.AppendLine(message)
        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(ReadXmlFile)}")
        End Try
    End Sub
End Class
