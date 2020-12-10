using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMending : MonoBehaviour
{
    public void CardPlayed()
    {
        ClanCard currentCard = this.gameObject.transform.parent.gameObject.GetComponent<MainHand>()
            .clanCard.GetComponent<ClanCard>();
        currentCard.Health += 4;
        if(currentCard.Health > currentCard.maxHealth)
        {
            currentCard.Health = currentCard.maxHealth;
        }
    }
}
