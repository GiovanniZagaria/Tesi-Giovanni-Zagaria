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
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IdeaButtonHandler : MonoBehaviour
{
    public Button ideaButton;             // Il pulsante "Aiuto"
    public GameObject textBoxPrefab;      // Prefab per la TextBox dinamica
    public Transform textBoxContainer;    // Container per le TextBox (es. un Panel in cui le TextBox vengono aggiunte)
    public StoryManager storyManager;     // Gestore della storia salvata

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

    void OnAiSuggestionReceived(string suggestion)
    {
        // Crea una nuova TextBox con il testo della storia generata
        GameObject newTextBox = Instantiate(textBoxPrefab, textBoxContainer);

        // Trova i componenti nella nuova TextBox
        Text textComponent = newTextBox.transform.Find("StoryText").GetComponent<Text>();
        Button copyButton = newTextBox.transform.Find("CopyButton").GetComponent<Button>();

        // Imposta il testo e il pulsante copia
        textComponent.text = suggestion;
        copyButton.onClick.AddListener(() => CopyToClipboard(textComponent.text));

        // Salva la storia aggiornata
        storyManager.SaveStory(storyManager.GetSavedStory() + "\n" + suggestion);
    }

    // Metodo per copiare il testo negli appunti
    void CopyToClipboard(string text)
    {
        GUIUtility.systemCopyBuffer = text;
    }
}
