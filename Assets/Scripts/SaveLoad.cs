using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
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

    public FlaskManager flaskManager;  // Aggiungi riferimento a FlaskManager

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
            date = string.Empty
        };

        foreach (var inputField in inputFields)
        {
            dati.testiInputField.Add(inputField.text);
        }

        if (File.Exists(pathToFile))
        {
            string datiJsons = File.ReadAllText(pathToFile);
            SaveData dati2 = JsonUtility.FromJson<SaveData>(datiJsons);
            Cambio(dati2.imagePath);
        }

        foreach (var image in path)
        {
            dati.imagePath.Add(image);
        }

        dati.date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        foreach (var check in checkImage)
        {
            dati.checker.Add(check.texture.name != "square");
        }

        string datiJson = JsonUtility.ToJson(dati);
        File.WriteAllText(pathToFile, datiJson);

        // Invia dati salvati all'API
        flaskManager.SendChoicesToAI(datiJson);  // Invoca il metodo FlaskManager per inviare i dati
    }

    public void CaricaDati(int slot)
    {
        pathToFile = Application.persistentDataPath + "/datiInputField" + slot + ".json";
        imageOn = Resources.Load<Texture>("Texture/check");

        if (File.Exists(pathToFile))
        {
            string datiJson = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJson);

            for (int i = 0; i < Mathf.Min(dati.testiInputField.Count, inputFields.Count); i++)
            {
                inputFields[i].text = dati.testiInputField[i];

                if (dati.checker[i])
                {
                    Texture texture = imageOn;
                    if (texture != null && checkImage[i] != null)
                    {
                        checkImage[i].texture = texture;
                    }
                }
            }

            for (int i = 0; i < Mathf.Min(dati.imagePath.Count, images.Count); i++)
            {
                if (!string.IsNullOrEmpty(dati.imagePath[i]) && File.Exists(dati.imagePath[i]))
                {
                    byte[] fileData = File.ReadAllBytes(dati.imagePath[i]);
                    Texture2D texture2D = new Texture2D(2, 2);
                    texture2D.LoadImage(fileData);
                    Texture texture = texture2D;

                    if (images[i] != null && images2[i] != null)
                    {
                        images[i].texture = texture;
                        images2[i].texture = texture;
                    }
                }
            }

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



/*using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
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

    private void Awake()
    {
        // Imposta il percorso del file JSON nel quale verranno salvati i dati
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
            date = string.Empty
        };

        // Itera attraverso tutti gli InputField e aggiungi i loro testi alla lista
        foreach (var inputField in inputFields)
        {
            dati.testiInputField.Add(inputField.text);
        }

        if (File.Exists(pathToFile))
        {
            string datiJsons = File.ReadAllText(pathToFile);
            SaveData dati2 = JsonUtility.FromJson<SaveData>(datiJsons);
            Cambio(dati2.imagePath);
        }

        foreach (var image in path)
        {
            Debug.Log("n" + image);
            dati.imagePath.Add(image);
        }

        dati.date = DateTime.Now.Date.ToString("dd-MM-yyyy") + "  -  " + DateTime.Now.ToString("HH:mm:ss");
        foreach (var check in checkImage)
        {
            if (check.texture.name == "square")
            {
                dati.checker.Add(false);
            }
            else
            {
                dati.checker.Add(true);
            }

        }
        // Assegna altri valori a "dati" per ogni InputField aggiuntivo

        string datiJson = JsonUtility.ToJson(dati);
        File.WriteAllText(pathToFile, datiJson);
        PlayerPrefs.SetString("SavedLevel", "New Scene");
    }

    public void CaricaDati(int slot)
    {
        pathToFile = Application.persistentDataPath + "/datiInputField" + slot + ".json";
        
        imageOn = Resources.Load<Texture>("Texture/check");

        if (File.Exists(pathToFile))
        {
            string datiJson = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJson);

            //CArica Testo + Check Immagine
            for (int i = 0; i < Mathf.Min(dati.testiInputField.Count, inputFields.Count); i++)
            {
                inputFields[i].text = dati.testiInputField[i];

                if (dati.checker[i])
                {
                    // Carica la texture dalla path dell'immagine
                    Texture texture = imageOn;

                    if (texture != null)
                    {
                        // Verifica se il componente esiste prima di tentare di cambiarlo
                        if (checkImage[i] != null)
                        {
                            // Cambia la texture della RawImage
                            checkImage[i].texture = texture;
                        }
                        else
                        {
                            Debug.LogError("Il componente RawImage non è stato trovato su questo GameObject.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Impossibile caricare l'immagine dalla path specificata: ");
                    }
                }
            }
            //Carica immagini
            for (int i = 0; i < Mathf.Min(dati.imagePath.Count, images.Count); i++)
            {
                if (!string.IsNullOrEmpty(dati.imagePath[i]))
                {
                    if (File.Exists(dati.imagePath[i]))
                    {
                        // Leggi i byte dal file
                        byte[] fileData = File.ReadAllBytes(dati.imagePath[i]);

                        // Crea una nuova texture2D
                        Texture2D texture2D = new Texture2D(2, 2);
                        texture2D.LoadImage(fileData); // Carica l'immagine dalla byte array

                        // Converti Texture2D a Texture
                        Texture texture = texture2D;

                        // Assegna la texture al materiale
                        if (images[i] != null && images2[i] != null)
                        {
                            // Cambia la texture della RawImage
                            images[i].texture = texture;
                            images2[i].texture = texture;
                        }
                        else
                        {
                            Debug.LogError("Il componente RawImage non è stato trovato su questo GameObject.");
                        }
                    }

                    // Verifica se il componente esiste prima di tentare di cambiarlo

                }
            }

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
*/
