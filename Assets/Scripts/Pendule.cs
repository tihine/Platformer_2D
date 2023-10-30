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
    Transform currentPendule;
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
        Grabbing = true;
        if (OnPendule)
        {
            jumpScript.OnGround();
            Longueurpendule = (currentOriginPendule - transform.position).magnitude / 5;
            playerMovesScript.setOnPendule(Grabbing);
        }
    }
    public void OnGrabbExit()
    {
        Grabbing = false;
        if (OnPendule)
        {
            jumpScript.OnFall();
            playerMovesScript.setOnPendule(Grabbing);
        }
    }
    public void SetCurrentPendule(GameObject pendule)
    {
        if(pendule.transform.parent != currentPendule)
        {
            VitTetaPendule = 0f;
            AccTetaPendule = 0f;
        }
        currentPendule = pendule.transform.parent;
        Longueurpendule = 1f;
        PosTetaPendule = currentPendule.transform.rotation.eulerAngles.z;
        currentOriginPendule = currentPendule.transform.position;
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
            if (Grabbing & OnPendule)
            {
                print("grabbing");
                Longueurpendule = (currentOriginPendule - transform.position).magnitude;
                float vitX = playerMovesScript.GetVitesseX();
                PosTetaPendule += VitTetaPendule * Time.fixedDeltaTime;
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule * Mathf.PI / 180) / Longueurpendule + Mathf.Cos(PosTetaPendule * Mathf.PI / 180) * vitX*Mathf.Cos(PosTetaPendule*Mathf.PI/180)/50 / Time.fixedDeltaTime;
                transform.position = new Vector3(currentOriginPendule.x + Longueurpendule * Mathf.Sin(PosTetaPendule * Mathf.PI / 180), currentOriginPendule.y - Longueurpendule * Mathf.Cos(PosTetaPendule * Mathf.PI / 180), 0);
            }
            else
            {
                Longueurpendule = 1f;
                PosTetaPendule += VitTetaPendule * Time.fixedDeltaTime; 
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule*Mathf.PI/180) / Longueurpendule;
            }
            currentPendule.Rotate(new Vector3(0, 0, 1), PosTetaPendule-currentPendule.rotation.eulerAngles.z);
        }
    }

    public bool GetGrabbing()
    {
        return Grabbing;
    }
}
