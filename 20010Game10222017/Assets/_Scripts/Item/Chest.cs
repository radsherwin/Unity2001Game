using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactables {

    public static Chest instance;

    private void Awake()
    {
        instance = this;
        inventory = Inventory.instance;
    }
    public Item item;
    public Transform cameraFollow;
    public bool hasChestInteracted = false;

    Inventory inventory;
    ChestUI chestui;

    bool pauseGame;
    public bool canUseChest = false;
    public bool isChestEmpty = false;

    public override void Interact()
    {
        

        if (canUseChest)
        {

            if (!isChestEmpty)
            {
                base.Interact();
                ChestInteract();
            }
           
        }
        
    }

    private void Start()
    {
        
        chestui = ChestUI.instance;
        chestui.equipButton.onClick.AddListener(ChestItem);
    }

    void ChestInteract()
    {
       
        hasChestInteracted = !hasChestInteracted;
        pauseGame = TogglePause();
        chestui.itemInfoUI.SetActive(!chestui.itemInfoUI.activeSelf);
        InventoryUI.instance.inventoryUI.SetActive(!InventoryUI.instance.inventoryUI.activeSelf);
        
        if(item != null)
        {
            Stats();
            chestui.titleofWeapon.text = item.name;
            chestui.descriptionText.text = item.description;
            chestui.iconInfoUI.sprite = item.icon;
            chestui.iconInfoUI.enabled = true;
        }
        
        
        
        if (hasChestInteracted == true)
        {
            chestui.isInChest = true;
            InventoryUI.instance.canOpenInventory = false;
            CameraMachine.instance.target = cameraFollow;
            
        }
        else
        {
            chestui.isInChest = false;
            InventoryUI.instance.canOpenInventory = true;
            CameraMachine.instance.target = CameraMachine.instance.defaultTarget;
            
            
        }    
    }

    public void Stats()
    {
        /*
        foreach(Text statText in inventoryItemInfoStats.GetComponentsInChildren<Text>())
        {
            Debug.Log(statText.text);
        }
        */

        Transform gameObjectSlot;
        int damageStat;
        int armorStat;
        int bonusStat;
        damageStat = item.GetStats(0);
        armorStat = item.GetStats(1);
        bonusStat = item.GetStats(2);

        //There exists a damage value
        if (damageStat > 0)
        {
            //Damage value is slot 0
            gameObjectSlot = chestui.itemInfoStats.transform.GetChild(0);
            gameObjectSlot.GetComponentInChildren<Text>().text = damageStat.ToString();
            gameObjectSlot.gameObject.SetActive(true);
            //Icon for child(0) is the damage icon....

            //There exists an armor value
            if (armorStat > 0)
            {
                //Armor value is slot 1
                gameObjectSlot = chestui.itemInfoStats.transform.GetChild(1);
                gameObjectSlot.GetComponentInChildren<Text>().text = armorStat.ToString();
                gameObjectSlot.gameObject.SetActive(true);

                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = chestui.itemInfoStats.transform.GetChild(2);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();
                    gameObjectSlot.gameObject.SetActive(true);
                }


            }
            //If no armor value exists
            else
            {
                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = chestui.itemInfoStats.transform.GetChild(1);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();

                    //Only 2 stats
                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {

                    chestui.itemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
            }



        }
        //There is no damage value
        else
        {
            //There exists an armor value
            if (armorStat > 0)
            {
                //Armor value is slot 0
                gameObjectSlot = chestui.itemInfoStats.transform.GetChild(0);
                gameObjectSlot.gameObject.SetActive(true);
                gameObjectSlot.GetComponentInChildren<Text>().text = armorStat.ToString();

                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = chestui.itemInfoStats.transform.GetChild(1);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();

                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {

                    chestui.itemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }


            }
            //There doesn't exist an armor value
            else
            {
                //There exists a bonus stat
                if (bonusStat > 0)
                {
                    //Bonus is slot 2
                    gameObjectSlot = chestui.itemInfoStats.transform.GetChild(0);
                    gameObjectSlot.gameObject.SetActive(true);
                    gameObjectSlot.GetComponentInChildren<Text>().text = bonusStat.ToString();
                    chestui.itemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    chestui.itemInfoStats.transform.GetChild(0).gameObject.SetActive(false);
                    chestui.itemInfoStats.transform.GetChild(1).gameObject.SetActive(false);
                    chestui.itemInfoStats.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }



    }

    void ChestItem()
    {
        ChestSlot.instance.AddFromChest(item);
        ChestInteract();
    }

    bool TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}
