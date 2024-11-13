using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using UnityEngine.UI;
using System.IO;
using System;

public class StoryGenerator : MonoBehaviour
{
    public List<GameObject> input;  // Manteniamo una sola dichiarazione di input
    private static string storia;
    public TMP_InputField titolo;
    private string[] imagepath = new string[3];
    private string pathToFile;

    // Metodo per raccogliere il testo da tutti i campi di input
    public void setInputText()
    {
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].GetComponent<TMP_InputField>() && input[i].GetComponent<TMP_InputField>().text != "" &&
            input[i].GetComponent<TMP_InputField>().text != null)
            {
                storia += input[i].GetComponent<TMP_InputField>().text + "\n";
            }
        }
        Debug.Log("Testo raccolto: " + storia); // Log per controllare il testo raccolto
    }

    // Metodo per raccogliere il contesto della storia dalle carte/testi selezionati
    public string CollectStoryContext()
    {
        string storyContext = "";

        // Itera attraverso la lista di input e raccoglie i testi
        foreach (GameObject go in input)
        {
            var inputField = go.GetComponent<TMP_InputField>();
            if (inputField != null && !string.IsNullOrEmpty(inputField.text))
            {
                storyContext += inputField.text + " ";
            }
        }

        Debug.Log("Contesto della storia raccolto: " + storyContext); // Log per controllare il contesto
        return storyContext.Trim();
    }

    private Document CreateDocument()
    {
        if (MenuController.isLoad)
        {
            pathToFile = Application.persistentDataPath + "/datiInputField" + SaveLoad.saveSlot + ".json";
        }
        Document document = new Document();
        Style style = document.Styles.Normal;
        style.Font.Name = "Arial";
        style.Font.Size = 12;
        Section section = document.AddSection();
        FileManager fileManager = gameObject.AddComponent<FileManager>();
        imagepath = fileManager.getPath();

        if (File.Exists(pathToFile))
        {
            string datiJsons = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJsons);
            Cambio(dati.imagePath);
        }
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].GetComponent<TMP_InputField>() && !string.IsNullOrEmpty(input[i].GetComponent<TMP_InputField>().text))
            {
                Paragraph paragraph = section.AddParagraph();
                paragraph.Format.Font.Color = MigraDoc.DocumentObjectModel.Color.FromCmyk(0, 0, 0, 100);
                paragraph.AddFormattedText(input[i].GetComponent<TMP_InputField>().text);
            }

            if (input[i].GetComponent<RawImage>() && input[i].GetComponent<RawImage>().texture.name != "place_holder")
            {
                Paragraph paragraph = section.AddParagraph();
                paragraph.Format.Font.Color = MigraDoc.DocumentObjectModel.Color.FromCmyk(0, 0, 0, 100);
                MigraDoc.DocumentObjectModel.Shapes.Image image = new();
                if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage1")
                {
                    image = section.AddImage(imagepath[0]);
                }
                else if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage2")
                {
                    image = section.AddImage(imagepath[1]);
                }
                else if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage3")
                {
                    image = section.AddImage(imagepath[2]);
                }
                image.Width = "5cm";
                image.Height = "5cm";
                image.Left = ShapePosition.Center;
            }
        }

        return document;
    }

    public void DownloadStoryPDF()
    {
        Document document = CreateDocument();
        document.UseCmykColor = true;
        string filename = titolo.text + ".pdf";

        const bool unicode = false;
        PdfDocumentRenderer pdfRenderer = new(unicode)
        {
            Document = document
        };

        try
        {
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filename);
        }
        catch (Exception e)
        {
            Debug.LogError("Errore durante il rendering o il salvataggio del file PDF: " + e.Message);
        }
    }

    private void Cambio(List<string> strings)
    {
        Debug.Log("Strings ricevuti per Cambio: " + string.Join(", ", strings)); // Log delle stringhe ricevute
        for (int i = 0; i < strings.Count; i++)
        {
            if (string.IsNullOrEmpty(imagepath[i]) && !string.IsNullOrEmpty(strings[i]))
            {
                imagepath[i] = strings[i];
            }
        }
    }
}



/*using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using UnityEngine.UI;
using System.IO;
using System;

public class StoryGenerator : MonoBehaviour
{
    public List<GameObject> input;  // Manteniamo una sola dichiarazione di input
    private static string storia;
    public TMP_InputField titolo;
    private string[] imagepath = new string[3];
    private string pathToFile;

    // Metodo per raccogliere il testo da tutti i campi di input
    public void setInputText()
    {
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].GetComponent<TMP_InputField>() && input[i].GetComponent<TMP_InputField>().text != "" &&
            input[i].GetComponent<TMP_InputField>().text != null)
            {
                storia += input[i].GetComponent<TMP_InputField>().text + "\n";
            }
        }
        Debug.Log(storia);
    }

    // Metodo per raccogliere il contesto della storia dalle carte/testi selezionati
    public string CollectStoryContext()
    {
        string storyContext = "";

        // Itera attraverso la lista di input e raccoglie i testi
        foreach (GameObject go in input)
        {
            var inputField = go.GetComponent<TMP_InputField>();
            if (inputField != null && !string.IsNullOrEmpty(inputField.text))
            {
                storyContext += inputField.text + " ";
            }
        }

        return storyContext.Trim();
    }

    private Document CreateDocument()
    {
        if (MenuController.isLoad)
        {
            pathToFile = Application.persistentDataPath + "/datiInputField" + SaveLoad.saveSlot + ".json";
        }
        Document document = new Document();
        Style style = document.Styles.Normal;
        style.Font.Name = "Arial";
        style.Font.Size = 12;
        Section section = document.AddSection();
        FileManager fileManager = gameObject.AddComponent<FileManager>();
        imagepath = fileManager.getPath();
            
        if (File.Exists(pathToFile))
        {
            string datiJsons = File.ReadAllText(pathToFile);
            SaveData dati = JsonUtility.FromJson<SaveData>(datiJsons);
            Cambio(dati.imagePath);
        }
        for (int i = 0; i < input.Count; i++)
        {
            if (input[i].GetComponent<TMP_InputField>() && !string.IsNullOrEmpty(input[i].GetComponent<TMP_InputField>().text))
            {
                Paragraph paragraph = section.AddParagraph();
                paragraph.Format.Font.Color = MigraDoc.DocumentObjectModel.Color.FromCmyk(0, 0, 0, 100);
                paragraph.AddFormattedText(input[i].GetComponent<TMP_InputField>().text);
            }

            if (input[i].GetComponent<RawImage>() && input[i].GetComponent<RawImage>().texture.name != "place_holder")
            {
                Paragraph paragraph = section.AddParagraph();
                paragraph.Format.Font.Color = MigraDoc.DocumentObjectModel.Color.FromCmyk(0, 0, 0, 100);
                MigraDoc.DocumentObjectModel.Shapes.Image image = new();
                if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage1")
                {
                    image = section.AddImage(imagepath[0]);
                }
                else if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage2")
                {
                    image = section.AddImage(imagepath[1]);
                }
                else if (input[i].GetComponent<RawImage>().gameObject.name == "RawImage3")
                {
                    image = section.AddImage(imagepath[2]);
                }
                image.Width = "5cm";
                image.Height = "5cm";
                image.Left = ShapePosition.Center;
            }
        }

        return document;
    }

    public void DownloadStoryPDF()
    {
        Document document = CreateDocument();
        document.UseCmykColor = true;
        string filename = titolo.text + ".pdf";

        const bool unicode = false;
        PdfDocumentRenderer pdfRenderer = new(unicode)
        {
            Document = document
        };

        try
        {
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(filename);
        }
        catch (Exception e)
        {
            Debug.LogError("Errore durante il rendering o il salvataggio del file PDF: " + e.Message);
        }
    }

    private void Cambio(List<string> strings)
    {
        Debug.Log(string.Join(", ", strings));
        for (int i = 0; i < strings.Count; i++)
        {
            if (string.IsNullOrEmpty(imagepath[i]) && !string.IsNullOrEmpty(strings[i]))
            {
                imagepath[i] = strings[i];
            }
        }
    }
}*/

