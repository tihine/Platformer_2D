using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    private void Start()
    {
        AudioListener.volume = SoundManager.Instance.VolumeGeneral;
    }

    public static void changeVolume(float newVolume)
    {
        SoundManager.Instance.VolumeGeneral = newVolume;
        AudioListener.volume = SoundManager.Instance.VolumeGeneral;
    }

    public static void changeVolumeSons(float newVolume)
    {
        SoundManager.Instance.VolumeSons = newVolume;
        AudioListener.volume = SoundManager.Instance.VolumeGeneral * SoundManager.Instance.VolumeSons;
    }
    
    public static void changeVolumeMusiques(float newVolume)
    {
        SoundManager.Instance.VolumeMusiques = newVolume;
        AudioListener.volume = SoundManager.Instance.VolumeGeneral * SoundManager.Instance.VolumeMusiques;
    }
}
