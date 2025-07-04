using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [Tooltip("Units per second while walking.")]
    [SerializeField] private float walkSpeed = 4f;
    [Tooltip("Units per second while running (Left‑Shift held).")]
    [SerializeField] private float runSpeed = 7f;
    [Tooltip("Units per second while crouching (Left‑Ctrl held).")]
    [SerializeField] private float crouchSpeed = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;            

    // Private references
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D coll;

    // State variables
    private float horizontalInput;
    private bool isRunning;
    private bool isCrouching;
    private bool isGrounded;

    // Collider data (for crouch resize)
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    private Vector2 respawnPoint;
    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();

        originalColliderSize = coll.size;
        originalColliderOffset = coll.offset;
    }

    private void Start()
    {
        respawnPoint = transform.position;   // first point spawn
    }
    public void SetCheckpoint(Vector3 pos)
    {
        respawnPoint = pos;
    }
    public void Kill() // call this when the player "dies"
    {
        transform.position = respawnPoint;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        isDead = false;
    }

    private void Update()
    {
        // Gather input every frame (independent of physics)
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        // Attempt jump (only allowed if grounded and not crouching)
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching) Jump();

        // Flip sprite based on move direction
        if (horizontalInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(horizontalInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.M))
        {
            Kill();
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        float speed;

        if (isCrouching)
            speed = crouchSpeed;
        else if (isRunning)
            speed = runSpeed;
        else
            speed = walkSpeed;

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Apply crouch collider resize
        HandleCrouchCollider();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity first for consistent jump height
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void HandleCrouchCollider()
    {
        // Reduce collider height by 50% while crouching.
        if (isCrouching)
        {
            coll.size = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
            coll.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * 0.15f);
        }
        else
        {
            // Restore collider when standing (only if space above is clear)
            RaycastHit2D ceiling = Physics2D.Raycast(transform.position, Vector2.up, originalColliderSize.y, whatIsGround);
            if (!ceiling)
            {
                coll.size = originalColliderSize;
                coll.offset = originalColliderOffset;
            }
            else
            {
                // Still blocked stay crouched
                isCrouching = true;
            }
        }
    }
    private void UpdateAnimator()
    {
        // Speed parameter drives walk/run blend tree (absolute velocity *because* flip is done via scale)
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isRunning", isRunning && !isCrouching && horizontalInput != 0);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
}
