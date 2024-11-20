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
                    newSprite(gameObject.name);
                    ChangeTexture(gameObject.name);

                    if (testo == null)
                    {
                        Debug.LogError("Testo è null. Controlla se è assegnato correttamente.");
                        return;
                    }

                    testo.text = GetComponent<Text>().text;
                    string promptText = testo.text;
                    Debug.Log("Testo della carta: " + promptText);

                    // Verifica che promptText non sia vuoto
                    if (!string.IsNullOrEmpty(promptText))
                    {
                        // Otteniamo la storia corrente
                        string storyText = FindObjectOfType<StoryManager>().GetCurrentStory();
                        Debug.Log("Testo della storia corrente: " + storyText);

                        // Se la storia è vuota, passiamo una frase predefinita
                        if (string.IsNullOrEmpty(storyText))
                        {
                            storyText = "Inizia la storia..."; // Stringa predefinita
                            Debug.LogWarning("La storia era vuota, impostata la stringa iniziale.");
                        }

                        // Passiamo la scelta e la storia all'IA
                        FindObjectOfType<FlaskManager>().SendChoiceAndStoryToAI(promptText, storyText, OnAISuccess);
                        FindObjectOfType<StoryManager>().AddChoice(promptText);
                    }
                    else
                    {
                        Debug.LogWarning("promptText è vuoto, nessuna richiesta inviata all'IA.");
                    }
                }
            }
        }
    }



    private void OnAISuccess(string suggestion)
    {
        Debug.Log("Suggerimento ricevuto: " + suggestion);
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
