using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public StoryGenerator storyGenerator;  // Riferimento alla classe StoryGenerator
    private string currentStory = "";      // La storia corrente generata

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
        Debug.Log("Nuovo testo aggiunto alla storia: " + newText);
    }

    // Metodo per ottenere la storia corrente
    public string GetCurrentStory()
    {
        return currentStory;  // Ritorna la storia corrente generata finora
    }
}
