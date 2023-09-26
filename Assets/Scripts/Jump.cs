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
    private bool isFalling = false;
    void Start()
    {
        isFalling = false;
    }

    private void Falling()
    {
        transform.position += Vector3.down * (Time.deltaTime * jumpSpeed * gravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On rencontre un truc !!");
        isFalling = false;
    }

    void Update()
    {
        if(Gamepad.current.aButton.wasPressedThisFrame && !isFalling)
        {
            timer = Time.time + timeOfJump;
            time = Time.time;
        }
        if ((Gamepad.current.aButton.IsPressed() || Input.GetKeyDown(KeyCode.Space)) && !isFalling)
        {
            if (time < timer)
            {
                transform.position += Vector3.up * (Time.deltaTime * jumpSpeed);
                time += Time.deltaTime;
            }
            else
            {
                isFalling = true;
            }
            
        }
        
        if(Gamepad.current.aButton.wasReleasedThisFrame)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            Falling();
        }
    }
}
