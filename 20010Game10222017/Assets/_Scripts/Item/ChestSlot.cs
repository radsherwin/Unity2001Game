using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour {
    public static ChestSlot instance;

    private void Awake()
    {
        instance = this;
    }

    public bool isChestEmpty = false;

    Inventory inventory;
    Chest chest;

    private void Start()
    {
        inventory = Inventory.instance;
        chest = Chest.instance;
    }

    
    public void AddFromChest(Item chestItem)
    {
        Debug.Log(chestItem);
        
        if (chestItem != null)
        {
            bool wasPickedUp = inventory.AddItem(chestItem);

            if (wasPickedUp)
            {
                isChestEmpty = true;
            }
        }

    }
}
