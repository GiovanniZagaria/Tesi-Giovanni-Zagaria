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
                UnityEngine.Debug.LogError("Error sending choice to AI: " + request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Choice sent successfully: " + choice);
            }
        }
    }

    public void SendStoryTextToAI(string storyText)
    {
        StartCoroutine(SendStoryTextCoroutine(storyText));
    }

    private IEnumerator SendStoryTextCoroutine(string storyText)
    {
        string sendStoryTextUrl = "http://127.0.0.1:5000/send_story"; // URL per inviare il testo della storia

        // Prepara i dati JSON da inviare
        JObject jsonData = new JObject
        {
            { "story", storyText }
        };

        using (UnityWebRequest request = new UnityWebRequest(sendStoryTextUrl, "POST"))
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
                UnityEngine.Debug.LogError("Error sending story text to AI: " + request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Story text sent successfully: " + storyText);
                // Esempio di utilizzo della risposta
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"].ToString();
                // Logica per gestire la risposta, come aggiornare la storia
                UpdateStory(suggestion);
            }
        }
    }

    private IEnumerator ReceiveContentCoroutine()
    {
        while (true) // Ciclo infinito per ricevere continuamente
        {
            // Logica per ottenere il contenuto da inviare
            // Esempio: Se hai una funzione che ottiene il testo della storia
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
        // Logica per recuperare il testo della storia corrente
        return storyText; // Restituisce il testo della storia
    }

    private void UpdateStory(string suggestion)
    {
        // Logica per aggiornare la storia con la risposta dall'IA
        UnityEngine.Debug.Log("Updated Story: " + suggestion);
        // Qui potresti anche voler appendere la risposta alla tua storia
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






/*using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;

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

    public void SendChoicesToAI(string choice)
    {
        StartCoroutine(SendChoiceCoroutine(choice));
    }

    private IEnumerator SendChoiceCoroutine(string choice)
    {
        string sendChoiceUrl = "http://127.0.0.1:5000/send_choice";

        JObject jsonData = new JObject
        {
            { "choice", choice }
        };

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Error sending choice to AI: " + request.error);
            }
            else
            {
                UnityEngine.Debug.Log("Choice sent successfully: " + choice);
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
            }
        }
    }

    void OnApplicationQuit()
    {
        if (flaskProcess != null && !flaskProcess.HasExited)
        {
            flaskProcess.Kill();
            UnityEngine.Debug.Log("Flask server fermato.");
        }
    }
}*/
