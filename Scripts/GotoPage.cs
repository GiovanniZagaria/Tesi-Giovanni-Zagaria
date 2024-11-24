using UnityEngine;
using System;


public class GotoPage : MonoBehaviour
{
    public MegaBookBuilder book;
    public float num;

    // Nuovi campi aggiunti
    public CartaSelezionata cartaSelezionataScript;  // Campo per collegare lo script CartaSelezionata
    public int numeroCarta;  // Campo per associare il numero del case in CartaSelezionata

    // Riferimento alla gestione IA (FlaskManager)
    public FlaskManager flaskManager;

    // Lista dei numeri per cui non si richiede la CartaSelezionata
    public int[] numeriSenzaCartaSelezionata = {4, 6, 2, 11, 22 };

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    // Vai alla pagina del libro MegaBookBuilder
                    book.GoToPage(num);

                    // Verifica se il numero della pagina è tra quelli per cui non si richiede CartaSelezionata
                    if (!Array.Exists(numeriSenzaCartaSelezionata, n => n == numeroCarta))
                    {
                        // Se il numero non è tra quelli elencati, aggiorna la CartaSelezionata
                        if (cartaSelezionataScript != null)
                        {
                            cartaSelezionataScript.AggiornaCarta(numeroCarta);  // Passa il numero del caso selezionato

                            // Invia il testo aggiornato all'IA
                            if (flaskManager != null)
                            {
                                string titolo = cartaSelezionataScript.titolo;
                                string testo = cartaSelezionataScript.testo;

                                if (!string.IsNullOrEmpty(testo))
                                {
                                    flaskManager.SendChoiceAndStoryToAI(
                                        titolo,
                                        testo,
                                        OnAISuccess
                                    );
                                }
                                else
                                {
                                    Debug.LogWarning("Il testo della carta è vuoto, nessuna richiesta inviata all'IA.");
                                }
                            }
                            else
                            {
                                Debug.LogError("FlaskManager non è assegnato in GotoPage.");
                            }
                        }
                        else
                        {
                            Debug.LogError("CartaSelezionataScript non è assegnato in GotoPage.");
                        }
                    }
                    else
                    {
                        Debug.Log("Nessuna carta selezionata richiesta per questo numero.");
                    }
                }
            }
        }
    }

    private void OnAISuccess(string suggestion)
    {
        Debug.Log("Suggerimento ricevuto dall'IA: " + suggestion);

        // Aggiungi il suggerimento alla storia o gestiscilo come necessario
        StoryManager storyManager = FindObjectOfType<StoryManager>();
        if (storyManager != null)
        {
            storyManager.AppendToStory(suggestion);
        }
        else
        {
            Debug.LogError("StoryManager non trovato nella scena.");
        }
    }
}
