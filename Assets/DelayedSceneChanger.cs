using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneChanger : MonoBehaviour
{
    public string nextSceneName = "TheEnd";
    public float delay = 5.0f;

    void Start()
    {
        // Start the coroutine to change the scene after the delay
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        // Change the scene
        SceneManager.LoadScene(nextSceneName);
    }
}
