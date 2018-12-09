using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

    [SerializeField] private GameObject tile;
    private float size = 2f;

    // Use this for initialization
    void Start () {
        CreateTile();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateTile()
    {
        Instantiate(tile);

        for (float x = transform.position.x; x < 15;x += size)
        {
            for(float y = transform.position.y; y > -9; y -= size)
            {
                Instantiate(tile,this.gameObject.transform).transform.position = new Vector2(x, y);
            }
        }
    }
}
