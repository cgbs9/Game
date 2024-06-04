using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BadPressurePlate : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign the enemy prefab in the inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block")) // Ensure the Block item has the tag "Block"
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Spawn the enemy at the position of the BadPressurePlate
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
