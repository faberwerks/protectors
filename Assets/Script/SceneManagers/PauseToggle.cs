using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public void TogglePause()
    {
        GameManager.pauseChecker = true;
    }
}
