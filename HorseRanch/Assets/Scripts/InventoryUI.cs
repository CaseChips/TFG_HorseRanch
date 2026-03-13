using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public Image[] itemIcons;

    public Image[] highlightBoxes;

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i < InventoryManager.instance.inventorySlots.Count)
            {
                itemIcons[i].sprite = InventoryManager.instance.inventorySlots[i].icon;
                itemIcons[i].enabled = true; 
            }
            else
            {

                itemIcons[i].sprite = null;
                itemIcons[i].enabled = false; 
            }

            if (i == InventoryManager.instance.selectedSlotIndex)
            {
                highlightBoxes[i].enabled = true;
            }
            else
            {
                highlightBoxes[i].enabled = false;
            }
        }
    }
}