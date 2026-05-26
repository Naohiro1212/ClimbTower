using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpForce = 5.0f;

    private float horizontalInput;
    private bool jumpRequested;
    private bool isJumped;

    // 外部から動けるかどうかを制御するフラグ
    public bool canMove;

    void Start()
    {
        canMove = false;
        rb = GetComponent<Rigidbody2D>();
        isJumped = false;
    }

    void Update()
    {
        horizontalInput = 0f;

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput = 1f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumped)
        {
            jumpRequested = true;
            isJumped = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = false;
        }
    }

    // Groundとの接触を検出して、ジャンプ状態をリセットする
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Groundタグを持つオブジェクトと接触した場合、ジャンプ状態をリセットする
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumped = false;
        }
    }
}
