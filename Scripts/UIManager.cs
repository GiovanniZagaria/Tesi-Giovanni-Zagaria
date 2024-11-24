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
    public StoryManager storyManager;
    public FlaskManager flaskManager;

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
            // Visualizza solo la storia, senza inviare nulla a FlaskManager
            string currentStory = storyManager.GetCurrentStory();

            // Mostra "Caricamento..." mentre la storia viene visualizzata
            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = "Caricamento...";
            }

            if (!string.IsNullOrEmpty(currentStory))
            {
                tmpTextComponent.text = currentStory; // Visualizza la storia attuale
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
        // Svuota la textbox prima di visualizzare la nuova risposta
        TMP_Text tmpTextComponentSuccess = tmpTextComponent.GetComponent<TMP_Text>();
        if (tmpTextComponentSuccess != null)
        {
            tmpTextComponentSuccess.text = "";  // Rimuove tutto il testo precedente
            tmpTextComponentSuccess.text = suggestion;  // Mostra solo la risposta corrente
        }

        // Aggiorna la storia con la nuova risposta (se serve per altre logiche)
        storyManager.AppendToStory(suggestion);
    }


    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'IA: " + errorMessage);
    }

    private void OnCopyButtonClicked()
    {
        if (tmpTextComponent != null)
        {
            GUIUtility.systemCopyBuffer = tmpTextComponent.text; // Copia il testo
        }
        else
        {
            Debug.LogError("Non è stato possibile copiare il testo: nessun componente TMP_Text trovato.");
        }
    }

    // Funzione per inviare la scelta fatta dall'utente
    public void PlayerMakesChoice(string choice)
    {
        // La storia si aggiorna con la scelta
        storyManager.AddChoice(choice);

        // Invia solo la scelta e la storia a FlaskManager, senza inviare altre informazioni
        string currentStory = storyManager.GetCurrentStory();
        if (!string.IsNullOrEmpty(currentStory))
        {
            flaskManager.SendChoiceAndStoryToAI(choice, currentStory, OnAISuccess);
        }
        else
        {
            Debug.LogWarning("La storia corrente è vuota, nessuna richiesta inviata all'IA.");
        }
    }
}


/*
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
    public StoryManager storyManager;
    public FlaskManager flaskManager;

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
            // Visualizza solo la storia, senza inviare nulla a FlaskManager
            string currentStory = storyManager.GetCurrentStory();

            // Mostra "Caricamento..." mentre la storia viene visualizzata
            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = "Caricamento...";
            }

            if (!string.IsNullOrEmpty(currentStory))
            {
                tmpTextComponent.text = currentStory; // Visualizza la storia attuale
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
        // Svuota la textbox prima di aggiungere la risposta
        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = "";  // Svuota la Textbox
            tmpTextComponent.text = suggestion;  // Aggiungi solo la nuova risposta
        }

        // Aggiungi la risposta alla storia, se desiderato
        storyManager.AppendToStory(suggestion);
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore nell'IA: " + errorMessage);
    }

    private void OnCopyButtonClicked()
    {
        if (tmpTextComponent != null)
        {
            GUIUtility.systemCopyBuffer = tmpTextComponent.text; // Copia il testo
        }
        else
        {
            Debug.LogError("Non è stato possibile copiare il testo: nessun componente TMP_Text trovato.");
        }
    }

    // Funzione per inviare la scelta fatta dall'utente
    public void PlayerMakesChoice(string choice)
    {
        // La storia si aggiorna con la scelta
        storyManager.AddChoice(choice);

        // Invia solo la scelta e la storia a FlaskManager, senza inviare altre informazioni
        string currentStory = storyManager.GetCurrentStory();
        if (!string.IsNullOrEmpty(currentStory))
        {
            flaskManager.SendChoiceAndStoryToAI(choice, currentStory, OnAISuccess);
        }
        else
        {
            Debug.LogWarning("La storia corrente è vuota, nessuna richiesta inviata all'IA.");
        }
    }
}*/



