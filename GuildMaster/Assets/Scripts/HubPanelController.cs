using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPanelController : MonoBehaviour
{
    public GameObject hubPanel, homePanel, battlePanel, guildPanel, blacksmithPanel, auctionPanel, leaderboardPanel, optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateAllPanels()
    {
        homePanel.SetActive(false);
        //battlePanel.SetActive(false);
        guildPanel.SetActive(false);
        //blacksmithPanel.SetActive(false);
        //auctionPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        //optionsPanel.SetActive(false);
    }

    public void OnClickHomePanel()
    {
        DeactivateAllPanels();
        homePanel.SetActive(true);
    }

    public void OnClickGuildPanel()
    {
        DeactivateAllPanels();
        guildPanel.SetActive(true);
    }

    public void OnClickLeaderboardPanel()
    {
        DeactivateAllPanels();
        leaderboardPanel.SetActive(true);
    }
}
