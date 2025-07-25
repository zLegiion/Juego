using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float boostedSpeed = 1.25f;
    private bool movingRight = true;

    public LayerMask wallLayer;
    public float wallCheckDistance = 0.1f;
    public float rayOriginOffset = 0.5f;

    public int contactDamage = 25;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Vector2 moveDirection = movingRight ? Vector2.right : Vector2.left;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);

        Vector2 raycastOrigin = (Vector2)transform.position + moveDirection * rayOriginOffset;

        RaycastHit2D wallHit = Physics2D.Raycast(raycastOrigin, moveDirection, wallCheckDistance, wallLayer);

        if (wallHit.collider != null)
        {
            Flip();
        }

        Vector3 localScale = transform.localScale;
        localScale.x = movingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    void Flip()
    {
        movingRight = !movingRight;
    }

    public void TakeDamage(int amount)
    {
        moveSpeed = boostedSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerFearController playerFear = collision.collider.GetComponent<PlayerFearController>();
            if (playerFear != null)
            {
                playerFear.TakeDamageFromEnemy();
            }
        }
    }
}