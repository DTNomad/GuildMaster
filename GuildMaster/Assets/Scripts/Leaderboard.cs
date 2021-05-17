using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public TMP_Dropdown leaderboardDropdown;
    string[] rankList;

    //reset default dropdown value on logout and relogin (make reset function for UGUI and vars), DO THIS FOR ALL CHANGED VALUES OR JUST FORCE CLOSE GAMES INSTEAD OF LOGOUT

    public void HandleLeaderboardDropdown(int val)
    {
        if(val == 0)
        {
            leaderboardText.text = "";
        }
        else if(val == 1)
        {
            PlayFabController.PFC.GetLevelLeaderboard();
            StartCoroutine(GetText());
        }
        else if(val == 2)
        {
            PlayFabController.PFC.GetGoldLeaderboard();
            StartCoroutine(GetText());
        }
        else
        {
            Debug.LogError("ERROR IN DROPDOWN");
        }
    }

    IEnumerator GetText()
    {
        yield return new WaitForSeconds(0.5f);
        leaderboardText.text = PlayFabController.PFC.GetLeaderboardText();
    }

    public void OnClickLeaderboardReload()
    {
        switch(leaderboardDropdown.value)
        {
            case 0:
                leaderboardText.text = "";
                break;
            case 1:
                Debug.Log("1");
                PlayFabController.PFC.GetLevelLeaderboard();
                StartCoroutine(GetText());
                break;
            case 2:
                Debug.Log("2");
                PlayFabController.PFC.GetGoldLeaderboard();
                StartCoroutine(GetText());
                break;
            default:
                Debug.Log("Error in reload leaderboard");
                break;
        }
    }
}
