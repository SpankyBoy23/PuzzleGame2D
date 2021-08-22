using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float perviousTime;
    public float fallTime = 0.8f;
    public static int height = 10;
    public static int width = 6;
    int roundedX;
    int roundedY;
    public static Transform[,] grid = new Transform[width, height];
    [SerializeField] float countDown = 2;

    private void Update()
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

        if(Time.time - perviousTime > (Input.GetKey(KeyCode.DownArrow)? fallTime / 10 : fallTime)){
            transform.position += new Vector3(0, -1f, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1f, 0);
                AddToGird();
                this.enabled = false;
                BlockSpawner.blockSpawner.NewBlock();
            }
            perviousTime = Time.time;
        }
        bool ValidMove()
        {
             roundedX = Mathf.RoundToInt(transform.position.x);
             roundedY = Mathf.RoundToInt(transform.position.y);
             
             if (roundedX < 0 || roundedX >= width || roundedY < 0|| roundedY >= height)
             {
                return false;
             }
             if(grid[roundedX,roundedY] !=null){
             return false;
             }
           
            return true;
        }
        if(transform.position.y >= 10)
        {
            countDown -= Time.deltaTime;
        }
        if(countDown < 0)
        {
            Debug.Log("Game-Over");
        }
     
    }
    void AddToGird()
    {
        roundedX = Mathf.RoundToInt(transform.position.x);
        roundedY = Mathf.RoundToInt(transform.position.y);
       // Debug.Log(roundedX);
      //  Debug.Log(roundedY);
        grid[roundedX, roundedY] = transform;
       // Debug.Log("X "+roundedX +",Y "+ roundedY);
    }

   
}
