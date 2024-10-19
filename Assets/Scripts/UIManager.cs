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

    private bool isTextboxVisible = false;

    private void Start()
    {
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
        if (storyTextbox != null)
        {
            var textComponent = storyTextbox.GetComponent<Text>();
            if (textComponent == null)
            {
                textComponent = storyTextbox.GetComponent<TMP_Text>(); // Prova TMP_Text se Text è nullo
            }
            if (textComponent != null)
            {
                textComponent.text = suggestion;
                storyManager.AppendToStory(suggestion);
            }
            else
            {
                Debug.LogError("Componente Text o TMP_Text mancante su storyTextbox.");
            }
        }
        else
        {
            Debug.LogError("storyTextbox non assegnato in UIManager.");
        }
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'AI: " + errorMessage);
    }

    private void OnCopyButtonClicked()
    {
        var textComponent = storyTextbox.GetComponent<Text>();
        if (textComponent == null)
        {
            textComponent = storyTextbox.GetComponent<TMP_Text>();
        }

        if (textComponent != null)
        {
            GUIUtility.systemCopyBuffer = textComponent.text;
        }
        else
        {
            Debug.LogError("Non è stato possibile copiare il testo: nessun componente Text o TMP_Text trovato.");
        }
    }
}
