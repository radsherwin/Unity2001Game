﻿using UnityEngine;

public class ItemPickup : Interactables {

    public Item item;

    public override void Interact()
    {
        base.Interact();
        
        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
