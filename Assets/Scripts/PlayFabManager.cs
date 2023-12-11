using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public GameObject usernameWindow;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject canvasGame;
    [SerializeField] GameObject canvasLeaderboard;
    [SerializeField] GameObject rowPrefab;
    [SerializeField] Transform tableRows;
    // Start is called before the first frame update
    void Start()
    {
        usernameWindow.SetActive(false);
        canvasLeaderboard.SetActive(false);
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError); //anonymous account
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            Debug.Log(name);
        }
        if (name == null)
            usernameWindow.SetActive(true);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="PlatformScore",
                    Value=score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        //scoreText.text = "";
        foreach(Transform item in tableRows)
        {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, tableRows);
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
            TMP_Text[] texts = newRow.GetComponentsInChildren<TMP_Text>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            //scoreText.text += item.Position + " " + item.DisplayName + " " + item.StatValue+"\n";
        }
        canvasGame.SetActive(false);
        canvasLeaderboard.SetActive(true);
        //scoreText.gameObject.SetActive(true);
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = usernameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
    }
}