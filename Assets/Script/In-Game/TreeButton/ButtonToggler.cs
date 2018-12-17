using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggler : MonoBehaviour
{
    /*
    //  Later should change the name because this is not spawning the tree, only toggling the tree button
    //  Changes made here also need to be change to the TileScript
    */

    public Button toggle;               //Button for toggling the Select bool between false and true

    public bool Select { get; set; }    //Select variable. used if player Selects a tree to place.

    private void Start()
    {
        Select = false;
    }

    private void Update()
    {
        toggle.onClick.AddListener(ChangeSelect); //Command that calls a function when the toggle button is clicked


        if(Select)
            toggle.GetComponent<Image>().color = Color.red; //Tree place mode
        else
            toggle.GetComponent<Image>().color = Color.white; //Can't place tree

    }

    //Changing the state of "Select" according to the button press.
    void ChangeSelect()
    {
        if (!Select && GameManager.seed >= toggle.GetComponent<TreeProperties>().seedCost)
        {
            Select = true;
        }
        else
        {
            Select = false;
        }
    }
}
