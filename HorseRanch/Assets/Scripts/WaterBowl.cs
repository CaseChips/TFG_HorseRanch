using UnityEngine;

public class WaterBowl : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;
    public Sprite fullBowlSprite;
    private bool isFull = false;

    public void Interact()
    {
        ItemData currentItem = InventoryManager.instance.GetActiveItem();

        if (currentItem != null && currentItem.itemName == "Watering Can")
        {
            if (!isFull)
            {
                FillBowl();
            }
        }
        else
        {
            Debug.Log("You need to select the watering can in your hotbar.");
        }
    }

    void FillBowl()
    {
        isFull = true;
        if (linkedHorse != null) linkedHorse.IncreaseStat("hunger", 20f);
        GetComponent<SpriteRenderer>().sprite = fullBowlSprite;
        Debug.Log("Water bowl filled.");
    }
}