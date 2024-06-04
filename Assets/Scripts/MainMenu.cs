using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("Sun_Exploding");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void LoadCredits ()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadInstructions ()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadMainMenu ()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}

