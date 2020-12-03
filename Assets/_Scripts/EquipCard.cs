using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCard : MonoBehaviour
{
    private int weap;
    private int arm;
    private bool inPlay;

    public int Weap
    {
        get
        {
            return weap;
        }
        set
        {
            weap = value;
        }
    }

    public int Arm
    {
        get
        {
            return arm;
        }
        set
        {
            arm = value;
        }
    }

    public bool InPlay
    {
        get
        {
            return inPlay;
        }
        set
        {
            inPlay = value;
        }
    }



    void Start()
    {
        inPlay = false;
    }

    void Update()
    {        
    }








}
