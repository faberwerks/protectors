using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public enum TreeType : byte { FRUIT, SUPPORT }; //enum for the 2 tree types

    public TreeType type;           //to determine the type of the tree

    public Sprite appleTree, appleTreeHarvest;
    public Sprite mangoTree, mangoTreeHarvest;
    public Sprite orangeTree, orangeTreeHarvest;
    public Sprite oakTree;
    public Sprite mapleTree;
    public Sprite grapeTree;

    public bool isEffectedByMaple = false;
    protected bool harvestable = false;                     //to check whether the tree is harvestable or not

    public int treeTypeNumber;

    public float maxHealth;
    public float health = 100f;            //tree's health
    // public float SeedCost { get; set; }       //cost to make the tree
    [SerializeField] private float seedValue;      //value of the seed recieved when harvested
    [SerializeField] private float scoreValue;     //value of the score received when harvested
    [SerializeField] private float oriHarvestTime;
    [SerializeField] protected float harvestTime;  //time to harvest
    [SerializeField] protected float harvestTimer;  //timer 
    [SerializeField] private float mapleMod;         //maple tree effect's percentage value 
    [SerializeField] private float mapleTimer;       //maple tree's timer
    [SerializeField] private float healCount;        //grape tree healing amount
    [SerializeField] private float healAmount;       //the value grape tree gives every heal 

    


    // Use this for initialization
    void Start()
    {
        oriHarvestTime = harvestTime;
        maxHealth = health;
        GameManager.gameStart = true;
        type = CheckTreeType();
        harvestTimer = harvestTime;

    }

    // Update is called once per frame
    void Update()
    {
        switch (type)   //the function differs depending on the tree type (FRUIT or SUPPORT)
        {
            case (TreeType.FRUIT):
                BearFruit();
                break;
            case (TreeType.SUPPORT):
                TreeEffect();
                break;
        }
        CheckHealth();
    }

    //check the health of the Tree
    protected void CheckHealth()
    {
        if(mapleTimer > 0)
        {
            mapleTimer -= Time.deltaTime;
            if (mapleTimer <= 0) Destroy(gameObject);
        }

        if (health <= 0)    //when health reaches 0, destroy the tree
        {
            if (treeTypeNumber < 3)
            {
                GameManager.numberOfFruitTree -= 1;
            }
            else
            {
                GameManager.numberofSuppTree -= 1;
            }
            if (treeTypeNumber == 5)
            {
                DeactiveMaplePower();
            }
            Destroy(gameObject);
        }
    }

    private TreeType CheckTreeType()
    {
        if (treeTypeNumber < 4)
        {
            if (treeTypeNumber == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = appleTree;
            }
            else if (treeTypeNumber == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = mangoTree;
            }
            else if (treeTypeNumber == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = orangeTree;
            }
            else if (treeTypeNumber == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = oakTree;
                GameManager.numberofSuppTree += 1;
                return TreeType.SUPPORT;
            }
            GameManager.numberOfFruitTree += 1;
            GameManager.hasPlantedFruit = true;
            return TreeType.FRUIT;
        }

        else
        {
            if (treeTypeNumber == 4)
            {
                this.GetComponent<SpriteRenderer>().sprite = grapeTree;
            }
            else if (treeTypeNumber == 5)
            {
                this.GetComponent<SpriteRenderer>().sprite = mapleTree;
            }
            GameManager.numberofSuppTree += 1;
            gameObject.layer = 10;
            return TreeType.SUPPORT;
        }
    }

    //The main algorithm for harvestable tree (FRUIT tree)
    protected void BearFruit()
    {
        if (harvestTimer <= 0 && !harvestable)
        {
            harvestTimer = harvestTime;
        }
        else if (!harvestable)
        {
            harvestTimer -= Time.deltaTime;
            if (harvestTimer <= 0)
            {
                harvestable = true;             //when timer runs out, set tree into harvestable

                if (treeTypeNumber == 0)
                {
                    this.GetComponent<SpriteRenderer>().sprite = appleTreeHarvest;
                }
                else if (treeTypeNumber == 1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = mangoTreeHarvest;
                }
                else if (treeTypeNumber == 2)
                {
                    this.GetComponent<SpriteRenderer>().sprite = orangeTreeHarvest;
                }
                else
                {
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.black);
                }

            }
        }
    }

    //The main algorithm for supporting tree (SUPPORT tree)
    protected void TreeEffect()
    {
        if (treeTypeNumber == 4)
        {
            Invoke("GrapePower", 2f);
        }
        else if (treeTypeNumber == 5)
        {
            MaplePower();
        }
    }

    //Things what will happen when a FRUIT tree is harvested
    protected void Harvest()
    {
        if (harvestable)
        {
            //access gamemanager to set seed and score

            GameManager.score += scoreValue;
            GameManager.seed += seedValue;

            if (treeTypeNumber == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = appleTree;
            }
            else if (treeTypeNumber == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = mangoTree;
            }
            else if (treeTypeNumber == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = orangeTree;
            }
            //Debug.Log("Score: " + GameManager.score);
            //Debug.Log("Seed: " + GameManager.seed);
            harvestable = false;                   //set back the harvestable to false
        }
    }

    //Do Harvest when the tree is clicked by the mouse
    private void OnMouseDown()
    {
        Harvest();
    }

    private void GrapePower()
    {
        for (float x = -1f; x <= 1; x++)
        {
            for (float y = -1f; y <= 1; y++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(x, y), 2f, LayerMask.GetMask("Tree"));
                if (hitInfo)
                {
                    if ((x != 0 || y != 0) && hitInfo.transform.gameObject.GetComponent<Tree>().health < hitInfo.transform.gameObject.GetComponent<Tree>().maxHealth)
                    {
                        hitInfo.transform.gameObject.GetComponent<Tree>().health += healAmount;
                        healCount-=1;
                    }
                    if (healCount <= 0) Destroy(gameObject);
                }
            }
        }
        CancelInvoke();
    }

    private void MaplePower()
    {
        for (float x = -1f; x <= 1; x++)
        {
            for (float y = -1f; y <= 1; y++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(x, y), 2f, LayerMask.GetMask("Tree"));
                if (hitInfo)
                {
                    if ((x != 0 || y != 0) && !hitInfo.transform.gameObject.GetComponent<Tree>().isEffectedByMaple)
                    {
                        var affectedTree = hitInfo.transform.gameObject.GetComponent<Tree>();
                        affectedTree.isEffectedByMaple = true;
                        affectedTree.harvestTime *= ((100 - mapleMod) / 100);
                    }
                }
            }
        }
    }

    private void DeactiveMaplePower()
    {
        for (float x = -1f; x <= 1; x++)
        {
            for (float y = -1f; y <= 1; y++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(x, y), 2f, LayerMask.GetMask("Tree"));
                if (hitInfo)
                {
                    if ((x != 0 || y != 0) && hitInfo.transform.gameObject.GetComponent<Tree>().isEffectedByMaple)
                    {
                        var affectedTree = hitInfo.transform.gameObject.GetComponent<Tree>();
                        affectedTree.isEffectedByMaple = false;
                        affectedTree.harvestTime = affectedTree.oriHarvestTime;
                        Debug.Log("deactive");
                    }
                }
            }
        }
    }

    public void InitialiseAttribute(float health, float seedValue, float scoreValue, 
        float harvestTime, int treeTypeNumber, float mapleMod, float mapleTimer, 
        float healCount, float healAmount)
    {
        this.treeTypeNumber = treeTypeNumber;
        this.health = health;
        this.seedValue = seedValue;
        this.scoreValue = scoreValue;
        this.harvestTime = harvestTime;
        this.mapleMod = mapleMod;
        this.mapleTimer = mapleTimer;
        this.healCount = healCount;
        this.healAmount = healAmount;
    }
}
    
