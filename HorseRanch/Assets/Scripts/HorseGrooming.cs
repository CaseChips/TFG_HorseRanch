using UnityEngine;

public class HorseGrooming : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;

    private bool hasBeenBrushed = false;

    public void Interact()
    {
        ItemData currentItem = InventoryManager.instance.GetActiveItem();

        if (currentItem != null && currentItem.itemName == "Brush")
        {
            if (!hasBeenBrushed)
            {
                hasBeenBrushed = true;

                if (linkedHorse != null)
                {
                    linkedHorse.IncreaseStat("comfort", 30f);
                }

                Debug.Log("Swish swish! The horse looks shiny and happy.");

                // Trigger particle + sound
            }
            else
            {
                Debug.Log("The horse's coat is already perfectly brushed today!");
            }
        }
        else
        {
            Debug.Log("You need a Brush to groom the horse.");
        }
    }
}