using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TutorialManager: MonoBehaviour
{
    public GameObject[] texts;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Portal;
    private int textsIndex = 0;
    private PlayerMoves playerMovesScript;
    private Jump jumpScript;
    private bool onPortal = false;
    private GestionnaireScene gestionnaireSceneScript;
    private bool tutorialDone = false;

    void Start()
    {
        jumpScript = Player.GetComponent<Jump>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
        texts[textsIndex].SetActive(true);
    }
    void Update()
    {
       
        if (textsIndex == 0)
        {
            if(playerMovesScript.GetVitesseX()!=0)
            {
                texts[textsIndex].SetActive(false);
                textsIndex++;
                texts[textsIndex].SetActive(true);
            }
        } else if (textsIndex == 1)
        {
            if(jumpScript.GetVitesseY()!=0)
            {
                texts[textsIndex].SetActive(false);
                textsIndex++;
                texts[textsIndex].SetActive(true);
            }
        }else if (textsIndex == 2)
        {
            if(playerMovesScript.GetIsSprinting())
            {
                texts[textsIndex].SetActive(false);
                textsIndex++;
                texts[textsIndex].SetActive(true);
            }
        }else if (textsIndex == 3)
        {
            if (playerMovesScript.GetIsDashing())
            {
                texts[textsIndex].SetActive(false);
                Portal.SetActive(true);
                tutorialDone = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision"); 
        if (other.CompareTag("Player")&& tutorialDone)
        {
            gestionnaireSceneScript.ChangeScene("Tutorial2");
        }
        
    }

}