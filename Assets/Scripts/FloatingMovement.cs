using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMovement : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Speed of the floating movement
    public float floatAmplitude = 0.5f; // Amplitude of the floating movement

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Record the initial position of the sprite
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave for smooth floating
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        
        // Update the sprite's position
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
