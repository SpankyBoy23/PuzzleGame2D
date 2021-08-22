using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teterminos : MonoBehaviour
{
  //  int roundedX;
  //  public int roundedY;
    public static int height = 12;
    public static int width = 6;
    float previousTime;
    private float fallTime = 0.8f;
    public BlockColor blockColor = BlockColor.Red;
    public static Transform[,] grid = new Transform[width, height];
    private bool moveable = true;

    void Update()
    {
        if (moveable)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }
            if (transform.position.y <= 0)
            {
                //  BlockSpawner.blockSpawner.NewBlock();
                //  this.GetComponent<Teterminos>().enabled = false;
            }
            if (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    // Debug.Log("Working");
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGird();
                    if (GetComponent<PowerBlocksPlayer1>())
                        GetComponent<PowerBlocksPlayer1>().isFallen = true;
                    else if (GetComponent<TeterminoBlockPlayer1>())
                        GetComponent<TeterminoBlockPlayer1>().isFallen = true;
                    moveable = false;
                    BlockSpawner.blockSpawner.NewBlock();
                }
                previousTime = Time.time;
            }
        }
        int roundedX2 = Mathf.RoundToInt(transform.position.x);
        int roundedY2 = Mathf.RoundToInt(transform.position.y);
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
            
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                // Debug.Log("Working");
                if (grid[roundedX2, roundedY2+1] != null)
                {
                    // grid[roundedY2,roundedY2+1].GetComponent<Teterminos>()
                    grid[roundedX2, roundedY2 + 1] = null;
                }
                grid[roundedX2, roundedY2] = null;
                transform.position -= new Vector3(0, -1, 0);
                AddToGird();
             //   if (GetComponent<PowerBlocksPlayer1>())
                  //  GetComponent<PowerBlocksPlayer1>().isFallen = true;
              //  else if (GetComponent<TeterminoBlockPlayer1>())
               //     GetComponent<TeterminoBlockPlayer1>().isFallen = true;
              //  moveable = false;
               // BlockSpawner.blockSpawner.NewBlock();
            }
            previousTime = Time.time;
        }
        




    }
    bool ValidMove()
    {

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);
        //  Debug.Log(roundedY);

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
}
