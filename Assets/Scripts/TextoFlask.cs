using System;
using UnityEngine;

public class TextoFlask : MonoBehaviour
{
    public FlaskManager flaskManager;  // Riferimento a FlaskManager

    // Questo metodo riceve il testo da TextConverter e lo invia a FlaskManager
    public void InviaTestoAlFlask(string testo)
    {
        if (flaskManager != null)
        {
            // Chiamata a FlaskManager per inviare il testo
            flaskManager.SendChoiceAndStoryToAI("", testo, (response) =>
            {
                // Gestisci la risposta dell'IA (opzionale)
                Debug.Log("Risposta IA: " + response);
            });
        }
        else
        {
            Debug.LogError("FlaskManager non è assegnato in TextoFlask!");
        }
    }
}
