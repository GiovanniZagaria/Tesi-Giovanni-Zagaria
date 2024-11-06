using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckRequired : MonoBehaviour
{
    public List<TMP_InputField> inputFields;
    public Button finish;

    void Start()
    {
        finish.gameObject.SetActive(false);
        Check();
    }
    // Start is called before the first frame update
    public void Check()
    {
        bool check = true;
        foreach (TMP_InputField input in inputFields)
        {
            string testoInserito = input.text.Trim();
            if (string.IsNullOrEmpty(testoInserito))
            {
                // Se uno qualsiasi non è compilato, imposta il flag a false e esci dal loop
                check = false;
                break;
            }
        }
        if (check)
        {
            finish.gameObject.SetActive(true);
        }
        else
        {
            finish.gameObject.SetActive(false);
        }
    }
}
