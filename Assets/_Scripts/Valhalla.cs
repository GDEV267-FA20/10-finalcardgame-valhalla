using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valhalla : MonoBehaviour
{

    [Header("all you bruv")]
    public List<GameObject> players;


    void Awake()
    {
    }
    void Start()
    {       
        LayoutGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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

            StartCoroutine(Wait(0.5f));
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
