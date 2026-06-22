using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private GameNormalTimer timer;
    [SerializeField] private GameStartTimer gameStartTimer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airControlMultiplier = 0.3f;

    [Header("Normal Ground Move")]
    [SerializeField] private float groundAcceleration = 30f;
    [SerializeField] private float groundDeceleration = 40f;

    [Header("Ice Move")]
    [SerializeField] private float iceAcceleration = 30f;
    [SerializeField] private float iceDeceleration = 30.0f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Physics Materials")]
    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D playerMaterial;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip goalSound;
    [SerializeField] private AudioClip moyaSound;

    [SerializeField] private AudioSource bgmSource;

    private float horizontalInput;
    private bool jumpRequested;

    public bool canMove = true;
    public bool isGrounded;

    private Collider2D currentGroundCollider;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (playerCollider == null)
        {
            playerCollider = GetComponent<Collider2D>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (timer == null)
        {
            timer = FindAnyObjectByType<GameNormalTimer>();
        }

        if (gameStartTimer == null)
        {
            gameStartTimer = FindAnyObjectByType<GameStartTimer>();
        }
    }

    private void Update()
    {
        bool started = gameStartTimer == null || gameStartTimer.IsStarted();
        bool pausedOrStopped = timer != null && timer.IsStop();
        bool canControl = canMove && started && !pausedOrStopped;

        if (!canControl)
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

        if (horizontalInput > 0.01f) sr.flipX = true;
        else if (horizontalInput < -0.01f) sr.flipX = false;

        float speed = Mathf.Abs(rb.linearVelocity.x);
        if (speed < 0.03f) speed = 0f;
        animator.SetFloat("Speed", speed);

        if (Keyboard.current.spaceKey.wasPressedThisFrame ||
            Keyboard.current.wKey.wasPressedThisFrame ||
            Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        bool started = gameStartTimer == null || gameStartTimer.IsStarted();
        bool pausedOrStopped = timer != null && timer.IsStop();
        bool canControl = canMove && started && !pausedOrStopped;

        if (!canControl)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        if (isGrounded && IsOnIce())
        {
            MoveOnIce();
        }
        else if (isGrounded)
        {
            MoveOnNormalGround();
        }
        else
        {
            MoveInAir();
        }

        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        jumpRequested = false;
    }

    private void MoveOnNormalGround()
    {
        float targetX = horizontalInput * moveSpeed;
        float currentX = rb.linearVelocity.x;

        float accelRate = Mathf.Abs(horizontalInput) > 0.01f ? groundAcceleration : groundDeceleration;
        float newX = Mathf.MoveTowards(currentX, targetX, accelRate * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);

        if (playerCollider != null && playerMaterial != null)
        {
            playerCollider.sharedMaterial = playerMaterial;
        }
    }

    private void MoveOnIce()
    {
        float targetX = horizontalInput * moveSpeed;
        float currentX = rb.linearVelocity.x;

        float accelRate = Mathf.Abs(horizontalInput) > 0.01f
            ? groundAcceleration
            : iceDeceleration;

        float newX = Mathf.MoveTowards(currentX, targetX, accelRate * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);

        if (playerCollider != null && playerMaterial != null)
        {
            playerCollider.sharedMaterial = playerMaterial;
        }
    }

    private void MoveInAir()
    {
        float airAcceleration = moveSpeed * airControlMultiplier;
        float targetX = rb.linearVelocity.x + horizontalInput * airAcceleration * Time.fixedDeltaTime;
        targetX = Mathf.Clamp(targetX, -moveSpeed, moveSpeed);

        rb.linearVelocity = new Vector2(targetX, rb.linearVelocity.y);

        if (playerCollider != null && playerMaterial != null)
        {
            playerCollider.sharedMaterial = playerMaterial;
        }
    }

    private void CheckGrounded()
    {
        if (groundCheck == null)
        {
            isGrounded = false;
            currentGroundCollider = null;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
        currentGroundCollider = hit.collider;
    }

    private bool IsOnIce()
    {
        if (currentGroundCollider == null)
        {
            return false;
        }

        return currentGroundCollider.CompareTag("Ice");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canMove)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Moya"))
        {
            canMove = false;
            StartCoroutine(PlaySoundAndLoadScene(moyaSound, "GameOverScene"));
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            canMove = false;
            StartCoroutine(PlaySoundAndLoadScene(goalSound, "ClearScene"));
        }
    }

    private IEnumerator PlaySoundAndLoadScene(AudioClip clip, string sceneName)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (bgmSource != null)
        {
            bgmSource.Stop();
        }

        float waitTime = 0f;

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
            waitTime = clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneName);
    }
}