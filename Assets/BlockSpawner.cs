using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] Blocks;
    public static BlockSpawner blockSpawner;
    public int secondComingBlock,thirdComingBlock;
    public int currentBlock;

    private void Start()
    {
        blockSpawner = this;
        currentBlock = Random.Range(0, Blocks.Length);
        secondComingBlock = Random.Range(0, Blocks.Length);
        NewBlock();

    }
    public void NewBlock()
    {
        currentBlock = secondComingBlock;
        secondComingBlock = thirdComingBlock;
        thirdComingBlock = Random.Range(0, Blocks.Length);
        var a= Instantiate(Blocks[currentBlock], transform.position, Quaternion.identity);
        a.transform.parent = null;

        
    }
}
