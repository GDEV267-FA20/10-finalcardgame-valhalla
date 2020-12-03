using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMending : MonoBehaviour
{
    void CardPlayed()
    {
        GameObject grandpa = this.transform.parent.transform.parent.gameObject;
        grandpa.GetComponent<MainHand>().clanCard.GetComponent<ClanCard>().health += 4;
    }
}
