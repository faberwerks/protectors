using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject lumber;


    private int checker;
    

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        Invoke("Spawn", 2f);
    }


   
    private void SpawnLumberjack(int startLoc)
    {
        Vector2 dir = new Vector2(0,0);
        switch (startLoc)
        {
            case 1:
                dir = Vector2.down;
                break;
            case 2:
                dir = Vector2.left;
                break;
            case 3:
                dir = Vector2.up;
                break;
            case 4:
                dir = Vector2.right;
                break;
        }
        GameObject newLumberJack = (GameObject)Instantiate(lumber);
        newLumberJack.GetComponent<LumberjackController>().InitialiseDir(dir, startLoc);
    }

    //Spawns a lumberjack at a set position
    private void Spawn()
    {
        checker = Random.Range(1, 5); //Determines which position the lumberjack is spawned in
        SpawnLumberjack(checker);
        CancelInvoke();
    }
}
