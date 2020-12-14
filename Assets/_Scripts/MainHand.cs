using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHand : MonoBehaviour
{
    public Valhalla valhalla;
    public GameObject equipCard;
    public GameObject clanCard;
    public List<GameObject> elixirs;
    public List<GameObject> mend;
    public List<GameObject> ener;
    public List<GameObject> tain;

    public bool enerActive;
    public bool tainActive;
    public bool skipActive;
    public bool isOut = false;

    [Header("all you: ")]
    public Slider healthSlider;
    public GameObject elixirButton;


    void Start()
    {
        valhalla = Camera.main.GetComponent<Valhalla>();
        elixirs = new List<GameObject>();
        mend = new List<GameObject>();
        ener = new List<GameObject>();
        tain = new List<GameObject>();
        enerActive = false;
        tainActive = false;
        skipActive = false;
    }

    public void UseMending()
    {
        clanCard.GetComponent<ClanCard>().health += 4;
        if (clanCard.GetComponent<ClanCard>().health > clanCard.GetComponent<ClanCard>().maxHealth) clanCard.GetComponent<ClanCard>().health = clanCard.GetComponent<ClanCard>().maxHealth;
        healthSlider.value = this.clanCard.GetComponent<ClanCard>().Health;

        int i = 0;
        foreach(GameObject elix in elixirs)
        {
            if(mend[0] == elix)
            {
                mend.RemoveAt(0);
                elixirs.RemoveAt(i);
                return;
            }
            i++;
        }
    }
    public void UseEnergy()
    {
        enerActive = true;

        int i = 0;
        foreach (GameObject elix in elixirs)
        {
            if (ener[0] == elix)
            {
                ener.RemoveAt(0);
                elixirs.RemoveAt(i);
                return;
            }
            i++;
        }
    }
    public void UseTainted()
    {
        tainActive = true;

        int i = 0;
        foreach (GameObject elix in elixirs)
        {
            if (tain[0] == elix)
            {
                tain.RemoveAt(0);
                elixirs.RemoveAt(i);
                return;
            }
            i++;
        }
    }

    public void Attack()
    {
        int mod = 0;
        int ind = valhalla.attackerInt;
        MainHand hand = valhalla.players[ind].GetComponent<MainHand>();
        mod += hand.clanCard.GetComponent<ClanCard>().attack;
        mod += hand.equipCard.GetComponent<EquipCard>().Weap;
        mod += this.equipCard.GetComponent<EquipCard>().Arm;
        
        if (hand.tainActive)
        {
            Debug.Log("Skip active");
            if (this.gameObject.name == "BLUE") valhalla.skipInt = 3;
            if (this.gameObject.name == "PURPLE") valhalla.skipInt = 0; //don't ask why it's rolled back
            if (this.gameObject.name == "RED") valhalla.skipInt = 1;
            if (this.gameObject.name == "YELLOW") valhalla.skipInt = 2;

            Debug.Log("SkipInt: " + valhalla.skipInt);
            hand.tainActive = false;
        }
        
        

        int damage = 3;
        damage += mod;
        Debug.Log("start health: "+ this.clanCard.GetComponent<ClanCard>().Health + " - " + damage +" mod: "+mod);
        if (damage > 0)
        {
            this.clanCard.GetComponent<ClanCard>().Health -= damage;
            Debug.Log("New Health: " + this.clanCard.GetComponent<ClanCard>().Health);
        }
        else Debug.Log("Attack fuckin sucked m8, no damage");


        healthSlider.value = this.clanCard.GetComponent<ClanCard>().Health;
        if (this.clanCard.GetComponent<ClanCard>().Health <= 0)
        {
            this.clanCard = null;
            valhalla.dead++;            
            Debug.Log("Dead: "+valhalla.dead);
        }
        foreach(GameObject player in valhalla.players)
        {
            if (!isOut)
            {
                player.transform.GetChild(0).GetComponent<ClanDeck>().CheckHealth();
                this.transform.GetChild(2).GetComponent<DeathRep>().Check();
            }
        }        
        valhalla.NextTurn();
    }
    
    public void PlayerOut()
    {
        isOut = true;
        equipCard.transform.position = new Vector3(0, 0, -20);
        equipCard.transform.eulerAngles = Vector3.zero;
        equipCard.GetComponent<EquipCard>().InPlay = false;
        equipCard = null;

        elixirButton.transform.position = new Vector2(0, -1000);
        healthSlider.transform.parent.gameObject.SetActive(false);

        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).GetComponent<DeathRep>().Check();

        if (this.name == "BLUE")
        {
            valhalla.blue.transform.position = new Vector2(0, -1000);
            this.transform.GetChild(2).transform.position = new Vector3(0, -2.5f);
            foreach(Transform card in this.transform.GetChild(0).transform)
            {
                card.gameObject.SetActive(false);
            }

            valhalla.selectButtons.transform.Find("Blue").gameObject.SetActive(false);
        }
        if (this.name == "PURPLE")
        {
            valhalla.purple.transform.position -= new Vector3(0, -1000);
            this.transform.GetChild(2).transform.position = new Vector3(-5,0);
            foreach (Transform card in this.transform.GetChild(0).transform)
            {
                card.gameObject.SetActive(false);
            }
            valhalla.selectButtons.transform.Find("Purp").gameObject.SetActive(false);
        }
        if (this.name == "RED")
        {
            valhalla.red.transform.position -= new Vector3(0, -1000);
            this.transform.GetChild(2).transform.position = new Vector3(0, 2.5f);
            foreach (Transform card in this.transform.GetChild(0).transform)
            {
                card.gameObject.SetActive(false);
            }
            valhalla.selectButtons.transform.Find("Red").gameObject.SetActive(false);
        }
        if (this.name == "YELLOW")
        {
            valhalla.yellow.transform.position -= new Vector3(0, -1000);
            this.transform.GetChild(2).transform.position = new Vector3(5, 0);
            foreach (Transform card in this.transform.GetChild(0).transform)
            {
                card.gameObject.SetActive(false);
            }
            valhalla.selectButtons.transform.Find("Yell").gameObject.SetActive(false);
        }
        this.transform.GetChild(2).transform.parent = null;
    }

    public void SetMaxHealthSlider()
    {
        healthSlider.maxValue = clanCard.GetComponent<ClanCard>().Health;
        healthSlider.value = healthSlider.maxValue;
        healthSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(179 - ((14 - healthSlider.maxValue) * 12.5f), 20.2f);
    }

    public void Scavenge()
    {
        int lastAlive = valhalla.lastAlive;        
        List<MainHand> list = valhalla.hands;

        valhalla.hands[lastAlive].equipCard.transform.position = new Vector3(0, 0, -20);
        valhalla.hands[lastAlive].equipCard.transform.eulerAngles = Vector3.zero;
        valhalla.hands[lastAlive].equipCard = null;

        equipCard.transform.position = valhalla.players[lastAlive].transform.position;
        equipCard.transform.rotation = valhalla.players[lastAlive].transform.rotation;
        valhalla.hands[lastAlive].equipCard = equipCard;
        equipCard = null;
        
        valhalla.EquipCleanup();         
    }

    public void SetClanCard(GameObject card)
    {
        clanCard = card;
    }

    public void SetEquip(GameObject card)
    {
        equipCard = card;
    }

    public void SetElixMenu()
    {
        valhalla.menuPlayer = this;

        valhalla.mending.text = "x " + mend.Count;
        valhalla.energy.text = "x " + ener.Count;
        valhalla.tainted.text = "x " + tain.Count;

        if (mend.Count == 0) valhalla.mTint.SetActive(true);
        else valhalla.mTint.SetActive(false);
        if (ener.Count == 0) valhalla.eTint.SetActive(true);
        else valhalla.eTint.SetActive(false);
        if (tain.Count == 0) valhalla.tTint.SetActive(true);
        else valhalla.tTint.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (elixirs.Count > 0) elixirButton.SetActive(true);
        else elixirButton.SetActive(false);
    }
}
