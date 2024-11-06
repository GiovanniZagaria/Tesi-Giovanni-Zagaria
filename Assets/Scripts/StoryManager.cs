/*using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private string currentStory = ""; // Storia attuale
    private List<string> choicesMade = new List<string>(); // Scelte fatte dall'utente
    private FlaskManager flaskManager;

    void Start()
    {
        flaskManager = FindObjectOfType<FlaskManager>();
        LoadStoryData(); // Carica i dati salvati all'avvio
    }

    // Aggiunge una scelta alla storia e la invia all'IA
    public void AddChoice(string choice)
    {
        if (!string.IsNullOrEmpty(choice))
        {
            choicesMade.Add(choice);
            currentStory += " " + choice; // Aggiungi la scelta alla storia
            flaskManager.SendChoicesToAI(choice); // Invia la scelta all'IA
            Debug.Log("Choice added: " + choice); // Debug
        }
    }

    // Aggiunge testo alla storia
    public void AppendToStory(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            currentStory += " " + newText; // Aggiungi il nuovo testo alla storia
            Debug.Log("Updated Story: " + currentStory); // Debug
        }
    }

    // Salva i dati della storia
    public void SaveStoryData()
    {
        PlayerPrefs.SetString("CurrentStory", currentStory);
        PlayerPrefs.SetString("ChoicesMade", string.Join(",", choicesMade)); // Salva le scelte come stringa
        PlayerPrefs.Save();
        Debug.Log("Story data saved."); // Debug
    }

    // Carica i dati della storia
    public void LoadStoryData()
    {
        if (PlayerPrefs.HasKey("CurrentStory"))
        {
            currentStory = PlayerPrefs.GetString("CurrentStory");
            Debug.Log("Current story loaded: " + currentStory); // Debug
        }
        if (PlayerPrefs.HasKey("ChoicesMade"))
        {
            choicesMade = new List<string>(PlayerPrefs.GetString("ChoicesMade").Split(',')); // Carica le scelte
            Debug.Log("Choices loaded: " + string.Join(", ", choicesMade)); // Debug
        }
    }

    // Metodo per ottenere la storia attuale
    public string GetCurrentStory()
    {
        return currentStory;
    }

    // Metodo per ottenere tutte le scelte fatte
    public List<string> GetChoicesMade()
    {
        return choicesMade;
    }
}*/

using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private string currentStory = ""; // Storia attuale
    private List<string> choicesMade = new List<string>(); // Scelte fatte dall'utente
    private FlaskManager flaskManager;

    void Start()
    {
        flaskManager = FindObjectOfType<FlaskManager>();
        LoadStoryData(); // Carica i dati salvati all'avvio
    }

    // Aggiunge una scelta alla storia e la invia all'IA
    public void AddChoice(string choice)
    {
        if (!string.IsNullOrEmpty(choice))
        {
            choicesMade.Add(choice);
            currentStory += " " + choice; // Aggiungi la scelta alla storia
            flaskManager.SendChoicesToAI(choice); // Invia la scelta all'IA
            Debug.Log("Choice added: " + choice); // Log della scelta aggiunta
        }
    }

    // Aggiunge testo alla storia
    public void AppendToStory(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            currentStory += " " + newText; // Aggiungi il nuovo testo alla storia
            Debug.Log("Updated Story: " + currentStory); // Log della storia aggiornata
        }
    }

    // Salva i dati della storia
    public void SaveStoryData()
    {
        PlayerPrefs.SetString("CurrentStory", currentStory);
        PlayerPrefs.SetString("ChoicesMade", string.Join(",", choicesMade)); // Salva le scelte come stringa
        PlayerPrefs.Save();
        Debug.Log("Story data saved."); // Log di salvataggio della storia
    }

    // Carica i dati della storia
    public void LoadStoryData()
    {
        if (PlayerPrefs.HasKey("CurrentStory"))
        {
            currentStory = PlayerPrefs.GetString("CurrentStory");
            Debug.Log("Current story loaded: " + currentStory); // Log della storia caricata
        }
        if (PlayerPrefs.HasKey("ChoicesMade"))
        {
            choicesMade = new List<string>(PlayerPrefs.GetString("ChoicesMade").Split(',')); // Carica le scelte
            Debug.Log("Choices loaded: " + string.Join(", ", choicesMade)); // Log delle scelte caricate
        }
    }

    // Metodo per ottenere la storia attuale
    public string GetCurrentStory()
    {
        return currentStory;
    }

    // Metodo per ottenere tutte le scelte fatte
    public List<string> GetChoicesMade()
    {
        return choicesMade;
    }
}

