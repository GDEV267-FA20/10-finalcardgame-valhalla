using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eEnergy : MonoBehaviour
{
    public void CardPlayed()
    {
        GameObject energy = this.gameObject;
        energy.GetComponent<Valhalla>().attackerInt--;
        if (energy.GetComponent<Valhalla>().attackerInt < 0)
        {
            energy.GetComponent<Valhalla>().attackerInt = 3;
        }
    }
}
