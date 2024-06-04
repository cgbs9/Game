using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveAndAnimate : MonoBehaviour
{
    public Vector3 targetPosition; // Set this to the desired target position in the Inspector or through code
    public float speed = 1.0f;     // Adjust this to control the movement speed
    private Animator animator;     // Reference to the Animator component
    private bool hasReachedTarget = false;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        
        // Set the initial target position if not set in the Inspector
        if (targetPosition == Vector3.zero)
        {
            targetPosition = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
        }
        
        Debug.Log("Starting Position: " + transform.position);
        Debug.Log("Target Position: " + targetPosition);
    }

    void Update()
    {
        // Move the object towards the target position if it hasn't reached it yet
        if (!hasReachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Debug.Log("Current Position: " + transform.position);
            
            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                hasReachedTarget = true;
                
                Debug.Log("Reached Target Position");
                
                // Play the animation
                if (animator != null)
                {
                    animator.SetTrigger("PlayAnimation"); // Assumes you have an animation trigger named "PlayAnimation"
                }
            }
        }
    }

    // This method will be called at the end of the animation via an Animation Event
    public void OnAnimationComplete()
    {
        Debug.Log("Animation Complete");
        SceneManager.LoadScene("TutorialLevel");
    }
}
