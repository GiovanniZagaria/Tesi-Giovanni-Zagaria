using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class AIHelper : MonoBehaviour
{
    private string apiEndpoint = "http://localhost:5000/get_suggestion"; // URL dell'app Flask

    public IEnumerator FetchSuggestion(string storyContext, System.Action<string> onSuccess, System.Action<string> onError)
    {
        var jsonContent = new
        {
            story = storyContext
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
                var jsonResponse = JsonUtility.FromJson<AIResponse>(responseText);
                onSuccess?.Invoke(jsonResponse.suggestion);
            }
        }
    }

    [System.Serializable]
    public class AIResponse
    {
        public string suggestion; // Campo per contenere il suggerimento ricevuto dall'API
    }
}
