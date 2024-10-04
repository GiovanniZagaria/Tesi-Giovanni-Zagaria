using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AIHelper : MonoBehaviour
{
    private string apiUrl = "https://api.together.xyz/v1/chat/completions"; // Endpoint dell'API Together
    private string apiKey = "29753fd69be8903061f2b955add0fae38638917867f0ebe08ef643d2a75372be"; // La tua chiave API

    // Coroutine per inviare una richiesta all'API e ottenere un suggerimento
    public IEnumerator FetchSuggestion(string currentStory, Action<string> onSuccess, Action<string> onError)
    {
        // Crea i dati JSON per la richiesta
        string jsonData = "{\"prompt\":\"" + currentStory + "\", \"max_tokens\":150}";

        // Configura la richiesta HTTP
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // Invia la richiesta e aspetta la risposta
        yield return request.SendWebRequest();

        // Gestione delle risposte
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parsing della risposta JSON
            string responseText = request.downloadHandler.text;
            string suggestion = ParseSuggestion(responseText); // Funzione che estrarrà il suggerimento dalla risposta JSON
            onSuccess?.Invoke(suggestion); // Chiama la funzione di callback onSuccess
        }
        else
        {
            onError?.Invoke("Errore: " + request.error); // Chiama la funzione di callback onError
        }
    }

    // Funzione per estrarre il suggerimento dalla risposta JSON
    private string ParseSuggestion(string jsonResponse)
    {
        // Supponiamo che il suggerimento sia nella chiave "choices" del JSON
        // Dovrai adattare questa parte in base al formato della risposta dell'API
        var json = JsonUtility.FromJson<ResponseData>(jsonResponse);
        return json.choices[0].text.Trim(); // Estrarre il testo del suggerimento
    }

    // Classe di supporto per il parsing del JSON
    [System.Serializable]
    private class ResponseData
    {
        public ChoiceData[] choices;
    }

    [System.Serializable]
    private class ChoiceData
    {
        public string text;
    }
}
