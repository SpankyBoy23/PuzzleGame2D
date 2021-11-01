using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class net_BlockSpawner : NetworkBehaviour
{
    public GameObject[] Blocks;

    public static net_BlockSpawner blockSpawner;

    public int secondComingBlock;
    public int thirdComingBlock;
    public int currentBlock;

    public bool first;

    public int blackBlock;

    // Start is called before the first frame update
    void Start()
    {
        blockSpawner = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBlock(uint netId, int currentBlockId)
    {
        if (blackBlock <= 0)
        {
            CmdNewBlock(netId, currentBlockId);
        }
        else
        {
            blackBlock--;

            currentBlockId = Blocks.Length - 1;

            CmdNewBlock(netId, currentBlockId);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdNewBlock(uint netId , int currentBlockId) 
    {
        GameObject a = Instantiate(Blocks[currentBlockId], transform.position, Quaternion.identity);
        NetworkIdentity netIdentity = NetworkIdentity.spawned[netId];
        NetworkServer.Spawn(a, netIdentity.connectionToClient);
    }
}
