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
    public GameObject startPanel, loginPanel, registerPanel, leaderboardPanel;
    [SerializeField]
    public TMP_InputField loginEmailInput, loginPasswordInput;
    public TMP_Text loginStatus, registerStatus;
    private float loginWaitTime = 1f;

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
        // var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        // PlayerPrefs.DeleteAll();

        // if (PlayerPrefs.HasKey("EMAIL"))
        // {
        //     Debug.Log("IF");
        //     userEmail = PlayerPrefs.GetString("EMAIL");
        //     userPass = PlayerPrefs.GetString("PASSWORD");
        //     Debug.Log(userEmail + " " + userPass);
        //     //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
        //     //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        // }
        // else
        // {
        //     Debug.Log("ELSE");
        // }

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
        loginStatus.text = error.GenerateErrorReport();
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
        leaderboardPanel.SetActive(true);
        SetStats();
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
        Debug.LogError(error.GenerateErrorReport());
        registerStatus.text = error.GenerateErrorReport();
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

    private void OnUpdateFailure(PlayFabError error)
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
            Debug.Log(userEmail);
            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPass };
            //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            loginEmailInput.text = userEmail;
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

}