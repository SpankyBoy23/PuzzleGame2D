using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
    public GameState currentGameState;

    public Text statusText;

    public float currentTimer;
    public float maxTimeToStart;

    public Player firstPlayer;

    public static GameManager singleton;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer == false) return;

        ProcessGameStateOnServer();
    }

    [Server]
    void ProcessGameStateOnServer() 
    {
        if(currentGameState == GameState.Waiting) 
        {
            statusText.text = $"Waiting for other players";

            if(NetworkServer.connections.Count == 2) 
            {
                Player[] players = (Player[])FindObjectsOfType(typeof(Player));
              

                /*if(players[0].isReady == true && players[1].isReady == true) 
                {
                    currentGameState = GameState.Starting;
                    RpcStartMatch();
                }*/
            }
        }
        else
        if(currentGameState == GameState.Starting) 
        {
            currentTimer += Time.deltaTime;

            if(currentTimer >= 4f) 
            {
                currentGameState = GameState.Playing;
            }
        }
    }

    [ClientRpc]
    void RpcStartMatch()
    {
        Player[] players = (Player[])FindObjectsOfType(typeof(Player));
        statusText.text = $"{players[0].username} vs {players[1].username}";
    }
}

public enum GameState 
{
    Waiting,
    Starting,
    Playing,
    Ending
}
