using UnityEngine;

public class FeedSource : MonoBehaviour, IInteractable 
{
    public ItemData feedItemData;

    public AudioSource audioSource;

    public AudioClip pickupSound;

    public void Interact()
    {
        if (pickupSound != null) audioSource.PlayOneShot(pickupSound);
        InventoryManager.instance.AddItem(feedItemData);
    }
}