using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valhalla : MonoBehaviour
{
    [Header("For Delegate Menu Selection:")]
    public float[] position;
    public float xOffset;
    public float yOffset;
    public GameObject menuBG;
    public GameObject selectButtons;
    string archID;

    [Header("all you bruv")]
    public List<GameObject> players;

    void Awake()
    {
        menuBG.SetActive(false);
    }
    void Start()
    {
        LayoutGame();
    }

    void FixedUpdate()
    {
        
        foreach(GameObject player in players)
        {
            if (player.GetComponent<MainHand>().clanCard == null)
            {
                if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(true);
                if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(true);
                if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(true);
                if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(true);
                return;
            }

            if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(false);
            if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(false);
            if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(false);
            if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(false);

            GameObject card = player.GetComponent<MainHand>().clanCard;
            card.transform.position = player.transform.position;
            card.transform.eulerAngles = player.transform.eulerAngles;
            card.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<MainHand>().equipCard.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void LayoutGame()
    {
        foreach(GameObject player in players)
        {
            GameObject equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
            Debug.Log(equipGO.name);
            while(equipGO.GetComponent<EquipCard>().inPlay == true)
            {
                equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
            }
            equipGO.GetComponent<EquipCard>().inPlay = true;

            player.GetComponent<MainHand>().equipCard = equipGO;
            equipGO.transform.position = player.transform.position;
            equipGO.transform.rotation = player.transform.rotation;
        }

        GameObject tempBlue = Instantiate(CardGeneration.S.DeckCover, GameObject.FindGameObjectWithTag("BlueDeck").transform);
        GameObject tempPurp = Instantiate(CardGeneration.S.DeckCover, GameObject.FindGameObjectWithTag("PurpDeck").transform);
        GameObject tempRed =  Instantiate(CardGeneration.S.DeckCover, GameObject.FindGameObjectWithTag("RedDeck").transform);
        GameObject tempYell = Instantiate(CardGeneration.S.DeckCover, GameObject.FindGameObjectWithTag("YellDeck").transform);


        selectButtons.SetActive(true);
        List<GameObject> GO = new List<GameObject>();
        GO.Add(tempBlue); GO.Add(tempPurp); GO.Add(tempRed); GO.Add(tempYell);

        foreach(GameObject go in GO)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = 11;
            go.transform.localScale = new Vector2(16, 20.7f);
        }
    }

    public void SelectMenu(string id)
    {
        archID = id;
        if (id == "PurpDeck")
        {            
            players[1].transform.position = new Vector2(-1, -5);
            players[1].transform.eulerAngles = Vector3.zero;
        }
        if(id == "RedDeck")
        {            
            players[2].transform.position = new Vector2(10,-5);
            players[2].transform.eulerAngles = Vector3.zero;
        }
        if(id == "YellDeck")
        {            
            players[3].transform.position = new Vector2(-1,-5);
            players[3].transform.eulerAngles = Vector3.zero;
        }


        menuBG.SetActive(true);
        selectButtons.SetActive(false);
        GameObject tempGO = GameObject.FindGameObjectWithTag(id);
        int i = 0;
        int z = 0;
        foreach(Transform card in tempGO.transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(false);
            card.transform.localEulerAngles = Vector3.zero;

            if (i <= 5)
            {
                card.transform.localPosition = new Vector3(position[0] + (xOffset * i), position[1], position[2]);
                i++;
            }            
            if(i > 5)
            {
                card.transform.localPosition = new Vector3(position[0] + (xOffset * z), position[1] -yOffset, position[2]);
                i++;
                z++;
            }
        }
    }

    public void SelectCard(int ind)
    {
        GameObject player = null;
        if (archID == "BlueDeck") player = players[0];
        if (archID == "PurpDeck") player = players[1];
        if (archID == "RedDeck") player = players[2];
        if (archID == "YellDeck") player = players[3];

        if (player == null) return;
        GameObject playedCard = GameObject.FindGameObjectWithTag(archID).transform.GetChild(ind).gameObject;
        player.GetComponent<MainHand>().clanCard = playedCard;
        playedCard.transform.position = player.transform.position;
        playedCard.transform.eulerAngles = player.transform.eulerAngles;
        playedCard.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<MainHand>().equipCard.GetComponent<SpriteRenderer>().sortingOrder + 1;
        playedCard.GetComponent<ClanCard>().inPlay = true;
    }

    public void DeckReturn()
    {
        players[1].transform.position = new Vector2(-7, 1);
        players[1].transform.eulerAngles = new Vector3(0,0,270);
      
        players[2].transform.position = new Vector2(1,5);
       
        players[3].transform.position = new Vector2(7, -1);
        players[3].transform.eulerAngles = new Vector3(0,0,90);
      
        foreach (Transform card in GameObject.FindGameObjectWithTag("BlueDeck").transform)
        {            
            if (card.tag == "cover") card.gameObject.SetActive(true);
            card.transform.position = GameObject.FindGameObjectWithTag("BlueDeck").transform.position;
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("PurpDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            card.transform.position = GameObject.FindGameObjectWithTag("PurpDeck").transform.position;
            card.transform.rotation = players[1].transform.rotation;
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("RedDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            card.transform.position = GameObject.FindGameObjectWithTag("RedDeck").transform.position;
            card.transform.rotation = players[2].transform.rotation;
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("YellDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            card.transform.position = GameObject.FindGameObjectWithTag("YellDeck").transform.position;
            card.transform.rotation = players[3].transform.rotation;
        }
    }
}
