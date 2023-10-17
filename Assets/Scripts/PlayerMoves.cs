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
    [SerializeField] ParticleSystem particles;
    [SerializeField] ParticleSystem dash_particles;
    bool isMoving = false;
    bool isSprinting = false;
    Vector2 direction = Vector2.zero;
    [SerializeField] public EnergyBar energyBar;
    [SerializeField] public float energy;
    [SerializeField] float energy_loss_sprint;
    public bool malusEnergy = false;

    // Start is called before the first frame update
    void Start()
    {
        player_transform=GetComponent<Transform>();
        new_speed = speed;
        energy = 20f;
        energyBar.SetEnergy(energy);
        StartCoroutine(RestoreEnergyCoroutine());
        particles.gameObject.SetActive(false);
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
        }
    }
    public float getDirection()
    {
        return direction.x;
    }
    
    #region sprint
    public void canSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isSprinting = true;
            
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
            Debug.Log("spriint");
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
            Debug.Log("dashing");
            dash();
        }
    }
    public void dash()
    {
        float dash_duration = 0f;
        new_speed = speed * dash_factor;
        dash_particles.Play();
        while (dash_duration < dash_timer)
        {
            Debug.Log(dash_duration);
            player_transform.Translate(direction * Time.deltaTime * new_speed);
            dash_duration += Time.deltaTime;
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
    #endregion dash

    IEnumerator RestoreEnergyCoroutine()
    {
        if (energy < 20)
        {
            if (malusEnergy)
            {
                for (int i = 0; i < 19; i++)
                {
                    energy += 1;
                    energyBar.SetEnergy(energy);
                    yield return new WaitForSeconds(0.7f);
                }
                malusEnergy = false;
            }
            energy += 1;
            energyBar.SetEnergy(energy);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(RestoreEnergyCoroutine());

    }
}
