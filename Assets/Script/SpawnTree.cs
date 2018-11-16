using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTree : MonoBehaviour
{

    public bool select = false;

    public GameObject Prefab;
    
    public int RayDistance = 10;
    private Vector3 Point;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 instantiatePosition = Input.mousePosition;
            Debug.Log(instantiatePosition);
            Prefab = Instantiate(Prefab, instantiatePosition, Quaternion.identity);
            Prefab.name = "Tree";

            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ////Make Whatever a Raycast layer or if you don't want it just exclude it
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Point = hit.point;
            //    Instantiate(Prefab, Point, Quaternion.identity);
            //}
        }
    }
}
