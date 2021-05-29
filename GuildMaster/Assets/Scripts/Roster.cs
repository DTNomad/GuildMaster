using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Roster : MonoBehaviour
{
    //VARS
    public static Roster PR;

    public Unit[] unitRoster = new Unit[50];
    public bool[] unlockedRoster = new bool[50];
    private int unlockedRosterNum;

    string tempJson;

    [SerializeField]
    public TMP_Text RosterText;


    //FUNCTS
    private void OnEnable()
    {
        if (Roster.PR == null)
        {
            Roster.PR = this;
        }
        else
        {
            if (Roster.PR != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void initializeUnlockedRoster()
    {
        unlockedRosterNum = 10;
        for(int i=0; i<50; i++)
        {
            if(i < unlockedRosterNum)
            {
                unlockedRoster[i] = true;
            }
            else
            {
                unlockedRoster[i] = false;
            }
        }
        Debug.Log("Initialized unlocked roster");
    }

    private int FindNextOpenIndex()
    {
        //int index = Array.FindIndex(unitRoster, i => i == null);
        for(int i=0; i<unlockedRosterNum; i++)
        {
            if((unitRoster[i] == null || unitRoster[i].GetUnitClass() == "") && unlockedRoster[i] == true)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddUnitToRoster(Unit u)
    {
        int addToRosterIndex = FindNextOpenIndex();
        if(addToRosterIndex == -1)
        {
            Debug.Log("Roster is full");
        }
        else
        {
            unitRoster[addToRosterIndex] = u;
        }
    }

    public void PrintRosterArray()
    {
        RosterText.text = "";
        for (int i=0; i<unlockedRosterNum; i++)
        {
            if(unitRoster[i] != null && unitRoster[i].GetUnitClass() != "")
            {
                RosterText.text += unitRoster[i].GetUnitClass() + ", ";
            }
            else
            {
                RosterText.text += "empty, ";
            }
        }
        //Debug.Log(RosterText.text);
    }

    public string SaveRosterToJSON()
    {
        SaveObject so = new SaveObject { saveUnitRoster = unitRoster, saveUnlockedRoster = unlockedRoster, saveUnlockedRosterNum = unlockedRosterNum };
        string jsonData = JsonUtility.ToJson(so);

        //temp
        //tempJson = jsonData;

        return jsonData;
    }

    public void LoadRosterFromJSON(string jsonData)
    {
        //temp (use string parameter to load)
        SaveObject so = JsonUtility.FromJson<SaveObject>(jsonData);
        unitRoster = so.saveUnitRoster;
        unlockedRoster = so.saveUnlockedRoster;
        unlockedRosterNum = so.saveUnlockedRosterNum;
    }

    public void AddTestUnit()
    {
        int addToRosterIndex = FindNextOpenIndex();
        if (addToRosterIndex == -1)
        {
            Debug.Log("Roster is full");
        }
        else
        {
            //unitRoster[addToRosterIndex] = new Unit("class", 1, 5, 10);
        }
    }

    public int GetRosterUnitRarity(int slotNum)
    {
        if (slotNum >= unlockedRosterNum)
        {
            return -1;
        }
        if ((unitRoster[slotNum] == null || unitRoster[slotNum].GetUnitClass() == "") && unlockedRoster[slotNum] == true)
        {
            return unitRoster[slotNum].GetUnitRarity();
        }
        return -1;
    }

    [Serializable]
    public struct SaveObject
    {
        public Unit[] saveUnitRoster;
        public bool[] saveUnlockedRoster;
        public int saveUnlockedRosterNum;
    }
}
