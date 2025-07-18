using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float boostedSpeed = 1.25f;
    private bool movingRight = true;

    public Transform groundCheck;
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.1f;

    public int contactDamage = 25;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Detección de paredes usando Raycast
        RaycastHit2D wallHit = Physics2D.Raycast(groundCheck.position, direction, wallCheckDistance, wallLayer);
        if (wallHit.collider != null)
        {
            Flip();
        }

        // Voltear sprite
        spriteRenderer.flipX = !movingRight;
    }

    void Flip()
    {
        movingRight = !movingRight;
    }

    public void TakeDamage(int amount)
    {
        // Al recibir daño, aumenta velocidad
        moveSpeed = boostedSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }
}
