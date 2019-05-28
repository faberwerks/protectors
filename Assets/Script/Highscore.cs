using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public TMP_Text highscoreText;
    public TMP_Text timeText;

    private void Start()
    {
        int highscore = PlayerPrefs.GetInt("score", 0);
        highscoreText.text = "Highscore: " + highscore.ToString();
        int time = PlayerPrefs.GetInt("time", 0);
        timeText.text = "Time: " + time / 60 + ":" + time % 60;
    }
}
