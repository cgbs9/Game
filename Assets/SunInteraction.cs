using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunInteraction : MonoBehaviour
{
    // The name of the scene to load when F is pressed
    public string nextSceneName;

    // Update is called once per frame
    void Update()
    {
        // Check if F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
