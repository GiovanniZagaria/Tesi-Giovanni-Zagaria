using UnityEngine;
using System.Collections;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json; 
using System.Collections.Generic; // Per l'uso delle liste

public class AIntegration : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();
    private string apiUrl = "https://api.together.ai/v1/suggestions"; // Sostituisci con l'endpoint reale

    public async void GetSuggestion(string userInput)
    {
        // Crea la richiesta da inviare all'API
        var requestBody = new
        {
            model = "meta-llama/Llama-3.2-11B-Vision-Instruct-Turbo", // Modello da usare
            messages = new List<object> { new { role = "user", content = userInput } }, // Messaggi precedenti
            max_tokens = 1000, // Numero massimo di token
            temperature = 0.7, // Controllo della creatività
            top_p = 1, // Parametro di diversità
            top_k = 50, // Altro parametro di diversità
            repetition_penalty = 1, 
            stop = new[] { "<|eot_id|>", "<|eom_id|>" } //interruzione
        };

        // Invia la richiesta all'API
        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var suggestion = JsonConvert.DeserializeObject<AIResponse>(jsonResponse);
            // Gestisci il suggerimento qui (ad esempio, visualizzalo nel gioco)
            Debug.Log("Suggerimento dall'IA: " + suggestion.choices[0].message.content);
        }
        else
        {
            Debug.LogError("Errore nella richiesta all'API: " + response.StatusCode);
        }
    }

    // Crea una classe per deserializzare la risposta dall'API
    public class AIResponse
    {
        public List<Choice> choices; // Lista di scelte

        public class Choice
        {
            public Message message;
        }

        public class Message
        {
            public string content; // Contenuto del messaggio
        }
    }

    // Censura contenuti inappropriati
    private string CensorContent(string input)
    {
        string[] inappropriateWords = { "cazzo", "vaffanculo" };
        foreach (var word in inappropriateWords)
        {
            input = input.Replace(word, "****"); // Censura la parola
        }
        return input;
    }

    // Salva la scelta dell'utente
    public void SaveUserChoice(string choice)
    {
        PlayerPrefs.SetString("UserChoice", choice);
        PlayerPrefs.Save();
    }

    // Carica la scelta precedente dell'utente
    public string LoadUserChoice()
    {
        return PlayerPrefs.GetString("UserChoice", "");
    }

    // Gestisci la scelta della carta
    public void OnCardChosen(string card)
    {
        string previousChoices = LoadUserChoice();
        GetSuggestion(previousChoices + " " + card); // Invoca la funzione di suggerimento
        SaveUserChoice(card); // Salva la scelta corrente
    }
}
