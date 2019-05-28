using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    public AudioSource preStartAudioSource;
    public AudioSource postStartAudioSource;

    private bool hasStarted = false;
    private bool postStartBGMHasStarted = false;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.gameStart && !hasStarted)
        {
            hasStarted = true;
        }
        if (hasStarted)
        {
            preStartAudioSource.volume /= 1.1f;

            if (!postStartBGMHasStarted)
            {
                postStartAudioSource.Play();
                postStartBGMHasStarted = true;
            }
        }

    }
}
