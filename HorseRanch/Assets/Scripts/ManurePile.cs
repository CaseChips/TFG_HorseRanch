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
                MissionManager.instance.CheckManure();
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Equip Pitchfork");
        }
    }
}