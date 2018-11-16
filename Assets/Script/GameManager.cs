using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject lumber;
    public GameObject tree;

    public static float seed;
    public static float score = 0f;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnTimer;

    private int checker;

    public Text scoreText;
    public Text seedText;

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

    public void AddScore(float newScore)
    {
        score += newScore;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddSeed(float newSeed)
    {
        seed += newSeed;
        UpdateSeed();
    }

    private void UpdateSeed()
    {
        seedText.text = "Seed: " + seed;
    }

    private void SpawnLumberjack(int startLoc)
    {
        Vector2 dir = new Vector2(0, 0);
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
        if (spawnTimer <= 0)
        {
            checker = Random.Range(1, 5); //Determines which position the lumberjack is spawned in
            SpawnLumberjack(checker);
            spawnTimer = spawnTime;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }
    }
}