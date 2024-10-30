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
                    testo.text = GetComponent<Text>().text;

                    // Recupera il testo dalla pagina di sinistra
                    string promptText = testo.text; // Supponiamo che testo contenga il testo della carta
                    Debug.Log("Testo della carta: " + promptText); // Debug

                    // Controlla se il testo è presente
                    if (!string.IsNullOrEmpty(promptText))
                    {
                        // Crea la richiesta per l'IA
                        string requestToAI = $"Prendendo il testo: \"{promptText}\", continua la storia in modo congruo.";
                        Debug.Log("Richiesta inviata all'IA: " + requestToAI); // Debug

                        // Invia la richiesta all'IA
                        FindObjectOfType<FlaskManager>().SendChoicesToAI(requestToAI);
                    }
                    else
                    {
                        Debug.LogWarning("Nessun testo da inviare all'IA.");
                    }

                    // Raccogliere il testo della storia
                    string storyText = FindObjectOfType<StoryManager>().GetCurrentStory(); // Ottieni la storia corrente
                    Debug.Log("Testo della storia corrente: " + storyText); // Debug

                    // Invia il testo della storia all'IA solo se è presente
                    if (!string.IsNullOrEmpty(storyText))
                    {
                        FindObjectOfType<FlaskManager>().SendStoryTextToAI(storyText);
                    }

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





/*using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePrompts : MonoBehaviour
{
    private string pathNuovoSprite = "Sprites/Spunti/";
    public SpriteRenderer scelta;
    public TMP_Text testo;
    private string pathNuovaTexture = "Texture/Spunti/";
    public Renderer pageTexture;
    public GameObject qui;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!MenuController.isPaused)
        {
            Chose();
        }
    }

    public void ChangeTexture(string nomeCarta)
    {
        // Carica la texture dalla cartella Resources utilizzando il percorso specificato
        Texture nuovaTexture = Resources.Load<Texture>(pathNuovaTexture + nomeCarta);

        if (nuovaTexture != null)
        {
            // Assicurati che l'oggetto abbia un componente Renderer
            Renderer renderer = pageTexture;

            Material[] materiali = renderer.materials;

            // Cambia solo il primo materiale (puoi adattare questa logica)
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
                    ChangeTexture(name);
                    testo.text = GetComponent<Text>().text;                    //GetComponent<Collider>().enabled = false;

                }
            }
        }
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
    }
}
*/