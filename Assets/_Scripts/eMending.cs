using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMending : MonoBehaviour
{
    public void CardPlayed (ClanCard currentDelegate)
    {
        currentDelegate.Health += 4;
        if(currentDelegate.Health > currentDelegate.maxHealth)
        {
            currentDelegate.Health = currentDelegate.maxHealth;
        }
    }
}
