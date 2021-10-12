using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string username;

    public bool isReady;

    [Command]
    void CmdSetup(string name) 
    {
        username = name;
        isReady = true;

        if (GameManager.singleton.firstPlayer == null)
        {
            GameManager.singleton.firstPlayer = this;
            RpcSetup(GameManager.singleton.firstPlayer.netId);
        }
        else
        {
            RpcSetup(GameManager.singleton.firstPlayer.netId);
        }
    }

    [ClientRpc]
    void RpcSetup(uint netId) 
    {
       /* if (isClient && isServer)
        {
            if (hasAuthority) 
            {
                NewBlock();
                return;
            }
        }
*/
        GameManager.singleton.firstPlayer = NetworkIdentity.spawned[netId].GetComponent<Player>();
        Debug.Log("workingrpcsetup-1");
        if (hasAuthority)
        {
            if (GameManager.singleton.firstPlayer.netId != this.netId)
            {
                Camera localCam = GameObject.Find("Main Camera (1)").GetComponent<Camera>();
                Camera nonLocalCam = GameObject.Find("Main Camera (2)").GetComponent<Camera>();

                Debug.Log("workingrpcsetup");

                Vector3 localCamPosition = localCam.transform.position;

                localCam.transform.position = nonLocalCam.transform.position;
                nonLocalCam.transform.position = localCamPosition;

             //   NewBlock();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterPlayer(this, netId);

        if (hasAuthority == false) return;

        username = PlayerPrefs.GetString("Username");
       
        CmdSetup(username);
    }

    public void NewBlock() 
    {
        if(GameManager.singleton.firstPlayer.netId == netId) 
        {
            if (net_BlockSpawner.blockSpawner.first == false) 
            {
                net_BlockSpawner.blockSpawner.currentBlock = Random.Range(0, 6);
                net_BlockSpawner.blockSpawner.secondComingBlock = Random.Range(0, 6);
                net_BlockSpawner.blockSpawner.thirdComingBlock = Random.Range(0, 6);

                net_BlockSpawner.blockSpawner.NewBlock(netId , net_BlockSpawner.blockSpawner.currentBlock);

                net_Predictor.predictor.start = true;
                net_BlockSpawner.blockSpawner.first = true;
            }
            else 
            {
                net_BlockSpawner.blockSpawner.currentBlock = net_BlockSpawner.blockSpawner.secondComingBlock;
                net_BlockSpawner.blockSpawner.secondComingBlock = net_BlockSpawner.blockSpawner.thirdComingBlock;
                net_BlockSpawner.blockSpawner.thirdComingBlock = Random.Range(0, 6);

                net_BlockSpawner.blockSpawner.NewBlock(netId, net_BlockSpawner.blockSpawner.currentBlock);
            }
        }
        else 
        {
            if (net_BlockSpawner2.blockSpawner2.first == false)
            {
                net_BlockSpawner2.blockSpawner2.currentBlock = Random.Range(0, 6);
                net_BlockSpawner2.blockSpawner2.secondComingBlock = Random.Range(0, 6);
                net_BlockSpawner.blockSpawner.thirdComingBlock = Random.Range(0, 6);

                net_BlockSpawner2.blockSpawner2.NewBlock(netId, net_BlockSpawner2.blockSpawner2.currentBlock);
                net_Predictor.predictor.start = true;
                net_BlockSpawner2.blockSpawner2.first = true;
            }
            else
            {
                net_BlockSpawner2.blockSpawner2.currentBlock = net_BlockSpawner2.blockSpawner2.secondComingBlock;
                net_BlockSpawner2.blockSpawner2.secondComingBlock = net_BlockSpawner2.blockSpawner2.thirdComingBlock;
                net_BlockSpawner2.blockSpawner2.thirdComingBlock = Random.Range(0, 6);

                net_BlockSpawner2.blockSpawner2.NewBlock(netId, net_BlockSpawner2.blockSpawner2.currentBlock);
            }
        }
    }

    bool firstTime;

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false) return;

        if(GameManager.singleton.currentGameState == GameState.Playing) 
        {
            if(GameManager.players.Count >= 2) 
            {
                if (firstTime == true) return;
                firstTime = true;
                NewBlock();
            }
        }
    }

    void OnDestroy()
    {
        GameManager.UnRegisterPlayer(netId);
        
    }
}
