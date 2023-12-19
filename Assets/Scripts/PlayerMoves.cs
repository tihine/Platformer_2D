using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoves : MonoBehaviour
{
    [Header("Game Design Settings")]
    [SerializeField] float speed = 4f;
    [SerializeField] int sprint_factor = 3;
    [SerializeField] int dash_factor = 4;
    [SerializeField] float dash_timer = 0.5f;
    [SerializeField] public float energy;
    [SerializeField] float energy_loss_sprint;
    [SerializeField] private float distanceBeforeDeath = -8;
    
    [Header("Others Settings")]
    [SerializeField] float new_speed;
    Transform player_transform;
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem dash_particles;
    [SerializeField] public ParticleSystem death_particules;
    bool isMoving = false;
    public bool isSprinting = false;
    public bool isDashing = false;
    Vector2 direction = Vector2.zero;
    [SerializeField] public EnergyBar energyBar;
   
    [HideInInspector]
    public bool malusEnergy = false;
    [HideInInspector]
    public bool OnPenduleGrabb;
    float vitesse;
    [HideInInspector]
    public Vector3 startingPosition;
    private bool isDying = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player_transform=GetComponent<Transform>();
        new_speed = speed;
        energy = 20f;
        energyBar.SetEnergy(energy);
        StartCoroutine(RestoreEnergyCoroutine());
        particles.gameObject.SetActive(false);
        OnPenduleGrabb = false;
        direction = Vector2.zero;
        isMoving = false;
        startingPosition = transform.position;
        vitesse = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving & !OnPenduleGrabb)
        {
            if (vitesse < new_speed)
            {
                vitesse += new_speed / 20;
            }
            else
            {
                vitesse = new_speed;
            }
            player_transform.Translate(direction*Time.fixedDeltaTime*vitesse);
        }
        if(!isMoving & !OnPenduleGrabb)
        {
            if(vitesse > 0)
            {
                vitesse -= new_speed / 20;
            }
            else
            {
                vitesse = 0;
            }
            player_transform.Translate(direction * Time.fixedDeltaTime * vitesse);
        }

        sprint(isSprinting);

        if (transform.position.y < distanceBeforeDeath)
        {
            Die();
        }
    }

    public void SetMoving(bool moving)
    {
        isMoving = moving;
        if(moving)
        {
            new_speed = speed;
        }
        else
        {
            new_speed = 0;
        }
    }
    public float GetVitesseX()
    {
        if(direction.x < 0)
        {
            return -new_speed;
        }
        else if (direction.x >0)
        {
            return new_speed;
        }
        else
        {
            return 0;
        }   
    }
    public bool GetIsSprinting()
    {
        return isSprinting;
    }
    
    public void SetSpeed(float newSpeed)
    {
        new_speed = newSpeed;
    }
    
    public float GetSpeed()
    {
        return new_speed;
    }
    
    public float GetOriginalSpeed()
    {
        return speed;
    }
    
    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void setOnPendule(bool Onpendule)
    {
        OnPenduleGrabb= Onpendule;
    }
    public void moveCharacter(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction.x > 0)
        {
            particles.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (direction.x < 0)
        {
            particles.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
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
            new_speed = 0;
        }
    }
    public float getDirection()
    {
        return direction.x;
    }

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            StartCoroutine(DieCoroutine(0.5f));
        }
    }

    private IEnumerator DieCoroutine(float secondsBeforeRespawn)
    {
        SoundSingleton.Instance.PlayDie();
        yield return new WaitForSeconds(secondsBeforeRespawn);
        transform.position = startingPosition;
        StartCoroutine(RestoreEnergyCoroutine());
        isDying = false;
    }

    #region sprint
    public void canSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isSprinting = true;
            SoundSingleton.Instance.PlaySprint();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            isSprinting = false;
            
        }
    }

    public void sprint(bool isSprinting)
    {
        if (isSprinting && (energy > 0))
        {
            new_speed = speed * sprint_factor;
            particles.gameObject.SetActive(true);
            energy -= energy_loss_sprint;
            energyBar.SetEnergy(energy);
            if (energy <= 0)
            {
                malusEnergy = true;
            }
        }
        else
        {
            new_speed = speed;
            particles.gameObject.SetActive(false);
        }
    }
    #endregion sprint

    #region dash
    public void canDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isDashing = true;
            dash();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isDashing = false;

        }
    }
    public void dash()
    {
        if (energy > 8f)
        {
            SoundSingleton.Instance.PlayDash();
            float dash_duration = 0f;
            new_speed = speed * dash_factor;
            dash_particles.Play();
            while (dash_duration < dash_timer)
            {
                player_transform.Translate(direction * Time.fixedDeltaTime * new_speed);
                dash_duration += Time.fixedDeltaTime;
            }
            energy -= 8f;
            energyBar.SetEnergy(energy);
            if (energy <= 0)
            {
                malusEnergy = true;
            }
            new_speed = speed;
            dash_duration = 0f;
        }
    }
    #endregion dash

    
    public IEnumerator RestoreEnergyCoroutine()
    {
        if (energy < 20)
        {
            if (malusEnergy)
            {
                for (int i = 0; i < 19; i++)
                {
                    energy += 1;
                    energyBar.SetEnergy(energy);
                    yield return new WaitForSeconds(1.5f);
                }
                malusEnergy = false;
            }
            energy += 1;
            energyBar.SetEnergy(energy);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(RestoreEnergyCoroutine());

    }
}
