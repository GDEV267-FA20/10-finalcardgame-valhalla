using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardPhase {
    idle,
    selectMenu
}


public class ClanCard : MonoBehaviour
{
    int sortingLayer;
    bool faceUp;
    GameObject cover;
    public CardPhase phase;

    [Header("Put in")]
    public GameObject FaceDownPrefab;

    void Start()
    {
        faceUp = true;
    }

    void Update()
    {
        if (faceUp == false)
        {
            cover = Instantiate(FaceDownPrefab, this.transform);
        }
        if(faceUp == true)
        {
            Destroy(cover);
        }        
    }
}
