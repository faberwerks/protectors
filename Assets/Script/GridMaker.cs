using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject border;
    private int size = 2;

    // Use this for initialization
    void Start () {
        CreateTile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateTile()
    {
        for (int x = (int)transform.position.x; x <= 16;x += size)
        {
            for(int y = (int)transform.position.y; y >= -11; y -= size)
            {
                if(x == (int)transform.position.x || y == (int)transform.position.y || x == 16 || y == -11)
                {
                    Instantiate(border, this.gameObject.transform).transform.position = new Vector2(x, y);
                }
                else
                {
                    Instantiate(tile, this.gameObject.transform).transform.position = new Vector2(x, y);
                }
            }
        }
    }
}
