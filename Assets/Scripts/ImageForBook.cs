using UnityEngine;
using UnityEngine.UI;

public class ImageForBook : MonoBehaviour
{
    private RawImage rawImage;
    public RawImage image;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    public void ChangeImage()
    {
        rawImage = image;
    }
}
