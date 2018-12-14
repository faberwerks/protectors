using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour {

    private SpawnTree spawn;

    public void Start()
    {
        spawn = GameManager.FindObjectOfType<GameManager>().GetComponent<SpawnTree>();  
    }

    public void OnClick()
    {
        spawn.toggle.GetComponent<Image>().color = Color.white;
        spawn.toggle = this.gameObject.GetComponent<Button>();
        Debug.Log("hit");
    }
}
