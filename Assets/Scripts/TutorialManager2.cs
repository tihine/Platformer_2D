using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TutorialManager2 : MonoBehaviour
{
    public GameObject[] texts;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Elevator;
    private int textsIndex = 0;
    private PlayerMoves playerMovesScript;
    private Pendule penduleScript;
    private bool elevatorDone = false;

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
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator")){
            elevatorDone = true;
        }
        Debug.Log("bool=true");
    }
}