using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float masse = 2;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float timeOfJump = 1f;
    private float timer;
    private float time;
    
    public bool isFalling = false;
    private bool isJumping = true;
    private bool isOnGround = true;
    
    public int nbPressedXButton;
    void Start()
    {
        isFalling = false;
        nbPressedXButton = 0;
    }

    private void Falling()
    {
        if (!isJumping)
        {
            transform.position += Vector3.down * (Time.deltaTime * jumpSpeed * gravity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On rencontre un truc !!");
        isFalling = false;
        isOnGround = true;
        nbPressedXButton = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On rencontre un truc en trigger");
        isFalling = false;
        isOnGround = true;
        nbPressedXButton = 0;
    }

    void Update()
    {
        if(Gamepad.current.aButton.wasPressedThisFrame && (isOnGround || nbPressedXButton < 2))
        {
            timer = Time.time + timeOfJump;
            time = Time.time;

            nbPressedXButton += 1;
            Debug.Log("nbpressed : " + nbPressedXButton);
                
            //On saute et on est plus sur le sol. On ne pourra plus resauter tant qu'on est pas sur le sol ! (sauf double saut)
            isJumping = true;
            isOnGround = false;
        }
        if(isJumping)
        {
            if (time < timer)
            {
                transform.position += Vector3.up * (Time.deltaTime * jumpSpeed);
                time += Time.deltaTime;
            }
            else
            {
                isFalling = true;
                isJumping = false;
            }
        }

        if (isFalling && !isOnGround)
        {
            //Debug.Log("on tombe");
            Falling();
        }
        
        
        
        // if(Gamepad.current.aButton.wasPressedThisFrame && !isFalling)
        // {
        //     timer = Time.time + timeOfJump;
        //     time = Time.time;
        // }
        // if ((Gamepad.current.aButton.IsPressed() || Input.GetKeyDown(KeyCode.Space)) && !isFalling)
        // {
        //     nbPressedXButton += 1;
        //     Debug.Log("on appuie" + nbPressedXButton);
        //     if (nbPressedXButton == 2)
        //     {
        //         Debug.Log("on double saute");
        //         timer = Time.time + timeOfJump;
        //         time = Time.time;
        //     }
        //     if (time < timer)
        //     {
        //         transform.position += Vector3.up * (Time.deltaTime * jumpSpeed);
        //         time += Time.deltaTime;
        //     }
        //     else
        //     {
        //         isFalling = true;
        //         nbPressedXButton = 0;
        //     }
        //     
        // }
        //
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     //DEBUG to stop falling
        //     isFalling = false;
        // }
        //
        // if(Gamepad.current.aButton.wasReleasedThisFrame && nbPressedXButton != 1)
        // {
        //     isFalling = true;
        // }
        //
        // if (isFalling)
        // {
        //     Falling();
        // }
    }
}
