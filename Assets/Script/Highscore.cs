using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{

    public Text highscoreUI;
    public Text timeUI;

    // Update is called once per frame
    void Update()
    {
        int highscore = PlayerPrefs.GetInt("score", 0);
        highscoreUI.text = "Highscore: " + highscore.ToString();
        int time = PlayerPrefs.GetInt("time", 0);
        timeUI.text = "Time: " + time / 60 + ":" + time % 60;

    }
}
