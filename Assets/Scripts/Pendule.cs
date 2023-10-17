using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Linq.Expressions;

public class Pendule : MonoBehaviour
{
    bool OnPendule = false;
    bool Grabbing = false;
    private Jump jumpScript;
    GameObject currentPendule;
    Vector3 RelativePos;
    // Start is called before the first frame update
    void Start()
    {
        OnPendule = false;
        Grabbing = false;
        jumpScript = GetComponent<Jump>();
    }

    public void OnPenduleEnter()
    {
        OnPendule = true;   
    }
    public void OnPenduleExit()
    {
        OnPendule = false;  
    }
    public void OnGrabbEnter()
    {
        Grabbing = true;
    }
    public void OnGrabbExit()
    {
        Grabbing = false;
        jumpScript.OnFall();
    }
    public void SetCurrentPendule(GameObject pendule)
    {
        currentPendule = pendule;
    }

    public void GrabLiane(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            OnGrabbEnter();
            if (OnPendule)
            {
                jumpScript.OnGround();
                RelativePos = new Vector3(
                    transform.position.x - currentPendule.transform.position.x,
                    transform.position.y - currentPendule.transform.position.y - currentPendule.GetComponent<Renderer>().bounds.size.y / 2,
                    0
                    );
            }
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            OnGrabbExit();
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Grabbing & OnPendule)
        {
            transform.position = RelativePos;
            print("Sur le pendule");
        }

    }
}
