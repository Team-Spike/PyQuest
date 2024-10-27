using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    private Rigidbody2D rb;

    // Add an AudioSource for the rolling sound
    public AudioSource jumpSound;
    public AudioSource rollingSound;  // Rolling sound audio source

    private bool isMoving = false; // Track if the player is currently moving

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        // Optional: Get the AudioSource components (if attached to the player)
        if (jumpSound == null)
        {
            jumpSound = GetComponent<AudioSource>();
        }

        if (rollingSound == null)
        {
            rollingSound = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");

        // Set velocity based on movement
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Play or stop the rolling sound based on movement
        if (Mathf.Abs(moveX) > 0.1f && isGrounded)  // Check if player is moving
        {
            if (!isMoving)
            {
                isMoving = true;
                if (rollingSound != null && !rollingSound.isPlaying)
                {
                    rollingSound.loop = true; // Ensure the sound loops while moving
                    rollingSound.Play();
                }
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                if (rollingSound != null && rollingSound.isPlaying)
                {
                    rollingSound.Stop();
                }
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Player is now in the air

            // Play the jump sound
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
            else
            {
                Debug.LogError("Jump sound not assigned!");
            }

            // Stop rolling sound when jumping
            if (rollingSound != null && rollingSound.isPlaying)
            {
                rollingSound.Stop();
            }
        }
    }

    // Detect when the player lands back on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is back on the ground
        }
    }
}
