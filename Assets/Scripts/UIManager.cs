using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button helpButton;
    public GameObject storyTextbox;
    public TextMeshProUGUI tmpTextComponent;
    public Button copyButton;
    public Button closeButton;
    public AIHelper aiHelper;
    public StoryManager storyManager;  // Assicurati di avere un riferimento a StoryManager
    public FlaskManager flaskManager;  // Riferimento al FlaskManager per inviare il testo

    private bool isTextboxVisible = false;

    private void Start()
    {
        if (helpButton == null) Debug.LogError("helpButton non assegnato!");
        if (storyTextbox == null) Debug.LogError("storyTextbox non assegnato!");       
        if (copyButton == null) Debug.LogError("copyButton non assegnato!");
        if (closeButton == null) Debug.LogError("closeButton non assegnato!");
        if (aiHelper == null) Debug.LogError("aiHelper non assegnato!");
        if (storyManager == null) Debug.LogError("storyManager non assegnato!");
        if (flaskManager == null) Debug.LogError("flaskManager non assegnato!");

        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        

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
        

        if (isTextboxVisible)
        {
            string currentStory = storyManager.GetCurrentStory();
            TMP_Text tmpTextComponent = storyTextbox.GetComponent<TMP_Text>();

            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = "Caricamento...";  // Mostra "Caricamento..."
            }

            if (!string.IsNullOrEmpty(currentStory))
            {
                // Invia il testo direttamente al FlaskManager
                flaskManager.SendChoiceAndStoryToAI("", currentStory, OnAISuccess);
            }
            else
            {
                Debug.LogWarning("La storia corrente è vuota, nessuna richiesta inviata all'IA.");
            }
        }
    }

    private void OnCloseButtonClicked()
    {
        isTextboxVisible = false;
        storyTextbox.SetActive(false);
        copyButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        
    }

    private void OnAISuccess(string suggestion)
    {
        TMP_Text tmpTextComponentSuccess = tmpTextComponent.GetComponent<TMP_Text>();
        if (tmpTextComponentSuccess != null)
        {
            tmpTextComponentSuccess.text = suggestion;
        }
        else
        {
            Debug.LogError("Nessun componente TMP_Text trovato in storyTextbox.");
        }

        // Aggiungi la risposta dell'IA alla storia
        storyManager.AppendToStory(suggestion);
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'IA: " + errorMessage);
    }

    private void OnCopyButtonClicked()
    {
        TMP_Text tmpTextComponentCopy = tmpTextComponent.GetComponent<TMP_Text>();
        if (tmpTextComponentCopy != null)
        {
            GUIUtility.systemCopyBuffer = tmpTextComponentCopy.text;
        }
        else
        {
            Debug.LogError("Non è stato possibile copiare il testo: nessun componente TMP_Text trovato.");
        }
    }

    public void PlayerMakesChoice(string choice)
    {
        storyManager.AddChoice(choice);
        string currentStory = storyManager.GetCurrentStory();
        flaskManager.SendChoiceAndStoryToAI(choice, currentStory, OnAISuccess);
    }
}
