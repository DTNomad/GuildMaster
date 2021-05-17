using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public int unitRarity;
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

    public Unit(int newRarity, string newUnitClass, int newLevel, int newCurExp, int newExpToNextLevel, int newUnitCurHp, int newUnitMaxHp, int newUnitCurMp, int newUnitMaxMp, int newAtk, int newMatk, int newDef, int newMdef, float newSpd)
    {
        unitRarity = newRarity;
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
}
