using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    
    public static SoundManager Instance = null;
    public float VolumeGeneral = 0.5f;
    public float VolumeSons = 1f;
    public float VolumeMusiques = 1f;

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

    private void Start()
    {
        Volume.changeVolume(VolumeGeneral);
        Volume.changeVolumeSons(VolumeSons);
        Volume.changeVolumeMusiques(VolumeMusiques);
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.clip = sound;
        soundSource.Play();
        soundSource.volume = VolumeGeneral * VolumeSons;
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
        musicSource.volume = VolumeGeneral * VolumeMusiques;
    }

    public void StopPlaying()
    {
        musicSource.clip = null;
    }

}
