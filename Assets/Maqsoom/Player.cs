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
        NewBlock();
    }

    [ClientRpc]
    void RpcSetup(uint netId) 
    {
        if (isClient && isServer) return;

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
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority == false) return;

        username = PlayerPrefs.GetString("Username");

        CmdSetup(username);
    }

    public void NewBlock() 
    {
        if(GameManager.singleton.firstPlayer.netId == netId) 
        {
            net_BlockSpawner.blockSpawner.NewBlock(netId);
        }
        else 
        {
            net_BlockSpawner2.blockSpawner2.NewBlock(netId);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
