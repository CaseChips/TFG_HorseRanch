using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private List<IInteractable> interactablesInRange = new List<IInteractable>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable target = GetClosestInteractable();
            if (target != null)
            {
                target.Interact(); 
            }
        }
    }

    IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float minDistance = float.MaxValue;

        foreach (var item in interactablesInRange)
        {
            float dist = Vector2.Distance(transform.position, ((MonoBehaviour)item).transform.position);
            if (dist < minDistance)
            {
                closest = item;
                minDistance = dist;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Add(interactable);
            Debug.Log("Object in range!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
        }
    }
}