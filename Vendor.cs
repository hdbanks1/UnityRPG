using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    public GameObject ShopUI;
    public Text Invitation;
    PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        ShopUI.SetActive(false);
        pm.GetComponent<PlayerMovement>().enabled = true;
    }
     void OnTriggerEnter(Collider other)
    {
        ShopUI.SetActive(true);
        other.GetComponent<PlayerMovement>().enabled = false;
    }
    void OnTriggerExit(Collider other)
    {
        ShopUI.SetActive(false);
        other.GetComponent<PlayerMovement>().enabled = true;
    }
    public void ClosePanel()
    {
        ShopUI.SetActive(false);
       
    }
}
