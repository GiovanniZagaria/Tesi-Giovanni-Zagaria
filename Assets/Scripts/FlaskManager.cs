using System.Diagnostics;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;

    void Start()
    {
        StartFlaskServer(); // Avvia il server Flask quando l'applicazione Unity parte
    }

    private void StartFlaskServer()
    {
        // Specifica il percorso di Python e lo script app.py
        string pythonPath = @"C:\Users\miche\anaconda3\python.exe"; // Aggiorna con il tuo percorso di Python
        string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\Tesi-Giovanni-Zagaria\Assets\FlaskApp\app.py"; // Percorso corretto di app.py

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = pythonPath,
            Arguments = $"\"{appPath}\"", // Aggiungi le virgolette per gestire gli spazi nel percorso
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        flaskProcess = new Process { StartInfo = startInfo };
        flaskProcess.Start();

        Debug.Log("Flask server avviato.");
    }

    void OnApplicationQuit()
    {
        if (flaskProcess != null && !flaskProcess.HasExited)
        {
            flaskProcess.Kill(); // Chiudi il processo Flask quando Unity si chiude
            Debug.Log("Flask server fermato.");
        }
    }
}

 