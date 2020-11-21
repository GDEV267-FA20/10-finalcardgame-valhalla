using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanCard : MonoBehaviour
{
    int sortingLayer;
    bool faceUp;
    GameObject cover;

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
