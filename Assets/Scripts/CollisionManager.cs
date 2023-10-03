using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionManager : MonoBehaviour
{
    Vector3 position;
    Vector3 vitesse;
    [SerializeField]
    GameObject Player;

    Vector3 PhantomPosition;
    private Jump jumpScript;
    
    void Start()
    {
        position = transform.position;
        PhantomPosition = position;
        transform.position = position;
        jumpScript = Player.GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        position = Player.transform.position;
        float vitY = jumpScript.GetVitesseY();
        vitesse = new Vector3(0, vitY, 0);
        PhantomPosition = position + vitesse;
        this.transform.position = PhantomPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Player)
        {
            jumpScript.ChangeFalling(false);
            
            Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2, 0);
            Player.transform.position = OnGroundPosition;
            transform.position = OnGroundPosition;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != Player)
        {
            jumpScript.ChangeFalling(true);
        }
    }
}
