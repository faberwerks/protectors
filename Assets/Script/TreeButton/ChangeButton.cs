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

    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spawn.toggle = this.gameObject.GetComponent<Button>();
        }
    }
}
