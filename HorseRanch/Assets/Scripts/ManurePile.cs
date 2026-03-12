using UnityEngine;

public class ManurePile : MonoBehaviour, IInteractable
{
    public HorseStats linkedHorse;
    public void Interact()
    {
        Debug.Log("You cleaned up the manure pile.");

        Destroy(gameObject);

        if(linkedHorse != null)
        {
            linkedHorse.IncreaseStat("hygiene", 25f);
        }
    }
}