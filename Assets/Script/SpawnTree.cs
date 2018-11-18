using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTree : MonoBehaviour
{
    public Button toggle;

    public bool select = false;

    public Transform prefab;
    
    public float distance = 10f;

    private Vector3 point, targetPoint;

    public void Start()
    {

    }

    public void Update()
    {
        toggle.onClick.AddListener(ChangeSelect);

        point = Input.mousePosition;
        targetPoint = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, distance));
        //prefab.position = targetPoint;
        if (Input.GetMouseButtonDown(0) && select)
        {
            Instantiate(prefab, targetPoint, Quaternion.identity);

            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ////Make Whatever a Raycast layer or if you don't want it just exclude it
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Point = hit.point;
            //    Instantiate(prefab, Point, Quaternion.identity);
            //}
        }

        if(select)
            toggle.GetComponent<Image>().color = Color.red;
        else
            toggle.GetComponent<Image>().color = Color.white;
    }

    void ChangeSelect()
    {
        if (!select)
        {
            select = true;
        }
        else
        {
            select = false;
        }
    }
}
