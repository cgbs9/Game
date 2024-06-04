using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this namespace for UI components

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // Reference to the UI Slider component
    private Animator animator;
    public Vector3 respawnPosition;
    public GameObject retryPanel;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        animator = GetComponent<Animator>();
        respawnPosition = transform.position;
        retryPanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        retryPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Respawn()
    {
        transform.position = respawnPosition;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
        retryPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death Zone"))
        {
            Die();
        }
    }

    public void SetRespawnPosition(Vector3 newRespawnPosition)
    {
        respawnPosition = newRespawnPosition;
    }
}