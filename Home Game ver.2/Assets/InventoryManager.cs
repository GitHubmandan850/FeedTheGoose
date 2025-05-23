using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuactivated;
    public ItemSlot[] itemSlot;
    private CameraStateManager cameraStateManager;
    void Start()
    {
        cameraStateManager = GameObject.Find("Player").GetComponent<CameraStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuactivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuactivated = false;
            cameraStateManager.lockmouse = true;
        }
        else if (Input.GetButtonDown("Inventory") && !menuactivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuactivated = true;
            cameraStateManager.lockmouse = false;
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName,leftOverItems, itemSprite, itemDescription);
                    return leftOverItems;
            }
        }
        return quantity;
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
