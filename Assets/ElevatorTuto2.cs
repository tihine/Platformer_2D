using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTuto2 : MonoBehaviour
{
    public bool contactElevator = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Player")
        {
            contactElevator=true;
        }

    }
}
