using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates
{
    startup,
    select,
    preRound,
    playFirstRound,
    playRegRound,
    endRound
}

public class Valhalla : MonoBehaviour
{
    GameStates gameState = GameStates.startup;
    public List<MainHand> hands;

    [Header("all you bruv")]
    public List<GameObject> players;
    public GameObject ElixirMenu;
    [HideInInspector]
    public Text mending;
    public Text energy;
    public Text tainted;
    public GameObject mTint;
    public GameObject eTint;
    public GameObject tTint;

    [Header("For End of Round: ")]
    public GameObject winMenu;
    public GameObject scavButtons;
    [HideInInspector]
    public int dead;
    public int lastAlive;
    bool trigger1 = false;
    bool trigger2 = false;
    
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
    bool trigger = false;
    public GameObject blue;    //attack buttons
    public GameObject purple;
    public GameObject red;
    public GameObject yellow;




    [Header("all you bruv")]
    //public List<GameObject> players;

    [Header("Elixir Variables")]
    public MainHand menuPlayer;
    public bool skipScan;
    public bool secondAttack;


    void Awake()
    {
        hands = new List<MainHand>();
        foreach(GameObject player in players)
        {
            hands.Add(player.GetComponent<MainHand>());
        }
        foreach(Transform child in attackButtons.transform)
        {
            child.gameObject.SetActive(false);
        }
        menuBG.SetActive(false);
        revealCovers = new List<GameObject>();

        mending = ElixirMenu.transform.Find("mendingCount").GetComponent<Text>();
        energy = ElixirMenu.transform.Find("energyCount").GetComponent<Text>();
        tainted = ElixirMenu.transform.Find("taintedCount").GetComponent<Text>();

        mTint = ElixirMenu.transform.Find("mTint").gameObject;
        eTint = ElixirMenu.transform.Find("eTint").gameObject;
        tTint = ElixirMenu.transform.Find("tTint").gameObject;

        dead = 0;
        lastAlive = -1;
        skipScan = false;
        secondAttack = false;
        menuPlayer = null;
    }
    void Start()
    {
        blue = attackButtons.transform.GetChild(0).gameObject;
        purple = attackButtons.transform.GetChild(1).gameObject;
        red = attackButtons.transform.GetChild(2).gameObject;
        yellow = attackButtons.transform.GetChild(3).gameObject;

        LayoutGame();
    }

    void Update()
    {
        if (gameState == GameStates.preRound)
        {
            if (!trigger2)
            {
                trigger1 = false;
                trigger2 = true;
                selectButtons.SetActive(true);
                selectButtons.transform.Find("Blue").gameObject.SetActive(false);
                selectButtons.transform.Find("Purp").gameObject.SetActive(false);
                selectButtons.transform.Find("Red").gameObject.SetActive(false);
                selectButtons.transform.Find("Yell").gameObject.SetActive(false);
            }

            if (hands[0].clanCard == null)
            {
                selectButtons.transform.Find("Blue").gameObject.SetActive(true);
            }
            else
            {

            }
            if (hands[1].clanCard == null)
            {
                selectButtons.transform.Find("Purp").gameObject.SetActive(true);
            }
            if (hands[2].clanCard == null)
            {
                selectButtons.transform.Find("Red").gameObject.SetActive(true);
            }
            if (hands[3].clanCard == null)
            {
                selectButtons.transform.Find("Yell").gameObject.SetActive(true);
            }

            if (hands[0].clanCard != null && hands[1].clanCard != null && hands[2].clanCard != null && hands[3].clanCard != null)
            {
                foreach (GameObject cover in revealCovers)
                {
                    cover.SetActive(false);
                }
                gameState = GameStates.playRegRound;
            }
        }

        
        if (dead == 3)
        {
            dead = 0;
            EndRound();
        }
    }

    void FixedUpdate()
    {
        if (players[0].GetComponent<MainHand>().clanCard == null) blue.SetActive(false);
        if (players[1].GetComponent<MainHand>().clanCard == null) purple.SetActive(false);
        if (players[2].GetComponent<MainHand>().clanCard == null) red.SetActive(false);
        if (players[3].GetComponent<MainHand>().clanCard == null) yellow.SetActive(false);

        //Debug.Log(gameState);

        foreach (GameObject player in players)
        {
            if (player.GetComponent<MainHand>().clanCard == null) return;
            GameObject card = player.GetComponent<MainHand>().clanCard;
            card.transform.position = player.transform.position;
            card.transform.eulerAngles = player.transform.eulerAngles;
            card.GetComponent<SpriteRenderer>().sortingOrder = 2;

            if (gameState == GameStates.select)
            { 
                if (player.GetComponent<MainHand>().clanCard == null)
                {
                    if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(true);
                    if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(true);
                    if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(true);
                    if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(true);
                }
                else
                {
                    if (player == players[0]) selectButtons.transform.Find("Blue").gameObject.SetActive(false);
                    if (player == players[1]) selectButtons.transform.Find("Purp").gameObject.SetActive(false);
                    if (player == players[2]) selectButtons.transform.Find("Red").gameObject.SetActive(false);
                    if (player == players[3]) selectButtons.transform.Find("Yell").gameObject.SetActive(false);
                }
            }            
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
                    gameState = GameStates.playFirstRound;
                }
                break;

            case GameStates.playFirstRound:
                if (trigger) return;
                trigger = true;
                foreach(MainHand hand in hands)
                {
                    hand.SetMaxHealthSlider(); // sets the healthbar max value to the starting health
                    hand.healthSlider.gameObject.SetActive(true);
                }
                attackerInt = FindFirstAttacker();
                SetAttackButtons(players[attackerInt]);
                break;

            case GameStates.playRegRound:
                if (trigger1) return;
                trigger = false;
                trigger1 = true;
                foreach (MainHand hand in hands)
                {
                    if(hand.gameObject != players[lastAlive]) hand.SetMaxHealthSlider(); // sets the healthbar max value to the starting health
                    hand.healthSlider.gameObject.SetActive(true);
                }
                ResetAttackButtons();
                SetAttackButtons(players[lastAlive]);
                break;

            case GameStates.endRound:
                break;

            
        }
    }

    public void FillEquipment(int player)
    {
        for(int i = 0; i<4; i++)
        {
            Debug.Log("Player: " + i + " on fillCheck0");
            if (hands[i] == hands[player])
            {
                Debug.Log("Player: " + i + " on fillCheck1");
                if (hands[i].equipCard == null)
                {
                    Debug.Log("Player: " + i + " on fillCheck2");
                    GameObject equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
                    while (equipGO.GetComponent<EquipCard>().InPlay == true)
                    {
                        equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
                    }
                    equipGO.GetComponent<EquipCard>().InPlay = true;

                    equipGO.transform.position = players[i].transform.position;
                    equipGO.transform.rotation = players[i].transform.rotation;
                    hands[i].SetEquip(equipGO);
                    Debug.Log("Player " + i + ": " + equipGO.name);
                }
            }
        }
    }
  
    void EndRound()
    {
        if (hands[0].clanCard != null) lastAlive = 0;
        if (hands[1].clanCard != null) lastAlive = 1;
        if (hands[2].clanCard != null) lastAlive = 2;
        if (hands[3].clanCard != null) lastAlive = 3;

        trigger2 = false;

        gameState = GameStates.endRound;
        winMenu.SetActive(true);
    }

    public void NextTurn()
    {
        MainHand hand = players[attackerInt].GetComponent<MainHand>();
        if (hand.enerActive)
        {
            secondAttack = true;
            hand.enerActive = false;
        }
        if (secondAttack)
        {
            secondAttack = false;            
        }
        else
        {
            attackerInt++;
            if (attackerInt >= 4) attackerInt = 0;
            hand = players[attackerInt].GetComponent<MainHand>();
            if (hand.clanCard == null) NextTurn();            
        }
        ResetAttackButtons();
        SetAttackButtons(players[attackerInt]);
    }

    public void UseMend()
    {
        menuPlayer.UseMending();
    }
    public void UseEner()
    {
        menuPlayer.UseEnergy();
    }
    public void UseTain()
    {
        menuPlayer.UseTainted();
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
        if (attacker.name == "BLUE")
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
        foreach (MainHand hand in hands)
        {
            hand.healthSlider.maxValue = 0;
            hand.healthSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20.2f);
        }
        foreach (GameObject player in players)
        {
            GameObject equipGO = CardGeneration.S.equipDeck[Random.Range(0, 8)];
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
        int param = 0; //for sequential function 'SetCardButtons'
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

            if (i > 4)
            {
                card.transform.localPosition = new Vector3(position[0] + (xOffset * z), position[1] - yOffset, position[2]);
                i++;
                z++;
            }
            if (i <= 4)
            {
                card.transform.localPosition = new Vector3(position[0] + (xOffset * i), position[1], position[2]);
                i++;
            }            
            
        }
        SetCardButtons(param);
    }

    public void SetCardButtons(int player)
    {
        ClanDeck deck = players[player].transform.GetChild(0).gameObject.GetComponent<ClanDeck>();
        topRow[4].SetActive(false);

        if (deck.bladedancer.isDead) topRow[0].SetActive(false);
        else topRow[0].SetActive(true);
        if (deck.beserker.isDead) topRow[1].SetActive(false);
        else topRow[1].SetActive(true);
        if (deck.ranger.isDead) topRow[2].SetActive(false);
        else topRow[2].SetActive(true);
        if (deck.warrior.isDead) topRow[3].SetActive(false);
        else topRow[3].SetActive(true);
        if (deck.sureshot.isDead) bottomRow[0].SetActive(false);
        else bottomRow[0].SetActive(true);
        if (deck.headsman.isDead) bottomRow[1].SetActive(false);
        else bottomRow[1].SetActive(true);
        if (deck.brute.isDead) bottomRow[2].SetActive(false);
        else bottomRow[2].SetActive(true);
        if (deck.shieldmaiden.isDead) bottomRow[3].SetActive(false);
        else bottomRow[3].SetActive(true);
        if (deck.goliath.isDead) bottomRow[4].SetActive(false);
        else bottomRow[4].SetActive(true);

        if (deck.ClanDeath()) topRow[4].SetActive(true);
    }

    public void SelectCard(int ind)
    {
        int play = -1;
        GameObject player = null;
        if (archID == "BlueDeck") play = 0;
        if (archID == "PurpDeck") play = 1;
        if (archID == "RedDeck") play = 2;
        if (archID == "YellDeck") play = 3;

        player = players[play];

        if (player == null) return;
        GameObject playedCard = GameObject.FindGameObjectWithTag(archID).transform.GetChild(ind).gameObject;
        player.GetComponent<MainHand>().SetClanCard(playedCard);
        /*
        player.GetComponent<MainHand>().clanCard = playedCard;
        Debug.Log(playedCard.name + "   " + player.GetComponent<MainHand>().clanCard.name);
        playedCard.transform.position = player.transform.position;
        */
        playedCard.transform.eulerAngles = player.transform.eulerAngles;
        playedCard.GetComponent<SpriteRenderer>().sortingOrder = 1;
        playedCard.GetComponent<ClanCard>().inPlay = true;        
        
        GameObject tempCover = Instantiate(CardGeneration.S.DeckCover, playedCard.transform);
        tempCover.transform.localScale = new Vector2(16, 23);
        tempCover.transform.eulerAngles = player.transform.eulerAngles;
        tempCover.GetComponent<SpriteRenderer>().sortingOrder = 2;
        revealCovers.Add(tempCover);

        FillEquipment(play);
    }

    public void DeckReturn()
    {
        players[1].transform.position = new Vector2(-7, 1);
        players[1].transform.eulerAngles = new Vector3(0,0,270);
      
        players[2].transform.position = new Vector2(1,4.75f);
       
        players[3].transform.position = new Vector2(7, -1);
        players[3].transform.eulerAngles = new Vector3(0,0,90);
      
        foreach (Transform card in GameObject.FindGameObjectWithTag("BlueDeck").transform)
        {            
            if (card.tag == "cover") card.gameObject.SetActive(true);
            if (players[0].GetComponent<MainHand>().clanCard != null)
            {
                if (card.gameObject.name == players[0].GetComponent<MainHand>().clanCard.name) { }
            }
            else
            {
                card.transform.position = GameObject.FindGameObjectWithTag("BlueDeck").transform.position;
                card.transform.rotation = players[0].transform.rotation;
            }
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("PurpDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            if (players[1].GetComponent<MainHand>().clanCard != null)
            {
                if (card.gameObject.name == players[1].GetComponent<MainHand>().clanCard.name) { }                
            }
            else
            {
                card.transform.position = GameObject.FindGameObjectWithTag("PurpDeck").transform.position;
                card.transform.rotation = players[1].transform.rotation;
            }
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("RedDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            if (players[2].GetComponent<MainHand>().clanCard != null)
            {
                if (card.gameObject.name == players[2].GetComponent<MainHand>().clanCard.name) { }                
            }
            else
            {
                card.transform.position = GameObject.FindGameObjectWithTag("RedDeck").transform.position;
                card.transform.rotation = players[2].transform.rotation;
            }
        }
        foreach (Transform card in GameObject.FindGameObjectWithTag("YellDeck").transform)
        {
            if (card.tag == "cover") card.gameObject.SetActive(true);
            if(players[3].GetComponent<MainHand>().clanCard != null)
            {
                if (card.gameObject.name == players[3].GetComponent<MainHand>().clanCard.name) { }
            }
            else
            {
                card.transform.position = GameObject.FindGameObjectWithTag("YellDeck").transform.position;
                card.transform.rotation = players[3].transform.rotation;
            }
        }
    }    

    public void EquipCleanup()
    {
        if (lastAlive != 0 && hands[0].equipCard != null)
        {
            hands[0].equipCard.transform.position = new Vector3(0, 0, -20);
            hands[0].equipCard.transform.eulerAngles = Vector3.zero;
            hands[0].equipCard.GetComponent<EquipCard>().InPlay = false;
            hands[0].equipCard = null;
        }
        if (lastAlive != 1 && hands[1].equipCard != null)
        {
            hands[1].equipCard.transform.position = new Vector3(0, 0, -20);
            hands[1].equipCard.transform.eulerAngles = Vector3.zero;
            hands[1].equipCard.GetComponent<EquipCard>().InPlay = false;
            hands[1].equipCard = null;
        }
        if (lastAlive != 2 && hands[2].equipCard != null)
        {
            hands[2].equipCard.transform.position = new Vector3(0, 0, -20);
            hands[2].equipCard.transform.eulerAngles = Vector3.zero;
            hands[2].equipCard.GetComponent<EquipCard>().InPlay = false;
            hands[2].equipCard = null;
        }
        if (lastAlive != 3 && hands[3].equipCard != null)
        {
            hands[3].equipCard.transform.position = new Vector3(0, 0, -20);
            hands[3].equipCard.transform.eulerAngles = Vector3.zero;
            hands[3].equipCard.GetComponent<EquipCard>().InPlay = false;
            hands[3].equipCard = null;
        }
        Debug.Log("Last Alive: " + lastAlive);
        gameState = GameStates.preRound;
    }

    public void DrawElixir()
    {
        GameObject elix = CardGeneration.S.elixirDeck[2]; //change this to Random.Range(0, 11)
        hands[lastAlive].elixirs.Add(elix);
        
        if (elix.tag == "Mending") hands[lastAlive].mend.Add(elix);
        if (elix.tag == "Energy") hands[lastAlive].ener.Add(elix);
        if (elix.tag == "Tainted") hands[lastAlive].tain.Add(elix);
        EquipCleanup();
    }
}
