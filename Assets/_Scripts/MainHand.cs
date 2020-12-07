using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHand : MonoBehaviour
{
    public Valhalla valhalla;
    public GameObject equipCard;
    public GameObject clanCard;

    [Header("all you: ")]
    public Slider healthSlider;


    void Start()
    {
        valhalla = Camera.main.GetComponent<Valhalla>();
        
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
        Debug.Log("start health: "+ this.clanCard.GetComponent<ClanCard>().health + " - " + damage +" mod: "+mod);
        this.clanCard.GetComponent<ClanCard>().health -= damage;
        Debug.Log("New Health"+ this.clanCard.GetComponent<ClanCard>().health);

        healthSlider.value = this.clanCard.GetComponent<ClanCard>().health;
        if (this.clanCard.GetComponent<ClanCard>().health <= 0) this.clanCard = null;

    }

    public void SetMaxHealthSlider()
    {
        healthSlider.maxValue = clanCard.GetComponent<ClanCard>().Health;
        healthSlider.value = healthSlider.maxValue;
        healthSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(179 - ((14 - healthSlider.maxValue) * 12.5f), 20.2f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
