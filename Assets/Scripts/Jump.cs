using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float masse = 2;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float timeOfJump = 0.5f;
    private float timer;
    private float time;
    
    public bool isFalling = false;
    private bool isJumping = true;
    private bool isOnGround = true;
    private float vitesse;
    private float acceleration;
    
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
            transform.position += Vector3.up * (vitesse * Time.fixedDeltaTime);
            vitesse += acceleration * Time.fixedDeltaTime;
            acceleration = -gravity;
        }
    }

    public void Plafond()
    {
        isJumping = false;
        vitesse = 0;
        acceleration = 0;
        isFalling = true;
    }
    public void OnGround()
    {
        isOnGround = true;
        isFalling = false;
        vitesse = 0;
        acceleration = 0;
        nbPressedXButton = 0;
    }
    public void SetNbPressedXButton(int nbX)
    {
        nbPressedXButton = nbX;
    }
    public void OnFall()
    {
        isOnGround = false;
        isFalling = true;
    }
    
    public void JumpOnCode()
    {
        if (isOnGround)
        {
            timer = Time.time + timeOfJump;
            time = Time.time;

            nbPressedXButton += 1;

            //On saute et on est plus sur le sol. On ne pourra plus resauter tant qu'on est pas sur le sol ! (sauf double saut)
            vitesse = vitesse + jumpSpeed;
            isJumping = true;
            isOnGround = false;
            isFalling= false;
        }
    }
    public float GetVitesseY()
    {
        if (isOnGround)
        {
            return 0;
        }
        return vitesse;
    }

    public float GetGravity()
    {
        return gravity;
    }
    
    public void Jumping(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if ((isOnGround || nbPressedXButton < 2))
            {
                timer = Time.time + timeOfJump;
                time = Time.time;

                nbPressedXButton += 1;

                //On saute et on est plus sur le sol. On ne pourra plus resauter tant qu'on est pas sur le sol ! (sauf double saut)
                vitesse = vitesse + jumpSpeed;
                isJumping = true;
                isOnGround = false;
                isFalling= false;
            }
        }
    }

    void FixedUpdate()
    {
        if(isJumping)
        {
            if (time < timer)
            {
                transform.position += Vector3.up * (vitesse*Time.fixedDeltaTime);
                vitesse += acceleration*Time.fixedDeltaTime;
                acceleration = -gravity;
                time += Time.fixedDeltaTime;
            }
            else
            {
                isFalling = true;
                isJumping = false;
            }
        }

        if (isFalling && !isJumping && !isOnGround)
        {
            Falling();
        }
        if(isOnGround) 
        {
            acceleration = 0;
            vitesse = 0;
        }
    }
}
