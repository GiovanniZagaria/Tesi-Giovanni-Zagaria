using System.IO;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private string storyFilePath;

    private void Start()
    {
        // Percorso del file .txt per la storia
        storyFilePath = Path.Combine(Application.persistentDataPath, "story.txt");

        // Se il file non esiste, creane uno vuoto
        if (!File.Exists(storyFilePath))
        {
            File.WriteAllText(storyFilePath, "");
        }
    }

    // Ottieni il contenuto attuale della storia
    public string GetCurrentStory()
    {
        return File.ReadAllText(storyFilePath);
    }

    // Aggiungi nuovo testo alla storia e salvalo
    public void AppendToStory(string newText)
    {
        File.AppendAllText(storyFilePath, newText + "\n");
    }

    // Carica la storia salvata (può essere usata all'inizio del gioco)
    public void LoadStory()
    {
        if (File.Exists(storyFilePath))
        {
            string loadedStory = File.ReadAllText(storyFilePath);
            Debug.Log("Storia caricata: " + loadedStory);
        }
        else
        {
            Debug.LogWarning("Nessuna storia salvata trovata.");
        }
    }
}
