using TMPro;
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
