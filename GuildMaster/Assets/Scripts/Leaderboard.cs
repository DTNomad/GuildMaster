using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
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
        //0.1f is too short for some
        yield return new WaitForSeconds(0.5f);
        leaderboardText.text = PlayFabController.PFC.GetLeaderboardText();
        //string rawText = PlayFabController.PFC.GetLeaderboardText();
        //rankList = rawText.Split(',');
        //leaderboardText.text = ("{0,2}{1,20}{2,3}", rankList[0], rankList[1], rankList[2]);
    }
}
