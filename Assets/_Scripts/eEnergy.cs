using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eEnergy : MonoBehaviour
{
    void CardPlayed()
    {
        if(Valhalla.attackerInt == 0)
        {
            Valhalla.attackerInt = 3;
        } else
        {
            Valhalla.attackerInt--;
        }
    }
}
