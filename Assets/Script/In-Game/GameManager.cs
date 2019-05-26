using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioClip loseClip;

    public static GameManager instance = null;  //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;                   //To have reference of the lumberjack
    public GameObject tree;                     //To have reference of the tree
    public GameObject countdown;

    public Text scoreText;
    public Text seedText;
    public Text timeText;
    public Text countdownText;

    public Canvas pauseCanvas;
    public Canvas gameOverCanvas;

    public static bool gameStart;                   //Game's state (start after the first tree planted)
    public static bool pauseChecker;                //To trigger pause
    public static bool hasPlantedFruit;             //Check if a Fruit Tree has been planted
    private bool startCountdown;                    //

    public static float seed;
    public static float score;
    public float lowestSeedCost;                //holds the lowest seed cost for FRUIT Tree
    [SerializeField] private float spawnTime;   //Lumberjack spawn time
    [SerializeField] private float gameTimer;
    [SerializeField] private float finalCountdown;
    private float spawnTimer;



    public static int numberOfFruitTree;    //Counts Fruit types trees
    public static int numberofSuppTree;     //Count Supp types trees

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
        lowestSeedCost = 2f;                        //Orange Tree
        finalCountdown = 15f;                       //debug
        countdown.SetActive(false);
        pauseChecker = false;
        hasPlantedFruit = false;
        pauseCanvas.gameObject.SetActive(false);
        startCountdown = false;
        score = 0f;
        gameTimer = 0;
        numberOfFruitTree = 0;
        numberofSuppTree = 0;
        gameStart = false;
        spawnTime = 5f;                             //debug
        spawnTimer = spawnTime;
        seed = 30f;
        Time.timeScale = 1f;
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfFruitTree > 0) startCountdown = false;
        Gameplay();
        if (Input.GetKeyDown("escape") || pauseChecker)
        {
            TogglePause();
            pauseChecker = false;
        }
    }

    private void LateUpdate()
    {
        if (gameStart) CheckLose();
    }

    private void Gameplay()
    {
        if (gameStart)
        {
            if (startCountdown) finalCountdown -= Time.deltaTime;
            gameTimer += Time.deltaTime;
            spawnTimer -= Time.deltaTime;
            Spawn();
            TextUpdate();

        }
    }

    #region Text Update

    //Handles all updated texts
    private void TextUpdate()
    {
        UpdateScore();
        UpdateSeed();
        UpdateTime();
        UpdateCountdown();
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
    private void UpdateCountdown()
    {
        if (startCountdown)
        {
            countdown.SetActive(true);
            countdownText.text = "Final Countdown:\n" + (int)finalCountdown;
        }
        else
        {
            countdown.SetActive(false);
        }
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

    private void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    private void CheckLose()
    {
        if (numberOfFruitTree <= 0)
        {
            if ((numberofSuppTree <= 0 || seed < lowestSeedCost || finalCountdown <= 0) && Time.timeScale == 0f)
            {

            }
            else if (numberofSuppTree <= 0 || seed < lowestSeedCost || finalCountdown <= 0)
            {
                EndGame();
            }
            else if (hasPlantedFruit)
            {
                startCountdown = true;
            }
        }
    }

    private void EndGame()
    {
        score += (int)gameTimer * 10;
        Time.timeScale = 0;
        GetComponent<AudioSource>().PlayOneShot(loseClip);
        ScoreCalculation();
        gameOverCanvas.gameObject.SetActive(true);
    }

    private void ScoreCalculation()
    {
        // add the formula to convert time to score (time * value of all planted trees)
        // need to add tree score values, and value counter
        if ((score + gameTimer * 10) > PlayerPrefs.GetInt("score", 0))
        {

            PlayerPrefs.SetInt("score", (int)score);
            PlayerPrefs.SetInt("time", (int)gameTimer);
        }
    }
}
