using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactDistance = 1f; 
    public float interactRadius = 0.3f; 

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractInDirection();
        }
    }

    void InteractInDirection()
    {
        float x = animator.GetFloat("Horizontal");
        float y = animator.GetFloat("Vertical");
        Vector2 facingDir = new Vector2(x, y).normalized;

        if (facingDir == Vector2.zero) facingDir = Vector2.down;

        Vector2 checkPosition = (Vector2)transform.position + (facingDir * interactDistance);

        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPosition, interactRadius);

        foreach (Collider2D hit in hits)
        {
            IInteractable interactable = hit.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
                return; 
            }
        }

        Debug.Log("Nothing to interact with there!");
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                float x = anim.GetFloat("Horizontal");
                float y = anim.GetFloat("Vertical");
                Vector2 facingDir = new Vector2(x, y).normalized;
                if (facingDir == Vector2.zero) facingDir = Vector2.down;

                Vector2 checkPosition = (Vector2)transform.position + (facingDir * interactDistance);

                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(checkPosition, interactRadius);
            }
        }
    }
}