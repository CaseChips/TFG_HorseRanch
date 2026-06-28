using UnityEngine;

public class FeedTrough : MonoBehaviour, IInteractable
{
    [Header("Audio Feedback")]
    public AudioSource audioSource;
    public AudioClip goodMixSound;
    public AudioClip badMixSound;
    public AudioClip perfectMixSound;
    public AudioClip pouringSound;

    [Header("Capacity")]
    public int maxCapacity = 5;
    
    public ItemData hayData;
    public ItemData grainData;
    public ItemData veggieData;

    private int currentHay = 0;
    private int currentGrain = 0;
    private int currentVeggie = 0;
    
    private bool minigameCompleted = false; 

    public void Interact()
    {
        if (minigameCompleted) return;

        if (pouringSound != null) audioSource.PlayOneShot(pouringSound);

        int totalFeed = currentHay + currentGrain + currentVeggie;
        
        if (totalFeed >= maxCapacity) return;

        ItemData activeItem = InventoryManager.instance.GetActiveItem();

        if (activeItem == null) return;

        if (activeItem == hayData)
        {
            InventoryManager.instance.RemoveItem(activeItem);
            currentHay++;
        }
        else if (activeItem == grainData)
        {
            InventoryManager.instance.RemoveItem(activeItem);
            currentGrain++;
        }
        else if (activeItem == veggieData)
        {
            InventoryManager.instance.RemoveItem(activeItem);
            currentVeggie++;
        }

        ProvideFeedback();
    }

    private void ProvideFeedback()
    {
        int totalFeed = currentHay + currentGrain + currentVeggie;
        float grainRatio = (float)currentGrain / totalFeed;

        if (grainRatio > 0.3f)
        {
            if (badMixSound != null) audioSource.PlayOneShot(badMixSound);
        }
        else if (totalFeed < maxCapacity)
        {
            if (goodMixSound != null) audioSource.PlayOneShot(goodMixSound);
        }
        else if (totalFeed >= maxCapacity && grainRatio <= 0.3f)
        {
            if (perfectMixSound != null) audioSource.PlayOneShot(perfectMixSound);
            GetComponent<SpriteRenderer>().enabled = false;
            
            MissionManager.instance.AdvanceMission();
            minigameCompleted = true; 
        }
    }
}