using UnityEngine;

public class ManurePile : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;
    public void Interact()
    {
        ItemData currentItem = InventoryManager.instance.GetActiveItem();

        if (currentItem != null && currentItem.itemName == "Pitchfork")
        {
            if (linkedHorse != null)
            {
                linkedHorse.IncreaseStat("hygiene", 20f);
            }

            Debug.Log("Cleaned the stall with the Pitchfork!");
            Destroy(gameObject); 
        }
        else
        {
            Debug.Log("Gross! You need to equip the Pitchfork to clean this up.");
        }
    }
}