using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eTainted : MonoBehaviour
{
    public void CardPlayed()
    {
        GameObject tainted = this.gameObject;
        tainted.GetComponent<Valhalla>().attackerInt++;
        if(tainted.GetComponent<Valhalla>().attackerInt > 3)
        {
            tainted.GetComponent<Valhalla>().attackerInt = 0;
        }
    }
}
