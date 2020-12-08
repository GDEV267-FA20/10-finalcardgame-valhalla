using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGeneration : MonoBehaviour
{
    public static CardGeneration S;

    List<GameObject> blueClan;
    List<GameObject> purpClan;
    List<GameObject> redClan;
    List<GameObject> yellClan;

    public List<GameObject> equipDeck;
    public List<GameObject> elixirDeck;

    int ind = 0;
    int secInd = 0;

    [Header("Put them laddies in")]
    public List<Sprite> clanSprites;
    public List<Sprite> equipSprites;
    public List<Sprite> elixirSprites;

    public GameObject cardPrefab;
    public GameObject equipPrefab;
    public GameObject DeckCover;

    public GameObject eEnergyPrefab;
    public GameObject eTaintedPrefab;
    public GameObject eMendingPrefab;

    void Awake()
    {
        if (S == null) S = this;

        blueClan = new List<GameObject>();
        purpClan = new List<GameObject>();
        redClan = new List<GameObject>();
        yellClan = new List<GameObject>();

        elixirDeck = new List<GameObject>();

        string id = "missing";
        for(int i = 0; i < 4; i++)
        {
            for (int z = 0; z < 10; z++)
            {
                GameObject temp = Instantiate(cardPrefab);
                ClanCard card = temp.GetComponent<ClanCard>();
                temp.GetComponent<SpriteRenderer>().sprite = clanSprites[ind];
                temp.transform.localScale = new Vector3(1, 0.9f, 1);
                if (i == 0)
                {
                    if (z == 4)
                    {
                        id = "Blue Chieftain";
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        card.Health = 10;
                        card.attack = 0;
                    }
                    else if (z > 4)
                    {
                        id = "Blue " + z;
                        card.Health = z + 5;
                        if (z == 5) card.attack = 0;
                        else card.attack = -z + 6;
                    }
                    else
                    {
                        id = "Blue " + (z + 1);
                        temp.GetComponent<ClanCard>().Health = z + 6;
                        card.attack = -z + 3;
                    }
                    temp.transform.position = GameObject.FindGameObjectWithTag("BlueDeck").transform.position;
                    temp.transform.parent = GameObject.FindGameObjectWithTag("BlueDeck").transform;
                    temp.transform.rotation = GameObject.FindGameObjectWithTag("BlueDeck").transform.rotation;
                    blueClan.Add(temp);
                }
                if (i == 1)
                {
                    if (z == 4)
                    {
                        id = "Purp Chieftain";
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        card.Health = 10;
                        card.attack = 0;
                    }
                    else if (z > 4)
                    {
                        id = "Purp " + z;
                        card.Health = z + 5;
                        if (z == 5) card.attack = 0;
                        else card.attack = -z + 6;
                    }
                    else
                    {
                        id = "Purp " + (z + 1);
                        temp.GetComponent<ClanCard>().Health = z + 6;
                        card.attack = -z + 3;
                    }
                    temp.transform.position = GameObject.FindGameObjectWithTag("PurpDeck").transform.position;
                    temp.transform.parent = GameObject.FindGameObjectWithTag("PurpDeck").transform;
                    temp.transform.rotation = GameObject.FindGameObjectWithTag("PurpDeck").transform.rotation;
                    purpClan.Add(temp);
                }
                if (i == 2)
                {
                    if (z == 4)
                    {
                        id = "Red Chieftain";
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        card.Health = 10;
                        card.attack = 0;
                    }
                    else if (z > 4)
                    {
                        id = "Red " + z;
                        card.Health = z + 5;
                        if (z == 5) card.attack = 0;
                        else card.attack = -z + 6;
                    }
                    else
                    {
                        id = "Red " + (z + 1);
                        temp.GetComponent<ClanCard>().Health = z + 6;
                        card.attack = -z + 3;
                    }
                    temp.transform.position = GameObject.FindGameObjectWithTag("RedDeck").transform.position;
                    temp.transform.parent = GameObject.FindGameObjectWithTag("RedDeck").transform;
                    temp.transform.rotation = GameObject.FindGameObjectWithTag("RedDeck").transform.rotation;
                    redClan.Add(temp);
                }
                if (i == 3)
                {
                    if (z == 4)
                    {
                        id = "Yell Chieftain";
                        temp.GetComponent<SpriteRenderer>().sortingOrder = 5;
                        card.Health = 10;
                        card.attack = 0;
                    }
                    else if (z > 4)
                    {
                        id = "Yell " + z;
                        card.Health = z + 5;
                        if (z == 5) card.attack = 0;
                        else card.attack = -z + 6;
                    }
                    else
                    {
                        id = "Yell " + (z + 1);
                        temp.GetComponent<ClanCard>().Health = z + 6;
                        card.attack = -z + 3;
                    }
                    temp.transform.position = GameObject.FindGameObjectWithTag("YellDeck").transform.position;
                    temp.transform.parent = GameObject.FindGameObjectWithTag("YellDeck").transform;
                    temp.transform.rotation = GameObject.FindGameObjectWithTag("YellDeck").transform.rotation;
                    yellClan.Add(temp);
                }
                temp.name = id;
                ind++;
            }            
        }

        for(int x = 0; x < 9; x++)
        {
            GameObject temp = null;
            if(x== 0)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().Arm = -1;                                 //fitted armor
                temp.name = "Refined - Fitted";
            }
            if (x == 1)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().Arm = 0;                                 //leather armor
                temp.name = "Refined - Leather";
            }
            if (x == 2)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().Arm = 1;                                 //ragged armor
                temp.name = "Refined - Ragged";
            }
            if (x == 3)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().Arm = -1;                                 //fitted armor
                temp.name = "Tempered - Fitted";
            }
            if (x == 4)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().Arm = 0;                                 //leather armor
                temp.name = "Tempered - Leather";
            }
            if (x == 5)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().Arm = 1;                                 //ragged armor
                temp.name = "Tempered - Ragged";
            }
            if (x == 6)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().Arm = -1;                                 //fitted armor
                temp.name = "Worn - Fitted";
            }
            if (x == 7)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().Arm = 0;                                 //leather armor
                temp.name = "Worn - Leather";
            }
            if (x == 8)
            {
                temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().Weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().Arm = 1;                                 //ragged armor
                temp.name = "Worn - Ragged";
            }
            temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            temp.transform.position = new Vector3(0, 0, -20);
            secInd++;
            equipDeck.Add(temp);
            temp.transform.parent = GameObject.FindGameObjectWithTag("EquipDeck").transform;
        }
        GameObject parent = GameObject.FindGameObjectWithTag("ElixirDeck");
        for (int i = 0; i<4; i++)
        {
            GameObject tempMending = Instantiate(eMendingPrefab);
            tempMending.GetComponent<SpriteRenderer>().sprite = elixirSprites[0];
            tempMending.name = "ELIX_mending";
            tempMending.tag = "Mending";

            GameObject tempEnergy = Instantiate(eEnergyPrefab);
            tempEnergy.GetComponent<SpriteRenderer>().sprite = elixirSprites[1];
            tempEnergy.name = "ELIX_energy";
            tempEnergy.tag = "Energy";

            GameObject tempTainted = Instantiate(eTaintedPrefab);
            tempTainted.GetComponent<SpriteRenderer>().sprite = elixirSprites[2];
            tempTainted.name = "ELIX_tainted";
            tempTainted.tag = "Tainted";

            tempMending.transform.localScale = new Vector3(1, 0.8f, 1);
            tempEnergy.transform.localScale = new Vector3(1, 0.8f, 1);
            tempTainted.transform.localScale = new Vector3(1, 0.8f, 1);

            

            tempEnergy.transform.parent = parent.transform;
            tempMending.transform.parent = parent.transform;
            tempTainted.transform.parent = parent.transform;

            elixirDeck.Add(tempMending);
            elixirDeck.Add(tempEnergy);
            elixirDeck.Add(tempTainted);
        }
        Instantiate(DeckCover, parent.transform);
        DeckCover.transform.localScale = new Vector3(17,19,1);
    }
    void Start()
    {        
    }

    void Update()
    {
        
    }
}
