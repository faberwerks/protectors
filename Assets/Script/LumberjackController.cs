using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackController : MonoBehaviour {
    
    private GameObject attackedTree;
    
    [SerializeField] private Vector2 dir;

    private int startDir;
    private int treeLayer;

    public float stamina; 
    public float damage;
    private float searchCooldownTime; //How long the Lumberjack idles in the spawn area
    private float searchCooldownTimer; //The actual countdown

    private bool hit; 
    private bool isAttacking = false; 
    private bool isCarryingWood; 

    // Use this for initialization
    void Start () {
        //InitializeDir();
        searchCooldownTime = 2f;
        searchCooldownTimer = searchCooldownTime;
        isCarryingWood = false;
        stamina = 100;
        damage = 5f;
        SetRandomPosition();
        treeLayer = LayerMask.GetMask("Tree");
    }
     
	// Update is called once per frame
	void Update () {
        FindTree();
        ChangeColour();
    }

    //Main function of the lumberjack
    private void FindTree()
    {
        //Cooldown of the lumberjack before it attack again
        if (searchCooldownTimer <= 0)
        {
            if (!isAttacking)
            {
                if (!isCarryingWood)
                {
                    hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, treeLayer);
                    if (hit)
                    {
                        Move();
                    }
                    else 
                    {
                        SetRandomPosition();
                    }
                }
                else
                {
                    Move();
                }
            }
            else
            {
                Invoke("Attack", 0.5f);
            }
        }
        else
        {
            searchCooldownTimer -= Time.deltaTime;
        }
    }

    //to spawn randomly 
    private void SetRandomPosition()
    {
        switch (startDir)//Sets position according to a random range. Depends on the start point as well (up or down)
        {
            case 1:
                transform.SetPositionAndRotation(new Vector3(Random.Range(-11f, 14f), 13f, 0), Quaternion.identity);
                break;

            case 2:
                transform.SetPositionAndRotation(new Vector3(14f, Random.Range(-13f, 13f), 0), Quaternion.identity);
                break;
            case 3:
                transform.SetPositionAndRotation(new Vector3(Random.Range(-11f, 14f), -13f, 0), Quaternion.identity);
                break;
            case 4:
                transform.SetPositionAndRotation(new Vector3(-13f, Random.Range(-13f, 13f), 0), Quaternion.identity);
                break;
        }
        
    }

    // just read :)
    private void Move()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    //Sets an initial 
     public void InitialiseDir(Vector2 dir/*Speed according to direction*/, int startDir /*Origin Position*/)
    {
        this.startDir = startDir;
        this.dir = dir;
        SetRandomPosition();
    }
    
    //what the lumberjack do when collide with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tree" && !isCarryingWood)
        {
            attackedTree = (GameObject) collision.gameObject;
            isAttacking = true;
        }
        if (collision.tag == "Spawner" && isCarryingWood)
        {
            //Debug.Log("Collide With Spawner . Change Direction");
            stamina = 100;
            dir *= -1;
            isCarryingWood = false;
            SetRandomPosition();
            hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, treeLayer);
            searchCooldownTimer = searchCooldownTime;
        }
    }

    //what lumberjack do when attacking
    private void Attack()
    {
        if (stamina > 0 && attackedTree != null) //Attacks if stamina is more than 0
        {
            stamina -= 10;
            attackedTree.GetComponent<Tree>().health -= damage;
        }
        else if (stamina <= 0 || attackedTree == null)
        {
            //Debug.Log("Stamina Abis . Change Direction");
            hit = false;
            dir *= -1;
            isAttacking = false;
            isCarryingWood = true;
        }
        

        CancelInvoke(); //To cancel the attack command so that the lumberjack does not continue attacking.
    }

    //to change color when it is carrying wood or not (more for debugging at the moment)
    private void ChangeColour()
    {
        if (isCarryingWood)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
            
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        }

    }
}