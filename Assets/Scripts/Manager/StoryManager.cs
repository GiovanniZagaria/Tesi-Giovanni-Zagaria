using System.IO;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // Percorso per salvare il file della storia
        filePath = Application.persistentDataPath + "/story.txt";
    }

    // Metodo per salvare la storia aggiornata in un file di testo
    public void SaveStory(string story)
    {
        File.WriteAllText(filePath, story);
    }

    // Metodo per recuperare la storia salvata in precedenza
    public string GetSavedStory()
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return ""; // Restituisce una stringa vuota se non esiste alcuna storia salvata
    }

    // Metodo per ottenere le informazioni della carta corrente selezionata dall'utente
    public string GetCurrentCardInfo()
    {
        // Esempio di come ottenere le informazioni della carta corrente (puoi modificarlo)
        return "Carta corrente: Esempio di informazione sulla carta";
    }
}
