using UnityEngine;

public class HorseInteraction : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;

    public AudioSource audioSource;

    public AudioClip brushing;

    public AudioClip neigh;

    private bool hasBeenBrushed = false;

    public void Interact()
    {
        ItemData currentItem = InventoryManager.instance.GetActiveItem();

        if (currentItem == null)
        {
            if (neigh != null) audioSource.PlayOneShot(neigh);
            Debug.Log("You pet the horse.");
            return; 
        }

        switch (currentItem.itemName)
        {
            case "Brush":
                if (!hasBeenBrushed)
                {
                    if (brushing != null) audioSource.PlayOneShot(brushing);
                    hasBeenBrushed = true;
                    if (linkedHorse != null) linkedHorse.IncreaseStat("comfort", 30f);
                    MissionManager.instance.AdvanceMission();
                }
                else
                {
                    Debug.Log("Coat was already brushed");
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