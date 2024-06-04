using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Speed of player movement
    public float jumpForce = 10f; // Force of player jump
    public int maxJumps = 2; // Maximum number of jumps allowed (for double jump)
    public LayerMask groundMask; // Layer mask to determine what is considered ground
    public Transform groundCheck; // Transform representing the ground check position
    public float groundCheckRadius = 0.2f; // Radius of the ground check circle
    public int damageAmount = 20; // Damage amount dealt by the player
    public Collider2D attackCollider; // Reference to the player's attack collider
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public AudioClip footstepSound;
    public float footstepDelay = 0.5f;
    
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool facingRight = true;
    private int jumpCount;
    private float footstepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        jumpCount = maxJumps;
        footstepTimer = footstepDelay;
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded && rb.velocity.y <= 0.1f)
        {
            jumpCount = maxJumps; // Reset the jump count when the player is grounded
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput)); // Set Speed parameter in Animator

        if ((moveInput < 0 && facingRight) || (moveInput > 0 && !facingRight))
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interect();
        }

        HandleFootsteps(moveInput);

    }

    private void HandleFootsteps(float moveInput)
    {
        if (isGrounded && Mathf.Abs(moveInput) > 0)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                footstepTimer = footstepDelay;
                audioSource.PlayOneShot(footstepSound);
            }
        }
        else
        {
            footstepTimer = footstepDelay; // Reset the timer when not moving or not grounded
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        if (!facingRight)
        {
            Vector3 scale = projectile.transform.localScale;
            scale.x *= -1;
            projectile.transform.localScale = scale;

            projectile.GetComponent<Projectile>().speed *= -1;
        }
    }
    private void Jump()
    {
        // Apply vertical impulse to the player
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpCount--; // Decrease the jump count
    }

    private void Attack()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");
    }

    private void Interect()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in interactables)
        {
             IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("Interacting with: " + collider.gameObject.name);
                interactable.Interact();
                break; // Interact with the first interactable object found
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a visual representation of the ground check radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
