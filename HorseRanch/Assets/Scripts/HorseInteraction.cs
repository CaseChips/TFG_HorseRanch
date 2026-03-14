using UnityEngine;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;

    private bool hasBeenBrushed = false;

    public void Interact()
    {
        ItemData currentItem = InventoryManager.instance.GetActiveItem();

        if (currentItem == null)
        {
            Debug.Log("You pet the horse. (Try equipping a tool!)");
            return; 
        }

        switch (currentItem.itemName)
        {
            case "Brush":
                if (!hasBeenBrushed)
                {
                    hasBeenBrushed = true;
                    if (linkedHorse != null) linkedHorse.IncreaseStat("comfort", 30f);
                    Debug.Log("Swish swish! The horse looks shiny and happy.");
                }
                else
                {
                    Debug.Log("The horse's coat is already perfectly brushed today!");
                }
                break;

            case "HoofPick":
                HoofMinigame.instance.OpenMinigame(linkedHorse);
                break;

            default:
                Debug.Log("You can't use the " + currentItem.itemName + " on the horse!");
                break;
        }
    }
}