using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class net_BlockSpawner2 : NetworkBehaviour
{
    public GameObject[] Blocks;
    
    public static net_BlockSpawner2 blockSpawner2;

    [SyncVar]
    public int secondComingBlock;
    [SyncVar]
    public int thirdComingBlock;
    [SyncVar]
    public int currentBlock;

    // Start is called before the first frame update
    void Start()
    {
        blockSpawner2 = this;

        if (isServer)
        {
            currentBlock = Random.Range(0, Blocks.Length);
            secondComingBlock = Random.Range(0, Blocks.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBlock(uint netId)
    {
        CmdNewBlock(netId);
    }

    [Command(requiresAuthority = false)]
    void CmdNewBlock(uint netId)
    {
        currentBlock = secondComingBlock;
        secondComingBlock = thirdComingBlock;
        thirdComingBlock = Random.Range(0, Blocks.Length);

        GameObject a = Instantiate(Blocks[currentBlock], transform.position, Quaternion.identity);
        NetworkIdentity netIdentity = NetworkIdentity.spawned[netId];
        NetworkServer.Spawn(a, netIdentity.connectionToClient);
    }
}
