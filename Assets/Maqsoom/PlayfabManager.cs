using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    public string playerName;
    public Score currentScore;

    public TMP_InputField playerNameField;

    public TMP_Text playerNameText;
    public TMP_Text playerRankText;
    public TMP_Text playerWinsText;
    public TMP_Text playerLosesText;

    public TMP_Text firstRankText;
    public TMP_Text secondRankText;
    public TMP_Text thirdRankText;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerPrefs.GetString("playerName");
        playerNameField.text = PlayerPrefs.GetString("playerName");
        playerNameText.text = PlayerPrefs.GetString("playerName");

        Login();
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(playerNameField.text))
        { 
            if(playerName != playerNameField.text) 
            {
                playerName = playerNameField.text;
                playerNameText.text = playerNameField.text;
                PlayerPrefs.SetString("playerName", playerName);
            }
        }
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        SubmitPlayerName();
        GetLeaderboard();
        GetLeaderboardAroundPlayer();
        GetLeaderboardAroundPlayerLoses();

        Debug.Log("Account created successfully");
    }

    public void SubmitPlayerName() 
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = playerName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnPlayerNameDisplay, OnError);
    }

    void OnPlayerNameDisplay(UpdateUserTitleDisplayNameResult result) 
    {
        Debug.Log("PlayerName updated"  + result.DisplayName);
    }

    public void SendLeaderboard(Score score) 
    {
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

    public void GetLeaderboard() 
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "Wins",
            StartPosition = 0,
            MaxResultsCount = 3,
            ProfileConstraints = new PlayerProfileViewConstraints 
            {
                ShowDisplayName = true
            },
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result) 
    {
        firstRankText.text = result.Leaderboard[0].DisplayName;
        secondRankText.text = result.Leaderboard[1].DisplayName;
        thirdRankText.text = result.Leaderboard[2].DisplayName;
    }

    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Wins",
            MaxResultsCount = 1,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true
            },
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }


    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        /*foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + item.DisplayName + item.StatValue);
        }*/

        playerRankText.text = $"Rank                         {result.Leaderboard[0].Position+1}";
        playerWinsText.text = $"Win                        {result.Leaderboard[0].StatValue}";
    }

    public void GetLeaderboardAroundPlayerLoses()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Loses",
            MaxResultsCount = 1,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true
            },
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGetLosses, OnError);
    }

    void OnLeaderboardAroundPlayerGetLosses(GetLeaderboardAroundPlayerResult result)
    {
        playerLosesText.text = $"Lose                        {result.Leaderboard[0].StatValue}";
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
}

[System.Serializable]
public struct Score 
{
    public int wins;
    public int loses;
}