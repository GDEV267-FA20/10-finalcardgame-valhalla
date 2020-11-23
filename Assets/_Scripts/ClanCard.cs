using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClanCard : MonoBehaviour
{
    int sortingLayer;
    bool faceUp;
    GameObject cover;
    public bool inPlay;

    [Header("Put in")]
    public GameObject FaceDownPrefab;

    void Start()
    {
        inPlay = false;
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
