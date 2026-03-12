using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;

    [Header("Components")]
    public Rigidbody2D rb; 

    private Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");   

        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}