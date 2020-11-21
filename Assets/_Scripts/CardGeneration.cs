using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGeneration : MonoBehaviour
{
    List<GameObject> blueClan;
    List<GameObject> purpClan;
    List<GameObject> redClan;
    List<GameObject> yellClan;
    int ind = 0;
    int secInd = 0;

    [Header("Put them laddies in")]
    public List<Sprite> clanSprites;
    public List<Sprite> equipSprites;
    public GameObject cardPrefab;
    public GameObject equipPrefab;

    void Awake()
    {
        string id = "missing";
        for(int i = 0; i < 4; i++)
        {
            for (int z = 0; z < 10; z++)
            {
                GameObject temp = Instantiate(cardPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = clanSprites[ind];
                temp.transform.localScale = new Vector3(1, 0.9f, 1);
                if (i == 0)
                {
                    id = "Blue " + z;
                    temp.transform.position = GameObject.FindGameObjectWithTag("BlueDeck").transform.position;
                }
                if (i == 1)
                {
                    id = "Purp " + z;
                    temp.transform.position = GameObject.FindGameObjectWithTag("PurpDeck").transform.position;
                }
                if (i == 2)
                {
                    id = "Red " + z;
                    temp.transform.position = GameObject.FindGameObjectWithTag("RedDeck").transform.position;
                }
                if (i == 3)
                {
                    id = "Yell " + z;
                    temp.transform.position = GameObject.FindGameObjectWithTag("YellDeck").transform.position;
                }
                temp.name = id;
                ind++;
            }            
        }

        for(int x = 0; x < 9; x++)
        {
            if(x== 0)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().arm = -1;                                 //fitted armor
                temp.name = "Refined - Fitted";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 1)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().arm = 0;                                 //leather armor
                temp.name = "Refined - Leather";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 2)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 1;                                //refined weapon
                temp.GetComponent<EquipCard>().arm = 1;                                 //ragged armor
                temp.name = "Refined - Ragged";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 3)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().arm = -1;                                 //fitted armor
                temp.name = "Tempered - Fitted";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 4)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().arm = 0;                                 //leather armor
                temp.name = "Tempered = Leather";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 5)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = 0;                                //tempered weapon
                temp.GetComponent<EquipCard>().arm = 1;                                 //ragged armor
                temp.name = "Tempered = Ragged";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 6)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().arm = -1;                                 //fitted armor
                temp.name = "Worn - Fitted";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 7)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().arm = 0;                                 //leather armor
                temp.name = "Worn - Leather";
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
                secInd++;
            }
            if (x == 8)
            {
                GameObject temp = Instantiate(equipPrefab);
                temp.GetComponent<SpriteRenderer>().sprite = equipSprites[secInd];
                temp.GetComponent<EquipCard>().weap = -1;                                //worn weapon
                temp.GetComponent<EquipCard>().arm = 1;                                 //ragged armor
                temp.name = "Worn - Ragged";
                secInd++;
                temp.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                temp.transform.position = new Vector3(0, 0, -20);
            }            
        }        
    }
    void Start()
    {        
    }

    void Update()
    {
        
    }
}
