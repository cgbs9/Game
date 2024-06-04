using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookInteraction : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    public float interactionDistance = 2.0f; // Maximum distance for interaction

    private Transform playerTransform;

    void Start()
    {
        // Find the player object (assuming it has a tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure your player has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Check the distance between the player and the book
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Check if the player is within interaction distance and presses the "F" key
            if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
            {
                // Load the next scene
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
