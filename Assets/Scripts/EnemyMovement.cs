using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float followSpeed = 3.5f;
    public float detectionRange = 5f;
    public float jumpForce = 5f;
    public float jumpCooldown = 2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform player;

    private bool isGrounded;
    private float lastJumpTime;
    private bool playerDetected = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }

        if (playerDetected)
        {
            FollowPlayer();
        }
        
    }

    void FollowPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Move towards the player horizontally
        Vector2 newPosition = new Vector2(transform.position.x + directionToPlayer.x * followSpeed * Time.deltaTime, transform.position.y);
        transform.position = newPosition;

        // Jump if the player is above and the enemy is grounded
        if (player.position.y > transform.position.y && isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }
}