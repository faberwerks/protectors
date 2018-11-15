using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackController : MonoBehaviour {

    public GameObject spawnerTop, spawnerBottom, spawnerRight, spawnerLeft;
    public GameObject attackedTree;

    private Vector2 dir;

    public int edgeX,edgeY,edgeMinX,edgeMinY;
    public int centerRange;
    private int startDir;
    private int treeLayer;

    public float stamina;
    public float damage;

    private bool Reverse;
    private bool hit;
    private bool isAttacking = false;
    private bool isCarryingWood;

    // Use this for initialization
    void Start () {
        //InitializeDir();
        isCarryingWood = false;
        Reverse = false;
        stamina = 100;
        damage = 5f;
        SetRandomPosition();
        treeLayer = LayerMask.GetMask("Tree");    
    }
     
	// Update is called once per frame
	void Update () {
        DirChange();
        FindTree();
    }

    private void FindTree()
    {
        
        if (!isAttacking)
        {
            if (hit)
            {
                Move(); 
            }
            else if (isCarryingWood)
            {
                Move();
                Debug.Log("MOVE");
            }
            else
            {
                SetRandomPosition();
                hit = Physics2D.Raycast(transform.position, dir, Mathf.Infinity, treeLayer);
            }
        }
        else
        {
            Invoke("Attack", 0.5f);
        }
    }

    private void SetRandomPosition()
    {
        switch (startDir)
        {
            case 1:
                transform.SetPositionAndRotation(new Vector3(Random.Range(-11, 16), 13f, 0), Quaternion.identity);
                break;

            case 2:
                transform.SetPositionAndRotation(new Vector3(13f, Random.Range(-13, 13), 0), Quaternion.identity);
                break;
            case 3:
                transform.SetPositionAndRotation(new Vector3(Random.Range(-11, 16), -13f, 0), Quaternion.identity);
                break;
            case 4:
                transform.SetPositionAndRotation(new Vector3(-13f, Random.Range(-13, 13), 0), Quaternion.identity);
                break;
        }
        
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tree" )
        {
            Debug.Log("ENTERED COLLIISION");
            attackedTree = (GameObject) collision.gameObject;
            isAttacking = true;
        }

        if (collision.transform.tag == "Spawner")
        {
            stamina = 100;
            Reverse = true;
        }
    }


    private void Attack()
    {
        if (stamina > 0)
        {
            stamina -= 10;
            Debug.Log("Strike");
            attackedTree.GetComponent<Tree>().health -= damage;
            CancelInvoke();
        }

        else if (stamina <= 0)
        {
            Reverse = true;
            isAttacking = false;
            isCarryingWood = true;
            Debug.Log("Collide with tree");
        }

    }

    private void DirChange()
    {
        if(Reverse)
        {
            dir *= -1;
            Reverse = false;
        }
    }
}
