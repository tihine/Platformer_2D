using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ManagerClickSound : MonoBehaviour
{
    [SerializeField] private List<Button> buttonsList;
    
    void Start()
    {
        var instance = SoundSingleton.Instance;
        foreach (var button in buttonsList)
        {
            button.onClick.AddListener(() => { instance.PlayClick();});
        }
    }
}
