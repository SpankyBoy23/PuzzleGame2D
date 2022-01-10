using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

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
    public GameObject exitButton;

    public static Dictionary<uint, Player> players = new Dictionary<uint, Player>();

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
    }

    public bool mapSet;

    void OnNameChange(int oldValue , int newValue) 
    {
        int a = newValue;
        mapSet = true;

        if (mapSet == true) return;

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

            Score score = new Score
            {
                wins = PlayerPrefs.GetInt("Wins"),
                loses = PlayerPrefs.GetInt("Loses")
            };

            SendLeaderboard(score, true);
        }
        else 
        {
            i.sprite = winSprite;

            Score score = new Score
            {
                wins = PlayerPrefs.GetInt("Wins"),
                loses = PlayerPrefs.GetInt("Loses")
            };

            SendLeaderboard(score, false);
        }

        if (net_CharacterManager.Singleton.first.playerId == winId)
        {
            this.Wait(1, () => { net_CharacterManager.Singleton.first.chargeAttack = true; });

            this.Wait(1.5f, () => {  net_CharacterManager.Singleton.first.animator.Play("ChargeAttack");   });

            net_CharacterManager.Singleton.first.animator.SetBool("Win", true);
        }
        else 
        {
            this.Wait(1.5f, () => { net_CharacterManager.Singleton.first.animator.SetBool("Lose", true); });
            
        }

        if (net_CharacterManager.Singleton.second.playerId == winId)
        {
            net_CharacterManager.Singleton.second.chargeAttack = true;

            this.Wait(1, () => { net_CharacterManager.Singleton.second.animator.Play("ChargeAttack"); });

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
            Debug.LogError(mapId);
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

    public void ExitGame() 
    {
        if(NetworkServer.active && NetworkClient.active) 
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
        AdManager.instance.ShowInterstitial();
      // SceneManager.LoadScene("Menu");
    }

    public void SendLeaderboard(Score score , bool lose)
    {
        if (lose)
            score.loses++;
        else
            score.wins++;

        PlayerPrefs.SetInt("Wins", score.wins);
        PlayerPrefs.SetInt("Loses", score.loses);

        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = "Wins",
                    Value = score.wins
                },

                new StatisticUpdate()
                {
                    StatisticName = "Loses",
                    Value = score.loses
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sended");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
    public void showAd()
    {
      //  FindObjectOfType<AdManager>().ShowInterstitial();
    }
}

public enum GameState 
{
    Waiting,
    Starting,
    Playing,
    Ending
}
