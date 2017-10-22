using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

#region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject rightHandLocation;
    Equipment[] curEquipment;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        //Get length of enum Equipment Slot
        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        curEquipment = new Equipment[numOfSlots];

        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem)
    {
        if (inventory.items.Contains(newItem))
        {
            

            //Index number of enum EquipmentSlot
            int slotIndex = (int)newItem.equipSlot;

            Equipment oldItem = null;

            if(curEquipment[slotIndex] = newItem)
            {
                Unequip(slotIndex);
            }
            else
            {
                if (curEquipment[slotIndex] != null)
                {
                    oldItem = curEquipment[slotIndex];
                }

                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(newItem, oldItem);
                }

                curEquipment[slotIndex] = newItem;

                //Parenting weapon to gameobject
                if (curEquipment[3])
                {

                }
            }

            
        }


  
    }

    public void Unequip(int slotIndex)
    {
        if(curEquipment[slotIndex] != null)
        {
            Equipment oldItem = curEquipment[slotIndex];
            curEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }
}
