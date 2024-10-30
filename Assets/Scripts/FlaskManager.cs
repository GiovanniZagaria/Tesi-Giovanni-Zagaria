using System.Diagnostics;
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
}
