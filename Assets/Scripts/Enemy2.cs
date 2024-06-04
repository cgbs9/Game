using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject dropPrefab;  // Reference to the drop object prefab
    public int health = 100;

    // This method simulates taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    void Die()
    {
        // Instantiate the drop object at the enemy's position
        Instantiate(dropPrefab, transform.position, Quaternion.identity);
        
        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
