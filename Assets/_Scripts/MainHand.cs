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
    }

    public void Attack()
    {
        int mod = 0;
        int ind = valhalla.attackerInt;
        mod += valhalla.players[ind].GetComponent<MainHand>().clanCard.GetComponent<ClanCard>().attack;
        mod += valhalla.players[ind].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;
        mod += this.equipCard.GetComponent<EquipCard>().Arm;

        int damage = 3;
        damage += mod;
        Debug.Log("start health: "+ this.clanCard.GetComponent<ClanCard>().Health + " - " + damage +" mod: "+mod);
        this.clanCard.GetComponent<ClanCard>().Health -= damage;
        Debug.Log("New Health: "+ this.clanCard.GetComponent<ClanCard>().Health);

        healthSlider.value = this.clanCard.GetComponent<ClanCard>().Health;
        if (this.clanCard.GetComponent<ClanCard>().Health <= 0)
        {
            this.clanCard = null;
            valhalla.dead++;
            Debug.Log("Dead: "+valhalla.dead);
        }
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
        Debug.Log("Last Alive: " + lastAlive);
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

    public void SetElixMenu()
    {
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
    void Update()
    {
        if (elixirs.Count > 0) elixirButton.SetActive(true);
        else elixirButton.SetActive(false);
    }
}
