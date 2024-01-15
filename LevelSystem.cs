using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField]
    public int level;
    public int experience;
    public int XpToNextLevel;
    public int maxHP;
    public int maxMP;
    public int currentHP;
    public int currentMP;

    bool canLevel;
   // public Unit unit;


    // Start is called before the first frame update
    public void Start()
    {
        //level = unit.unitLevel;
        level = 1;
        experience = 0;
        XpToNextLevel = 100;
        maxHP = 100;
        maxMP = 20;
        canLevel = true;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience <= XpToNextLevel)
        {
            //unit.unitLevel++;           
            experience -= XpToNextLevel;
        }

    }

    public void LevelUp()
    {
        if (canLevel)
        {


            level++;
            maxHP = maxHP += level;
            maxMP = maxMP += level;
            currentHP = maxHP;
            currentMP = maxMP;
            experience = 0;
        }
        if (level == 99)
        {
            canLevel = false;
        }
    }
    public void Update()
    {
        if (experience >= XpToNextLevel) {
            LevelUp();
        }
    }
}
