using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovementScript;
    float horizontal;
    Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gamepad = Gamepad.current;
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal == 0)
        {
            Vector2 move = gamepad.dpad.ReadValue();
            {
                horizontal = move.x;

            }
        }
        if (horizontal == 0)
        {
            Vector2 move2 = gamepad.leftStick.ReadValue();
            {
                horizontal = move2.x;
            }
        }
        playerMovementScript.moveCharacter(new Vector3(horizontal, 0, 0));
    }
}
