using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
    public static SoundSingleton Instance = null;
    
    [Header("Musics")]
    [SerializeField] private AudioClip MenuMusic;
    [SerializeField] private AudioClip GameMusic;

    [Header("Gameplay Sounds")]
    [SerializeField] private AudioClip SprintSound;
    [SerializeField] private AudioClip DashSound;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip GrabSound;
    [SerializeField] private AudioClip AscenseurSound;
    [SerializeField] private AudioClip MoveSound;

    [Header("UI Sounds")]
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioClip WinSound;
    [SerializeField] private AudioClip DieSound;

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

    #region Music

    public void PlayMusicMenu()
    {
        SoundManager.Instance.PlayMusic(MenuMusic);
    }
    
    public void PlayMusicGame()
    {
        SoundManager.Instance.PlayMusic(GameMusic);
    }

    #endregion


    #region Sounds

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
    
    public void PlayGrab()
    {
        SoundManager.Instance.PlaySound(GrabSound);
    }
    
    public void PlayAscenseur()
    {
        SoundManager.Instance.PlaySound(AscenseurSound);
    }
    
    public void PlayMove()
    {
        SoundManager.Instance.PlaySound(MoveSound);
    }
    
    public void PlayClick()
    {
        SoundManager.Instance.PlaySound(ClickSound);
    }
    
    public void PlayWin()
    {
        SoundManager.Instance.PlaySound(WinSound);
    }
    
    public void PlayDie()
    {
        SoundManager.Instance.PlaySound(DieSound);
    }
    
    #endregion
}
