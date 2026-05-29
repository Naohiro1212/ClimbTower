using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Move")]
    public float moveSpeed = 5f;
    public float airControlMultiplier = 0.3f;

    [Header("Jump")]
    public float jumpForce = 8f;

    [SerializeField] GameNormalTimer timer;

    private float horizontalInput;
    private bool jumpRequested;
    public bool canMove;

    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;
        

        horizontalInput = 0f;

        // 移動入力の処理
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
        {
            jumpRequested = true;
        }

        // タイマーがストップとしている場合、プレイヤーの動きを停止する   
        if (timer.IsStop())
        {
            canMove = false;
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (isGrounded)
        {
            float targetX = horizontalInput * moveSpeed;
            rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);
        }
        else
        {
            float airAcceleration = moveSpeed * airControlMultiplier;
            float targetX = rb.linearVelocity.x + horizontalInput * airAcceleration * Time.fixedDeltaTime;
            targetX = Mathf.Clamp(targetX, -moveSpeed, moveSpeed);

            rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);
        }

        if (jumpRequested)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}