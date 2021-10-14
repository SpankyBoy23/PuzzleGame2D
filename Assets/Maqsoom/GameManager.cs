using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;

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

    public GameObject waitingForOtherPlayers;

    public static Dictionary<uint, Player> players = new Dictionary<uint, Player>();

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
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
        if(newValue != -1)
        FindObjectOfType<EnvironmentManager>().environments[a].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(decide == true) 
        {
            if (net_CharacterManager.Singleton.first.hasAuthority) 
            {
                net_CharacterManager.Singleton.first.animator.SetBool("Walk", false);
            }
            if (net_CharacterManager.Singleton.second.hasAuthority)
            {
                net_CharacterManager.Singleton.second.animator.SetBool("Walk", false);
            }
        }

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
        decide = true;

        winOrLoseObj.SetActive(true);

        uint loseId = 0;
        uint winId = 0;

        Player[] players = GetAllPlayers();

        foreach(var player in players) 
        {
            if(player.netId == netId) 
            {
                loseId = netId;
            }
            else 
            {
                winId = player.netId;
            }
        }

        if(NetworkClient.localPlayer.netId == netId) 
        {
            i.sprite = loseSprite;
        }
        else 
        {
            i.sprite = winSprite;
        }

        if(net_CharacterManager.Singleton.first.playerId == winId) 
        {
            net_CharacterManager.Singleton.first.animator.SetBool("Win", true);
        }
        else 
        {
            net_CharacterManager.Singleton.first.animator.SetBool("Lose", true);
        }

        if (net_CharacterManager.Singleton.second.playerId == winId)
        {
            net_CharacterManager.Singleton.second.animator.SetBool("Win", true);
        }
        else
        {
            net_CharacterManager.Singleton.second.animator.SetBool("Lose", true);
        }
    }

    [Server]
    void ProcessGameStateOnServer() 
    {
        if(mapId == -1) 
        {
            mapId = Random.Range(0, FindObjectOfType<EnvironmentManager>().environments.Length);
        }

        if(currentGameState == GameState.Waiting) 
        {
         //   statusText.text = $"Waiting for other players";

            if(NetworkServer.connections.Count == 2) 
            {
               
                currentGameState = GameState.Starting;


              //  RpcStartMatch();
            }
        }
        else
        if(currentGameState == GameState.Starting) 
        {
            currentTimer += Time.deltaTime;

            if(currentTimer >= 2f) 
            {
                currentGameState = GameState.Playing;
            }
        }
    }

    [ClientRpc]
    void RpcStartMatch()
    {


        Player[] players = GetAllPlayers();

      /*  foreach(var player in players) 
        {
            if (player.hasAuthority)
            {
                player.NewBlock();
            }
        }*/
    }

    public static void RegisterPlayer(Player player , uint netId) 
    {
        players.Add(netId, player);
    }

    public static void UnRegisterPlayer(uint netId) 
    {
        players.Remove(netId);
    }

    public static Player[] GetAllPlayers() 
    {
        return players.Values.ToArray();
    }

    private void OnDisable()
    {
        firstPlayer = null;
        currentGameState = GameState.Waiting;
    }
}

public enum GameState 
{
    Waiting,
    Starting,
    Playing,
    Ending
}
