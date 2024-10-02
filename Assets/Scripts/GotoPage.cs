using UnityEngine;

public class GotoPage : MonoBehaviour
{
    public MegaBookBuilder book;
    public float num;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    book.GoToPage(num);
                }
            }
        }
    }
}
