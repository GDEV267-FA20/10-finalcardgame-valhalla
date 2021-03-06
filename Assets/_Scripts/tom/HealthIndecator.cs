﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndecator : MonoBehaviour // Indicator 
{

    private Slider slider;

    public int health = 0;



    void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = slider.maxValue;

    }


    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health += value;
            
            if (health > slider.maxValue)
            {
                health = (int)slider.maxValue;
                slider.value = health;
            }
            else if(health > 0)
            {
                slider.value = health;
            }
            else // put something here when you want this to do something when the player dies
            {
                slider.value = 0;
                
                
            }
            
        }
    }
}
