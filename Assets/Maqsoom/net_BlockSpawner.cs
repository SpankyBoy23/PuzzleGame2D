using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class net_BlockSpawner2 : NetworkBehaviour
{
    public GameObject[] Blocks;

    public static net_BlockSpawner2 blockSpawner2;

    // Start is called before the first frame update
    void Start()
    {
        blockSpawner2 = this;
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
        GameObject a = Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
        NetworkIdentity netIdentity = NetworkIdentity.spawned[netId];
        NetworkServer.Spawn(a, netIdentity.connectionToClient);
    }
}
