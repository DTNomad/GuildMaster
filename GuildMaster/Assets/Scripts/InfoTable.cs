using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTable : MonoBehaviour
{
    public static InfoTable IT;
    private List<ClassInfo> classTable;

    private void OnEnable()
    {
        if (InfoTable.IT == null)
        {
            InfoTable.IT = this;
        }
        else
        {
            if (InfoTable.IT != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeClassTable()
    {
        //swordsman 1-5
        classTable.Add(new ClassInfo(1, "Swordsman", 1));
        classTable.Add(new ClassInfo(2, "Knight", 2));
        classTable.Add(new ClassInfo(3, "Paladin", 3));
        classTable.Add(new ClassInfo(4, "Crusader", 2));
        classTable.Add(new ClassInfo(5, "Paladin", 3));
        //magician 6-10
        classTable.Add(new ClassInfo(6, "Magician", 1));
        classTable.Add(new ClassInfo(7, "Wizard", 2));
        classTable.Add(new ClassInfo(8, "Arcanist", 3));
        classTable.Add(new ClassInfo(9, "Sorceror", 2));
        classTable.Add(new ClassInfo(10, "???", 3));
        //archer 11-15
        classTable.Add(new ClassInfo(11, "Archer", 1));
        classTable.Add(new ClassInfo(12, "Hunter", 2));
        classTable.Add(new ClassInfo(13, "Sniper", 3));
        classTable.Add(new ClassInfo(14, "Ranger", 2));
        classTable.Add(new ClassInfo(15, "Pathfinder", 3));
        //rogue 16-20
        classTable.Add(new ClassInfo(16, "Rogue", 1));
        classTable.Add(new ClassInfo(17, "Bandit", 2));
        classTable.Add(new ClassInfo(18, "Assassin", 3));
        classTable.Add(new ClassInfo(19, "Trickster", 2));
        classTable.Add(new ClassInfo(20, "Bard", 3));
        //cleric 21-25
        classTable.Add(new ClassInfo(21, "Cleric", 1));
        classTable.Add(new ClassInfo(22, "Priest", 2));
        classTable.Add(new ClassInfo(23, "???", 3));
        classTable.Add(new ClassInfo(24, "Mystic", 2));
        classTable.Add(new ClassInfo(25, "Oracle", 3));
        //ninja 26-30
        classTable.Add(new ClassInfo(26, "Ninja", 1));
        classTable.Add(new ClassInfo(27, "Samurai", 2));
        classTable.Add(new ClassInfo(28, "Shogun", 3));
        classTable.Add(new ClassInfo(29, "Shinobi", 2));
        classTable.Add(new ClassInfo(30, "???", 3));
        //barbarian 31-35
        classTable.Add(new ClassInfo(31, "Barbarian", 1));
        classTable.Add(new ClassInfo(32, "Warlord", 2));
        classTable.Add(new ClassInfo(33, "Chieftain", 3));
        classTable.Add(new ClassInfo(34, "Gladiator", 2));
        classTable.Add(new ClassInfo(35, "Berserker", 3));
        //sage 36-40
        classTable.Add(new ClassInfo(36, "Sage", 1));
        classTable.Add(new ClassInfo(37, "Druid", 2));
        classTable.Add(new ClassInfo(38, "Beastmaster", 3));
        classTable.Add(new ClassInfo(39, "Alchemist", 2));
        classTable.Add(new ClassInfo(40, "Shaman", 3));
        //occultist 41-45
        classTable.Add(new ClassInfo(41, "Occultist", 1));
        classTable.Add(new ClassInfo(42, "Conjurer", 2));
        classTable.Add(new ClassInfo(43, "Warlock", 3));
        classTable.Add(new ClassInfo(44, "Necromancer", 2));
        classTable.Add(new ClassInfo(45, "Deathlord", 3));
        //elementalist 46-50
        classTable.Add(new ClassInfo(46, "Elementalist", 1));
        classTable.Add(new ClassInfo(47, "Pyromancer", 2));
        classTable.Add(new ClassInfo(48, "???", 3));
        classTable.Add(new ClassInfo(49, "Cryomancer", 2));
        classTable.Add(new ClassInfo(50, "???", 3));
    }

    public List<int> GetAvailableAscendanciesIds(ClassInfo unitClass)
    {
        List<int> classIndexes = new List<int>();
        if(unitClass.classId % 5 == 1) //base class returns first ascensions
        {
            classIndexes.Add(unitClass.classId + 1);
            classIndexes.Add(unitClass.classId + 3);
        }
        else if(unitClass.classId % 5 == 2 || unitClass.classId % 5 == 4) //first ascension returns following ascension
        {
            classIndexes.Add(unitClass.classId + 1);
        }
        return classIndexes; //id oob or already max ascension
    }

    public struct ClassInfo
    {
        public int classId; //unique id to quickly get/compare classes
        public string className; //use for class name display
        public int classTier; //1: base class, 2: first ascension, 3: second ascension

        public ClassInfo(int newClassId, string newClassName, int newClassTier)
        {
            classId = newClassId;
            className = newClassName;
            classTier = newClassTier;
        }

        public override string ToString() => $"({className}, tier:{classTier})";
    }
}
