using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Linq;


public class AIHelper : MonoBehaviour
{
    private string apiEndpoint = "http://localhost:5000/get_suggestion";

    public IEnumerator FetchSuggestion(string storyContext, System.Action<string> onSuccess, System.Action<string> onError)
    {
        string sanitizedStory = ReplaceUnsupportedCharacters(storyContext);

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
                Debug.Log("Risposta grezza dall'IA: " + responseText);

                try
                {
                    var jsonResponse = JsonUtility.FromJson<AIResponse>(responseText);
                    string suggestion = LimitSuggestionToTwoLines(jsonResponse.suggestion);
                    Debug.Log("Suggerimento ricevuto: " + suggestion);
                    onSuccess?.Invoke(suggestion);
                }
                catch (System.Exception e)
                {
                    onError?.Invoke("Errore nel parsing della risposta: " + e.Message);
                }
            }
        }
    }

    private string LimitSuggestionToTwoLines(string suggestion)
    {
        var lines = suggestion.Split('\n');
        return string.Join("\n", lines.Length > 2 ? lines.Take(2) : lines);
    }

    private string ReplaceUnsupportedCharacters(string input)
    {
        return input.Replace("’", "'")
                    .Replace("è", "e")
                    .Replace("é", "e")
                    .Replace("à", "a")
                    .Replace("ù", "u")
                    .Replace("ò", "o")
                    .Replace("“", "\"")
                    .Replace("”", "\"")
                    .Replace("‘", "'");
    }

    [System.Serializable]
    public class AIResponse
    {
        public string suggestion;
    }
}
