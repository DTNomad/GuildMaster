using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject startPanel, loginPanel, registerPanel, leaderboardPanel;

    public void OnClickViewLeaderboard()
    {
        //StartCoroutine(NextScreen());
        leaderboardPanel.SetActive(true);
    }

    IEnumerator NextScreen()
    {
        transition.SetTrigger("TransitionStart");
        Debug.Log("TRIGGER");
        yield return new WaitForSeconds(transitionTime);
        leaderboardPanel.SetActive(true);
    }
}
