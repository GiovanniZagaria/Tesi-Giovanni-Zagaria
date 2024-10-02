using TMPro;
using UnityEngine;

public class TextForBook : MonoBehaviour
{
    private TextMeshPro text;
    private Transform ncanvas;
    private Canvas numcanvas;
    public TMP_InputField textInputField;
    string newtext;
    private Transform numpage;
    private TMP_Text npage;
    private Transform prev;
    private Transform next;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.pageToDisplay = 1;
        ncanvas = transform.GetChild(0);
        numcanvas = ncanvas.GetComponent<Canvas>();


        numpage = numcanvas.transform.GetChild(0);
        prev = numcanvas.transform.GetChild(1);
        next = numcanvas.transform.GetChild(2);

        npage = numpage.GetComponent<TMP_Text>();


        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        npage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (string.IsNullOrEmpty(textInputField.text.Trim()) && !textInputField.isActiveAndEnabled)
        {
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

    }

    public void Prevpage()
    {
        if (text.pageToDisplay > 1)
            text.pageToDisplay--;
        npage.text = text.pageToDisplay + "/" + text.textInfo.pageCount;
    }

    public void Nextpage()
    {
        if (text.pageToDisplay < text.textInfo.pageCount)
            text.pageToDisplay++;
        npage.text = text.pageToDisplay + "/" + text.textInfo.pageCount;
    }
}
