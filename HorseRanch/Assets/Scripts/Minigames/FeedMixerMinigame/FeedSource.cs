using UnityEngine;

public class FeedSource : MonoBehaviour, IInteractable 
{
    public ItemData feedItemData; 

    public void Interact()
    {
        InventoryManager.instance.AddItem(feedItemData);
    }
}