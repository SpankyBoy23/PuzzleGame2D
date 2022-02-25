using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerForBot : MonoBehaviour
{
    public GameObject[] Blocks;
    public static BlockSpawnerForBot blockSpawner;
    public int blackBlock = 0;

    private void Start()
    {
        blockSpawner = this;
       
        NewBlock();

    }
    public void NewBlock()
    {
        if (LogicManager.intance.canSpawn )
        {
            if (blackBlock <= 0)
            {
                var a = Instantiate(Blocks[Random.Range(1, Blocks.Length - 1)], transform.position, Quaternion.identity);
                a.transform.parent = null;
            }
            else
            {
                Instantiate(Blocks[Blocks.Length-1], transform.position, Quaternion.identity);
                blackBlock--;
            }
           
        }
    }
}
