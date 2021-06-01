using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public int unitRarity;
    public string unitName;
    public string unitClass;
    public int unitLevel;
    public int unitCurExp;
    public int unitExpToNextLevel;
    public int unitCurHp;
    public int unitMaxHp;
    public int unitCurMp;
    public int unitMaxMp;
    public int unitAtk;
    public int unitMatk;
    public int unitDef;
    public int unitMdef;
    public float unitSpd;
    //implement exp/stat object
    //normalize all stats/exp temporarily
    //influence from pkmn

    public Unit(int newRarity, string newUnitName, string newUnitClass, int newLevel, int newCurExp, int newExpToNextLevel, int newUnitCurHp, int newUnitMaxHp, int newUnitCurMp, int newUnitMaxMp, int newAtk, int newMatk, int newDef, int newMdef, float newSpd)
    {
        unitRarity = newRarity;
        unitName = newUnitName;
        unitClass = newUnitClass;
        unitLevel = newLevel;
        unitCurExp = newCurExp;
        unitExpToNextLevel = newExpToNextLevel;
        unitCurHp = newUnitCurHp;
        unitMaxHp = newUnitMaxHp;
        unitCurMp = newUnitCurMp;
        unitMaxMp = newUnitMaxMp;
        unitAtk = newAtk;
        unitMatk = newMatk;
        unitDef = newDef;
        unitMdef = newMdef;
        unitSpd = newSpd;
    }

    public Unit(int newRarity, string newUnitClass)
    {
        unitRarity = newRarity;
        unitClass = newUnitClass;
        if(unitClass == "Swordsman")
        {
            if(unitRarity == 1)
            {

            }
            else if(unitRarity == 2)
            {

            }
            else
            {

            }
        }
        else if (unitClass == "Magician")
        {
            if (unitRarity == 1)
            {

            }
            else if (unitRarity == 2)
            {

            }
            else
            {

            }
        }
    }

    public string GetUnitClass()
    {
        return unitClass;
    }

    public int GetUnitRarity()
    {
        return unitRarity;
    }
}

[Serializable]
public class ExpTable
{
    public Dictionary<int, int> table;

    public ExpTable()
    {
        table = new Dictionary<int, int>();
        initialize();
    }

    private void initialize()
    {
        for(int i=1; i<=100; i++)
        {
            table.Add(i, i * 10);
        }
    }

    public int getExpToNextLevel(int unitLevel)
    {
        return table[unitLevel];
    }
}

