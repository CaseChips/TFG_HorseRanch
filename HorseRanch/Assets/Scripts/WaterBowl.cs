using UnityEngine;

public class WaterBowl : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;
    private bool isFull = false;

    public AudioSource audioSource;

    public AudioClip pouringWater;
    public AudioClip winSound;

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
        if (pouringWater != null) audioSource.PlayOneShot(pouringWater);
        if (winSound != null) audioSource.PlayOneShot(winSound);
        isFull = true; 
        if (linkedHorse != null) linkedHorse.IncreaseStat("hunger", 20f);
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Water bowl filled.");
        MissionManager.instance.AdvanceMission();
    }
}