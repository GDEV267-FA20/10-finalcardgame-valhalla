using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCard : MonoBehaviour
{

    private int damageDelt;
    private int damageTaken;

    private MeshRenderer meshRend;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
    }

    public int DamageDelt
    {
        get => damageDelt;
        set => damageTaken = value;
    }

    public int DamageTaken
    {
        get => damageTaken;
        set => damageTaken = value;
    }

    public void SetMaterial(Material mat)
    {
        meshRend.material = mat;
    }    
        




}
