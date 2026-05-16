using UnityEngine;

public class FeedTrough : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;

    public void Interact()
    {
        FeedMixerMinigame.instance.OpenMinigame(linkedHorse);

        GetComponent<SpriteRenderer>().enabled = false;
    }
}