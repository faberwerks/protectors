using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;                   //To have reference of the lumberjack
    public GameObject tree;                     //To have reference of the tree

    public Text scoreText;
    public Text seedText;
    public Text timeText;

    public Canvas pause;

    public static bool gameStart;

    private bool paused = false;
    public static float seed;
    public static float score = 0f;
    [SerializeField] private float spawnTime;  //Lumberjack spawn time
    [SerializeField] private float gameTimer;
    private float spawnTimer;

    public static int numberOfTrees;    //Counts ALL types of tree
    private int checker;        //Holds the random number to determine spawned lumberjack's position



    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        //debugging purpose
        pause.enabled = false;
        gameTimer = 0;
        numberOfTrees = 0;
        gameStart = false;
        spawnTime = 5f;                       //debug
        spawnTimer = spawnTime;
        seed = 30f;
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        Gameplay();
        if (Input.GetKeyDown("escape"))
            paused = TogglePause();

    }

    private void Gameplay()
    {
        if (gameStart)
        {
            gameTimer += Time.deltaTime;
            spawnTimer -= Time.deltaTime;
            Spawn();
            TextUpdate();
        }
        if (gameStart && numberOfTrees == 0)
        {
            EndGame();
        }
    }

    #region Text Update

    //Handles all updated texts
    private void TextUpdate()
    {
        UpdateScore();
        UpdateSeed();
        UpdateTime();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    private void UpdateSeed()
    {
        seedText.text = "Seed: " + seed;
    }
    private void UpdateTime()
    {
        timeText.text = "Time: " + (int)gameTimer / 60 + ":" + (int)gameTimer % 60; // Minute:Second

    }
    #endregion

    private void SpawnLumberjack(int startLoc)
    {
        Vector2 dir = new Vector2(0, 0);
        switch (startLoc)
        {
            case 1:                     //From top
                dir = Vector2.down;
                break;
            case 2:                     //From right
                dir = Vector2.left;
                break;
            case 3:                     //From bottom
                dir = Vector2.up;
                break;
            case 4:                     //From left
                dir = Vector2.right;
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
            spawnTimer = spawnTime;
        }
    }


    bool TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pause.enabled = true;
            return true;
        }
        else
        {
            Time.timeScale = 1f;
            pause.enabled = false;
            return false;
        }
    }

    private void EndGame()
    {
        gameObject.SetActive(false);
    }


}
