using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AnotherFileBrowser.Windows;
using UnityEngine.Networking;
public class FileManager : MonoBehaviour
{
    public RawImage rawImage;
    public RawImage rawImage1;
    static private string[] imagePath = new string[3];

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;
        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            Debug.Log(rawImage.gameObject.name);
            setPath(path);
            Debug.Log(path);
            StartCoroutine(LoadImage(path));
        }
        );
    }

    private IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var urwTexture = DownloadHandlerTexture.GetContent(uwr);
                rawImage.texture = urwTexture;
                rawImage1.texture = urwTexture;
            }
        }
    }

    private void setPath(string path)
    {
        switch (rawImage.gameObject.name)
        {
            case "RawImage1": imagePath[0] = path; break;
            case "RawImage2": imagePath[1] = path; break;
            case "RawImage3": imagePath[2] = path; break;
        }
    }

    public string[] getPath()
    {
        return imagePath;
    }
}
