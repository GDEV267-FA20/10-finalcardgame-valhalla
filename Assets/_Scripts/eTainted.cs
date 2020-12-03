using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eTainted : MonoBehaviour
{
    void CardPlayed()
    {
        if(Valhalla.attackerInt == 3)
        {
            Valhalla.attackerInt = 0;
        } else
        {
            Valhalla.attackerInt++;
        }
    }
}
