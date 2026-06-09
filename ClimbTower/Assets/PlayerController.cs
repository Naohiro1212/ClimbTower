using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameNormalTimer timer;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airControlMultiplier = 0.3f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private float horizontalInput;
    private bool jumpRequested;

    public bool canMove = true;
    public bool isGrounded;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (!canMove)
        {
            horizontalInput = 0f;
            jumpRequested = false;
            return;
        }

        horizontalInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame ||
            Keyboard.current.wKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }

        if (timer != null && timer.IsStop())
        {
            canMove = false;
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        if (!canMove)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float targetX;

        if (isGrounded)
        {
            targetX = horizontalInput * moveSpeed;
        }
        else
        {
            float airAcceleration = moveSpeed * airControlMultiplier;
            targetX = rb.linearVelocity.x + horizontalInput * airAcceleration * Time.fixedDeltaTime;
            targetX = Mathf.Clamp(targetX, -moveSpeed, moveSpeed);
        }

        rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);

        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        jumpRequested = false;
    }

    private void CheckGrounded()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }
}