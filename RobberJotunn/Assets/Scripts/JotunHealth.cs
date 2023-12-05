using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JotunHealth : MonoBehaviour
{
    // Slider for Fill
    public Slider slider;

    // Use to set the enemy health
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    // change health
    public void SetHealth(float health){
        slider.value = health;
    }
}
