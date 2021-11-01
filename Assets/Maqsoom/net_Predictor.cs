using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class net_Predictor : MonoBehaviour
{
    public SpriteRenderer secondComingBlock;
    public SpriteRenderer thirdComingBlock;

    public Sprite[] blockSprites;

    public static net_Predictor predictor;

    public bool start;

    void Start()
    {
        predictor = this;
    }

    void Update()
    {
        if (start == false) return;

        if (GameManager.singleton.firstPlayer.netId == NetworkClient.localPlayer.GetComponent<Player>().netId)
        {
            if (net_BlockSpawner.blockSpawner.blackBlock <= 0)
            {
                secondComingBlock.sprite = blockSprites[net_BlockSpawner.blockSpawner.secondComingBlock];
                thirdComingBlock.sprite = blockSprites[net_BlockSpawner.blockSpawner.thirdComingBlock];
            }
            else
            {
                secondComingBlock.sprite = blockSprites[blockSprites.Length - 1];
                thirdComingBlock.sprite = blockSprites[blockSprites.Length - 1];
            }
        }
        else
        {
            if (net_BlockSpawner2.blockSpawner2.blackBlock <= 0)
            {
                secondComingBlock.sprite = blockSprites[net_BlockSpawner2.blockSpawner2.secondComingBlock];
                thirdComingBlock.sprite = blockSprites[net_BlockSpawner2.blockSpawner2.thirdComingBlock];
            }
            else
            {
                secondComingBlock.sprite = blockSprites[blockSprites.Length - 1];
                thirdComingBlock.sprite = blockSprites[blockSprites.Length - 1];
            }
        }
    }
}
