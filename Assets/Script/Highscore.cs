using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text highscoreUI;
    public Text timeUI;

	// Update is called once per frame
	void Update () {
        highscoreUI.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        int time = PlayerPrefs.GetInt("highscore", 0);
        timeUI.text = "Time: " + time / 60 + ":" + time % 60;

    }
}
