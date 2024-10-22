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
    public StoryManager storyManager;
    public FlaskManager flaskManager; // Aggiungi il riferimento a FlaskManager

    private bool isTextboxVisible = false;

    public void Start()
    {
        // Avvia il server Flask quando parte l'interfaccia
        flaskManager.StartFlaskServer();

        // Nascondi la TextBox, il bottone "Copia", il pulsante "X" e il panel all'avvio
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
            Text uiTextComponent = storyTextbox.GetComponent<Text>();
            if (uiTextComponent != null)
            {
                uiTextComponent.text = suggestion;
            }
            else
            {
                Debug.LogError("Nessun componente Text o TMP_Text trovato in storyTextbox.");
            }
        }

        storyManager.AppendToStory(suggestion);
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'AI: " + errorMessage);
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
            Text uiTextComponent = storyTextbox.GetComponent<Text>();
            if (uiTextComponent != null)
            {
                GUIUtility.systemCopyBuffer = uiTextComponent.text;
            }
            else
            {
                Debug.LogError("Non è stato possibile copiare il testo: nessun componente Text o TMP_Text trovato.");
            }
        }
    }
}
