using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuactivated;
    public ItemSlot[] itemSlot;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuactivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuactivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuactivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuactivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++) 
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
