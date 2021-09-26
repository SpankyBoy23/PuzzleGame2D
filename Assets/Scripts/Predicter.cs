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
        secondComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.secondComingBlock];
        thirdComingBlock.sprite = blockSprites[BlockSpawnerPlayer2.blockSpawner.thirdComingBlock];
    }
}
