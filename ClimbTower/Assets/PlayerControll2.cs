using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControll2 : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float jumpForce = 5.0f;

    private float horizontalInput;
    private bool jumpRequested;
    private bool isJumped;

    void Start()
    {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            isJumped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Moya"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
