using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking; // Per utilizzare UnityWebRequest
using Newtonsoft.Json.Linq; // Assicurati di avere Newtonsoft.Json importato
using System.Collections; // Aggiungi questa riga

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText; // Variabile per memorizzare il testo della storia

    void Start()
    {
        StartFlaskServer();
        StartCoroutine(ReceiveContentCoroutine()); // Inizia a ricevere contenuto in modo continuo
    }

    public void StartFlaskServer()
    {
        string pythonPath = @"C:\Users\miche\anaconda3\python.exe"; // Percorso dell'interprete Python
        string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\FlaskApp\app.py"; // Percorso dell'app Flask

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

    public void SendChoicesToAI(string choice)
    {
        StartCoroutine(SendChoiceCoroutine(choice));
    }

    private IEnumerator SendChoiceCoroutine(string choice)
    {
        string sendChoiceUrl = "http://127.0.0.1:5000/send_choice"; // URL del server Flask per inviare le scelte

        // Prepara i dati JSON da inviare
        JObject jsonData = new JObject
        {
            { "choice", choice }
        };

        // Log del JSON che viene inviato
        UnityEngine.Debug.Log("Invio JSON per la scelta: " + jsonData.ToString());

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Invia la richiesta
            yield return request.SendWebRequest();

            // Gestione della risposta
            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Errore nell'invio della scelta all'IA: " + request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Scelta inviata con successo: " + choice);
                UnityEngine.Debug.Log("Risposta dall'IA: " + request.downloadHandler.text);
            }
        }
    }

    public void SendStoryTextToAI(string storyText)
    {
        StartCoroutine(SendStoryTextCoroutine(storyText));
    }

    private IEnumerator SendStoryTextCoroutine(string storyText)
    {
        string sendStoryTextUrl = "http://127.0.0.1:5000/get_suggestion";

        JObject jsonData = new JObject
    {
        { "story", storyText }
    };

        UnityEngine.Debug.Log("Invio la storia all'IA: " + storyText);
        UnityEngine.Debug.Log("Invio JSON per la storia: " + jsonData.ToString());

        using (UnityWebRequest request = new UnityWebRequest(sendStoryTextUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Error sending story text to AI: " + request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Story text sent successfully: " + storyText);
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();
                UnityEngine.Debug.Log("Suggerimento ricevuto dall'IA: " + suggestion);
                UpdateStory(suggestion);
            }
        }
    }



    private IEnumerator ReceiveContentCoroutine()
    {
        while (true) // Ciclo infinito per ricevere continuamente
        {
            // Logica per ottenere il contenuto da inviare
            storyText = GetCurrentStoryText(); // Recupera il testo attuale da inviare
            if (!string.IsNullOrEmpty(storyText))
            {
                // Invia il testo della storia
                SendStoryTextToAI(storyText); // Chiama la coroutine senza assegnare il risultato
            }
            yield return new WaitForSeconds(5f); // Attendere 5 secondi prima di inviare di nuovo
        }
    }

    private string GetCurrentStoryText()
    {
        UnityEngine.Debug.Log("Testo attuale della storia: " + storyText); // Log per vedere il testo attuale
        return storyText;
    }

    private void UpdateStory(string suggestion)
    {
        // Logica per aggiornare la storia con la risposta dall'IA
        UnityEngine.Debug.Log("Storia aggiornata: " + suggestion);
        // Qui potresti anche voler appendere la risposta alla tua storia
        storyText += " " + suggestion; // Esempio di come aggiornare la storia
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
