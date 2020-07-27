Imports System.Threading

Imports iec.AsyncIEC61850
'custom libraries
Imports tcpdnp.AsyncDNP3_0
'Imports rvb_sim.dnp

Module SetupTestUnit

    ''' <summary>
    ''' Sets factory of options for the unit under test.
    ''' </summary>
    Friend Sub SendSettings()

        Try

            For Each regulator In testJsonSettingsRegulators.Regulator

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- START")

                ' only send to regulator 1 when Single Phase checked
                If RVBSim.RVBSettings3Phase.Visible Or regulator.Id = 1 Then

                    ' SetText(RVBSim.lblMsgCenter, "Sending settings to the units ...")
                    SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:="Sending settings to the units...")

                    ' generate control names with regulator values.
                    For Each value In regulator.Values

                        Dim settingControlName As String = $"Settings{value.Name}Reg{regulator.Id}"
                        Dim iecDataType As DataType = DataType.none
                        Dim dataAddress As String = value.Value

                        Dim v() As Control = RVBSim.Controls.Find(settingControlName, True)

                        If v.Length > 0 Then

                            If v(0).Visible Then

                                Select Case v(0).GetType()

                                    Case GetType(TextBox)
                                        Dim iecTextbox As TextBox = CType(v(0), TextBox)
                                        Debug.WriteLine($"textbox: name: {iecTextbox.Name}, text: {iecTextbox.Text}")
                                        SendSettingValues(address:=dataAddress, value:=iecTextbox.Text, iecDataType:=DataType.int)

                                    Case GetType(NumericUpDown)
                                        Dim registerBox As NumericUpDown = CType(v(0), NumericUpDown)

                                        Debug.WriteLine($"NumericUpDown: alt-name: {registerBox.Name}, register: {value.Value}, value: {registerBox.Value * value.ScaleFactor}")

                                        SendSettingValues(address:=dataAddress, value:=registerBox.Value * value.ScaleFactor, iecDataType:=iecDataType)

                                    Case Else
                                        Debug.WriteLine($"something else control: alt-name: {v(0).Name}, text: {v(0).Text}, type: {v(0).GetType}")
                                End Select

                            End If
                        End If

                        If String.Equals("RVBEnable", value.Name) Then
                            Debug.WriteLine($"rvbenable only control: alt-name: {value.Name}, register: {value.Value}, value: 1 always to enable RVB.")

                            If ProtocolInUse = "iec" Then
                                iecDataType = DataType.bool
                                dataAddress = $"{value.Value}${value.Fc}${value.Id}${value.Sdi}"
                            End If
                            SendSettingValues(address:=dataAddress, value:=1, iecDataType:=iecDataType)
                        End If
                    Next

                    ' SetText(RVBSim.lblMsgCenter, "Sending completed ... reading Local Voltage")
                    SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:="Sending completed... reading specified values...")

                    If Not String.Equals(ReceivedErrorMsg, "None") Then sb.AppendLine($"{Now} Received {ReceivedErrorMsg} error")

                End If

                Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- END")

                '  End With
            Next

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            ' SetText(RVBSim.lblMsgCenter, message)
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)

        Finally
            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} --- {NameOf(SendSettings)} --- END")

        End Try
    End Sub


    Friend Function GetSpecificControl(ByRef rvbForm As RVBSim, featureType As String, featureName As String) As List(Of Control)

        Dim returnList As List(Of Control) = New List(Of Control)

        For Each regulator In testJsonSettingsRegulators.Regulator

            Dim settingControlName As String = $"{featureType}{featureName}Reg{regulator.Id}"

            Dim v() As Control = rvbForm.Controls.Find(settingControlName, True)

            If v.Length > 0 Then

                If v(0).Visible Then

                    returnList.Add(v(0))
                    Debug.Write($"--- {v(0).Name} is VISIBLE --- ")

                End If
            End If
        Next

        Return returnList
    End Function

    Private Sub SendSettingValues(address As String, value As UShort, Optional iecDataType As DataType = DataType.int)

        Dim WriteEvent As New ManualResetEvent(initialState:=False)

        Select Case ProtocolInUse
            Case "dnp"

                WriteEvent.Reset()

                dnp.Send(ManualEvent:=WriteEvent,
                         Destination:=RVBSim.DNPDestinationReg1.Value,
                         Source:=RVBSim.DNPSourceReg1.Value,
                         FunctionCode:=Mode.DirectOp,
                         ObjectX:=Objects.AnalogOutput,
                         Variation:=Variations.AnaOutBlockShort,
                         Qualifier:=QualifierField.AnaOutBlock16bitIndex,
                         Start16Bit:=1,
                         Stop16Bit:=CUShort(address),
                         Value:=value,
                         Status:=0)

                WriteEvent.WaitOne()

            Case "modbus"

                modbusWrite.WriteSingleRegister(startingAddress:=CUShort(address), value:=value)

            Case "iec"

                'enable RVB using IEC61850
                WriteEvent.Reset()
                iec61850.Send(WriteEvent, address, "Write", value, iecDataType)
                WriteEvent.WaitOne()

            Case Else
                MsgBox("Unsupported communication protocol")
                pause.Pause()

        End Select
    End Sub
End Module
