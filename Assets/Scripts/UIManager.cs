using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button helpButton;
    public GameObject storyTextbox;
    public GameObject panelBackground;
    public Button copyButton;
    public Button closeButton;
    public AIHelper aiHelper;
    public StoryManager storyManager;  // Assicurati di avere un riferimento a StoryManager

    private bool isTextboxVisible = false;

    private void Start()
    {
        // Debug
        if (helpButton == null) Debug.LogError("helpButton non assegnato!");
        if (storyTextbox == null) Debug.LogError("storyTextbox non assegnato!");
        if (panelBackground == null) Debug.LogError("panelBackground non assegnato!");
        if (copyButton == null) Debug.LogError("copyButton non assegnato!");
        if (closeButton == null) Debug.LogError("closeButton non assegnato!");
        if (aiHelper == null) Debug.LogError("aiHelper non assegnato!");
        if (storyManager == null) Debug.LogError("storyManager non assegnato!");

        // Nascondi la TextBox, i bottoni e il panel all'avvio
        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        panelBackground.SetActive(false);

        helpButton.onClick.AddListener(OnHelpButtonClicked);
        copyButton.onClick.AddListener(OnCopyButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnHelpButtonClicked()
    {
        isTextboxVisible = !isTextboxVisible;
        storyTextbox.SetActive(isTextboxVisible);
        copyButton.gameObject.SetActive(isTextboxVisible);
        closeButton.gameObject.SetActive(isTextboxVisible);
        panelBackground.SetActive(isTextboxVisible);

        if (isTextboxVisible)
        {
            // Ottieni la storia corrente
            string currentStory = storyManager.GetCurrentStory();
            StartCoroutine(aiHelper.FetchSuggestion(currentStory, OnAISuccess, OnAIError));
        }
    }

    private void OnCloseButtonClicked()
    {
        isTextboxVisible = false;
        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        panelBackground.SetActive(false);
    }

    private void OnAISuccess(string suggestion)
    {
        TMP_Text tmpTextComponent = storyTextbox.GetComponent<TMP_Text>();
        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = suggestion;
        }
        else
        {
            Debug.LogError("Nessun componente TMP_Text trovato in storyTextbox.");
        }

        // Aggiungi il suggerimento dell'IA alla storia
        storyManager.AppendToStory(suggestion);
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'IA: " + errorMessage);
    }

    private void OnCopyButtonClicked()
    {
        TMP_Text tmpTextComponent = storyTextbox.GetComponent<TMP_Text>();
        if (tmpTextComponent != null)
        {
            GUIUtility.systemCopyBuffer = tmpTextComponent.text;
        }
        else
        {
            Debug.LogError("Non è stato possibile copiare il testo: nessun componente TMP_Text trovato.");
        }
    }

    public void PlayerMakesChoice(string choice)
    {
        // Aggiungi la scelta alla storia
        storyManager.AddChoice(choice);

        // Invia la storia aggiornata all'IA
        string currentStory = storyManager.GetCurrentStory();
        StartCoroutine(aiHelper.FetchSuggestion(currentStory, OnAISuccess, OnAIError));
    }
}
