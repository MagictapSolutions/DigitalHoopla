using UnityEngine;
using System.Diagnostics;

public class KeyboardOpener : MonoBehaviour
{
    private Process oskProcess;

    public void OpenOSK()
    {
        // Path to the OSK.exe (On-Screen Keyboard executable) on Windows
        string oskPath = @"C:\Windows\System32\osk.exe";

        // Start the OSK.exe process
        oskProcess = Process.Start(oskPath);
    }

    public void CloseOSK()
    {
        // Check if the OSK process is running
        if (oskProcess != null && !oskProcess.HasExited)
        {
            // Close the OSK process
            oskProcess.CloseMainWindow();
            oskProcess.WaitForExit();
            oskProcess.Dispose();

            // Reset the OSK process variable
            oskProcess = null;
        }
    }
}