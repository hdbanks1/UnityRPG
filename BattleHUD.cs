using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Slider manaSlider;
    public Text hpText;
    public Text manaText;
    public Image playerIcon;

    public GameObject atkIcon;
    public GameObject defIcon;
    public GameObject debuffIcon;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl" + unit.unitLevel;
        hpSlider.maxValue = unit.currentHp;
        hpSlider.value = unit.currentHp;
        manaSlider.maxValue = unit.currentMana;
        manaSlider.value = unit.currentMana;
        hpText.text = unit.currentHp.ToString();
        manaText.text = unit.currentMana.ToString();
        
    }
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
        hpText.text = hp.ToString();
    }
    public void SetMana(int mana)
    {
        manaSlider.value = mana;
        manaText.text = mana.ToString();
    }
    public void GiveExperience(int xp)
    {
        xp = 10;
    }

}
