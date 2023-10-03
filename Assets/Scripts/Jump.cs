using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float masse = 2;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float timeOfJump = 0.5f;
    private float timer;
    private float time;
    
    public bool isFalling = false;
    private bool isJumping = true;
    private bool isOnGround = true;
    private float vitesse;
    
    public int nbPressedXButton;
    void Start()
    {
        nbPressedXButton = 0;
        isOnGround = false;
        isFalling = true;
        isJumping = false;
    }

    private void Falling()
    {
        if (!isJumping)
        {
            transform.position += Vector3.down * (Time.deltaTime * jumpSpeed * gravity);
            vitesse = (Vector3.down * (Time.deltaTime * jumpSpeed * gravity)).y;
        }
    }

    public void ChangeFalling(bool isCurrentlyFalling)
    {
        isOnGround = !isCurrentlyFalling;
        isFalling = isCurrentlyFalling;
        nbPressedXButton = 0;
    }

    public float GetVitesseY()
    {
        if (isOnGround)
        {
            return 0;
        }
        return vitesse;
    }

    void Update()
    {
        if((Gamepad.current.aButton.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Space)) && (isOnGround || nbPressedXButton < 2))
        {
            timer = Time.time + timeOfJump;
            time = Time.time;

            nbPressedXButton += 1;
            //Debug.Log("nbpressed : " + nbPressedXButton);
                
            //On saute et on est plus sur le sol. On ne pourra plus resauter tant qu'on est pas sur le sol ! (sauf double saut)
            isJumping = true;
            isOnGround = false;
        }
        if(isJumping)
        {
            if (time < timer)
            {
                transform.position += Vector3.up * (Time.deltaTime * jumpSpeed);
                vitesse = (Vector3.up * (Time.deltaTime * jumpSpeed)).y;
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
            Falling();
        }
    }
}
