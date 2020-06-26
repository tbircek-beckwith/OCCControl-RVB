Imports System.IO
Imports System.Threading.Tasks
Imports Squirrel

Public Class AppStartup

    ''' <summary>
    ''' Checks for the updates if there is one updates application for next start up
    ''' </summary>
    ''' <returns>AsyncStateMachine of Squirrel</returns>
    Public Async Function CheckForUpdates() As Task

#If DEBUG Then
        Dim urlOrPath As String = "C:\Users\TBircek\source\repos\rvbsim\rvbsim.package\profiles.package\Debug"
#Else
        Dim urlOrPath As String = "\\volta\Eng_Lab\Software Updates\RVBSim"
#End If
        Using updateManager As UpdateManager = New UpdateManager(urlOrPath:=urlOrPath)

            Try
                Dim updateInfo As UpdateInfo = Await updateManager.CheckForUpdate()
                If updateInfo.CurrentlyInstalledVersion IsNot Nothing Then Debug.WriteLine($"Current version: {updateInfo.CurrentlyInstalledVersion.Version}", "Informative")

                If updateInfo.ReleasesToApply.Count > 0 Then
                    Debug.WriteLine($"Found an update version: {updateInfo.FutureReleaseEntry.Version}", "Informative")
                    Await updateManager.UpdateApp()
                Else
                    Debug.WriteLine($"No updates: The current version: {updateInfo.FutureReleaseEntry.Version}", "Informative")
                End If

            Catch ex As Exception
                Debug.WriteLine($"Error: {ex.Message}", "Error")
            End Try
        End Using
    End Function
End Class
