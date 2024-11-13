using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;

public class FlaskManager : MonoBehaviour
{
    private Process flaskProcess;
    private string storyText;

    void Start()
    {
        StartFlaskServer();
        StartCoroutine(ReceiveContentCoroutine());
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

    public void SendChoiceAndStoryToAI(string choice, string story)
    {
        StartCoroutine(SendChoiceAndStoryCoroutine(choice, story));
    }

    private IEnumerator SendChoiceAndStoryCoroutine(string choice, string story)
    {
        string sendChoiceAndStoryUrl = "http://127.0.0.1:5000/get_suggestion";

        JObject jsonData = new JObject
        {
            { "choice", choice },
            { "story", story }
        };

        UnityEngine.Debug.Log("Invio JSON con scelta e storia: " + jsonData.ToString());

        using (UnityWebRequest request = new UnityWebRequest(sendChoiceAndStoryUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.LogError("Errore nell'invio della scelta e della storia all'IA: " + request.error);
            }
            else
            {
                var jsonResponse = JObject.Parse(request.downloadHandler.text);
                string suggestion = jsonResponse["suggestion"]?.ToString();
                UnityEngine.Debug.Log("Suggerimento ricevuto dall'IA: " + suggestion);
                UpdateStory(suggestion);
            }
        }
    }

    private IEnumerator ReceiveContentCoroutine()
    {
        while (true)
        {
            storyText = GetCurrentStoryText();
            if (!string.IsNullOrEmpty(storyText))
            {
                SendChoiceAndStoryToAI("Inserisci la tua scelta qui", storyText);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private string GetCurrentStoryText()
    {
        return storyText;
    }

    private void UpdateStory(string suggestion)
    {
        storyText += " " + suggestion;
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
