using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform weaponInventoryGrid;
    public GameObject inventoryUI;
    public GameObject inventoryItemInfoUI;
    public Text inventoryItemInfoName;
    public Text inventoryItemInfoDescription;
    public Image inventoryItemInfoIcon;
    public GameObject inventoryItemInfoStats;

    [HideInInspector]
    public bool canOpenInventory;
    Inventory inventory;
    bool pauseGame;

    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        canOpenInventory = true;
        //UpdateUI called whenever onItemChangedCallback is called. Which is called whenever an Item is added or removed from the inventory
        inventory.onItemChangedCallback += UpdateUI;

        slots = weaponInventoryGrid.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canOpenInventory)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                if(inventoryUI.activeSelf == false)
                {
                    if (inventoryItemInfoUI.activeSelf == true)
                    {
                        inventoryItemInfoUI.SetActive(false);
                    }
                }
                
                pauseGame = togglePause();
                
                
            }
        }
        
	}

    bool togglePause()
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

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    
}
