using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private PlayerMovement playerMovementScript;
    float horizontal;
    Gamepad gamepad;
    bool canSprint;
    float sprintDuration = 2f;
    float sprintRecovery = 0f;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gamepad = Gamepad.current;
        if ((Gamepad.current.leftTrigger.isPressed) && sprintDuration>=0) //   Input.GetAxis("LeftTrigger") > 0.5f ||
        {
            canSprint = true;
            sprintDuration -= Time.deltaTime; //substract frame time to the sprint duration
            Debug.Log("il reste" + sprintDuration);
        }
        else
        {
            canSprint = false;
            if (sprintDuration <= 0f) //!!!sprintDuration se régénère seulement quand on a écoulé les 2sec
            {
                sprintRecovery += Time.deltaTime;
                if(sprintRecovery>3f)
                {
                    sprintDuration = 2f;
                    sprintRecovery = 0f;
                }
            }
        }
        if(Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            playerMovementScript.dash(new Vector3(horizontal, 0, 0));
        }

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
        playerMovementScript.moveCharacter(new Vector3(horizontal, 0,0),canSprint);
    }

}
