using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private List<Button> buttonsList;

    private void Start()
    {
        var instance = SoundSingleton.Instance;
        foreach (var button in buttonsList)
        {
            button.onClick.AddListener(() => { instance.PlayClick();});
        }
    }

    public void PauseManager(bool isPaused)
    {
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        canvasPause.SetActive(isPaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager(!canvasPause.activeSelf);
        }
    }
}
