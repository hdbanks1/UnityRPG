using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button item;
    // Start is called before the first frame update
    void Start()
    {
        Button clone = Instantiate(item);

        clone.transform.SetParent(gameObject.transform, false);
        clone.GetComponentInChildren<Text>().text = "Add Items";
        clone.onClick.AddListener(AddItem);
    }

    public string CreateItem()
    {
        string[] listOfItems = new string[] { "Potion", "Medicine", "Stuff" };
        return listOfItems[Random.Range(0, 7)];
    }
    public void AddItem()
    {
        Button clone = Instantiate(item);
        clone.transform.SetParent(gameObject.transform, false);
        clone.GetComponentInChildren<Text>().text = CreateItem();
        clone.onClick.AddListener(() => UseItem(clone));
    }
    public void UseItem(Button button)
    {
        GameObject.Destroy(button.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
