using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Roster : MonoBehaviour
{
    //VARS
    public static Roster PR;

    public Unit[] unitRoster = new Unit[50];
    public bool[] unlockedRoster = new bool[50];
    private int unlockedRosterNum;

    string tempJson;

    [SerializeField]
    public TMP_Text RosterText, unitLabel1, unitLabel2, unitLabel3, unitLabel4, unitLabel5, unitLabel6, unitLabel7, unitLabel8, unitLabel9, unitLabel10;
    public TMP_Text RosterButtonName1, RosterButtonName2, RosterButtonName3, RosterButtonName4, RosterButtonName5, RosterButtonName6, RosterButtonName7, RosterButtonName8, RosterButtonName9, RosterButtonName10;
    public TMP_Text RosterButtonLevel1, RosterButtonLevel2, RosterButtonLevel3, RosterButtonLevel4, RosterButtonLevel5, RosterButtonLevel6, RosterButtonLevel7, RosterButtonLevel8, RosterButtonLevel9, RosterButtonLevel10;
    public Button UnitButton1, UnitButton2, UnitButton3, UnitButton4, UnitButton5, UnitButton6, UnitButton7, UnitButton8, UnitButton9, UnitButton10;
    public Outline UnitButtonOutline1, UnitButtonOutline2, UnitButtonOutline3, UnitButtonOutline4, UnitButtonOutline5, UnitButtonOutline6, UnitButtonOutline7, UnitButtonOutline8, UnitButtonOutline9, UnitButtonOutline10;

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
        //RosterText.text = "";
        ClearAllUnitLabels();
        for (int i=0; i<unlockedRosterNum; i++)
        {
            if(unitRoster[i] != null && unitRoster[i].GetUnitClass() != "")
            {
                //RosterText.text += unitRoster[i].GetUnitClass() + ", ";
                UpdateUnitLabel(i+1, unitRoster[i].GetUnitName());
                UpdateUnitLabel(i+1, "\n" + unitRoster[i].GetUnitClass());
                UpdateUnitLabel(i+1, "\nLvl: " + unitRoster[i].GetUnitLevel());
                UpdateUnitLabel(i+1, "\n" + unitRoster[i].GetUnitRarity() + " stars");

                //temp to test button
                UpdateUnitButtonNameLabel(i + 1, unitRoster[i].GetUnitName());
                UpdateUnitButtonLevelLabel(i + 1, "Lvl: " + unitRoster[i].GetUnitLevel());
            }
            else
            {
                UpdateUnitLabel(i+1, "");
                //RosterText.text += "empty, ";
            }
        }
        //Debug.Log(RosterText.text);
    }

    public void ClearAllUnitLabels()
    {
        unitLabel1.text = "";
        unitLabel2.text = "";
        unitLabel3.text = "";
        unitLabel4.text = "";
        unitLabel5.text = "";
        unitLabel6.text = "";
        unitLabel7.text = "";
        unitLabel8.text = "";
        unitLabel9.text = "";
        unitLabel10.text = "";

        RosterButtonName1.text = "";
        RosterButtonLevel1.text = "";
        RosterButtonName2.text = "";
        RosterButtonLevel2.text = "";
        RosterButtonName3.text = "";
        RosterButtonLevel3.text = "";
        RosterButtonName4.text = "";
        RosterButtonLevel4.text = "";
        RosterButtonName5.text = "";
        RosterButtonLevel5.text = "";
        RosterButtonName6.text = "";
        RosterButtonLevel6.text = "";
        RosterButtonName7.text = "";
        RosterButtonLevel7.text = "";
        RosterButtonName8.text = "";
        RosterButtonLevel8.text = "";
        RosterButtonName9.text = "";
        RosterButtonLevel9.text = "";
        RosterButtonName10.text = "";
        RosterButtonLevel10.text = "";
    }

    public void ClearUnitLabel(int unitNum)
    {
        switch (unitNum)
        {
            case 1:
                unitLabel1.text = "";
                RosterButtonName1.text = "";
                RosterButtonLevel1.text = "";
                break;
            case 2:
                unitLabel2.text = "";
                break;
            case 3:
                unitLabel3.text = "";
                break;
            case 4:
                unitLabel4.text = "";
                break;
            case 5:
                unitLabel5.text = "";
                break;
            case 6:
                unitLabel6.text = "";
                break;
            case 7:
                unitLabel7.text = "";
                break;
            case 8:
                unitLabel8.text = "";
                break;
            case 9:
                unitLabel9.text = "";
                break;
            case 10:
                unitLabel10.text = "";
                break;
            default:
                Debug.Log("ERROR: Roster/ClearUnitLabel switch statement");
                break;
        }
    }

    public void UpdateUnitLabel(int unitNum, string newText)
    {
        switch (unitNum)
        {
            case 1:
                unitLabel1.text += newText;
                break;
            case 2:
                unitLabel2.text += newText;
                break;
            case 3:
                unitLabel3.text += newText;
                break;
            case 4:
                unitLabel4.text += newText;
                break;
            case 5:
                unitLabel5.text += newText;
                break;
            case 6:
                unitLabel6.text += newText;
                break;
            case 7:
                unitLabel7.text += newText;
                break;
            case 8:
                unitLabel8.text += newText;
                break;
            case 9:
                unitLabel9.text += newText;
                break;
            case 10:
                unitLabel10.text += newText;
                break;
            default:
                Debug.Log("ERROR: Roster/UpdateUnitLabel switch statement");
                break;
        }
    }


    //UPDATE NAME AND LABEL WHEN MORE BUTTONS ARE MADE*****
    public void UpdateUnitButtonNameLabel(int unitNum, string newText)
    {
        switch (unitNum)
        {
            case 1:
                RosterButtonName1.text += newText;
                break;
            case 2:
                RosterButtonName2.text += newText;
                break;
            case 3:
                RosterButtonName3.text += newText;
                break;
            case 4:
                RosterButtonName4.text += newText;
                break;
            case 5:
                RosterButtonName5.text += newText;
                break;
            case 6:
                RosterButtonName6.text += newText;
                break;
            case 7:
                RosterButtonName7.text += newText;
                break;
            case 8:
                RosterButtonName8.text += newText;
                break;
            case 9:
                RosterButtonName9.text += newText;
                break;
            case 10:
                RosterButtonName10.text += newText;
                break;
            default:
                Debug.Log("ERROR: Roster/UpdateUnitLabel switch statement");
                break;
        }
    }

    public void UpdateUnitButtonLevelLabel(int unitNum, string newText)
    {
        switch (unitNum)
        {
            case 1:
                RosterButtonLevel1.text += newText;
                break;
            case 2:
                RosterButtonLevel2.text += newText;
                break;
            case 3:
                RosterButtonLevel3.text += newText;
                break;
            case 4:
                RosterButtonLevel4.text += newText;
                break;
            case 5:
                RosterButtonLevel5.text += newText;
                break;
            case 6:
                RosterButtonLevel6.text += newText;
                break;
            case 7:
                RosterButtonLevel7.text += newText;
                break;
            case 8:
                RosterButtonLevel8.text += newText;
                break;
            case 9:
                RosterButtonLevel9.text += newText;
                break;
            case 10:
                RosterButtonLevel10.text += newText;
                break;
            default:
                Debug.Log("ERROR: Roster/UpdateUnitLabel switch statement");
                break;
        }
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

    public void TestButtonColor()
    {
        var tempColor = Color.red;
        tempColor.a = 0.25f;
        //UnitButton1.GetComponent<Image>().color = tempColor;

        UnitButtonOutline1.effectColor = tempColor;
    }

    [Serializable]
    public struct SaveObject
    {
        public Unit[] saveUnitRoster;
        public bool[] saveUnlockedRoster;
        public int saveUnlockedRosterNum;
    }
}
