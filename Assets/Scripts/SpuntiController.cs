using TMPro;
using UnityEngine;

public class SpuntiController : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer render;
    string spriteName;
    public TMP_InputField textInputField;
    string newName;

    void Start()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (render.sprite == null)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<TextMeshPro>().text.Equals("");
        }
        else
        {
            gameObject.GetComponent<Collider>().enabled = true;
            spriteName = render.sprite.name;
            if (newName != spriteName)
            {
                newName = spriteName;
                textInputField.text = "";
            }
        }
    }
}
