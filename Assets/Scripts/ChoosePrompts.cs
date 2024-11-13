using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePrompts : MonoBehaviour
{
    private string pathNuovoSprite = "Sprites/Spunti/";
    public SpriteRenderer scelta; // SpriteRenderer per visualizzare la carta scelta
    public TMP_Text testo; // Testo che mostra informazioni sulla carta
    private string pathNuovaTexture = "Texture/Spunti/";
    public Renderer pageTexture; // Renderer per la pagina che mostra la texture
    public GameObject qui; // Oggetto associato alla scelta corrente
    public AIHelper aiHelper; // Riferimento all'AIHelper

    void Update()
    {
        if (!MenuController.isPaused)
        {
            Chose();
        }
    }

    public void ChangeTexture(string nomeCarta)
    {
        // Carica la texture dalla cartella Resources
        Texture nuovaTexture = Resources.Load<Texture>(pathNuovaTexture + nomeCarta);

        if (nuovaTexture != null)
        {
            Renderer renderer = pageTexture;
            Material[] materiali = renderer.materials;

            // Cambia solo il primo materiale
            materiali[1].mainTexture = nuovaTexture;
            qui.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Impossibile trovare la texture: " + pathNuovaTexture + nomeCarta);
        }
    }

    void Chose()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    // Cambia lo sprite e la texture
                    newSprite(gameObject.name);
                    ChangeTexture(gameObject.name);

                    // Verifica se il testo è stato correttamente assegnato
                    if (testo == null)
                    {
                        Debug.LogError("Testo è null. Controlla se è assegnato correttamente.");
                        return; // Esci se testo è null
                    }

                    testo.text = GetComponent<Text>().text;

                    // Recupera il testo dalla pagina di sinistra
                    string promptText = testo.text;
                    Debug.Log("Testo della carta: " + promptText);

                    // Controlla se il testo è presente
                    if (!string.IsNullOrEmpty(promptText))
                    {
                        // Crea la richiesta per l'IA
                        string requestToAI = $"Prendendo il testo: \"{promptText}\", questo è un testo contenuto in una carta, genera un suggerimento che risponda alla storia che sia brevissimo, massimo di due righe ";
                        Debug.Log("Richiesta inviata all'IA: " + requestToAI);

                        // Invia la richiesta all'IA
                        StartCoroutine(FindObjectOfType<AIHelper>().FetchSuggestion(promptText, OnAISuccess, OnAIError));
                    }
                    else
                    {
                        Debug.LogWarning("Nessun testo da inviare all'IA.");
                    }

                    // Raccogliere il testo della storia
                    string storyText = FindObjectOfType<StoryManager>().GetCurrentStory(); // Ottieni la storia corrente
                    Debug.Log("Testo della storia corrente: " + storyText); // Debug

                    // Invio di choice e storyText contemporaneamente a FlaskManager
                    FindObjectOfType<FlaskManager>().SendChoiceAndStoryToAI(promptText, storyText);

                    // Aggiungi la scelta e il testo al StoryManager
                    FindObjectOfType<StoryManager>().AddChoice(promptText); // Aggiungi la scelta
                    FindObjectOfType<StoryManager>().AppendToStory(storyText); // Aggiungi il testo

                    // Verifica le scelte memorizzate
                    var scelte = FindObjectOfType<StoryManager>().GetChoicesMade();
                    Debug.Log("Scelte memorizzate: " + string.Join(", ", scelte));
                }
            }
        }
    }

    private void OnAISuccess(string suggestion)
    {
        // Gestisci la risposta dell'IA
        Debug.Log("Suggerimento ricevuto: " + suggestion);
        // Aggiorna la storia o l'interfaccia utente qui
        FindObjectOfType<StoryManager>().AppendToStory(suggestion);
    }

    private void OnAIError(string errorMessage)
    {
        Debug.LogError("Errore durante la chiamata all'IA: " + errorMessage);
    }

    void newSprite(string name)
    {
        pathNuovoSprite = "Sprites/Spunti/";
        pathNuovoSprite += name;
        Sprite nuovoSprite = Resources.Load<Sprite>(pathNuovoSprite);

        // Ottenere il componente SpriteRenderer
        SpriteRenderer spriteRenderer = scelta;

        // Cambiare lo sprite
        if (spriteRenderer != null && nuovoSprite != null)
        {
            spriteRenderer.sprite = nuovoSprite;
        }
        else
        {
            Debug.LogError("Sprite non trovato o renderer non valido.");
        }
    }
}
