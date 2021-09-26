using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerForBot : MonoBehaviour
{
    public GameObject[] Blocks;
    public static BlockSpawnerForBot blockSpawner;


    private void Start()
    {
        blockSpawner = this;
       
        NewBlock();

    }
    public void NewBlock()
    {
       
        var a= Instantiate(Blocks[Random.Range(0,Blocks.Length)], transform.position, Quaternion.identity);
        a.transform.parent = null;

        
    }
}
