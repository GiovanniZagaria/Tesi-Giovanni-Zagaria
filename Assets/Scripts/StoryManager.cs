using System;
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

        if (flaskManager == null)
        {
            Debug.LogError("FlaskManager non trovato! Assicurati che il componente FlaskManager sia presente nella scena.");
        }

        LoadStoryData(); // Carica i dati salvati all'avvio
    }

    // Aggiunge una scelta alla storia e invia sia la scelta che la storia corrente all'IA
    public void AddChoice(string choice)
    {
        if (!string.IsNullOrEmpty(choice))
        {
            choicesMade.Add(choice);
            currentStory += " " + choice; // Aggiungi la scelta alla storia

            // Controlla che flaskManager sia disponibile prima di chiamare SendChoiceAndStoryToAI
            if (flaskManager != null)
            {
                flaskManager.SendChoiceAndStoryToAI(choice, currentStory, OnAISuccess); // Invia sia la scelta che la storia all'IA
                Debug.Log("Choice and current story sent to AI: " + choice + " | Current Story: " + currentStory); // Log della scelta e storia inviate
            }
            else
            {
                Debug.LogError("FlaskManager è null, impossibile inviare la scelta e la storia all'IA.");
            }
        }
    }

    private void OnAISuccess(string suggestion)
    {
        Debug.Log("Suggerimento ricevuto: " + suggestion);
        AppendToStory(suggestion); // Aggiorna la storia con il suggerimento ricevuto
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
