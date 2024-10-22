using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public StoryGenerator storyGenerator;  // Riferimento alla classe StoryGenerator
    public FlaskManager flaskManager;      // Riferimento a FlaskManager
    private string currentStory = "";      // La storia corrente generata
    private List<string> choicesMade = new List<string>(); // Lista delle scelte fatte

    // Metodo per ottenere il contesto della storia dalle carte e dagli input di testo
    public string GetStoryContext()
    {
        if (storyGenerator != null)
        {
            // Chiama il metodo CollectStoryContext di StoryGenerator per raccogliere il contesto
            return storyGenerator.CollectStoryContext();
        }
        else
        {
            Debug.LogError("StoryGenerator non assegnato a StoryManager!");
            return "";
        }
    }

    // Metodo per aggiungere nuovo testo (suggerimento dell'IA) alla storia
   public void AppendToStory(string newText)
{
    currentStory += newText + "\n";  // Aggiunge il nuovo testo con una nuova riga
    
    // Esegui il log solo se il testo non è vuoto o null
    if (!string.IsNullOrEmpty(newText))
    {
        UnityEngine.Debug.Log("Nuovo testo aggiunto alla storia: " + newText);
    }
    else
    {
        UnityEngine.Debug.LogWarning("Tentativo di aggiungere testo vuoto alla storia.");
    }
}


    // Metodo per ottenere la storia corrente
    public string GetCurrentStory()
    {
        return currentStory;  // Ritorna la storia corrente generata finora
    }

    // Metodo per aggiungere una scelta alla lista delle scelte fatte
    public void AddChoice(string choice)
    {
        choicesMade.Add(choice);
        currentStory += "Scelta: " + choice + "\n"; // Aggiungi la scelta alla storia
        Debug.Log("Scelta aggiunta: " + choice);

        // Invia la scelta all'API tramite FlaskManager
        if (flaskManager != null)
        {
            // Invia le scelte all'API per continuare la storia
            string jsonData = CreateJsonFromChoices();
            flaskManager.SendChoicesToAI(jsonData);
        }
        else
        {
            Debug.LogError("FlaskManager non assegnato a StoryManager!");
        }
    }

    // Metodo per ottenere tutte le scelte fatte
    public List<string> GetChoicesMade()
    {
        return choicesMade; // Ritorna tutte le scelte fatte
    }

    // Metodo per creare un JSON con le scelte fatte
    private string CreateJsonFromChoices()
    {
        // Crea un oggetto anonimo contenente la storia attuale e le scelte
        var data = new
        {
            storyContext = GetStoryContext(),
            choices = choicesMade
        };

        // Serializza l'oggetto in formato JSON
        return JsonUtility.ToJson(data);
    }
}
