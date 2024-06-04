using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // Reference to the slider component

    // Set the maximum health value of the health bar
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Update the health bar to the current health value
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
