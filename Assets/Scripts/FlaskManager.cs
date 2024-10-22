using System.Diagnostics; 
using UnityEngine;  // Assicurati di importare il corretto spazio di nomi per Unity

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;

    void Start()
    {
        StartFlaskServer();
    }

    private void StartFlaskServer()
    {
        string pythonPath = @"C:\Users\miche\anaconda3\python.exe"; 
        string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\FlaskApp\app.py"; 

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{appPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        flaskProcess = new Process { StartInfo = startInfo };
        flaskProcess.Start();

        UnityEngine.Debug.Log("Flask server avviato.");
    }

    void OnApplicationQuit()
    {
        if (flaskProcess != null && !flaskProcess.HasExited)
        {
            flaskProcess.Kill();
            UnityEngine.Debug.Log("Flask server fermato.");
        }
    }
}
