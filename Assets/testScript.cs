using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public float[,] Grid;
    public int vertical, horizontal, colums, rows;
    public GameObject prefab;

    private void Start()
    {
      //  vertical = (int)Camera.main.orthographicSize;
      //  horizontal = vertical * (Screen.height / Screen.width);
      //  colums = horizontal * 2;
      //  rows = vertical * 2;

        for(int i = 0;i < colums; i++)
        {
            for (int j = 0; j < rows; j++)
            {
             //   Grid[i, j] = Random.Range(0f, 1f);
                Instantiate(prefab, new Vector3(j, i, 0), Quaternion.identity);
            }
        }
    }
}
