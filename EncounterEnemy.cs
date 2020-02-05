using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {


            SceneManager.LoadScene("BattleScene");
            Debug.Log("Do something else here");
            Destroy(this.gameObject);
        }
    }
}
