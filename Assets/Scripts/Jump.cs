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

    public void Plafond()
    {
        isJumping = false;
        isFalling = true;
    }
    public void OnGround()
    {
        isOnGround = true;
        isFalling = false;
        nbPressedXButton = 0;
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
                isJumping = true;
                isOnGround = false;
                isFalling= false;
            }
        }
    }

    void Update()
    {
        if(isJumping)
        {
            Debug.Log("jumping");
            if (time < timer)
            {
                Debug.Log(timer);
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
