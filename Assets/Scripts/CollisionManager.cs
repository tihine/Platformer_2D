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
            Plateform_type type = collision.gameObject.GetComponent<Plateform_type>();
            if(type == Plateform_type.normal)
            {
                float distancex_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
                float distancey_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
                float distancex = System.Math.Abs(collision.gameObject.transform.position.x - Player.transform.position.x);
                float distancey = System.Math.Abs(collision.gameObject.transform.position.y - Player.transform.position.y);
                if (distancex<distancex_theory)
                {
                    jumpScript.ChangeFalling(false);
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                else if (distancey < distancey_theory)
                {
                    Vector3 OnGroundPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, PhantomPosition.y, 0);
                }

            }

            else if (type == Plateform_type.wall)
            {

            }
            else if (type == Plateform_type.bounce)
            {

            }
            else if (type == Plateform_type.pass)
            {

            }

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
