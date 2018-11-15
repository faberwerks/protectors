using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;
    public GameObject tree;

    public static float seed;
    public static float score = 0f;

    private int checker;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        seed = 30f;
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
