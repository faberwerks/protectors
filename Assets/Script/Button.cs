using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour {

    public Canvas canvas;
    [SerializeField]private static bool isOpen = false;

	
	
	public void OnClick () {
           Toggle();
    }

    private void Toggle()
    {
        if(!isOpen)
        {
           canvas.gameObject.SetActive(true);
            isOpen = true;
        }
        else if(isOpen) 
        {
            canvas.gameObject.SetActive(false);
            isOpen = false;
        }
    }
}
