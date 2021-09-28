using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeterminosPlayer2 : MonoBehaviour
{
 
    //  int roundedX;
    //  public int roundedY;
    public static int height = 12;
    public static int width = 6;
    float previousTime;
    public float fallTime = 0.8f;
    bool moveable = true;
    bool canSpawn = true;
    private float nextUpdate = .2f;
    public float countDown = 2f;

    public static Transform[,] grid2 = new Transform[width, height];
    public bool test;

    void Update()
    {
        int roundedX2 = Mathf.RoundToInt(transform.position.x);
        int roundedY2 = Mathf.RoundToInt(transform.position.y);
        if (moveable)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }

            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    if (grid2[roundedX2, 11] != null)
                    {
                        LogicManager.intance.canSpawn = false;
                        if (countDown <= 0)
                        {
                            Debug.Log("You Lost!");
                            // Time.timeScale = 0;
                            //  FindObjectOfType<BlockSpawnerPlayer2>().enabled = false;
                            
                        }
                    }
                    else
                    {
                        countDown = 2;
                    }
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGird();
                    if (GetComponent<PowerBlocksPlayer2>())
                        GetComponent<PowerBlocksPlayer2>().isFallen = true;
                    else if (GetComponent<TeterminoBlockPlayer2>())
                        GetComponent<TeterminoBlockPlayer2>().isFallen = true;
                    moveable = false;
                    // this.enabled = false;
                    if(canSpawn)
                    BlockSpawnerPlayer2.blockSpawner.NewBlock();
                }
                previousTime = Time.time;
            }
        }

      //  if(grid2[roundedX2,roundedY2+1] != gameObject)
        {
            //grid2[roundedX2, roundedY2 + 1] = null;
        }
       // test = ValidMove();

       
        /* if (!moveable && grid[roundedX2, roundedY2 - 1] == null)
         {
             Debug.Log("Working " + roundedX2 + " ," + roundedY2);
             //  grid[roundedX, roundedY+1] = null;
             transform.position += new Vector3(0, -1, 0);
             if (!ValidMove())
             {
                 grid[roundedX2, roundedY2] = null;
                 transform.position -= new Vector3(0, -1, 0);
                 AddToGird();
             }

         }
        if (ValidMove()&& !moveable)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            Debug.Log("We Can Move");
        }*/
        if (!moveable)
        {
         
            
            //   previousTime = Time.time;
            if (Time.time >= nextUpdate)
            {
              //  Debug.Log(Time.time + ">=" + nextUpdate);
                // Change the next update (current second+1)
                nextUpdate = Time.time + 0.2f;
                // Call your fonction
                UpdateEverySecond();
            }


        }
      
    }
    void UpdateEverySecond()
    {
        if (ValidMove())
        {
            grid2[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)+1] = null;
        }
        transform.position += new Vector3(0, -1, 0);
        if (!ValidMove())
        {
            // Debug.Log("Working");
            /*if (grid2[roundedX2, roundedY2 + 1] != null)
            {
               // Debug.Log("Happening" + roundedX2 + "," + roundedY2);
                // grid[roundedY2,roundedY2+1].GetComponent<Teterminos>()
              //  grid2[roundedX2, roundedY2 + 1] = null;
            }
           */
            // grid2[roundedX2, roundedY2] = null;
            transform.position -= new Vector3(0, -1, 0);
            AddToGird();

        }
    }

    bool ValidMove()
    {

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);


        if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
        {
            return false;
        }
        if (grid2[roundedX, roundedY] != null)
            return false;


        return true;
    }
    void AddToGird()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);

        grid2[roundedX, roundedY] = transform;
    }
}
