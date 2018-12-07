using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;                   //To have reference of the lumberjack
    public GameObject tree;                     //To have reference of the tree
    public GameObject gameOverText;

    public Text scoreText;                      
    public Text seedText;                       
    public Text timeText;

    public static bool gameStart;

    public static float seed;                   
    public static float score = 0f;             
    [SerializeField] private float spawnTime;  
    [SerializeField] private float gameTime;  

    public static int numberOfTrees;
    private int checker;                        //Holds the random number to determine spawned lumberjack's position

                       
                    

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
        gameTime = 0;
        numberOfTrees = 0;
        gameStart = false;
        spawnTime = 5f;
        seed = 30f;
        UpdateScore();
        UpdateSeed();
        UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {
        Gameplay();
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
    private void UpdateTime()
    {
        timeText.text = "Time: " + (int)gameTime/60 + ":" + (int)gameTime%60;  //Minutes : Second 
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
        if ((gameTime % spawnTime) <= 0.02f && !(gameTime <= 1))
        {
            checker = Random.Range(1, 5);   //Determines which position the lumberjack is spawned in
            SpawnLumberjack(checker);       //Calls the Spawning Function
            gameTime += Time.deltaTime;
        }
    }

    private void Gameplay()
    {
        if (gameStart)
        {
            Spawn();
            UpdateScore();
            UpdateSeed();
            UpdateTime();
            gameTime += Time.deltaTime;
        }

        if (gameStart && numberOfTrees == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        gameOverText.SetActive(true);
        gameObject.SetActive(false);
    }
}