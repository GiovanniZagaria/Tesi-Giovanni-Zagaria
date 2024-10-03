using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class AIntegration : MonoBehaviour
{
    // Endpoint aggiornato per il modello specifico di Together.ai
    private static readonly string apiUrl = "https://api.together.xyz/playground/chat/meta-llama/Llama-3.2-11B-Vision-Instruct-Turbo";
    private static readonly string apiKey = "29753fd69be8903061f2b955add0fae38638917867f0ebe08ef643d2a75372be";

    public static IEnumerator GetAiSuggestion(string cardData, string previousStory, System.Action<string> onCompletion)
    {
        string input = previousStory + "\n\nCard Information: " + cardData + "\nGenerate a story suggestion for a child-friendly game. Ensure the content is appropriate for children (no offensive language).";

        var postData = new
        {
            model = "meta-llama/Llama-3.2-11B-Vision-Instruct-Turbo", // Modello IA aggiornato
            messages = new List<object>
            {
                new {
                    role = "system", 
                    content = "You are a helpful assistant in a storytelling game."
                },
                new {
                    role = "user", 
                    content = input
                }
            },
            max_tokens = 512,   // Numero di token massimi
            temperature = 0.7,  // Creatività
            top_p = 0.7,        // Parametro di campionamento
            top_k = 50,         // Numero massimo di candidati durante la generazione
            repetition_penalty = 1.0, // Penalità per ripetizioni
            stop = new string[] {"<|eot_id|>", "<|eom_id|>"}, // Token di stop
            stream = false      // No streaming per semplificare la gestione
        };

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            string jsonContent = JsonConvert.SerializeObject(postData);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            string responseBody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
            string generatedText = responseObject["choices"][0]["message"]["content"].ToString();

            onCompletion(generatedText);
        }
    }
}
