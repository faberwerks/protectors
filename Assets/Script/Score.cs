using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI score;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        score.text = "Score :" + GameManager.score.ToString();
    }
}
