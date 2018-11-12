using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackController : MonoBehaviour {

    public GameObject SpawnerTop, SpawnerBottom, SpawnerRight, SpawnerLeft;
    public int edgeX,edgeY,edgeMinX,edgeMinY;
    public int centerRange;

    private Vector2 dir;
    public float stamina;

    private bool Reverse;
    private bool hit;
    private bool isAttacking = false;

    private int startDir;
    private int treeLayer;

    // Use this for initialization
    void Start () {
        //InitializeDir();
        Reverse = false;
        stamina = 100;
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
    
    //private void InitializeDir()
    //{
    //    if (SpawnerTop.transform.position.y > edgeY)
    //    {
    //        startDir = 1;
    //        dir = new Vector2(0, -1);
    //    }
    //    else if (SpawnerRight.transform.position.x > edgeX)
    //    {
    //        startDir = 2;
    //        dir = new Vector2(-1, 0);
    //    }
    //    else if (SpawnerBottom.transform.position.y > edgeMinY)
    //    {
    //        startDir = 3;
    //        dir = new Vector2(0, 1);
    //    }
    //    else if (SpawnerLeft.transform.position.x > edgeMinX)
    //    {
    //        startDir = 4;
    //        dir = new Vector2(1, 0);
    //    }    
    //}


    //Sets an initial 
     public void InitialiseDir(Vector2 dir/*Speed according to direction*/, int startDir /*Origin Position*/)
    {
        this.startDir = startDir;
        this.dir = dir;
        SetRandomPosition();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        /* if (collision.transform.tag == "Tree" && stamina != 0)
         {
             Debug.Log("COLLIDE");
             isAttacking = true;
             Invoke("Attack", 0.5f);
         }
         else if (stamina <= 0)
         {
             isCarryingWood = true;
         }

         if (collision.transform.tag == "Spawner")
         {
             stamina = 100;
             isCarryingWood = false;
         }
         */
       
        if (collision.transform.tag == "Tree")
        {
            if (stamina <= 0)
            {
                Reverse = true;
                isAttacking = false;
            }
        }

        if (collision.transform.tag == "Spawner")
        {
            stamina = 100;
            Reverse = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tree" )
        {
            Debug.Log("COLLIDE");
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
        stamina -= 10;
        Debug.Log("Strike");
        CancelInvoke();
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
