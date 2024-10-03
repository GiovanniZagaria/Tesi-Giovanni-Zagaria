using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IdeaButtonHandler : MonoBehaviour
{
    public Button ideaButton;       // Il pulsante "Aiuto"
    public Text storyTextbox;       // La Textbox dove si mostra la storia
    public StoryManager storyManager; // Classe che gestisce la storia salvata

    void Start()
    {
        ideaButton.onClick.AddListener(OnIdeaButtonClicked);
    }

    void OnIdeaButtonClicked()
    {
        string cardInfo = storyManager.GetCurrentCardInfo();  // Ottieni le informazioni della carta corrente
        string previousStory = storyManager.GetSavedStory();  // Ottieni la storia salvata fino a quel momento

        // Chiamata all'IA per ottenere suggerimenti
        StartCoroutine(AIntegration.GetAiSuggestion(cardInfo, previousStory, OnAiSuggestionReceived));
    }

    // Questo metodo viene chiamato quando si riceve il suggerimento dall'IA
    void OnAiSuggestionReceived(string suggestion)
    {
        // Aggiungi il suggerimento alla storia corrente nella Textbox
        storyTextbox.text += "\n" + suggestion;

        // Salva la storia aggiornata
        storyManager.SaveStory(storyTextbox.text);
    }
}
