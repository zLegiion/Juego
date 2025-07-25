using UnityEngine;
using UnityEngine.EventSystems;

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

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D coll;

    private float horizontalInput;
    private bool isRunning;
    private bool isCrouching;
    private bool isGrounded;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    private Vector2 respawnPoint;
    public bool isDead = false;

    private Vector2 moveDirection = Vector2.right; // Asegurarse de que esté inicializado

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
        respawnPoint = transform.position;
    }

    public void SetCheckpoint(Vector3 pos)
    {
        respawnPoint = pos;
        Debug.Log("Checkpoint establecido en: " + pos);
    }

    public void Kill()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Jugador respawneado en el checkpoint.");
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.LeftControl);

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching) Jump();

        if (horizontalInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(horizontalInput) * Mathf.Abs(scale.x);
            transform.localScale = scale;

            moveDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
        }
        UpdateAnimator();
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

        HandleCrouchCollider();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void HandleCrouchCollider()
    {
        if (isCrouching)
        {
            coll.size = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
            coll.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - originalColliderSize.y * 0.15f);
        }
        else
        {
            RaycastHit2D ceiling = Physics2D.Raycast(transform.position, Vector2.up, originalColliderSize.y, whatIsGround);
            if (!ceiling)
            {
                coll.size = originalColliderSize;
                coll.offset = originalColliderOffset;
            }
            else
            {
                isCrouching = true;
            }
        }
    }
    private void UpdateAnimator()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isRunning", isRunning && !isCrouching && horizontalInput != 0);
        anim.SetBool("isCrouching", isCrouching);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fragment"))
        {
            Object.FindAnyObjectByType<MemoryFragmentCounter>().AddFragment();
        }
    }

    public Vector2 GetFacingDirection()
    {
        return moveDirection;
    }
}