using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    public List<Unit> unitDB = new List<Unit>();

    public void BuildUnitDatabase()
    {
        unitDB = new List<Unit>()
        {
            new Unit(1, "test", "Swordsman", 1, 0, 10, 20, 20, 10, 10, 3, 3, 3, 3, 0.5f),
            new Unit(1, "test", "Magician", 1, 0, 10, 20, 20, 10, 10, 3, 3, 3, 3, 0.5f),
            new Unit(1, "test", "Archer", 1, 0, 10, 20, 20, 10, 10, 3, 3, 3, 3, 0.5f),
            new Unit(1, "test", "Rogue", 1, 0, 10, 20, 20, 10, 10, 3, 3, 3, 3, 0.5f),
            new Unit(1, "test", "Cleric", 1, 0, 10, 20, 20, 10, 10, 3, 3, 3, 3, 0.5f),
        };
    }
}
