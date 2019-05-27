using UnityEngine;

public class HighscoreButton : MonoBehaviour
{

    public Canvas highscoreCanvas;
    private bool isOpen = false;

    public void OnClick()
    {
        Toggle();
    }

    private void Toggle()
    {
        if (!isOpen)
        {
            highscoreCanvas.gameObject.SetActive(true);
            isOpen = true;
        }
        else if (isOpen)
        {
            highscoreCanvas.gameObject.SetActive(false);
            isOpen = false;
        }
    }
}
