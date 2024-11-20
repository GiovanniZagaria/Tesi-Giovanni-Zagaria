/*using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System;

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText;  // Storia da inviare (anche vuota)
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
        // Verifica se il server Flask è già in esecuzione
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
        // Verifica se è passato abbastanza tempo dal'ultima richiesta
        if (Time.time - lastRequestTime < requestCooldown || isRequestInProgress)
        {
            UnityEngine.Debug.Log("Inviando troppe richieste, attendo...");
            return;
        }

        // Memorizza l'ultima scelta
        if (!string.IsNullOrEmpty(choice))
        {
            lastChoice = choice;
        }

        string modifiedChoice = $"Rispondi esclusivamente alla domanda con un massimo di 280 caratteri. La risposta deve essere concisa, pertinente e direttamente legata alla scelta fornita, senza aggiungere idee extra o considerazioni. Rispondi in formato rich text per Unity (usa i tag <b>, <i>, <color>, ecc.). Non superare i 280 caratteri, inoltre ricorda che devi dare una risposta a un bambino/a quindi usa un linguaggio molto semplice ma diretto, come se dovessi dare una vera e propria soluzione: {choice}";

        // Log dei dati prima di inviarli
        UnityEngine.Debug.Log($"[DEBUG] Invio all'IA: Scelta: {choice}, Storia: {story}");

        isRequestInProgress = true;
        lastRequestTime = Time.time;  // Memorizza il tempo dell'ultima richiesta

        // Chiamata diretta senza verifiche
        StartCoroutine(SendChoiceAndStoryCoroutine(modifiedChoice, story, onAISuccess));
    }

    private IEnumerator SendChoiceAndStoryCoroutine(string choice, string story, Action<string> onAISuccess)
    {
        string sendChoiceAndStoryUrl = "http://127.0.0.1:5000/get_suggestion";

        // Creiamo il corpo della richiesta
        JObject jsonData = new JObject
        {
            { "choice", choice },
            { "story", story }
        };

        // Log del corpo della richiesta prima di inviarla
        UnityEngine.Debug.Log($"[DEBUG] Corpo della richiesta JSON: {jsonData.ToString()}");

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceAndStoryUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Log che indica che la richiesta sta per essere inviata
            UnityEngine.Debug.Log("[DEBUG] Inviando la richiesta all'IA...");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Log di errore nel caso in cui la richiesta fallisca
                UnityEngine.Debug.LogError("Errore nell'invio della richiesta all'IA: " + request.error);
            }
            else
            {
                // Log della risposta ricevuta
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();

                // Log della risposta dell'IA
                UnityEngine.Debug.Log($"[DEBUG] Risposta ricevuta dall'IA: {suggestion}");

                if (!string.IsNullOrEmpty(suggestion))
                {
                    // Taglia la risposta a 280 caratteri
                    if (suggestion.Length > 280)
                    {
                        suggestion = suggestion.Substring(0, 280);
                    }

                    // Taglia la risposta al primo doppio spazio
                    int doubleSpaceIndex = suggestion.IndexOf("  ");
                    if (doubleSpaceIndex != -1)
                    {
                        suggestion = suggestion.Substring(0, doubleSpaceIndex);
                    }

                    onAISuccess?.Invoke(suggestion);
                    UpdateStory(suggestion);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("La risposta ricevuta dall'IA è vuota.");
                }
            }
            isRequestInProgress = false;  // Fine della richiesta
        }
    }

    private void UpdateStory(string suggestion)
    {
        // Aggiungi la risposta dell'IA alla storia
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
}*/

using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System;
using System.Collections.Generic;  // Per il Dictionary
using System.Text;  // Per il StringBuilder


public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText;  // Storia da inviare (anche vuota)
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
        // Verifica se il server Flask è già in esecuzione
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
        // Verifica se è passato abbastanza tempo dall'ultima richiesta
        if (Time.time - lastRequestTime < requestCooldown || isRequestInProgress)
        {
            UnityEngine.Debug.Log("Inviando troppe richieste, attendo...");
            return;
        }

        // Memorizza l'ultima scelta
        if (!string.IsNullOrEmpty(choice))
        {
            lastChoice = choice;
        }

        string modifiedChoice = $"Rispondi esclusivamente alla domanda con un massimo di 280 caratteri. La risposta deve essere concisa, pertinente e direttamente legata alla scelta fornita, senza aggiungere idee extra o considerazioni. Rispondi in formato rich text per Unity (usa i tag <b>, <i>, <color>, ecc.). Non superare i 280 caratteri, inoltre ricorda che devi dare una risposta a un bambino/a quindi usa un linguaggio molto semplice ma diretto, come se dovessi dare una vera e propria soluzione: {choice}";

        // Log dei dati prima di inviarli
        UnityEngine.Debug.Log($"[DEBUG] Invio all'IA: Scelta: {choice}, Storia: {story}");

        isRequestInProgress = true;
        lastRequestTime = Time.time;  // Memorizza il tempo dell'ultima richiesta

        // Chiamata diretta senza verifiche
        StartCoroutine(SendChoiceAndStoryCoroutine(modifiedChoice, story, onAISuccess));
    }

    private IEnumerator SendChoiceAndStoryCoroutine(string choice, string story, Action<string> onAISuccess)
    {
        string sendChoiceAndStoryUrl = "http://127.0.0.1:5000/get_suggestion";

        // Creiamo il corpo della richiesta
        JObject jsonData = new JObject
        {
            { "choice", choice },
            { "story", story }
        };

        // Log del corpo della richiesta prima di inviarla
        UnityEngine.Debug.Log($"[DEBUG] Corpo della richiesta JSON: {jsonData.ToString()}");

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceAndStoryUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Log che indica che la richiesta sta per essere inviata
            UnityEngine.Debug.Log("[DEBUG] Inviando la richiesta all'IA...");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Log di errore nel caso in cui la richiesta fallisca
                UnityEngine.Debug.LogError("Errore nell'invio della richiesta all'IA: " + request.error);
            }
            else
            {
                // Log della risposta ricevuta
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();

                // Log della risposta dell'IA
                UnityEngine.Debug.Log($"[DEBUG] Risposta ricevuta dall'IA: {suggestion}");

                if (!string.IsNullOrEmpty(suggestion))
                {
                    // Taglia la risposta a 280 caratteri
                    if (suggestion.Length > 280)
                    {
                        suggestion = suggestion.Substring(0, 280);
                    }

                    // Taglia la risposta al primo doppio spazio
                    int doubleSpaceIndex = suggestion.IndexOf("  ");
                    if (doubleSpaceIndex != -1)
                    {
                        suggestion = suggestion.Substring(0, doubleSpaceIndex);
                    }

                    // Sostituisci le lettere accentate
                    suggestion = RimuoviAccenti(suggestion);

                    onAISuccess?.Invoke(suggestion);
                    UpdateStory(suggestion);
                }
                else
                {
                    UnityEngine.Debug.LogWarning("La risposta ricevuta dall'IA è vuota.");
                }
            }
            isRequestInProgress = false;  // Fine della richiesta
        }
    }

    // Funzione per rimuovere gli accenti
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
        // Aggiungi la risposta dell'IA alla storia
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

