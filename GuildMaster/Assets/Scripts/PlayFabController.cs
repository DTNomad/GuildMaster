using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayFabController : MonoBehaviour
{
    public static PlayFabController PFC;
    private string userEmail;
    private string userName;
    private string userPass;
    private string userPassConfirm;
    private string userId;
    private string userDisplayName;
    public GameObject startPanel, loginPanel, registerPanel, leaderboardPanel, rosterPanel, homePanel, hubPanel;
    [SerializeField]
    public TMP_InputField loginEmailInput, loginPasswordInput;
    public TMP_Text loginStatus, registerStatus, hubDisplayName;
    private float loginWaitTime = 1f;
    public bool doneLoading = false;

    public string leaderboardResultText = "";
    //temporary vars to load from playfab
    public int tempLevel = 0;
    public int tempGold = 0;

    private void OnEnable()
    {
        if (PlayFabController.PFC == null)
        {
            PlayFabController.PFC = this;
        }
        else
        {
            if (PlayFabController.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "62FF6"; // Please change this value to your own titleId from PlayFab Game Manager
        }
    }

    #region start

    public void OnClickStartLogin()
    {
        startPanel.SetActive(false);
        loginPanel.SetActive(true);
        LoadPlayerPrefLoginInfo();
    }

    public void OnClickStartRegister()
    {
        startPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnClickStartExit()
    {
        Application.Quit();
    }

    public void OnClickStartActivate()
    {
        leaderboardPanel.SetActive(false);
        startPanel.SetActive(true);

    }

    #endregion start

    #region login

    private void OnLoginSuccess(LoginResult result)
    {
        userId = result.PlayFabId;
        Debug.Log("Congratulations, your login attempt was successful!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        loginStatus.text = "Login Successful!";
        StartCoroutine(LoginWait());
    }

    private void OnLoginFailure(PlayFabError error)
    {
        // Debug.LogWarning("Something went wrong with your first API call.  :(");
        // Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        //loginStatus.text = error.GenerateErrorReport();
        loginStatus.text = "Incorrect login info or account does not exist.";
    }

    public void OnClickLogin()
    {
        //check for version mismatch

        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void OnClickLoginClear()
    {
        Debug.Log("CLEARED ALL PLAYERPREFS");
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("EMAIL");
        loginEmailInput.text = "";
        loginPasswordInput.text = "";
        loginStatus.text = "Cleared saved login data";
    }

    public void OnClickLoginBack()
    {
        loginPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    IEnumerator LoginWait()
    {
        yield return new WaitForSeconds(loginWaitTime);
        loginStatus.text = "";
        loginPanel.SetActive(false);
        hubPanel.SetActive(true);
        homePanel.SetActive(true);
        //leaderboardPanel.SetActive(true);
        //rosterPanel.SetActive(true);
        SetStats();
        LoadPlayerProfile();
    }

    #endregion login

    #region register
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = userName }, OnDisplaySuccess, OnUpdateFailure);
        //PlayFabClientAPI.AddOrUpdateContactEmail(new AddOrUpdateContactEmailRequest { EmailAddress = userEmail }, OnEmailSuccess, OnUpdateFailure);
        Debug.Log("Congratulations, your account creation was successul!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        registerStatus.text = "Successfully created account!";
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        string errorStr = error.Error.ToString();
        Debug.Log(errorStr);
        //Debug.LogError(error.GenerateErrorReport());
        //registerStatus.text = error.GenerateErrorReport();
        switch (errorStr)
        {
            case "EmailAddressNotAvailable":
                registerStatus.text = "Failed to register. Email address already in use.";
                break;
            case "UsernameNotAvailable":
                registerStatus.text = "Failed to register. Display name already in use.";
                break;
            default:
                registerStatus.text = error.GenerateErrorReport();
                break;
        }
        
    }

    private void OnDisplaySuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName);
        PlayFabClientAPI.AddOrUpdateContactEmail(new AddOrUpdateContactEmailRequest { EmailAddress = userEmail }, OnEmailSuccess, OnUpdateFailure);
    }

    private void OnEmailSuccess(AddOrUpdateContactEmailResult result)
    {
        Debug.Log(result.ToString());
    }

    public void OnUpdateFailure(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void OnClickRegister()
    {
        Debug.Log("ON CLICK REGISTER");
        Debug.Log("EMAIL: " + userEmail + "\nUSERNAME: " + userName + "\nPASSWORD: " + userPass);

        char[] numberArray = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        string errorCriteriaMessage = "Password must be have 6-20 characters, 1+ numerical character, and no whitespaces.";
        string errorMismatchMessage = "Passwords do not match.";
        int numericalCheck = userPass.IndexOfAny(numberArray);

        //Debug.Log(numericalCheck);

        //criteria check
        if (userPass.Length < 6 || userPass.Length > 20 || numericalCheck < 0)
        {
            registerStatus.text = errorCriteriaMessage;
        }
        else if (userPass != userPassConfirm)
        {
            registerStatus.text = errorMismatchMessage;
        }
        else
        {
            var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPass, Username = userName };
            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        }
    }

    public void OnClickRegisterBack()
    {
        registerPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    #endregion register

    #region getset
    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserName(string nameIn)
    {
        userName = nameIn;
    }

    public void GetUserPassword(string passIn)
    {
        userPass = passIn;
    }

    public void GetUserPasswordConfirm(string passIn)
    {
        userPassConfirm = passIn;
    }
    #endregion getset

    private void LoadPlayerPrefLoginInfo()
    {
        if (PlayerPrefs.HasKey("EMAIL"))
        {
            //Debug.Log("IF");
            userEmail = PlayerPrefs.GetString("EMAIL");
            //userPass = PlayerPrefs.GetString("PASSWORD");
            //Debug.Log(userEmail);
            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
            //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            loginEmailInput.text = userEmail;
            loginPasswordInput.text = "";
        }
        else
        {
            //Debug.Log("ELSE");
        }
    }

    

    #region PlayerStats

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "Level", Value = 1 },
                new StatisticUpdate { StatisticName = "Gold", Value = 0 },
            }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    private void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch(eachStat.StatisticName)
            {
                case "Level":
                    tempLevel = eachStat.Value;
                    break;
                case "Gold":
                    tempGold = eachStat.Value;
                    break;
            }
        }
    }

    #endregion PlayerStats

    #region leaderboard

    public void GetLevelLeaderboard()
    {
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Level", MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLevelLeaderboard, OnUpdateFailure);
    }

    public void GetGoldLeaderboard()
    {
        var request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "Gold", MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(request, OnGetGoldLeaderboard, OnUpdateFailure);
    }

    public void OnGetLevelLeaderboard(GetLeaderboardResult result)
    {
        //LAZY FIX, FORMAT STRING OR SEPARATE RANK,VALUE,NAME INTO DIFFERENT COLUMN TEXT ELEMENTS

        //leaderboardResultText = System.String.Format("{0,-4}{1,-20}{2,-5}\n", "Rank", "Name", "Level");
        leaderboardResultText = "Rank\tLevel\tName\n";
        int rankNum = 1;
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
            leaderboardResultText += rankNum + "\t" + player.StatValue + "\t" + player.DisplayName + "\n";
            //leaderboardResultText += System.String.Format("{0,-4}{1,-20}{2,-5}\n", rankNum, player.DisplayName, player.StatValue);
            rankNum++;
        }
    }

    public void OnGetGoldLeaderboard(GetLeaderboardResult result)
    {
        //LAZY FIX, FORMAT STRING OR SEPARATE RANK,VALUE,NAME INTO DIFFERENT COLUMN TEXT ELEMENTS

        //leaderboardResultText = System.String.Format("{0,-4}{1,-20}{2,-5}\n", "Rank", "Name", "Level");
        leaderboardResultText = "Rank\tGold\tName\n";
        int rankNum = 1;
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            Debug.Log(player.DisplayName + ": " + player.StatValue);
            leaderboardResultText += rankNum + "\t" + player.StatValue + "\t" + player.DisplayName + "\n";
            //leaderboardResultText += System.String.Format("{0,-4}{1,-20}{2,-5}\n", rankNum, player.DisplayName, player.StatValue);
            rankNum++;
        }
    }

    public string GetLeaderboardText()
    {
        return leaderboardResultText;
    }

    

    #endregion leaderboard

    #region inventory

    public void TestFunction()
    {
        
    }

    #endregion inventory

    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest() { PlayFabId = userId, Keys = null }, OnGetDataSuccess, OnUpdateFailure);
    }

    private void OnGetDataSuccess(GetUserDataResult result)
    {
        if(result.Data == null)
        {
            Debug.Log("no user data found");
        }
        else
        {
            Debug.Log(result.Data["test"].Value);
        }
    }

    public void SetUserData(string keyName, string userData)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {keyName, userData}
            }
        }, OnSetDataSuccess, OnUpdateFailure);
    }

    public void OnSetDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Result data ver: " + result.DataVersion);
    }



    public void SetUserRosterData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {"ROSTER", Roster.PR.SaveRosterToJSON()}
            }
        }, OnSetDataSuccess, OnUpdateFailure);
    }

    public void GetUserRosterData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = userId,
            Keys = new List<string> { "ROSTER" }
        },
        OnGetRosterDataSuccess,
        OnUpdateFailure);
    }

    private void OnGetRosterDataSuccess(GetUserDataResult result)
    {
        if (result.Data == null)
        {
            Debug.Log("no roster data found");
        }
        else
        {
            //Debug.Log(result.Data["ROSTER"].Value);
            Roster.PR.LoadRosterFromJSON(result.Data["ROSTER"].Value);
            Roster.PR.PrintRosterArray();
            //Debug.Log(result.Data["ROSTER1"].Value + "\n"
            //    + result.Data["ROSTER2"].Value + "\n"
            //    + result.Data["ROSTER3"].Value + "\n"
            //    + result.Data["ROSTER4"].Value + "\n"
            //    + result.Data["ROSTER5"].Value + "\n"
            //    + result.Data["ROSTER6"].Value + "\n"
            //    + result.Data["ROSTER7"].Value + "\n"
            //    + result.Data["ROSTER8"].Value + "\n"
            //    + result.Data["ROSTER9"].Value + "\n"
            //    + result.Data["ROSTER10"].Value + "\n");
        }
    }

    #region hub

    //public void OnClickHubLeaderboard()
    //{
    //    hubPanel.SetActive(false);
    //    leaderboardPanel.SetActive(true);
    //}

    //public void OnClickHubGuild()
    //{
    //    hubPanel.SetActive(false);
    //    rosterPanel.SetActive(true);
    //}

    public void LoadPlayerProfile()
    {
        var request = new GetPlayerProfileRequest { PlayFabId = userId };
        PlayFabClientAPI.GetPlayerProfile(request, OnLoadPlayerProfileSuccess, OnUpdateFailure);
    }

    private void OnLoadPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        if (result == null)
        {
            Debug.Log("no player profile found");
        }
        else
        {
            userDisplayName = result.PlayerProfile.DisplayName;
            hubDisplayName.text = userDisplayName;
        }
    }

    #endregion hub
}   