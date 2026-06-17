using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip goalSound;
    [SerializeField] private AudioClip moyaSound;

    [SerializeField] private AudioSource bgmSource;

    [Header("Timer")]
    [SerializeField] GameNormalTimer timer;

    private float horizontalInput;
    private bool jumpRequested;

    public bool canMove = true;
    public bool isGrounded;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
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

            if(audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!canMove)
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

        float waitTime = 0;

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
            waitTime = clip.length;
        }

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneName);
    }
}