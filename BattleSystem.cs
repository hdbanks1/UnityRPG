using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START,PLAYERTURN,TARGET,ENEMYTURN,WON,LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject enemyPrefab;
    public Transform playerSpace;
    public Transform playerSpace1;
    public Transform playerSpace2;

    public Transform enemySpace;
    public Transform enemySpace1;
    public Transform enemySpace2;
    public Text dialogueText;
    public GameObject skillMenu;
    
    public List<GameObject> enemies;
    Unit playerUnit;
    Unit playerUnit1;
    Unit playerUnit2;
    Unit enemyUnit;
    Unit enemyUnit1;
    Unit enemyUnit2;
    Skill Skill;
    Button skills;

    public BattleHUD playerHUD;
    public BattleHUD playerHUD1;
    public BattleHUD playerHUD2;
    public BattleHUD enemyHUD;
    //public BattleHUD enemyHUD1;
    //public BattleHUD enemyHUD2;

    public Camera PlayerCam;
    public Camera EnemyCam;
    public Camera Camera;

    int hitPercent;
    Animator anim;
    public GameObject missFx;
    public GameObject hitFx;
    public GameObject healFx;
    public GameObject buffFx;
    public GameObject debuffFx;
    int TurnCount;
    bool isBuffed;
    bool isDebuffed;
    public GameObject atkIcon;
    public GameObject defIcon;
    public GameObject debuffIcon;
    public GameObject AtkName;
    Text atkNameText;

    PlayerParty playerParty;
    public List<GameObject>players;
    Enemies enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        Skill = GetComponent<Skill>();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        skillMenu.SetActive(false);
        anim = GetComponent<Animator>();
        atkIcon.SetActive(false);
        defIcon.SetActive(false);
        debuffIcon.SetActive(false);
        
        
    }

    // Update is called once per frame
    public virtual IEnumerator SetupBattle()
    {
        //SpawnPlayers();
        //playerPrefab = playerParty.players[0];
        GameObject playerGO = Instantiate(playerPrefab, playerSpace);
       
        playerUnit = playerGO.GetComponent<Unit>();
        players.Add(playerGO);

        GameObject homie = Instantiate(playerPrefab1, playerSpace1);
        //playerPrefab1 = playerParty.players[1];
        playerUnit1 = homie.GetComponent<Unit>();
        players.Add(homie);
        GameObject homegirl = Instantiate(playerPrefab2, playerSpace2);
        //playerPrefab2 = playerParty.players[2];
        playerUnit2 = homegirl.GetComponent<Unit>();
        players.Add(homegirl);


        GameObject enemyGO = Instantiate(enemyPrefab, enemySpace);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemies.Add(enemyGO);


       GameObject enemy2 = Instantiate(enemies[0], enemySpace1);
        enemyUnit2 = enemy2.GetComponent<Unit>();

        GameObject enemy3 = Instantiate(enemies[0], enemySpace2);
        enemyUnit2 = enemy2.GetComponent<Unit>();



        dialogueText.text = "A wild " + enemyUnit.unitName + "Approaches";
        playerHUD.SetHUD(playerUnit);
        playerHUD1.SetHUD(playerUnit1);
        playerHUD2.SetHUD(playerUnit2);
        enemyHUD.SetHUD(enemyUnit);
        //enemyHUD1.SetHUD(enemyUnit1);
        //enemyHUD2.SetHUD(enemyUnit2);

        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        MainCameraView();
    }
    IEnumerator DeBuff()
    {

        if (playerUnit)
        {
            Instantiate(debuffFx, enemyUnit.transform);
            enemyUnit.damage /= 2;
            dialogueText.text = "Your strength has increased";
            TurnCount++; Debug.Log("turn count is" + TurnCount);

        }
        if(TurnCount==3)
        {
            StopCoroutine(DeBuff());
        }
        //if (enemyUnit)
        //{
        //    Instantiate(buffFx, playerUnit.transform);
        //   enemyUnit.damage++;
        //}
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator Buff()
    {
        
        if (playerUnit)
        {
            Instantiate(buffFx,playerUnit.transform);
            playerUnit.damage+=10;
            atkIcon.SetActive(true);
            dialogueText.text = "Your strength has increased";
            TurnCount = 3;
            TurnCount++; Debug.Log("turn count is" + TurnCount);
            skillMenu.SetActive(false);
            
        }
        if (TurnCount == 3)
        {
            StopCoroutine(DeBuff());
        }
        //if (enemyUnit)
        //{
        //    Instantiate(buffFx, playerUnit.transform);
        //   enemyUnit.damage++;
        //}
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    public void PlayerLevelUp()
    {
        if (enemyUnit.ExpGiven >= playerUnit.XpToLevel)
        {
            playerUnit.unitLevel++;
        }
    }
    IEnumerator PlayerHeal()
    {
        Instantiate(healFx,playerUnit.transform);
        
        playerUnit.Heal(15);
        playerHUD.SetHP(playerUnit.currentHp);
        dialogueText.text = "You feel renewed";
        playerHUD.SetMana(playerUnit.currentMana);

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.currentHp<=0;
        StartCoroutine(HitChance());
        PlayerView();
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        Instantiate(AtkName, playerUnit.transform);
        //atkNameText.text = "Attack";
        
        playerUnit.anim.SetTrigger("Attack");
        enemyUnit.anim.SetTrigger("Hurt");
        enemyHUD.SetHP(enemyUnit.currentHp);
        
        skillMenu.SetActive(false);
        
        yield return new WaitForSeconds(3f);
        

        
       
        if (isDead)
        {
            StopAllCoroutines();
            state = BattleState.WON;
            StartCoroutine(EndBattle());
            enemyUnit.GiveExperience(10);



        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            EnemyView();
        }
    }
    void HealthWarning()
    {
        if(playerUnit.currentHp <= playerUnit.MaxHp / 2)
        {
            Debug.Log("Suns starting to set there, big guy");
            dialogueText.text = "You should heal";
            playerHUD.hpText.color = Color.cyan;
             }
        else
        {
            playerHUD.hpText.color = Color.black;
        }
    }
    void AutoAttack()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (state == BattleState.PLAYERTURN)
            {
                StartCoroutine_Auto(PlayerAttack());
            }
        }
    }
    //IEnumerator SkillAttack()
    //{
    //    PlayerView();
    //    bool isDead = enemyUnit.TakeDamage(playerUnit.skillDamage);

    //    enemyHUD.SetHP(enemyUnit.currentHp);
    //    dialogueText.text = "The attack is successful";


    //    yield return new WaitForSeconds(2f);

    //    if (isDead)
    //    {
    //        state = BattleState.WON;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        state = BattleState.ENEMYTURN;
    //        StartCoroutine(EnemyTurn());
    //        EnemyView();
    //    }
    //}
    void TargetPlayer()
    {
       
    }


    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(HitChance());
        EnemyView();
        dialogueText.text = enemyUnit.unitName + "attacks!";
        //Instantiate(AtkName, enemyUnit.transform);
        enemyUnit.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);


     
       bool isDead = playerUnit.currentHp<=0;
        Instantiate(hitFx,playerUnit.transform);

        playerHUD.SetHP(playerUnit.currentHp);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            playerUnit.anim.SetTrigger("Dead");
           state = BattleState.LOST;
           StartCoroutine( EndBattle());
            MainCameraView();
            enemyHUD.hpText.color = Color.red;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            PlayerView();
        }
    }
    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle";
            MainCameraView();
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("OverWorld");
        }
        else if (state == BattleState.LOST){
            dialogueText.text = "You were defeated";
        }
    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose An Action:";
        HealthWarning();
        AutoAttack();
    }
   
    public void OnAttackButton()
    {
        if (state!= BattleState.PLAYERTURN) 
        return;

        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        if (playerUnit.currentHp == playerUnit.MaxHp)
          
            return;
        if (playerUnit.currentMana <= 0) 
        return;

        StartCoroutine(PlayerHeal());
    }
    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        skillMenu.SetActive(true);
    }
    public void CancelButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        skillMenu.SetActive(false);
    }
    public void EnemyView()
    {
        PlayerCam.enabled = false;
        EnemyCam.enabled = true;
        Camera.enabled = false;
    }
    public void PlayerView()
    {
        PlayerCam.enabled = true;
        EnemyCam.enabled = false;
        Camera.enabled = false;
    }
    public void MainCameraView()
    {
        PlayerCam.enabled = false;
        EnemyCam.enabled = false;
        Camera.enabled = true;
    }
    public void UseBuff()
    {
        playerUnit.StartCoroutine(Buff());

       
        if (playerUnit.currentMana <= 0)
        {
            Debug.Log("Sorry, no MP left");
            return;
        }
    }
    public void UseDeBuff()
    {
        playerUnit.StartCoroutine(DeBuff());

        if (playerUnit.currentMana <= 0)
        {
            
            return;
        }
    }
    public void UseSkill()
    {
        StartCoroutine(PlayerAttack());
        skillMenu.SetActive(false);
      
        
        enemyUnit.TakeDamage(playerUnit.damage*2);
        enemyHUD.SetHP(enemyUnit.currentHp);

        if (playerUnit.currentMana<= 0)
        {
            Debug.Log("Sorry, no MP left");
            return;
        }
        

    }

    void LevelUp()
    {
        if (enemyUnit.ExpGiven >= playerUnit.XpToLevel)
        {
            playerUnit.unitLevel++;
        }
    }
    //void CriticalHit()
    //{
    //    if(state == BattleState.PLAYERTURN && hitPercent < 8){
    //        state = BattleState.PLAYERTURN;
    //    }
    //}
    void DoNothing()
    {

        if (state == BattleState.PLAYERTURN)
        {
            StartCoroutine(EnemyTurn());
        }
        if (state == BattleState.ENEMYTURN)
        {
            PlayerTurn();
        }}
    IEnumerator HitChance()
    {
        if(state == BattleState.PLAYERTURN)
        hitPercent = Random.Range(0, 11);
        if (hitPercent<=4)
        {
             bool isDead = enemyUnit.TakeDamage(0);
            yield return new WaitForSeconds(1f);
            dialogueText.text = "You missed!";
            Debug.Log("You missed,attack failed");
            Instantiate(missFx, enemyUnit.transform);

            
        }
        //if (hitPercent >8)
        //{
        //    Debug.Log("Critical Hit");
        //    enemyUnit.TakeDamage(playerUnit.damage *= 2);
        //    CriticalHit();
        //}
        if (hitPercent >=5)
        {
            if (state == BattleState.PLAYERTURN)
            {
                bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
                yield return new WaitForSeconds(1f);
                dialogueText.text = "You got them!";
                dialogueText.text = "The attack is successful";
                Instantiate(hitFx, enemyUnit.transform);
            }
        }
        yield return new WaitForSeconds(1f);
        if (state == BattleState.ENEMYTURN)
            hitPercent = Random.Range(0, 11);
        if (hitPercent <= 4)
        {
            bool isDead = playerUnit.TakeDamage(0);
            yield return new WaitForSeconds(1f);
            Instantiate(missFx, playerUnit.transform);
            playerUnit.anim.SetTrigger("Dodge");


        }
        if (hitPercent >= 5)
        {
            if (state == BattleState.ENEMYTURN)
            {
                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
                yield return new WaitForSeconds(1f);
                playerUnit.anim.SetTrigger("Hurt");
                Instantiate(hitFx, playerUnit.transform);
            }

        }
    }
    public void SpawnPlayers()
    {
        Instantiate(players[0].GetComponent<PlayerParty>(), playerSpace);
        Instantiate(players[1].GetComponent<PlayerParty>(), playerSpace1);
        Instantiate(players[2].GetComponent<PlayerParty>(), playerSpace2);

    }
    IEnumerator EscapeChance()
    {
        if (Random.Range(0f, 1f) <= m_dropChance)
        {

            yield return new WaitForSeconds(2f);
            dialogueText.text = "You got away!";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("OverWorld");
        }
    }

    const float m_dropChance = 1f / 10f;  // Set odds here - e.g. 1 in 10 chance.
}

