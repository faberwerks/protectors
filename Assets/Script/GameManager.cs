using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;                   //To have reference of the lumberjack
    public GameObject tree;                     //To have reference of the tree

    public static float seed;                   //Seed the player has
    public static float score = 0f;             //Score he player has
    [SerializeField] private float spawnTime;   //Time to spawn the lumberjack
    [SerializeField] private float spawnTimer;  //Timer for the spawnTime

    private int checker;                        //Holds the random number to determine spawned lumberjack's position

    public Text scoreText;                      //Reference to the Score text
    public Text seedText;                       //Reference to the Seed text

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
    void Start()
    {
        //debugging purpose
        spawnTime = 5f;
        spawnTimer = spawnTime;
        seed = 30f;
        UpdateScore();
        UpdateSeed();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
        UpdateScore();
        UpdateSeed();
    }

    //Updates the score on screen
    private void UpdateScore()      
    {
        scoreText.text = "Score: " + score;
    }

    //Updates the seed on screen
    private void UpdateSeed()
    {
        seedText.text = "Seed: " + seed;
    }


    private void SpawnLumberjack(int startLoc)
    {
        Vector2 dir = new Vector2(0, 0);
        switch (startLoc)
        {
            case 1:                     //From top 
                dir = Vector2.down;     //Set direction to bot
                break;
            case 2:                     //From right
                dir = Vector2.left;     //Set direction to left
                break;
            case 3:                     //From bottom
                dir = Vector2.up;       //Set direction to up
                break;
            case 4:                     //From left
                dir = Vector2.right;    //Set direction to right
                break;
        }
        GameObject newLumberJack = (GameObject)Instantiate(lumber);                         //Instantiating a new Lumberjack
        newLumberJack.GetComponent<LumberjackController>().InitialiseDir(dir, startLoc);    //Accessing lumberjack's function to set its position
    }


    //Spawns a lumberjack at a set position
    private void Spawn()
    {
        if (spawnTimer <= 0)
        {
            checker = Random.Range(1, 5);   //Determines which position the lumberjack is spawned in
            SpawnLumberjack(checker);       //Calls the Spawning Function
            spawnTimer = spawnTime;         //Reset the timer to the spawnTime again
        }
        else
        {
            spawnTimer -= Time.deltaTime;   //Timer countdown
        }
    }
}