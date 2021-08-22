using CodeMonkey.Utils;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    public int[,] gridArray;
    public GameObject[] prefab;

    public Grid(int width,int height,float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                
            }
        }
    }
    
}
