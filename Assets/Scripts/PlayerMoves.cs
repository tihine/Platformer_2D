using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] int sprint_factor = 3;
    [SerializeField] int dash_factor = 20;
    [SerializeField] float dash_timer = 1f;
    [SerializeField] float new_speed;
    Transform player_transform;
    bool isMoving = false;
    bool isSprinting = false;
    Vector2 direction = Vector2.zero;
    [SerializeField] float sprintDuration = 2f;
    float sprintRecovery = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player_transform=GetComponent<Transform>();
        new_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            player_transform.Translate(direction*Time.deltaTime*new_speed);
        }

        sprint(isSprinting);
    }

    public void moveCharacter(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        direction.y = 0;
        if (context.phase == InputActionPhase.Started)
        {
            isMoving = true;
        }
        if (context.phase == InputActionPhase.Performed)
        {

        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isMoving = false;
        }
    }

    
    #region sprint
    public void canSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isSprinting = true;
            new_speed = speed * sprint_factor;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            isSprinting = false;
            new_speed = speed;
        }
    }

    public void sprint(bool isSprinting)
    {
        if (isSprinting && (sprintDuration >= 0))
        {
            sprintDuration -= Time.deltaTime;
            Debug.Log("spriint");
            Debug.Log(sprintDuration);
        }
        else
        {
            if (sprintDuration <= 0f) //!!!sprintDuration se régénère seulement quand on a écoulé les 2sec
            {
                sprintRecovery += Time.deltaTime;
                if (sprintRecovery > 3f)
                {
                    sprintDuration = 2f;
                    sprintRecovery = 0f;
                }
            }
        }
    }
    #endregion sprint

    public void canDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("dashing");
            dash();
        }
    }
    public void dash()
    {
        float dash_duration = 0f;
        new_speed = speed * dash_factor;
        while (dash_duration < dash_timer)
        {
            Debug.Log(dash_duration);
            dash_duration += Time.deltaTime;
        }
        new_speed = speed;
        dash_duration = 0f;
    }
    
}
