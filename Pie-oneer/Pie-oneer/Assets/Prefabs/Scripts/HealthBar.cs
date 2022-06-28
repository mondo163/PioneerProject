using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;

    //function called within the enemies behavior to preset the health bar for that bosses health.
    public void PresetHealth(int startValue)
    {
        HealthSlider.maxValue = startValue;
        HealthSlider.value = startValue;
        HealthSlider.minValue = 0;
    }

    //sets the health slider to a new value as long as it is under the max preset. 
    public void SetHealth(int curHealth)
    {   
        if(curHealth <= HealthSlider.maxValue) HealthSlider.value = curHealth;

    }
}
