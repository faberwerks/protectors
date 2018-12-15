using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    private TreeProperties properties;

    private bool isEmpty;

    [SerializeField] private GameObject tree;
    public GameObject gameManager;

    public Vector3 targetPoint;

    private bool select;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.FindObjectOfType<GameManager>().gameObject;
        isEmpty = true;
	}
	
	// Update is called once per frame
	void Update () {
        select = gameManager.GetComponent<SpawnTree>().Select;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && select && isEmpty)
        {
            Debug.Log("Plant Tree");
            select = gameManager.GetComponent<SpawnTree>().Select = false;
            properties = gameManager.GetComponent<SpawnTree>().toggle.GetComponent<TreeProperties>();
            tree.GetComponent<Tree>().InitialiseAttribute(properties.health, properties.seedValue, properties.scoreValue, properties.harvestTime,properties.treeTypeNumber);
            Instantiate(tree, transform.position, Quaternion.identity);
            GameManager.seed -= gameManager.GetComponent<SpawnTree>().toggle.GetComponent<TreeProperties>().seedCost; 

            GameManager.numberOfTrees += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            isEmpty = false;
        }
        else
        {
            isEmpty = true ;
        }
    }
}
