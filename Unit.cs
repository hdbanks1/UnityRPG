using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int agility;
    public int defense;

    public int damage;
    public int skillDamage;
    Skill Skill;
    public int MaxHp;
    public int currentHp;
     public Animator anim;
    public int MaxMana;
    public int currentMana;
    public int ExpGiven;
    public int XpToLevel;
    public int TotalXp;
    public int Currency;


    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        XpToLevel = 6;
    }
    public void GiveExperience(int amount)
    {
        ExpGiven += amount;
        if (ExpGiven>= XpToLevel)
             unitLevel++;
       
    }
   
    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0)
            return true;
        else
            return false;
    }
    public void Heal(int amount)
    {
        currentHp += amount;
        if (currentHp >= MaxHp)
            currentHp = MaxHp;
        currentMana -= amount/3;
        
        if (currentMana <= 0)
        {
            return ;
        }
    }
    
}
