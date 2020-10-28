Imports System.Environment
Imports System.IO
Imports System.Security.Permissions
Imports System.Threading

Module SaveLogFile

    Friend Sub SaveLog()

        Try

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(SaveLogFile)} -- Starts")

            Dim logFilePath As String = String.Empty

            Dim filePermit As FileIOPermission = New FileIOPermission(FileIOPermissionAccess.Write, logFilePath)

            If filePermit.Equals(FileIOPermissionAccess.Write) Then
                ' MessageBox.Show("allowed")
                logFilePath = My.Application.Info.DirectoryPath
            Else
                ' MessageBox.Show("disallowed")
                logFilePath = Path.Combine(path1:=GetFolderPath(SpecialFolder.LocalApplicationData),
                                           path2:="rvbsim",
                                           path3:="simulator")
            End If

            If Not Directory.Exists(logFilePath) Then
                Directory.CreateDirectory(logFilePath)
            End If

            My.Computer.FileSystem.WriteAllText(file:=Path.Combine(path1:=logFilePath, path2:="log.txt"), text:=sb.ToString, append:=False)

        Catch ex As Exception
            Dim message As String = $"{Now}{vbCrLf}{ex.StackTrace}:{vbCrLf}{ex.Message}"
            SetTextBox(textbox:=RVBSim.ErrorsTextBox, text:=message)
            sb.AppendLine(message)

        Finally

            Debug.WriteLine($"Current thread is # {Thread.CurrentThread.GetHashCode} {NameOf(SaveLogFile)} -- Ends")
        End Try

    End Sub

End Module
