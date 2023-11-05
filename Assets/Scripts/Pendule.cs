using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Linq.Expressions;

public class Pendule : MonoBehaviour
{
    [SerializeField]
    float speedClimb = 2f;
    [SerializeField]
    float speedBalance = 3f;
    float currenttetamax = 110f; //angle max que peut prendre teta pour eviter les tours complets
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
    bool climbing;
    float directionClimb;
    float directionBalance;
    float longueurmax;
    int canBalance; //0 no, 1 yes
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
        climbing = false;
        directionClimb = 0;
        directionBalance = 0f;
        longueurmax = 1f;
        canBalance = 1;
        currentPendule = null;  
    }

    public void OnPenduleEnter()
    {
        OnPendule = true;
    }
    public void OnPenduleExit()
    {
        OnPendule = false;
        currentPendule = null;
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
        longueurmax = pendule.transform.localScale.y;
        Longueurpendule = 1f;
        PosTetaPendule = currentPendule.transform.rotation.eulerAngles.z;
        currentOriginPendule = currentPendule.transform.position;
        currenttetamax = pendule.GetComponent<Plateform>().GetTetaMax();
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

    public void ClimbLiane(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if (context.action.phase == InputActionPhase.Started)
        {
            directionClimb = direction.y;
            directionBalance = direction.x;
            if(Grabbing & OnPendule)
            {
                climbing = true;
                VitTetaPendule += Mathf.Cos(PosTetaPendule * Mathf.PI / 180) * directionBalance * speedBalance;
            }
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            climbing = false;
            directionClimb = 0;
            directionBalance = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentPendule)
        {
            print("current pendule : " + currentPendule.name);
            if (Grabbing & OnPendule)
            {
                Longueurpendule = (currentOriginPendule - transform.position).magnitude;
                float climb = directionClimb * speedClimb * Time.fixedDeltaTime;
                if (climbing & climb < Longueurpendule & Longueurpendule - climb < longueurmax)
                {
                    Longueurpendule -= climb;
                }
                float vitX = playerMovesScript.GetVitesseX();
                PosTetaPendule += VitTetaPendule * Time.fixedDeltaTime;
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule * Mathf.PI / 180) / Longueurpendule + Mathf.Cos(PosTetaPendule * Mathf.PI / 180) * vitX * Mathf.Cos(PosTetaPendule * Mathf.PI / 180) / 50 * Time.fixedDeltaTime * canBalance;
                transform.position = new Vector3(currentOriginPendule.x + Longueurpendule * Mathf.Sin(PosTetaPendule * Mathf.PI / 180), currentOriginPendule.y - Longueurpendule * Mathf.Cos(PosTetaPendule * Mathf.PI / 180), 0);
                if (Mathf.Abs(PosTetaPendule) > currenttetamax & canBalance == 1)
                {
                    VitTetaPendule = 0;
                    canBalance = 0;
                }
                else if (Mathf.Abs(PosTetaPendule) > currenttetamax)
                {
                    canBalance = 0;
                }
                else
                {
                    canBalance = 1;
                }
            }
            else
            {
                Longueurpendule = longueurmax;
                PosTetaPendule += VitTetaPendule * Time.fixedDeltaTime; 
                VitTetaPendule += AccTetaPendule * Time.fixedDeltaTime;
                AccTetaPendule = -gravity * Mathf.Sin(PosTetaPendule*Mathf.PI/180) / Longueurpendule;
                if (Mathf.Abs(PosTetaPendule) > currenttetamax & canBalance == 1)
                {
                    VitTetaPendule = 0;
                    canBalance = 0;
                }
                else if (Mathf.Abs(PosTetaPendule) > currenttetamax)
                {
                    canBalance = 0;
                }
                else
                {
                    canBalance = 1;
                }
            }
            currentPendule.Rotate(new Vector3(0, 0, 1), PosTetaPendule-currentPendule.rotation.eulerAngles.z);
        }
    }

    public bool GetGrabbing()
    {
        return Grabbing;
    }
}
