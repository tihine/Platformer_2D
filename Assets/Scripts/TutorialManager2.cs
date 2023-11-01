using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TutorialManager2 : MonoBehaviour
{
    public GameObject[] texts;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Portal;
    [SerializeField] GameObject Elevator;
    private int textsIndex = 0;
    private PlayerMoves playerMovesScript;
    private Pendule penduleScript;
    private bool elevatorDone = false;
    private bool tutorialDone = false;

    void Start()
    {
        penduleScript = Player.GetComponent<Pendule>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
        texts[textsIndex].SetActive(true);
    }
    void Update()
    {

        if (textsIndex == 0)
        {
            if(elevatorDone==true)
            {
                Debug.Log("test true");
                texts[textsIndex].SetActive(false);
                textsIndex++;
                texts[textsIndex].SetActive(true);
            }
        }
        else if (textsIndex == 1)
        {
            if (penduleScript.GetGrabbing()==true)
            {
                texts[textsIndex].SetActive(false);
                tutorialDone = true;
                Portal.SetActive(true);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Elevator"){
            elevatorDone = true;
        } else if (collision.gameObject.tag == "Player" && tutorialDone == true)
        {
            SceneManager.LoadScene("Level 1");
        }
    }

}