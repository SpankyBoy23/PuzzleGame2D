using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string username;

    [SyncVar(hook = nameof(OnCharacterChange))]
    public int cId;

    public bool isReady;

    [Command]
    void CmdSetup(string name , int characterId) 
    {
        username = name;
        isReady = true;

        cId = characterId;

        if(net_CharacterManager.Singleton.first.playerId == 0) 
        {
            net_CharacterManager.Singleton.first.playerId = netId;
            net_CharacterManager.Singleton.first.netIdentity.AssignClientAuthority(connectionToClient);
        }
        else
        {
            net_CharacterManager.Singleton.second.playerId = netId;
            net_CharacterManager.Singleton.second.netIdentity.AssignClientAuthority(connectionToClient);
        }

        if (GameManager.singleton.firstPlayer == null)
        {
            GameManager.singleton.firstPlayer = this;
            RpcSetup(GameManager.singleton.firstPlayer.netId, characterId);
        }
        else
        {
            RpcSetup(GameManager.singleton.firstPlayer.netId, characterId);
        }
    }

    [ClientRpc]
    void RpcSetup(uint netId , int characterId) 
    {
       /* if (isClient && isServer)
        {
            if (hasAuthority) 
            {
                NewBlock();
                return;
            }
        }*/

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

        isReady = true;

    }

    void OnCharacterChange(int oldValue , int newValue) 
    {
        bool first = false;

        if(net_CharacterManager.Singleton.first.playerId == 0 || net_CharacterManager.Singleton.first.playerId == netId) 
        {
            first = true;
        }

        if (first)
        {
            net_CharacterManager.Singleton.first.Spawn(newValue , net_CharacterManager.Singleton.first.transform);
        }
        else
        {
            net_CharacterManager.Singleton.second.Spawn(newValue, net_CharacterManager.Singleton.second.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterPlayer(this, netId);

        if (hasAuthority == false) return;

        username = PlayerPrefs.GetString("Username");
        int characterId = PlayerPrefs.GetInt("CharacterIndex");
        CmdSetup(username , characterId);
    }
    
    public void NewBlock() 
    {
      
        if (GameManager.singleton.decide == true) return;

        if (GameManager.singleton.firstPlayer.netId == netId) 
        {
            if (net_BlockSpawner.blockSpawner.first == false) 
            {
                net_BlockSpawner.blockSpawner.currentBlock = Random.Range(0, 12);
                net_BlockSpawner.blockSpawner.secondComingBlock = Random.Range(0, 12);

                net_BlockSpawner.blockSpawner.NewBlock(netId , net_BlockSpawner.blockSpawner.currentBlock);

                net_Predictor.predictor.start = true;
                net_BlockSpawner.blockSpawner.first = true;
            }
            else 
            {
                net_BlockSpawner.blockSpawner.currentBlock = net_BlockSpawner.blockSpawner.secondComingBlock;
                net_BlockSpawner.blockSpawner.secondComingBlock = net_BlockSpawner.blockSpawner.thirdComingBlock;
                net_BlockSpawner.blockSpawner.thirdComingBlock = Random.Range(0, 12);

                net_BlockSpawner.blockSpawner.NewBlock(netId, net_BlockSpawner.blockSpawner.currentBlock);
            }
        }
        else 
        {
            if (net_BlockSpawner2.blockSpawner2.first == false)
            {
                net_BlockSpawner2.blockSpawner2.currentBlock = Random.Range(0, 12);
                net_BlockSpawner2.blockSpawner2.secondComingBlock = Random.Range(0, 12);
                net_BlockSpawner.blockSpawner.thirdComingBlock = Random.Range(0, 12);

                net_BlockSpawner2.blockSpawner2.NewBlock(netId, net_BlockSpawner2.blockSpawner2.currentBlock);
                net_Predictor.predictor.start = true;
                net_BlockSpawner2.blockSpawner2.first = true;
            }
            else
            {
                net_BlockSpawner2.blockSpawner2.currentBlock = net_BlockSpawner2.blockSpawner2.secondComingBlock;
                net_BlockSpawner2.blockSpawner2.secondComingBlock = net_BlockSpawner2.blockSpawner2.thirdComingBlock;
                net_BlockSpawner2.blockSpawner2.thirdComingBlock = Random.Range(0, 12);

                net_BlockSpawner2.blockSpawner2.NewBlock(netId, net_BlockSpawner2.blockSpawner2.currentBlock);
            }
        }
    }

    bool firstTime;

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false) return;
        if (isReady == false) return;

        if(GameManager.singleton.currentGameState == GameState.Playing) 
        {
            if(GameManager.players.Count >= 2)
            {
                if (firstTime == true) return;
                firstTime = true;
                GameManager.singleton.exitButton.SetActive(false);
                NewBlock();
            }
        }
 
        if(GameManager.players.Count >= 2) 
        {
            GameManager.singleton.waitingForOtherPlayers.SetActive(false);
        }
        else
        {
            GameManager.singleton.waitingForOtherPlayers.SetActive(true);
        }
    }

    void OnDestroy()
    {
        GameManager.UnRegisterPlayer(netId);
        
    }
}
