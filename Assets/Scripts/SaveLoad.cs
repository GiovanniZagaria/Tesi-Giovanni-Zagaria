using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    public List<TMP_InputField> inputFields;
    public List<RawImage> checkImage;
    public List<RawImage> images;
    public List<RawImage> images2;
    public List<TMP_Text> dateText;
    private string[] path = new string[3];
    private Texture imageOn;
    private string pathToFile;
    FileManager fileManager;
    static public int saveSlot;

    public FlaskManager flaskManager;  // Riferimento a FlaskManager

    private void Awake()
    {
        if (MenuController.isLoad)
        {
            CaricaDati(saveSlot);
        }
    }

    void Start()
    {
        fileManager = gameObject.AddComponent<FileManager>();
    }

    public void SalvaDati(int slot)
    {
        pathToFile = Application.persistentDataPath + "/datiInputField" + slot + ".json";
        path = fileManager.getPath();

        SaveData dati = new()
        {
            testiInputField = new List<string>(),
            checker = new List<bool>(),
            imagePath = new List<string>(),
            date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
        };

        // Aggiungi i testi degli InputField
        foreach (var inputField in inputFields)
        {
            dati.testiInputField.Add(inputField.text);
        }

        // Verifica il file esistente per gestire i percorsi
        if (File.Exists(pathToFile))
        {
            string datiJsons = File.ReadAllText(pathToFile);
            SaveData dati2 = JsonUtility.FromJson<SaveData>(datiJsons);
            Cambio(dati2.imagePath);
        }

        // Aggiungi i percorsi delle immagini
        foreach (var image in path)
        {
            dati.imagePath.Add(image);
        }

        // Aggiungi lo stato dei check
        foreach (var check in checkImage)
        {
            dati.checker.Add(check.texture.name != "square");
        }

        // Serializza e salva i dati
        string datiJson = JsonUtility.ToJson(dati);
        File.WriteAllText(pathToFile, datiJson);
        Debug.Log("Dati salvati: " + datiJson);

        // Invia i dati salvati sia come scelta che come storia all'API Flask, se disponibile
        if (flaskManager != null)
        {
            flaskManager.SendChoiceAndStoryToAI(datiJson, dati.date);
            Debug.Log("Dati inviati all'API Flask: " + datiJson);
        }
        else
        {
            Debug.LogError("FlaskManager non è disponibile, impossibile inviare i dati all'API.");
        }
    }

    public void CaricaDati(int slot)
    {
        pathToFile = Application.persistentDataPath + "/datiInputField" + slot + ".json";
        imageOn = Resources.Load<Texture>("Texture/check");

        if (File.Exists(pathToFile))
        {
            string datiJson = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJson);

            // Carica testi e stati dei check
            for (int i = 0; i < Mathf.Min(dati.testiInputField.Count, inputFields.Count); i++)
            {
                inputFields[i].text = dati.testiInputField[i];
                if (dati.checker[i] && checkImage[i] != null)
                {
                    checkImage[i].texture = imageOn;
                }
            }

            // Carica le immagini
            for (int i = 0; i < Mathf.Min(dati.imagePath.Count, images.Count); i++)
            {
                if (!string.IsNullOrEmpty(dati.imagePath[i]) && File.Exists(dati.imagePath[i]))
                {
                    byte[] fileData = File.ReadAllBytes(dati.imagePath[i]);
                    Texture2D texture2D = new Texture2D(2, 2);
                    texture2D.LoadImage(fileData);

                    if (images[i] != null && images2[i] != null)
                    {
                        images[i].texture = texture2D;
                        images2[i].texture = texture2D;
                    }
                }
            }

            // Carica la data
            for (int i = 0; i < 3; i++)
            {
                pathToFile = Application.persistentDataPath + "/datiInputField" + i + ".json";
                if (File.Exists(pathToFile))
                {
                    dateText[i].text = dati.date;
                }
            }
        }
        else
        {
            Debug.LogWarning("Il file JSON non esiste. Non ci sono dati da caricare.");
        }
    }

    private void Cambio(List<string> strings)
    {
        for (int i = 0; i < strings.Count; i++)
        {
            if (string.IsNullOrEmpty(path[i]) && !string.IsNullOrEmpty(strings[i]))
            {
                path[i] = strings[i];
            }
        }
    }
}
