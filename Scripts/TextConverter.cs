using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextConverter : MonoBehaviour
{
    private TextMeshPro text;
    public GameObject canvas;
    private Transform ncanvas;
    private Canvas numcanvas;
    private Transform inputfield;
    private Transform button;
    private TMP_InputField textInputField;
    string newtext;
    private Transform numpage;
    private TMP_Text npage;
    private Transform prev;
    private Transform next;
    public RawImage image;
    private Texture imageOn;
    private Texture imageOff;
    public static bool isOpen;

    public TextoFlask textoFlask;
   
    void Start()
    {
        imageOn = Resources.Load<Texture>("Texture/check");
        imageOff = Resources.Load<Texture>("Texture/square");
        text = GetComponent<TextMeshPro>();
        text.pageToDisplay = 1;
        ncanvas = transform.GetChild(0);
        numcanvas = ncanvas.GetComponent<Canvas>();
        canvas.gameObject.SetActive(true);
        inputfield = canvas.transform.GetChild(0);
        button = canvas.transform.GetChild(1);
        isOpen = false;

        numpage = numcanvas.transform.GetChild(0);
        prev = numcanvas.transform.GetChild(1);
        next = numcanvas.transform.GetChild(2);

        textInputField = inputfield.GetComponent<TMP_InputField>();
        npage = numpage.GetComponent<TMP_Text>();

        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        npage.gameObject.SetActive(false);

        if (textoFlask == null)
        {
            Debug.LogError("TextoFlask non assegnato nel TextConverter!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !MenuController.isPaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    textInputField.gameObject.SetActive(true);
                    button.gameObject.SetActive(true);
                    text.text.Equals("");
                    isOpen = true;
                }
            }
        }

        if (string.IsNullOrEmpty(textInputField.text.Trim()) && !textInputField.isActiveAndEnabled && gameObject.GetComponent<Collider>().enabled)
        {
            text.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
            newtext = "Clicca sul foglio per iniziare a scrivere...";
            text.alignment = TextAlignmentOptions.Center;
            next.gameObject.SetActive(false);
            prev.gameObject.SetActive(false);
            npage.gameObject.SetActive(false);
        }
        else if (string.IsNullOrEmpty(textInputField.text.Trim()) && textInputField.isActiveAndEnabled)
        {
            newtext = "";
            next.gameObject.SetActive(false);
            prev.gameObject.SetActive(false);
            npage.gameObject.SetActive(false);
        }
        else
        {
            newtext = textInputField.text;
            text.alignment = TextAlignmentOptions.TopLeft;
            text.color = Color.black;

            if (text.textInfo.pageCount <= 1)
            {
                next.gameObject.SetActive(false);
                prev.gameObject.SetActive(false);
                npage.gameObject.SetActive(false);
            }
            if (text.textInfo.pageCount > 1)
            {
                next.gameObject.SetActive(true);
                prev.gameObject.SetActive(true);
                npage.gameObject.SetActive(true);
                npage.text = text.pageToDisplay + "/" + text.textInfo.pageCount;
            }
        }

        text.text = newtext;

        if (!string.IsNullOrEmpty(textInputField.text.Trim()))
        {
            if (textoFlask != null)
            {
                textoFlask.InviaTestoAlFlask(newtext);
            }
        }
    }


    public void Nextpage()
    {
        if (text.pageToDisplay < text.textInfo.pageCount)
            text.pageToDisplay++;
        npage.text = text.pageToDisplay + "/" + text.textInfo.pageCount;
    }

    public void Prevpage()
    {
        if (text.pageToDisplay > 1)
            text.pageToDisplay--;
        npage.text = text.pageToDisplay + "/" + text.textInfo.pageCount;
    }

public void Close()
    {
        textInputField.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        isOpen = false;
    }
}