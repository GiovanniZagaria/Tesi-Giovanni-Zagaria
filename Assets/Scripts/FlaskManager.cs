using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;

public class FlaskManager : MonoBehaviour
{
    private string flaskUrl = "http://localhost:5000/get_suggestion"; // URL del server Flask

    // Metodo per avviare il server Flask e inviare una richiesta
    public void StartFlaskServer(string story)
    {
        StartCoroutine(FetchStorySuggestion(story));
    }

    // Coroutine per inviare la storia al server Flask e ottenere un suggerimento
    private IEnumerator FetchStorySuggestion(string story)
    {
        // Prepara i dati JSON da inviare
        JObject jsonData = new JObject
        {
            { "story", story }
        };

        using (UnityWebRequest request = new UnityWebRequest(flaskUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Attendi la risposta dal server Flask
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Errore nella connessione a Flask: " + request.error);
            }
            else
            {
                // Parse la risposta JSON
                try
                {
                    JObject responseJson = JObject.Parse(request.downloadHandler.text);
                    string suggestion = responseJson["suggestion"].ToString();
                    Debug.Log("Suggerimento ottenuto: " + suggestion);

                    // Aggiungi il suggerimento alla storia corrente (ad esempio, chiama StoryManager)
                    StoryManager storyManager = FindObjectOfType<StoryManager>();
                    if (storyManager != null)
                    {
                        storyManager.AppendToStory(suggestion);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Errore durante il parsing della risposta JSON: " + e.Message);
                }
            }
        }
    }
}






/*using System.Diagnostics; 
using UnityEngine;  // Assicurati di importare il corretto spazio di nomi per Unity

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;

    void Start()
    {
        StartFlaskServer();
    }

    public void StartFlaskServer()
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
*/