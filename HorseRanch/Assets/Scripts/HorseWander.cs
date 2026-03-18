using UnityEngine;

public class HorseWander : MonoBehaviour
{
    [Header("Wander Settings")]
    public float moveSpeed = 1.5f;
    public float wanderRadius = 3f;

    [Header("Idle Times")]
    public float minIdleTime = 2f;
    public float maxIdleTime = 6f;

    [Header("Give Up Settings")]
    public float maxWalkTime = 5f;
    private float walkTimer = 0f;

    private Vector2 startPosition;
    private Vector2 targetPosition;

    private bool isMoving = false;
    private float idleTimer = 0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;
        isMoving = false;
        idleTimer = Random.Range(minIdleTime, maxIdleTime);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
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

        walkTimer = 0f;

        if (animator != null) animator.SetBool("isWalking", true);
    }

    void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 direction = (targetPosition - currentPos).normalized;

        if (direction.x < 0) spriteRenderer.flipX = true;
        else if (direction.x > 0) spriteRenderer.flipX = false;

        if (animator != null)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        rb.linearVelocity = direction * moveSpeed;

        walkTimer += Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f || walkTimer >= maxWalkTime)
        {
            isMoving = false;

            rb.linearVelocity = Vector2.zero;

            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            idleTimer = Random.Range(4f, 8f);

            if (animator != null)
            {
                animator.SetBool("isWalking", false);

                if (Random.value > 0.5f)
                {
                    animator.SetTrigger("doGraze");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 center = Application.isPlaying ? startPosition : (Vector2)transform.position;
        Gizmos.DrawWireSphere(center, wanderRadius);
    }
}