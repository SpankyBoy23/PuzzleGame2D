using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTeterminos : MonoBehaviour
{

    //  int roundedX;
    //  public int roundedY;
    public static int height = 12;
    public static int width = 6;
    float previousTime;
    public float fallTime = 1f;
    bool moveable = true;
    bool canSpawn = true;
    private float nextUpdate = .2f;
    public int botMoves = 0;

    public static Transform[,] grid = new Transform[width, height];
    public bool test;
    [Space]
    [Header("Effects")]
    public GameObject DestoryEffect;

    private void Start()
    {
        //botMoves = Random.Range(-1, 5);
    }
    void Update()
    {
        int roundedX2 = Mathf.RoundToInt(transform.position.x);
        int roundedY2 = Mathf.RoundToInt(transform.position.y);
        if (moveable)
        {

               if (Time.time - previousTime > fallTime)
               {
                    transform.position += new Vector3(0, -1, 0);

                      
                    if (!ValidMove())
                    {
                       
                        if (grid[roundedX2, 11] != null)
                        {
                            Debug.Log("You Won!");
                        //Time.timeScale = 0;
                        //   FindObjectOfType<BlockSpawnerForBot>().enabled = false;
                        //  canSpawn = false;
                          LogicManager.intance.LastMoveBot();
                        // this.Wait(2f, () => { UIManager.intance.Endgame(0); });
                        LogicManager.intance.canSpawn = false;
                    }
                        transform.position -= new Vector3(0, -1, 0);
                        AddToGird();
                        if (GetComponent<BotTeterminosPower>())
                           GetComponent<BotTeterminosPower>().isFallen = true;
                        else if (GetComponent<BotTeterminosBlock>())
                           GetComponent<BotTeterminosBlock>().isFallen = true;
                         moveable = false;
                                   // this.enabled = false;
                       if (canSpawn)
                        BlockSpawnerForBot.blockSpawner.NewBlock();
                     }
                if (botMoves > 0)
                {
                    
                    int a = Random.Range(1, 3);
                    if (a == 1)
                    {
                        MoveLeft();
                        botMoves--;
                    }
                    else if (a == 2)
                    {
                        MoveRight();
                        botMoves--;
                    }
                }
                previousTime = Time.time;
                   
               }

                 

        }
        if (!moveable)
        {
            //   previousTime = Time.time;
            if (Time.time >= nextUpdate)
            {
                //   Debug.Log(Time.time + ">=" + nextUpdate);
                // Change the next update (current second+1)
                nextUpdate = Time.time + 0.1f;
                // Call your fonction
                UpdateEverySecond();
            }
        }
        void UpdateEverySecond()
        {
            if (ValidMove())
            {
                grid[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) + 1] = null;
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
            if (grid[roundedX, roundedY] != null)
                return false;


            return true;
        }
        void AddToGird()
        {
            int roundedX = Mathf.RoundToInt(transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.position.y);

            grid[roundedX, roundedY] = transform;
        }
        void MoveLeft()
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }

        }
        void MoveRight()
        {

            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
    }
    private void OnDestroy()
    {
        Instantiate(DestoryEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("BotBreak");
    }

}
