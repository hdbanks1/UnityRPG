using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    BattleSystem BattleSystem;
    Unit playerUnit;
    Unit enemyUnit;
    public Text skillText;
    public string skillName;
    public int skillCost;
    public int skillDamage;
    GameObject SkillList;
    public Image skillIcon;


    // Start is called before the first frame update
    public void Start()
    {
        
        skillText.text = skillName+ "  " + skillCost;
    }
    IEnumerator Bash()
    {
        BattleSystem = GetComponent<BattleSystem>();
        enemyUnit.TakeDamage(playerUnit.damage+3);
        StartCoroutine(PlayerAttack());
        
        yield return new WaitForSeconds(1f);
        SkillList.SetActive(false);
    }

    private string PlayerAttack()
    {
        throw new NotImplementedException();
    }
}
