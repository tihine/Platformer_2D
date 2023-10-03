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
        isFalling = false;
        isOnGround = true;
        nbPressedXButton = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        isFalling = false;
        isOnGround = true;
        nbPressedXButton = 0;
    }

    void Update()
    {
        if((Gamepad.current.aButton.wasPressedThisFrame || Input.GetKeyDown(KeyCode.Space)) && (isOnGround || nbPressedXButton < 2))
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
            Falling();
        }
    }
}
