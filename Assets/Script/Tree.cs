using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public enum TreeType : byte { FRUIT, SUPPORT };

    GameManager gameManager;

    public TreeType type;

    public float health;
    protected float seedCost;
    protected float seedValue;
    protected float scoreValue;
    [SerializeField] protected float harvestTime = 5f;
    [SerializeField] protected float harvestTimer = 0f;

    protected bool harvestable = false;

	// Use this for initialization
	void Start () {
        type = TreeType.FRUIT;

        health = 100f;
        seedValue = 10f;
        scoreValue = 100f;
        harvestTime = 5f;
        harvestTimer = harvestTime;
	}
	
	// Update is called once per frame
	void Update () {
        CheckHealth();
        switch (type)
        {
            case (TreeType.FRUIT):
                BearFruit();
                break;
            case (TreeType.SUPPORT):
                TreeEffect();
                break;
        }
	}

    protected void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

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
                harvestable = true;
                GetComponent<Renderer>().material.SetColor("_Color",Color.red);
            }
        }
    }

    protected void TreeEffect() { }

    protected void Harvest()
    {
        if (harvestable)
        {
            //access gamemanager to set seed and score
            GameManager.score += 100;
            GameManager.seed += 10;
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            Debug.Log("Score: " + GameManager.score);
            Debug.Log("Seed: " + GameManager.seed);
            harvestable = false;
        }
    }

    private void OnMouseDown()
    {
        Harvest();
    }
}
