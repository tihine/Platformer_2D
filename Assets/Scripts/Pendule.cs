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
    private PlayerMoves playerMovesScript;
    GameObject currentPendule;
    Vector3 currentOriginPendule;
    float PosTetaPendule;
    float VitTetaPendule;
    float AccTetaPendule;
    float Longueurpendule;
    float gravity;
    // Start is called before the first frame update
    void Start()
    {
        OnPendule = false;
        Grabbing = false;
        jumpScript = GetComponent<Jump>();
        gravity = 9.81f;
        playerMovesScript = GetComponent<PlayerMoves>();
        PosTetaPendule = 0f;
        VitTetaPendule = 0f;
        AccTetaPendule = 0f;
        Longueurpendule = 1f;
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
        print("GrabbEnter");
        Grabbing = true;
        if (OnPendule)
        {
            jumpScript.OnGround();
            Longueurpendule = (currentOriginPendule - transform.position).magnitude;
        }
    }
    public void OnGrabbExit()
    {
        print("GrabbExit");
        Grabbing = false;
        if (OnPendule)
        {
            jumpScript.OnFall();
        }
    }
    public void SetCurrentPendule(GameObject pendule)
    {
        currentPendule = pendule;
        currentOriginPendule = new Vector3(currentPendule.transform.position.x, currentPendule.transform.position.y + currentPendule.GetComponent<Renderer>().bounds.size.y / 2, 0);
        PosTetaPendule = currentPendule.transform.rotation.eulerAngles.z;
        VitTetaPendule = 0f;
        AccTetaPendule = 0f;
    }

    public void GrabLiane(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            OnGrabbEnter();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            OnGrabbExit();
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentPendule)
        {
            Longueurpendule = (currentOriginPendule - transform.position).magnitude;
            if (Grabbing & OnPendule)
            {
                print("Sur le pendule");
                float vitX = playerMovesScript.GetVitesseX();
                PosTetaPendule = VitTetaPendule * Time.fixedDeltaTime;
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule) / Longueurpendule + Mathf.Cos(PosTetaPendule) * vitX / Time.fixedDeltaTime / Longueurpendule;
            }
            else
            {
                PosTetaPendule = VitTetaPendule * Time.fixedDeltaTime;
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule) / Longueurpendule;
            }
            currentPendule.transform.RotateAround(currentOriginPendule ,new Vector3(0, 0, 1), PosTetaPendule);
        }
    }
}
