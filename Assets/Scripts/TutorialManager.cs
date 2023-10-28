using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TutorialManager: MonoBehaviour
{
    public GameObject[] texts;
    [SerializeField] GameObject Player;
    private int textsIndex = 0;
    private PlayerMoves playerMovesScript;
    private Jump jumpScript;

    void Start()
    {
        jumpScript = Player.GetComponent<Jump>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
    }
    void Update()
    {
        for(int i = 0; i < texts.Length; i++)
        {
            if (i == textsIndex)
            {
                texts[textsIndex].SetActive(true);
            } else {
                texts[textsIndex].SetActive(false);
            }
        }

        if (textsIndex == 0)
        {
            if(playerMovesScript.GetVitesseX()!=0)
            {
                textsIndex++;
            }
        } else if (textsIndex == 1)
        {
            if(jumpScript.GetVitesseY()!=0)
            {
                textsIndex++;
            }
        }else if (textsIndex == 2)
        {
            if(playerMovesScript.isSprinting == true)
            {
                textsIndex++;
            }
        }
    }


}