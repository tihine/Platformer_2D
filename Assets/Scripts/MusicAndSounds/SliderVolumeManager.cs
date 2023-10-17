using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeManager : MonoBehaviour
{
    [SerializeField] private Slider volGenSlider;
    [SerializeField] private Slider volSonSlider;
    [SerializeField] private Slider volMusiqueSlider;
    
    public void Start()
    {
        volGenSlider.value = SoundManager.Instance.VolumeGeneral;
        volSonSlider.value = SoundManager.Instance.VolumeSons;
        volMusiqueSlider.value = SoundManager.Instance.VolumeMusiques;
        
        Volume.changeVolume(volGenSlider.value);
        Volume.changeVolumeSons(volSonSlider.value);
        Volume.changeVolumeMusiques(volMusiqueSlider.value);

        volGenSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        volSonSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
        volMusiqueSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }
	
    public void ValueChangeCheck()
    {
        Volume.changeVolume(volGenSlider.value);
        Volume.changeVolumeSons(volSonSlider.value);
        Volume.changeVolumeMusiques(volMusiqueSlider.value);
    }

}
