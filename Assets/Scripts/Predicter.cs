using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predicter : MonoBehaviour
{
    public SpriteRenderer secondComingBlock;
    public SpriteRenderer thirdComingBlock;

    public Sprite[] blockSprites;

    void LateUpdate()
    {
        if(BlockSpawnerPlayer2.blockSpawner.blackBlock <= 0)
        {
            secondComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.secondComingBlock];
            thirdComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.thirdComingBlock];
        }
       else
        {
            secondComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.Blocks.Length - 1];
            thirdComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.Blocks.Length-1];
        }
    }
}
