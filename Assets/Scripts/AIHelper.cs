using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class AIHelper : MonoBehaviour
{
    private string apiEndpoint = "http://localhost:5000/get_suggestion"; // URL dell'app Flask
    public IEnumerator FetchSuggestion(string storyContext, System.Action<string> onSuccess, System.Action<string> onError)
    {
        string sanitizedStory = ReplaceUnsupportedCharacters(storyContext); // Sostituisci i caratteri non supportati

        var jsonContent = new
        {
            story = sanitizedStory
        };

        string jsonString = JsonUtility.ToJson(jsonContent);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest request = new UnityWebRequest(apiEndpoint, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("Errore: " + request.error);
            }
            else
            {
                var responseText = request.downloadHandler.text;
                Debug.Log("Risposta grezza dall'IA: " + responseText); // Log della risposta grezza

                try
                {
                    var jsonResponse = JsonUtility.FromJson<AIResponse>(responseText);
                    Debug.Log("Suggerimento ricevuto: " + jsonResponse.suggestion); // Log del suggerimento
                    onSuccess?.Invoke(jsonResponse.suggestion);
                }
                catch (System.Exception e)
                {
                    onError?.Invoke("Errore nel parsing della risposta: " + e.Message);
                }
            }
        }
    }


    private string ReplaceUnsupportedCharacters(string input)
    {
        return input.Replace("’", "'") // Sostituisci apostrofo con uno standard
                    .Replace("è", "e") // Sostituzioni di caratteri accentati
                    .Replace("é", "e")
                    .Replace("à", "a")
                    .Replace("ù", "u")
                    .Replace("ò", "o")
                    .Replace("’", "'")
                    .Replace("“", "\"") // Sostituzioni di virgolette
                    .Replace("”", "\"")
                    .Replace("‘", "'")
                    // Aggiungi altre sostituzioni secondo necessità
                    ;
    }


    [System.Serializable]
    public class AIResponse
    {
        public string suggestion; // Campo per contenere il suggerimento ricevuto dall'API
    }
}
