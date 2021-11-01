using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PowerBlock : NetworkBehaviour
{
    public bool isFallen = false;
    public bool breakNow = false;
    public TeterminosPlayer2 tP;
    int roundedX, roundedY;


    void Update()
    {
        if (hasAuthority == false) return;
        if (isFallen)
        {
            roundedX = Mathf.RoundToInt(transform.position.x);
            roundedY = Mathf.RoundToInt(transform.position.y);

            // X 0 , y +1
            if (roundedY < 11)
            {
                Function(0, 1);
            }


            // X 0 , y -1
            if (roundedY > 0)
            {
                Function(0, -1);
            }


            // X -1 , y 0
            if (roundedX > 0)
            {
                Function(-1, 0);
            }



            // X +1 , y 0
            if (roundedX < 5)
            {
                Function(1, 0);
            }

        }
    }

    [Command]
    void CmdFunction(GameObject go, uint netId)
    {
        if (GameManager.singleton.firstPlayer.netId == netId)
        {
            net_LogicManager.intance.first = false;
        }
        else
        {
            net_LogicManager.intance.first = true;

        }

        if (!net_LogicManager.intance.objectList.Contains(go))
            net_LogicManager.intance.objectList.Add(go);

        RpcFunction();
    }

    [ClientRpc]
    void RpcFunction()
    {
        if (hasAuthority == false) return;

        if (destroyed == true) return;
        destroyed = true;

        if (net_CharacterManager.Singleton.first.hasAuthority == true)
        {
            net_CharacterManager.Singleton.first.Walk();
        }
        else
        {
            net_CharacterManager.Singleton.second.Walk();
        }
    }

    public bool destroyed;

    void Function(int x, int y)
    {
        if (net_BlockMovement.grid2[roundedX + x, roundedY + y] != null)
        {
            Debug.Log("working power");
            if (net_BlockMovement.grid2[roundedX + x, roundedY + y].tag == this.tag)
            {
                //  Debug.Log((TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminosPlayer2>().blockColor == tP.blockColor) +":" + tP.blockColor);
                this.breakNow = true;
                if (net_BlockMovement.grid2[roundedX + x, roundedY + y].GetComponent<SimpleBlock>() != null)
                    net_BlockMovement.grid2[roundedX + x, roundedY + y].GetComponent<SimpleBlock>().breakNow = true;
                //TeterminosPlayer2.grid2[roundedX+x, roundedY + y].GetComponent<SpriteRenderer>().color = Color.red;
                if (!net_LogicManager.intance.objectList.Contains(net_BlockMovement.grid2[roundedX, roundedY].gameObject))
                {
                    CmdFunction(net_BlockMovement.grid2[roundedX, roundedY].gameObject , NetworkClient.localPlayer.netId);
                }

                //    GetComponent<SpriteRenderer>().color = Color.red;
                //  Destroy(this.gameObject, 2f);
                // Destroy(TeterminosPlayer2.grid2[roundedX+x, roundedY + y].gameObject,2f);

            }
        }
    }
}

