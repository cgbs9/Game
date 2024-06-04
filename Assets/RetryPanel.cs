using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryPanel : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Button respawnButton;

    void Start()
    {
        respawnButton.onClick.AddListener(playerHealth.Respawn);
    }
}
