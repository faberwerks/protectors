using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour {

    public TextMeshProUGUI highscoreUI;
    public TextMeshProUGUI timeUI;

	// Update is called once per frame
	void Update () {
        int highscore = PlayerPrefs.GetInt("score", 0);
        highscoreUI.text = "Highscore: " + highscore.ToString();
        int time = PlayerPrefs.GetInt("time", 0);
        timeUI.text = "Time: " + time / 60 + ":" + time % 60;

    }
}
