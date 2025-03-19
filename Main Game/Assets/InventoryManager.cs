using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuactivated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory") && menuactivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuactivated = false;
        }
        else if(Input.GetButtonDown("Inventory") && !menuactivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuactivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + "quantity = " + quantity + "itemSprite = " + itemSprite);
    }
}
