using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveDate : MonoBehaviour
{
    private TMP_Text testo;
    private string pathToFile;
    public int slots;

    void Start()
    {
        testo = GetComponent<TMP_Text>();
        ChangeDate(slots);
    }

    public void ChangeDate(int slot)
    {
        pathToFile = Application.persistentDataPath + "/datiInputField" + slot + ".json";
        if (File.Exists(pathToFile))
        {
            string datiJson = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJson);
            
            testo.text = dati.date;  
        }
    }

    public void SetDate()
    {
        testo.text = DateTime.Now.Date.ToString("dd-MM-yyyy") + "  -  " + DateTime.Now.ToString("HH:mm:ss");
    }
}
