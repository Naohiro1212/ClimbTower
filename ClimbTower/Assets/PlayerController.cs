using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Move")]
    public float moveSpeed = 5f;
    public float airControlMultiplier = 0.3f;

    [Header("Jump")]
    public float jumpForce = 8f;

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
    public bool canMove;

    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
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

            if(audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
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