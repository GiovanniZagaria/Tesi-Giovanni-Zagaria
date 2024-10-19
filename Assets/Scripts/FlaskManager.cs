using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;

    void Start()
    {
        // Avvia il server Flask eseguendo app.py
        StartFlaskServer();
    }

    private void StartFlaskServer()
    {
        // Specifica il percorso di Python e dello script app.py
        string pythonPath = @"C:\Users\miche\anaconda3\python.exe"; // Modifica con il percorso del tuo Python
        string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\FlaskApp\app.py"; // Assicurati di includere il nome del file

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = pythonPath;
        startInfo.Arguments = $"\"{appPath}\""; // Includi le virgolette per gestire gli spazi nel percorso
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        flaskProcess = new Process();
        flaskProcess.StartInfo = startInfo;
        flaskProcess.Start();

        Debug.Log("Flask server avviato.");
    }

    void OnApplicationQuit()
    {
        // Termina il processo di Flask quando l'applicazione Unity viene chiusa
        if (flaskProcess != null && !flaskProcess.HasExited)
        {
            flaskProcess.Kill();
            Debug.Log("Flask server fermato.");
        }
    }
}
