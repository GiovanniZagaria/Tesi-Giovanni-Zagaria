using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button helpButton;         // Il bottone "Aiuto"
    public GameObject storyTextbox;   // Il GameObject della TextBox
    public GameObject panelBackground; // Il GameObject del Panel/Background opaco
    public Button copyButton;         // Il bottone "Copia"
    public Button closeButton;        // Il bottone "X" per chiudere la TextBox
    public AIHelper aiHelper;         // Riferimento alla classe che gestisce l'IA
    public StoryManager storyManager; // Riferimento alla classe che gestisce la storia

    private bool isTextboxVisible = false; // Flag per tenere traccia della visibilità della TextBox

    private void Start()
    {
        // Nascondi la TextBox, il bottone "Copia", il pulsante "X" e il panel all'avvio
        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);  
        panelBackground.SetActive(false);  // Nascondi anche il pannello

        // Collega il bottone "Aiuto" al metodo OnHelpButtonClicked
        helpButton.onClick.AddListener(OnHelpButtonClicked);

        // Collega il bottone "Copia" al metodo OnCopyButtonClicked
        copyButton.onClick.AddListener(OnCopyButtonClicked);

        // Collega il bottone "X" al metodo per chiudere la TextBox
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    // Funzione per gestire la comparsa della TextBox e chiamare l'IA
    private void OnHelpButtonClicked()
    {
        // Alterna la visibilità della TextBox, del bottone "Copia", del bottone "X" e del panel
        isTextboxVisible = !isTextboxVisible;
        storyTextbox.SetActive(isTextboxVisible);
        copyButton.gameObject.SetActive(isTextboxVisible);
        closeButton.gameObject.SetActive(isTextboxVisible);
        panelBackground.SetActive(isTextboxVisible); // Mostra il panel insieme agli altri elementi

        // Se la TextBox è visibile, avvia la richiesta all'IA
        if (isTextboxVisible)
        {
            string currentStory = storyManager.GetCurrentStory();
            StartCoroutine(aiHelper.FetchSuggestion(currentStory, OnAISuccess, OnAIError));
        }
    }

    // Funzione per nascondere la TextBox, il panel e gli altri elementi quando si clicca "X"
    private void OnCloseButtonClicked()
    {
        // Nascondi tutti gli elementi: TextBox, bottone "Copia", bottone "X" e panel
        isTextboxVisible = false;
        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        panelBackground.SetActive(false);  // Nasconde il pannello opaco
    }

    // Callback per gestire la risposta dell'IA con successo
    private void OnAISuccess(string suggestion)
{
    if (storyTextbox != null)
    {
        var textComponent = storyTextbox.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = suggestion; // Imposta il testo solo se non è nullo
            storyManager.AppendToStory(suggestion);  // Aggiungi il suggerimento alla storia
        }
        else
        {
            Debug.LogError("Componente Text mancante su storyTextbox.");
        }
    }
    else
    {
        Debug.LogError("storyTextbox non assegnato in UIManager.");
    }
}

    // Callback in caso di errore nella chiamata all'IA
    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'AI: " + errorMessage);
    }

    // Funzione per copiare il contenuto della TextBox nella clipboard
    private void OnCopyButtonClicked()
    {
        GUIUtility.systemCopyBuffer = storyTextbox.GetComponent<Text>().text;
    }
}
