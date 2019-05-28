using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI score;

	// Update is called once per frame
	void Update () {
        score.text = "Score :" + GameManager.score.ToString();
    }
}
