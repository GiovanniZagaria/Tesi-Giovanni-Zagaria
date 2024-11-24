using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public void isOpen()
    {
        MenuController.isPaused = true;
    }

    public void isClose()
    {
        MenuController.isPaused = false;
    }
}
