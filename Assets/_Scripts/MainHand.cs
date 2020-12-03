using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainHand : MonoBehaviour
{
    public Valhalla valhalla;
    public GameObject equipCard;
    public GameObject clanCard;

    public Slider healthSlider;


    void Start()
    {
        valhalla = Camera.main.GetComponent<Valhalla>();
        
    }

    public void Attack()
    {
        int mod = 0;
        mod += valhalla.players[valhalla.attackerInt].GetComponent<MainHand>().equipCard.GetComponent<EquipCard>().Weap;
        mod += this.equipCard.GetComponent<EquipCard>().Arm;

        int damage = 3 + Random.Range(0, 3);
        damage += mod;

        Debug.Log("start health: "+ this.clanCard.GetComponent<ClanCard>().Health + " - " + damage +" mod: "+mod);
        this.clanCard.GetComponent<ClanCard>().Health -= damage;
        Debug.Log(this.clanCard.GetComponent<ClanCard>().Health);

        healthSlider.value = this.clanCard.GetComponent<ClanCard>().Health;


    }

    public void SetMaxHealthSlider()
    {
        healthSlider.maxValue = clanCard.GetComponent<ClanCard>().Health;
        healthSlider.value = healthSlider.maxValue;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
