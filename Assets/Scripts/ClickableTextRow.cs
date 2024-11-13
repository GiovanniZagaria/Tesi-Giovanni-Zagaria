using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableTextRow : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private Transform button;
    private Button Gotobutton;

    private void Start()
    {
        button = transform.GetChild(0);
        Gotobutton = button.GetComponent<Button>();

        Gotobutton.gameObject.SetActive(false);

        textComponent = GetComponent<TextMeshProUGUI>();

        // Assicurati che l'oggetto abbia un collider raycastable
        if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    Debug.Log(textComponent.text);
                }
            }
        }
    }
    void OnMouseOver()
    {
        Gotobutton.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        Gotobutton.gameObject.SetActive(false);
    }
}
