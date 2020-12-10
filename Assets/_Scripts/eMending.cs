using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMending : MonoBehaviour
{
    public void CardPlayed ()
    {
        GameObject currentCard = this.transform.parent.transform.parent.gameObject;
        currentCard.GetComponent<ClanCard>().Health += 4;
        if(currentCard.GetComponent<ClanCard>().Health > currentCard.GetComponent<ClanCard>().maxHealth)
        {
            currentCard.GetComponent<ClanCard>().Health = currentCard.GetComponent<ClanCard>().maxHealth;
        }
    }
}
