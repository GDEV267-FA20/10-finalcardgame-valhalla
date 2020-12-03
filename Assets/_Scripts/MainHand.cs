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

    private HealthIndecator sliderCode;

    void Start()
    {
        valhalla = Camera.main.GetComponent<Valhalla>();
        sliderCode = healthSlider.gameObject.GetComponent<HealthIndecator>();
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

        sliderCode.Health = damage * -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
