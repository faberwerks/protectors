﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTree : MonoBehaviour
{

    public Button toggle; //Button for toggling the Select bool between false and true

    public bool Select { get; set; } //Select variable. used if player Selects a tree to place.

    public Transform tree; //The tree we want to spawn
    
    public float distance = 10f; //Basically a distance used for the Z so it's always placed in front
    [SerializeField] private float treeCost = 5f; //Cost of a tree

    private Vector3 point/*The mouse position*/, targetPoint;/*The target area that we want to place the tree*/

    private void Start()
    {
        Select = false;
    }

    private void Update()
    {
        toggle.onClick.AddListener(ChangeSelect); //Command that calls a function when the toggle button is clicked
        point = Input.mousePosition; //To get the current mouse position
        targetPoint = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, distance)); //Convert the point according to World position
        if (Input.GetMouseButtonDown(0) && Select)
        {
                //Select = false; //sets Select to false so toggle button is instantly turned off.
                //Instantiate(tree,targetPoint, Quaternion.identity);
                //GameManager.seed -= treeCost;
                //GameManager.numberOfTrees += 1;
            

        }

        if(Select)
            toggle.GetComponent<Image>().color = Color.red; //Tree place mode
        else
            toggle.GetComponent<Image>().color = Color.white; //Can't place tree
    }

    //Changing the state of "Select" according to the button press.
    void ChangeSelect()
    {
        if (!Select && GameManager.seed >= treeCost)
        {
            Select = true;
        }
        else
        {
            Select = false;
        }
    }
}
