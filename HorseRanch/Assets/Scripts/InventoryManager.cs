using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryUI inventoryUI;

    [Header("Configuration")]
    public int maxSlots = 5;

    public List<ItemData> startingItems;

    public List<ItemData> inventorySlots = new List<ItemData>();
    public int selectedSlotIndex = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        foreach (ItemData item in startingItems)
        {
            AddItem(item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlotIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlotIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlotIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSlotIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) selectedSlotIndex = 4;

        selectedSlotIndex = Mathf.Clamp(selectedSlotIndex, 0, maxSlots - 1);

        inventoryUI.UpdateUI();
    }

    public bool AddItem(ItemData item)
    {
        if (inventorySlots.Count < maxSlots)
        {
            inventorySlots.Add(item);
            Debug.Log("Added to backpack: " + item.itemName);
            return true;
        }
        return false;
    }

    public ItemData GetActiveItem()
    {
        if (selectedSlotIndex < inventorySlots.Count)
        {
            return inventorySlots[selectedSlotIndex];
        }
        return null;
    }
}