using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    // Update is called once per frame
    void Update()
    {
        score.text = "Score :" + GameManager.score.ToString();
    }
}
