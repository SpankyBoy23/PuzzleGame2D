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

    [SyncVar(hook = nameof(OnNameChange))]
    public int mapId;

    public static GameManager singleton;

    public GameObject winOrLoseObj;
    public Image i;
    public Sprite loseSprite;
    public Sprite winSprite;
    public bool decide;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;

        if(isServer == true)
        {
            mapId = Random.Range(0, FindObjectOfType<EnvironmentManager>().environments.Length);
        }
    }

    void OnNameChange(int oldValue , int newValue) 
    {
        int a = newValue;

        if (a < 2)
        {
            MusicManager.instance.Play("1");
        }
        else if (a == 2)
        {
            MusicManager.instance.Play("2");
        }
        else
        {
            MusicManager.instance.Play("3");
        }

        FindObjectOfType<EnvironmentManager>().environments[a].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer == false) return;

        ProcessGameStateOnServer();
    }

    [Command(requiresAuthority = false)]
    public void CmdLoseGame(uint netId) 
    {
        if (decide == true) return;
        decide = true;

        RpcLoseGame(netId);
    }
    [ClientRpc]
    void RpcLoseGame(uint netId) 
    {
        winOrLoseObj.SetActive(true);

        if(NetworkClient.localPlayer.netId == netId) 
        {
            i.sprite = loseSprite;
        }
        else 
        {
            i.sprite = winSprite;
        }
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

    private void OnDisable()
    {
        firstPlayer = null;
    }
}

public enum GameState 
{
    Waiting,
    Starting,
    Playing,
    Ending
}
