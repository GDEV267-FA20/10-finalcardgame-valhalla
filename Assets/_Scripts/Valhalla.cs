using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates
{
    startup,
    select,
    firstTurn,
    nextTurn
}

public class Valhalla : MonoBehaviour
{
    GameStates gameState = GameStates.startup;
    List<MainHand> hands;

    
    [Header("For Delegate Menu Selection:")]
    public float[] position;
    public float xOffset;
    public float yOffset;
    public GameObject menuBG;
    public GameObject selectButtons;
    string archID;
    List<GameObject> revealCovers;
    public List<GameObject> topRow;
    public List<GameObject> bottomRow;

    [Header("For attacking:")]
    public GameObject attackButtons;
    public int attackerInt;
    bool fired = false;
    GameObject blue;    //attack buttons
    GameObject purple;
    GameObject red;
    GameObject yellow;

    public Slider[] healthSliders;
    


    [Header("all you bruv")]
    public List<GameObject> players;

    void Awake()
    {
        hands = new List<MainHand>();
        for(int i = 0; i <4; i++)
        {
            hands.Add(players[i].GetComponent<MainHand>());
        }
        foreach(Transform child in attackButtons.transform)
        {
            child.gameObject.SetActive(false);
        }
        menuBG.SetActive(false);
        revealCovers = new List<GameObject>();
    }
    void Start()
    {
        blue = attackButtons.transform.GetChild(0).gameObject;
        purple = attackButtons.transform.GetChild(1).gameObject;
        red = attackButtons.transform.GetChild(2).gameObject;
        yellow = attackButtons.transform.GetChild(3).gameObject;

        LayoutGame();
    }

    void FixedUpdate()
    {
        if (players[0].GetComponent<MainHand>().clanCard == null) blue.SetActive(false);
        if (players[1].GetComponent<MainHand>().clanCard == null) purple.SetActive(false);
        if (players[2].GetComponent<MainHand>().clanCard == null) red.SetActive(false);
        if (players[3].GetComponent<MainHand>().clanCard == null) yellow.SetActive(false);

        foreach (GameObject player in players)
        {
            

            if (player.GetComponent<MainHand>().clanCard == null)
            {
                if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(true);
                if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(true);
                if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(true);
                if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(true);
                return;
            }
            else
            {
                if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(false);
                if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(false);
                if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(false);
                if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(false);
            }

            GameObject card = player.GetComponent<MainHand>().clanCard;
            card.transform.position = player.transform.position;
            card.transform.eulerAngles = player.transform.eulerAngles;
            card.GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<MainHand>().equipCard.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }

        switch (gameState)
        {
            case GameStates.startup:
                break;

            case GameStates.select:
                if (players[0].GetComponent<MainHand>().clanCard != null && players[1].GetComponent<MainHand>().clanCard != null && players[2].GetComponent<MainHand>().clanCard != null && players[3].GetComponent<MainHand>().clanCard != null)
                {
                    foreach (GameObject cover in revealCovers)
                    {
                        cover.SetActive(false);
                    }
                    gameState = GameStates.firstTurn;
                }
                break;

            case GameStates.firstTurn:
                if (fired) return;
                fired = true;
                attackerInt = FindFirstAttacker();
                SetAttackButtons(players[attackerInt]);
                break;

        }
    }

    public void NextTurn()
    {
        ResetAttackButtons();
        attackerInt++;
        if (attackerInt >= 4) attackerInt = 0;

        if (players[attackerInt].GetComponent<MainHand>().clanCard == null) NextTurn();

        SetAttackButtons(players[attackerInt]);
    }

    void ResetAttackButtons()
    {
        foreach(Transform child in attackButtons.transform)
        {            
            child.GetComponent<Button>().image.color = Color.white;
            child.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
            child.gameObject.SetActive(false);
        }
    }


    void SetAttackButtons(GameObject attacker)
    {
        

        if(attacker.name == "BLUE")
        {
            purple.SetActive(true);
            purple.GetComponent<Button>().image.color = Color.blue;

            red.SetActive(true);
            red.GetComponent<Button>().image.color = Color.blue;

            yellow.SetActive(true);
            yellow.GetComponent<Button>().image.color = Color.blue;
        }
        if (attacker.name == "PURPLE")
        {
            blue.SetActive(true);
            blue.GetComponent<Button>().image.color = Color.magenta;

            red.SetActive(true);
            red.GetComponent<Button>().image.color = Color.magenta;

            yellow.SetActive(true);
            yellow.GetComponent<Button>().image.color = Color.magenta;
        }
        if (attacker.name == "RED")
        {
            purple.SetActive(true);
            purple.GetComponent<Button>().image.color = Color.red;

            blue.SetActive(true);
            blue.GetComponent<Button>().image.color = Color.red;

            yellow.SetActive(true);
            yellow.GetComponent<Button>().image.color = Color.red;
        }
        if (attacker.name == "YELLOW")
        {
            purple.SetActive(true);
            purple.GetComponent<Button>().image.color = Color.yellow;
            purple.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.black;


            red.SetActive(true);
            red.GetComponent<Button>().image.color = Color.yellow;
            red.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.black;

            blue.SetActive(true);
            blue.GetComponent<Button>().image.color = Color.yellow;
            blue.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.black;
        }
        
    }


    int FindFirstAttacker()
    {
        List<int> stats = new List<int>();
        int p0stat = -players[0].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Arm +
            players[0].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;

        int p1stat = -players[1].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Arm +
            players[1].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;

        int p2stat = -players[2].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Arm +
            players[2].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;

        int p3stat = -players[3].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Arm +
            players[3].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;
        stats.Add(p0stat); stats.Add(p1stat); stats.Add(p2stat); stats.Add(p3stat);

        int lowest = 2;
        int player = 0;
        int count = 0;
        foreach(int stat in stats)
        {
            Debug.Log(stat);
            if (stat < lowest)
            {
                lowest = stat;
                player = count;
            }
            count++;
        }
        return player;
    }

    void LayoutGame()
    {
        foreach(GameObject player in players)
        {
            GameObject equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
            Debug.Log(equipGO.name);
            while(equipGO.GetComponent<EquipCard>().InPlay == true)
            {
                equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
            }
            equipGO.GetComponent<EquipCard>().InPlay = true;

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
        gameState = GameStates.select;
    }

    public void SelectMenu(string id)
    {
        int param = 0; //for sequential function 'SetButtons'
        archID = id;
        if (id == "PurpDeck")
        {
            param = 1;
            players[1].transform.position = new Vector2(-1, -5);
            players[1].transform.eulerAngles = Vector3.zero;

            
        }
        else if(id == "RedDeck")
        {
            param = 2;
            players[2].transform.position = new Vector2(10,-5);
            players[2].transform.eulerAngles = Vector3.zero;

           
        }
        else if(id == "YellDeck")
        {
            param = 3;
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
        SetButtons(param);
    }

    public void SetButtons(int player)
    {
        ClanDeck deck = players[player].transform.GetChild(0).gameObject.GetComponent<ClanDeck>();
        topRow[4].SetActive(false);
        if (deck.bladedancer.isDead) topRow[0].SetActive(false);
        if (deck.beserker.isDead) topRow[1].SetActive(false);
        if (deck.ranger.isDead) topRow[2].SetActive(false);
        if (deck.warrior.isDead) topRow[3].SetActive(false);
        if (deck.sureshot.isDead) bottomRow[5].SetActive(false);
        if (deck.headsman.isDead) bottomRow[6].SetActive(false);
        if (deck.brute.isDead) bottomRow[7].SetActive(false);
        if (deck.shieldmaiden.isDead) bottomRow[8].SetActive(false);
        if (deck.goliath.isDead) bottomRow[9].SetActive(false);
        if (deck.bladedancer.isDead && deck.beserker.isDead &&
            deck.ranger.isDead && deck.warrior.isDead &&
            deck.sureshot.isDead && deck.headsman.isDead &&
            deck.brute.isDead && deck.shieldmaiden.isDead &&
            deck.goliath.isDead) topRow[4].SetActive(true);
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

        player.GetComponent<MainHand>().SetMaxHealthSlider(); // sets the healthbar max value to the starting health
        
        GameObject tempCover = Instantiate(CardGeneration.S.DeckCover, playedCard.transform);
        tempCover.transform.localScale = new Vector2(16, 23);
        tempCover.transform.eulerAngles = player.transform.eulerAngles;
        tempCover.GetComponent<SpriteRenderer>().sortingOrder = 1;
        revealCovers.Add(tempCover);
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
