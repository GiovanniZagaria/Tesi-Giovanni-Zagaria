using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;


public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText = "";  // Storia da inviare (anche vuota)
    private string lastChoice = "";  // Ultima scelta effettuata
    private bool isRequestInProgress = false;  // Flag per evitare richieste multiple simultanee
    private float requestCooldown = 2.0f;  // Cooldown tra le richieste (in secondi)
    private float lastRequestTime = 0;  // Tempo dell'ultima richiesta

    void Start()
    {
        StartFlaskServer();  // Avvia il server Flask se non è già in esecuzione
    }

    public void StartFlaskServer()
    {
        if (flaskProcess == null || flaskProcess.HasExited)
        {
            string pythonPath = @"C:\Users\miche\anaconda3\python.exe";
            string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\Tesi-Giovanni-Zagaria\FlaskApp\app.py";

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
        else
        {
            UnityEngine.Debug.Log("Il server Flask è già in esecuzione.");
        }
    }

    // Funzione per inviare la scelta e la storia all'IA
    public void SendChoiceAndStoryToAI(string choice, string story, Action<string> onAISuccess)
    {
        if (Time.time - lastRequestTime < requestCooldown || isRequestInProgress)
        {
            UnityEngine.Debug.Log("Inviando troppe richieste, attendo...");
            return; // Non inviare richiesta se non è passato abbastanza tempo o se è già in corso una richiesta
        }

        if (!string.IsNullOrEmpty(choice))
        {
            lastChoice = choice; // Salva la scelta corrente
        }

        // Creiamo un prompt per l'IA che risponde solo alla carta selezionata, non alla storia completa
        string modifiedChoice = $"Prendi il contesto della storia che segue e rispondi esclusivamente alla domanda legata alla carta selezionata con un massimo di 1000 caratteri. La risposta deve essere concisa, pertinente e direttamente legata alla scelta fornita. Ecco la storia: \n\n{story}\n\nLa domanda è: {choice}";

        UnityEngine.Debug.Log($"[DEBUG] Invio all'IA: Scelta: {choice}, Storia: {story}");

        isRequestInProgress = true;
        lastRequestTime = Time.time; // Aggiorna l'ora dell'ultima richiesta

        StartCoroutine(SendChoiceAndStoryCoroutine(modifiedChoice, story, onAISuccess));
    }

    private IEnumerator SendChoiceAndStoryCoroutine(string choice, string story, Action<string> onAISuccess)
    {
        string sendChoiceAndStoryUrl = "http://127.0.0.1:5000/get_suggestion";

        // Invia una sola richiesta con la scelta e la storia al contesto
        JObject jsonData = new JObject
        {
            { "choice", choice },
            { "story", story }
        };

        UnityEngine.Debug.Log($"[DEBUG] Corpo della richiesta JSON: {jsonData.ToString()}");

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceAndStoryUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            UnityEngine.Debug.Log("[DEBUG] Inviando la richiesta all'IA...");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Errore nell'invio della richiesta all'IA: " + request.error);
            }
            else
            {
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();

                UnityEngine.Debug.Log($"[DEBUG] Risposta ricevuta dall'IA: {suggestion}");

                if (!string.IsNullOrEmpty(suggestion))
                {
                    suggestion = suggestion.Substring(0, Math.Min(400, suggestion.Length));
                    suggestion = RimuoviAccenti(suggestion);

                    // Aggiungi solo la risposta dell'IA alla storia, non la scelta
                    onAISuccess?.Invoke(suggestion);
                    UpdateStory(suggestion);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("La risposta ricevuta dall'IA è vuota.");
                }
            }
            isRequestInProgress = false;
        }
    }

    private string RimuoviAccenti(string testo)
    {
        Dictionary<char, char> mappaAccenti = new Dictionary<char, char>()
        {
            {'á', 'a'}, {'à', 'a'}, {'â', 'a'}, {'ä', 'a'}, {'ã', 'a'}, {'å', 'a'},
            {'é', 'e'}, {'è', 'e'}, {'ê', 'e'}, {'ë', 'e'},
            {'í', 'i'}, {'ì', 'i'}, {'î', 'i'}, {'ï', 'i'},
            {'ó', 'o'}, {'ò', 'o'}, {'ô', 'o'}, {'ö', 'o'}, {'õ', 'o'},
            {'ú', 'u'}, {'ù', 'u'}, {'û', 'u'}, {'ü', 'u'},
            {'ç', 'c'},
            {'ñ', 'n'}
        };

        StringBuilder sb = new StringBuilder();
        foreach (char c in testo)
        {
            if (mappaAccenti.ContainsKey(c))
            {
                sb.Append(mappaAccenti[c]);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }


    private void UpdateStory(string suggestion)
    {
        // La storia viene aggiornata solo con la risposta ricevuta, non la scelta
        storyText += " " + suggestion;
        UnityEngine.Debug.Log($"[DEBUG] Storia aggiornata: {storyText}");
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


/*
 using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;  // Assicurati che TextMeshPro sia importato

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText = "";  // Storia da inviare (anche vuota)
    private string lastChoice = "";  // Ultima scelta effettuata
    private bool isRequestInProgress = false;  // Flag per evitare richieste multiple simultanee
    private float requestCooldown = 2.0f;  // Cooldown tra le richieste (in secondi)
    private float lastRequestTime = 0;  // Tempo dell'ultima richiesta

    public UIManager uiManager;  // Riferimento a UIManager
    public StoryManager storyManager;  // Riferimento a StoryManager

    void Start()
    {
        StartFlaskServer();  // Avvia il server Flask se non è già in esecuzione
    }

    public void StartFlaskServer()
    {
        if (flaskProcess == null || flaskProcess.HasExited)
        {
            string pythonPath = @"C:\Users\miche\anaconda3\python.exe";
            string appPath = @"C:\Users\miche\OneDrive\Desktop\Giovanni\Tesi-Giovanni-Zagaria\FlaskApp\app.py";

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
        else
        {
            UnityEngine.Debug.Log("Il server Flask è già in esecuzione.");
        }
    }

    public void SendChoiceAndStoryToAI(string choice, string story, Action<string> onAISuccess)
    {
        if (Time.time - lastRequestTime < requestCooldown || isRequestInProgress)
        {
            UnityEngine.Debug.Log("Inviando troppe richieste, attendo...");
            return;  // Non inviare richiesta se non è passato abbastanza tempo o se è già in corso una richiesta
        }

        if (!string.IsNullOrEmpty(choice))
        {
            lastChoice = choice;  // Salva la scelta corrente
        }

        // Crea un prompt per l'IA che risponde solo alla carta selezionata, non alla storia completa
        string modifiedChoice = $"Prendi il contesto della storia che segue e rispondi esclusivamente alla domanda legata alla carta selezionata con un massimo di 1000 caratteri. La risposta deve essere concisa, pertinente e direttamente legata alla scelta fornita. Ecco la storia: \n\n{story}\n\nLa domanda è: {choice}";

        UnityEngine.Debug.Log($"[DEBUG] Invio all'IA: Scelta: {choice}, Storia: {story}");

        isRequestInProgress = true;
        lastRequestTime = Time.time; // Aggiorna l'ora dell'ultima richiesta

        StartCoroutine(SendChoiceAndStoryCoroutine(modifiedChoice, story, onAISuccess));
    }

    private IEnumerator SendChoiceAndStoryCoroutine(string choice, string story, Action<string> onAISuccess)
    {
        string sendChoiceAndStoryUrl = "http://127.0.0.1:5000/get_suggestion";

        JObject jsonData = new JObject
        {
            { "choice", choice },
            { "story", story }
        };

        UnityEngine.Debug.Log($"[DEBUG] Corpo della richiesta JSON: {jsonData.ToString()}");

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceAndStoryUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            UnityEngine.Debug.Log("[DEBUG] Inviando la richiesta all'IA...");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Errore nell'invio della richiesta all'IA: " + request.error);
            }
            else
            {
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();

                UnityEngine.Debug.Log($"[DEBUG] Risposta ricevuta dall'IA: {suggestion}");

                if (!string.IsNullOrEmpty(suggestion))
                {
                    suggestion = suggestion.Substring(0, Math.Min(400, suggestion.Length));
                    suggestion = RimuoviAccenti(suggestion);

                    // Aggiungi solo la risposta dell'IA alla storia, non la scelta
                    onAISuccess?.Invoke(suggestion);
                    UpdateStory(suggestion);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("La risposta ricevuta dall'IA è vuota.");
                }
            }
            isRequestInProgress = false;
        }
    }

    private string RimuoviAccenti(string testo)
    {
        Dictionary<char, char> mappaAccenti = new Dictionary<char, char>()
        {
            {'á', 'a'}, {'à', 'a'}, {'â', 'a'}, {'ä', 'a'}, {'ã', 'a'}, {'å', 'a'},
            {'é', 'e'}, {'è', 'e'}, {'ê', 'e'}, {'ë', 'e'},
            {'í', 'i'}, {'ì', 'i'}, {'î', 'i'}, {'ï', 'i'},
            {'ó', 'o'}, {'ò', 'o'}, {'ô', 'o'}, {'ö', 'o'}, {'õ', 'o'},
            {'ú', 'u'}, {'ù', 'u'}, {'û', 'u'}, {'ü', 'u'},
            {'ç', 'c'},
            {'ñ', 'n'}
        };

        StringBuilder sb = new StringBuilder();
        foreach (char c in testo)
        {
            if (mappaAccenti.ContainsKey(c))
            {
                sb.Append(mappaAccenti[c]);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    private void UpdateStory(string suggestion)
    {
        if (uiManager != null && uiManager.tmpTextComponent != null)
        {
            uiManager.tmpTextComponent.text = suggestion;  // Mostra solo la risposta attuale nella textbox
        }

        // Aggiorna la storia nel StoryManager (se necessario)
        storyText += " " + suggestion;
        UnityEngine.Debug.Log($"[DEBUG] Storia aggiornata: {storyText}");
    }
}
*/



