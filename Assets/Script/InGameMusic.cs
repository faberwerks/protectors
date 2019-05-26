using UnityEngine;

public class InGameMusic : MonoBehaviour
{

    [SerializeField] private AudioSource music1;
    [SerializeField] private GameObject music2;

    private bool musicOneEnd = false;
    private bool musicTwoUsed = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStart)
        {
            musicOneEnd = true;
        }
        if (musicOneEnd)
        {
            music1.volume /= 1.1f;

            musicTwoUsed = true;
        }
        if (musicTwoUsed)
        {
            music2.SetActive(true);
        }
    }
}
