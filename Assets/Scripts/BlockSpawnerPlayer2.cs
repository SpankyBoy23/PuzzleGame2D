using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerPlayer2 : MonoBehaviour
{
    public GameObject[] Blocks;
    public static BlockSpawnerPlayer2 blockSpawner;
    public int secondComingBlock, thirdComingBlock;
    public int currentBlock;
    public int blackBlock;

    private void Start()
    {
        blockSpawner = this;
        currentBlock = Random.Range(0, Blocks.Length);
        secondComingBlock = Random.Range(0, Blocks.Length-2);
        NewBlock();
    }
    public void NewBlock()
    {
        currentBlock = secondComingBlock;
        secondComingBlock = thirdComingBlock;
        thirdComingBlock = Random.Range(0, Blocks.Length-2);
        // Debug.Log("Spawning");
        if (LogicManager.intance.canSpawn)
        {
            if(blackBlock <= 0)
            {
                var a = Instantiate(Blocks[currentBlock], transform.position, Quaternion.identity);
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
