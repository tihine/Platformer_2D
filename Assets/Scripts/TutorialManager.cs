using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TutorialManager: MonoBehaviour
{
    public GameObject[] texts;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Portal;
    [SerializeField] GameObject button;
    private int textsIndex = 0;
    private PlayerMoves playerMovesScript;
    private Jump jumpScript;
    private bool onPortal = false;
    private bool tutorialDone = false;

    public PlayFabManager playFabManager;
    float maxTime = 0f;

    void Start()
    {
        jumpScript = Player.GetComponent<Jump>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
        texts[textsIndex].SetActive(true);
        button.SetActive(false);
    }
    void Update()
    {
        maxTime += Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Player" && tutorialDone == true)
        {
            var t = (int)(-maxTime * 100);
            SoundSingleton.Instance.PlayWin();
            Debug.Log("t =" + t);
            playFabManager.SendLeaderBoard(t);
            button.SetActive(true);
            //SceneManager.LoadScene("Tutorial2");
        }
        
    }

}