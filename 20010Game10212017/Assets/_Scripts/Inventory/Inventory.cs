using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

#region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public bool canEquipfromChest;
    public int inventorySpace = 8;

    //Items in the players inventory
    public List<Item> items = new List<Item>();

    public Item equipmentItem;

    public bool AddItem (Item item)
    {

        if(item != null)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= inventorySpace)
                {
                    Debug.Log("Not enough room in Inventory!");
                    return false;
                }

                items.Add(item);

                //Call event AKA delegate. So everytime a change is made in the inventory this event is called. 
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
        }
        else if(item == null)
        {
            
            return false;
        }
       

        return true;
        
    }

    public void ChestAddItem()
    {
       
        
    }

    public void RemoveItem(Item item)
    {
        item.Remove();
        items.Remove(item);
        //int slotIndex = (int)equipItem.equipSlot;
        //EquipmentManager.instance.Unequip(slotIndex);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
