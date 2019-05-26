using UnityEngine;

public class HighScoreButton : MonoBehaviour
{

    public Canvas highscoreCanvas;
    [SerializeField] private static bool isOpen = false;



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
