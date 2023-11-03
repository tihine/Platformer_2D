using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Plateform_type
{
    normal,
    wall,
    bounce,
    pass, 
    liane,
    elevator,
}
public class Plateform : MonoBehaviour
{
    [SerializeField]
    Plateform_type type;

    public Plateform_type GetTypePlateform()
    {
        return type;
    }
}

