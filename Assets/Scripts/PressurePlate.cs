using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the inspector
    private bool isActive = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block")) // Ensure the Block item has the tag "Block"
        {
            isActive = true;
            OpenDoor();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            isActive = false;
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        // Implement the logic to open the door
        if (door != null)
        {
            door.SetActive(false); // Deactivate the door GameObject
        }
    }

    void CloseDoor()
    {
        // Implement the logic to close the door
        if (door != null)
        {
            door.SetActive(true); // Activate the door GameObject
        }
    }
}
