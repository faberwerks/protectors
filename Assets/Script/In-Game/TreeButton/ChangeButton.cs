using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour {

    public ButtonToggler toggler;

    public void Start()
    {
        toggler = GameManager.FindObjectOfType<GameManager>().GetComponent<ButtonToggler>();  
    }

    public void OnClick()
    {
        if (GameManager.seed >= this.GetComponent<TreeProperties>().seedCost)
        {
            toggler.toggle.GetComponent<Image>().color = Color.white;
            toggler.toggle = this.gameObject.GetComponent<Button>();
            toggler.Select = true;
        }
    }
}
