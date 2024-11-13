using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<string> testiInputField = new List<string>();
    public List<bool> checker = new List<bool>();
    public List<string> imagePath = new List<string>();
    public string date;

    // Metodo per aggiungere una scelta fatta in tempo reale
    public void AddChoice(string inputText, bool choice, string imagePath)
    {
        testiInputField.Add(inputText);
        checker.Add(choice);
        this.imagePath.Add(imagePath);
        date = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
}



/*using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<string> testiInputField;
    public List<bool> checker;
    public List<string> imagePath;
    public string date;
    // Aggiungi altri campi per ogni InputField che desideri salvare
}
*/
