using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    private bool isEmpty;

    [SerializeField] private GameObject tree;
    public GameObject gameManager;

    public Vector3 targetPoint;

    private bool select;

    // Use this for initialization
    void Start () {
        isEmpty = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        select = GameManager.FindObjectOfType<GameManager>().GetComponent<SpawnTree>().Select;
    }

    private void OnMouseOver()
    {
        //Debug.Log("Plant Tree");
            if (Input.GetMouseButtonDown(0) && select)
            {
                Debug.Log("Plant Tree");
                select = GameManager.FindObjectOfType<GameManager>().GetComponent<SpawnTree>().Select = false;
                //targetPoint = transform.position + new Vector3(0, 0, 0f);
                Instantiate(tree, transform.position, Quaternion.identity);
                GameManager.seed -= 5;
                isEmpty = true;

                GameManager.numberOfTrees += 1;
            }
    }
}
