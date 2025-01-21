using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dies fügt den Slider-Typ hinzu

public class HealthBar : MonoBehaviour
{
        public Slider healthSlider;

        public void SetMaxHealth(int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }

        public void SetHealth(int health)
        {
            healthSlider.value = health;
        }
    
}
