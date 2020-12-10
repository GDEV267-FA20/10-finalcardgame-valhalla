using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClanCard : MonoBehaviour
{
    int sortingLayer;
    bool faceUp;
    GameObject cover;
    public bool isDead;
    public bool inPlay;
    public int health;
    public int maxHealth;
    public int attack;

    [Header("Put in")]
    public GameObject FaceDownPrefab;



    public int Health
    { 
        get
        {
            return health;
        }
        set
        {
            health = value;




        }
    }



    void Start()
    {
        isDead = false;
        inPlay = false;
        faceUp = true;

        maxHealth = health;

    }

    void Update()
    {
        if (faceUp == false)
        {
            cover = Instantiate(FaceDownPrefab, this.transform);
        }
        if(faceUp == true)
        {
            Destroy(cover);
        }        
    }
}
