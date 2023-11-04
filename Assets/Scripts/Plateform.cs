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

    [SerializeField]
    float tetamax;
    public Plateform_type GetTypePlateform()
    {
        return type;
    }
    public float GetTetaMax()
    {
        return tetamax;
    }
}

