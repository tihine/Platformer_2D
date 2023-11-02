using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
    public static SoundSingleton Instance = null;

    [Header("Gameplay Sounds")]
    [SerializeField] private AudioClip SprintSound;
    [SerializeField] private AudioClip DashSound;
    [SerializeField] private AudioClip JumpSound;
    
    [Header("UI Sounds")]
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip WinSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    public void PlaySprint()
    {
        SoundManager.Instance.PlaySound(SprintSound);
    }
    
    public void PlayDash()
    {
        SoundManager.Instance.PlaySound(DashSound);
    }
    
    public void PlayJump()
    {
        SoundManager.Instance.PlaySound(JumpSound);
    }
    
    public void PlayClick()
    {
        SoundManager.Instance.PlaySound(ClickSound);
    }
    
    public void PlayWin()
    {
        SoundManager.Instance.PlaySound(WinSound);
    }
}
