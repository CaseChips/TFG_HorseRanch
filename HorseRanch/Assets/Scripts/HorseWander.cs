using UnityEngine;

public class HorseWander : MonoBehaviour
{
    [Header("Wander Settings")]
    public float moveSpeed = 1.5f;
    public float wanderRadius = 3f;

    [Header("Idle Times")]
    public float minIdleTime = 2f;
    public float maxIdleTime = 6f;

    private Vector2 startPosition;
    private Vector2 targetPosition;

    private bool isMoving = false;
    private float idleTimer = 0f;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        startPosition = transform.position;
        isMoving = false;
        idleTimer = Random.Range(minIdleTime, maxIdleTime);
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
        else
        {
            HandleIdle();
        }
    }

    void HandleIdle()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        targetPosition = startPosition + randomPoint;
        isMoving = true;

        if (animator != null) animator.SetBool("isWalking", true);
    }

    void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 direction = (targetPosition - currentPos).normalized;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true; 
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false; 
        }

        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            idleTimer = Random.Range(minIdleTime, maxIdleTime);

            if (animator != null) animator.SetBool("isWalking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 center = Application.isPlaying ? startPosition : (Vector2)transform.position;
        Gizmos.DrawWireSphere(center, wanderRadius);
    }
}