using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

    public GameObject tile;
    public GameObject border;
    public GameObject pavement;

    [SerializeField] private int xLength = 17;
    [SerializeField] private int yLength = 12;
    [SerializeField] public int size = 6;

    // Use this for initialization
    void Start () {
        CreateTile();
	}

    private void CreateTile()
    {
        int yCurr, xCurr;
        int xPos = (int)transform.position.x;
        int yPos = (int)transform.position.y;
        

        for (xPos = (int)transform.position.x, xCurr = 0; xCurr < xLength; xPos += size, xCurr += 1)
        {
            for (yPos = (int)transform.position.y, yCurr = 0; yCurr < yLength; yPos -= size, yCurr +=1)
            {
                var tilePos = new Vector2(xPos, yPos);
                if (xCurr == 0 || yCurr == 0 || xCurr == xLength-1|| yCurr == yLength-1)
                {
                    Instantiate(border, tilePos, Quaternion.identity, this.gameObject.transform);
                }
                else if (xCurr == 1 || yCurr == 1 || xCurr == xLength - 2 || yCurr == yLength - 2)
                {
                    Instantiate(pavement, tilePos, Quaternion.identity, this.gameObject.transform);
                }
                else
                {
                    Instantiate(tile, tilePos, Quaternion.identity, this.gameObject.transform);
                }
            }
        }
    }
}
