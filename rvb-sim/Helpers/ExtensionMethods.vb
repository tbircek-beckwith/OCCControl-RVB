
Imports System.Reflection
Imports System.Runtime.CompilerServices

''' <summary>
''' Extension methods
''' </summary>
Public Module ExtensionMethods

    ''' <summary>
    ''' Gets Child controls of the specified container control.
    ''' </summary>
    ''' <typeparam name="T">Container control to get child controls</typeparam>
    ''' <param name="parent">Container</param>
    ''' <returns>Returns a List of Child controls of the specified container control.</returns>
    <Extension()>
    Public Function GetChildControls(Of T As Control)(ByVal parent As Control) As List(Of T)
        Dim result As New List(Of Control)

        For Each control As Control In parent.Controls
            If TypeOf control Is T Then result.Add(control)
            result.AddRange(control.GetChildControls(Of T)())
        Next
        Return result.ToArray().Select(Of T)(Function(arg1) CType(arg1, T)).ToList()
    End Function

    ''' <summary>
    ''' Gets all controls in the form.
    ''' </summary>
    ''' <returns>Returns all controls in the form.</returns>
    Friend Function GetControls() As List(Of Control)

        ' returns every control
        Dim controls = RVBSim.AddressBox.GetChildControls(Of Control)

        For Each control As Control In controls
            Debug.WriteLine($"control.name ={control.Name}, control.text = {control.Text}, control.type = {control.GetType()}")
        Next

        Return controls
    End Function

    ''' <summary>
    ''' Gets the specified <see cref="Control"/>
    ''' <para>protocol: the name protocol in use. <see cref="CommunicationBaseModel(Of T).Name"/></para>
    ''' <para>settingName: the name of the setting value to use. ex: <see cref="DnpCommunicationModel.LocalVoltage"/></para>
    ''' <para>regulatorNumber: the regulator <see cref="CommunicationBaseModel(Of T).Id"/></para>
    ''' <para>searchAllChildren: whether search child <see cref="Control"/> or not</para>
    ''' </summary>
    ''' <param name="protocol">the name protocol in use. <see cref="CommunicationBaseModel(Of T).Name"/></param>
    ''' <param name="settingName">the name of the setting value to use. ex: <see cref="DnpCommunicationModel.LocalVoltage"/></param>
    ''' <param name="regulatorNumber">the regulator <see cref="CommunicationBaseModel(Of T).Id"/></param>
    ''' <param name="searchAllChildren">whether search children <see cref="Control"/> or not</param>
    ''' <returns>returns the specified controls</returns>
    <Extension()>
    Public Function GetControls(ByVal protocol As String, ByVal settingName As String, ByVal regulatorNumber As Integer, Optional ByVal searchAllChildren As Boolean = True) As Control()
        Dim t() As Control = RVBSim.Controls.Find($"{protocol}{settingName}Reg{regulatorNumber}", searchAllChildren)
        Return t
    End Function

    ''' <summary>
    ''' Sets values of controls and enable/disable
    ''' </summary>
    ''' <param name="enable">True sets the control enabled, false disables the control</param>
    <Extension()>
    Public Sub SetValues(ByRef enable As Boolean)

        ' TODO: Try to avoid late bounding
        ' flatten the Regulator object
        Dim modelList As List(Of Object) = New List(Of Object)
        For Each communicationModel As Regulator In Regulators
            modelList.AddRange(communicationModel.DnpCommunication)
            modelList.AddRange(communicationModel.ModbusCommunication)
            modelList.AddRange(communicationModel.IECCommunication)
        Next
        For Each model In modelList

            Dim item As Type = model.GetType()
            Dim props() As PropertyInfo = item.GetProperties()

            Debug.WriteLine($"Properties (N = {props.Length})")
            For Each prop In props
                If prop.GetIndexParameters().Length = 0 Then

                    Dim t() As Control = RVBSim.Controls.Find($"{model.Name}{prop.Name}Reg{model.Id}", True)

                    If t.Length > 0 Then
                        Debug.Write($"control: {model.Name}{prop.Name}Reg{model.Id}: old text: {t(0).Text} -- ")
                        t(0).Text = prop.GetValue(model)
                        t(0).Enabled = enable
                        Debug.WriteLine($"new text: {t(0).Text}")
                    End If
                End If
            Next
        Next
    End Sub
End Module
