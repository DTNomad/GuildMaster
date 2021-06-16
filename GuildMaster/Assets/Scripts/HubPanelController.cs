using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HubPanelController : MonoBehaviour
{
    public GameObject hubPanel, homePanel, adventurePanel, guildPanel, blacksmithPanel, auctionPanel, leaderboardPanel, optionsPanel;
    public TextMeshProUGUI clockDateText, clockTimeText, goldText, guildLevelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClock();
        ReloadHubData();
    }

    private void ReloadHubData()
    {
        goldText.text = 1 + "G";
        guildLevelText.text = "Guild Level: " + 1;
    }

    private void UpdateClock()
    {
        DateTime dt = DateTime.Now;
        string date = dt.ToString("MM-dd-yyyy");
        string time = dt.ToString("HH:mm:ss");
        clockDateText.text = date;
        clockTimeText.text = time;
    }

    public void DeactivateAllPanels()
    {
        homePanel.SetActive(false);
        adventurePanel.SetActive(false);
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

    public void OnClickAdventurePanel()
    {
        DeactivateAllPanels();
        adventurePanel.SetActive(true);
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
