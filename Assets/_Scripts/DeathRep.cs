using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRep : MonoBehaviour
{
    public List<GameObject> lads;

    [Header("all you:")]
    public ClanDeck deck;

    void Start()
    {
        foreach(Transform child in this.transform)
        {
            lads.Add(child.gameObject);
        }
    }

    void Update()
    {
        if (deck.bladedancer.isDead) lads[0].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.beserker.isDead) lads[1].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.ranger.isDead) lads[2].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.warrior.isDead) lads[3].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.sureshot.isDead) lads[4].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.headsman.isDead) lads[6].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.brute.isDead) lads[7].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.shieldmaiden.isDead) lads[8].GetComponent<SpriteRenderer>().color = Color.red;
        if (deck.goliath.isDead) lads[9].GetComponent<SpriteRenderer>().color = Color.red;

        if (deck.chieftain.isDead) lads[5].GetComponent<SpriteRenderer>().color = Color.red;
    }
}
