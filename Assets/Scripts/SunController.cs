using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunController : MonoBehaviour
{
    public string bossFightSceneName; // Name of the scene to load for the boss fight

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("LoadBossFightScene");
        }
    }

    void LoadBossFightScene()
    {
        // Load the boss fight scene
        SceneManager.LoadScene(bossFightSceneName);
    }
}
