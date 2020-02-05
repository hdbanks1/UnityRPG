using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    Unit playerUnit;
    BattleHUD playerHUD;

    public Text playerName;
    public Text PlayerLevel;
    public Text pLevelText;
    public Text ToNextLevel;
    public Text hpText;
    public Text mpText;
    public Slider hpSlider;
    public Slider mpSlider;
    public Text currencyText;

    // Start is called before the first frame update
    void Awake()
    {
        playerName.text = playerUnit.unitName;
        PlayerLevel.text = playerUnit.unitLevel.ToString() ;
        hpText.text = playerUnit.currentHp.ToString() + "/" + playerUnit.MaxHp.ToString();
        mpText.text = playerUnit.currentMana.ToString() + "/" + playerUnit.MaxMana.ToString();
        ToNextLevel.text = playerUnit.XpToLevel.ToString();
        hpSlider.value = playerUnit.currentHp;
        mpSlider.value = playerUnit.currentMana;
        currencyText.text = "$" + playerUnit.Currency.ToString();
    }
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
