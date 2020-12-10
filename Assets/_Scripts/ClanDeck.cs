using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanDeck : MonoBehaviour
{
    public ClanCard bladedancer;
    public ClanCard beserker;
    public ClanCard ranger;
    public ClanCard warrior;
    public ClanCard chieftain;
    public ClanCard sureshot;
    public ClanCard headsman;
    public ClanCard brute;
    public ClanCard shieldmaiden;
    public ClanCard goliath;
    public MainHand parent;

    [Header("all you:")]
    public string id; // Blue, Purp, Red, or Yell

    void Awake()
    {
        parent = this.transform.parent.GetComponent<MainHand>();

        bladedancer = null;
        beserker = null;
        ranger = null;
        warrior = null;
        chieftain = null;
        sureshot = null;
        headsman = null;
        brute = null;
        shieldmaiden = null;
        goliath = null;        
    }

    void Start()
    {
        int i = 1;
        bool chief = false;
        foreach (Transform card in this.gameObject.transform)
        {
            string name = card.gameObject.name;
            
            if (id + " " + i.ToString() == name && !chief)
            {
                switch (i)
                {
                    case 1:
                        bladedancer = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 2:
                        beserker = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 3:
                        ranger = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 4:
                        warrior = card.gameObject.GetComponent<ClanCard>();
                        break;                    
                }
            }
            else if (name == id + " Chieftain")
            {
                chieftain = card.gameObject.GetComponent<ClanCard>();
                chief = true;
            }
            else if(chief && id + " " + (i-1).ToString() == name)
            {
                switch (i - 1)
                {
                    case 5:
                        sureshot = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 6:
                        headsman = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 7:
                        brute = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 8:
                        shieldmaiden = card.gameObject.GetComponent<ClanCard>();
                        break;

                    case 9:
                        goliath = card.gameObject.GetComponent<ClanCard>();
                        break;
                }
            }
            i++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bladedancer.Health <= 0)
        {
            bladedancer.isDead = true;
            bladedancer.GetComponent<SpriteRenderer>().sprite = null;
            if (bladedancer.inPlay)
            {
                parent.clanCard = null;
                bladedancer.inPlay = false;
            }
            
        }
        if (beserker.Health <= 0)
        {
            beserker.isDead = true;
            beserker.GetComponent<SpriteRenderer>().sprite = null;
            if (beserker.inPlay)
            {
                parent.clanCard = null;
                beserker.inPlay = false;
            }
        }
        if (ranger.Health <= 0)
        {
            ranger.isDead = true;
            ranger.GetComponent<SpriteRenderer>().sprite = null;
            if (ranger.inPlay)
            {
                parent.clanCard = null;
                ranger.inPlay = false;
            }
        }
        if (warrior.Health <= 0)
        {
            warrior.isDead = true;
            warrior.GetComponent<SpriteRenderer>().sprite = null;
            if (warrior.inPlay)
            {
                parent.clanCard = null;
                warrior.inPlay = false;
            }
        }
        if (sureshot.Health <= 0)
        {
            sureshot.isDead = true;
            sureshot.GetComponent<SpriteRenderer>().sprite = null;
            if (sureshot.inPlay)
            {
                parent.clanCard = null;
                sureshot.inPlay = false;
            }
        }
        if(headsman.Health <= 0)
        {
            headsman.isDead = true;
            headsman.GetComponent<SpriteRenderer>().sprite = null;
            if (headsman.inPlay)
            {
                parent.clanCard = null;
                headsman.inPlay = false;
            }
        }
        if(brute.Health <= 0)
        {
            brute.isDead = true;
            brute.GetComponent<SpriteRenderer>().sprite = null;
            if (brute.inPlay)
            {
                parent.clanCard = null;
                brute.inPlay = false;
            }
        }
        if(shieldmaiden.Health <= 0)
        {
            shieldmaiden.isDead = true;
            shieldmaiden.GetComponent<SpriteRenderer>().sprite = null;
            if (shieldmaiden.inPlay)
            {
                parent.clanCard = null;
                shieldmaiden.inPlay = false;
            }
        }
        if(goliath.Health <= 0)
        {
            goliath.isDead = true;
            goliath.GetComponent<SpriteRenderer>().sprite = null;
            if (goliath.inPlay)
            {
                parent.clanCard = null;
                goliath.inPlay = false;
            }
        }
    }
}

