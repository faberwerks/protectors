using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTree : MonoBehaviour
{
    public Button toggle; //Button for toggling the select bool between false and true

    public bool select = false; //Select variable. used if player selects a tree to place.

    public Transform tree; //The tree we want to spawn
    
    public float distance = 10f; //Basically a distance used for the Z so it's always placed in front
    [SerializeField] private float treeCost = 5f; //Cost of a tree

    private Vector3 point/*The mouse position*/, targetPoint;/*The target area that we want to place the tree*/

    public void Start()
    {

    }

    public void Update()
    {
        toggle.onClick.AddListener(ChangeSelect); //Command that calls a function when the toggle button is clicked
        point = Input.mousePosition; //To get the current mouse position
        targetPoint = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, distance)); //Convert the point according to World position
        if (Input.GetMouseButtonDown(0) && select)
        {
            Instantiate(tree, targetPoint, Quaternion.identity); //create the instance of targetObject and place it at given position.
            select = false; //sets select to false so toggle button is instantly turned off.
            GameManager.seed -= treeCost;
            GameManager.numberOfTrees += 1;
        }

        if(select)
            toggle.GetComponent<Image>().color = Color.red; //Tree place mode
        else
            toggle.GetComponent<Image>().color = Color.white; //Can't place tree
    }

    //Changing the state of "select" according to the button press.
    void ChangeSelect()
    {
        if (!select && GameManager.seed >= treeCost)
        {
            select = true;
        }
        else
        {
            select = false;
        }
    }
}
