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
            print("collision" + collision.gameObject.name);
            if(collision.gameObject.GetComponent<Plateform>())
            {
                Debug.Log("Plateforme");
            }
            Plateform_type type = collision.gameObject.GetComponent<Plateform>().GetTypePlateform();
            float distancex_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
            float distancey_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float plat_size_x = collision.gameObject.GetComponent<Renderer>().bounds.size.x;
            float plat_size_y = collision.gameObject.GetComponent<Renderer>().bounds.size.y;
            float dist_x = collision.transform.position.x - Player.transform.position.x;
            float dist_y = collision.transform.position.y - Player.transform.position.y;
            
            if (type == Plateform_type.normal)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x)<=plat_size_x & dist_y<= 0)
                {
                    print("par dessus");
                    jumpScript.ChangeFalling(false);
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    print("par dessous");
                    //ne saute plus, bloqué vers le haut vers jumpScript
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    print("droite");
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    print("gauche");
                    //ne va plus vers la gauche
                }

            }

            else if (type == Plateform_type.wall)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    print("par dessus");
                    jumpScript.ChangeFalling(false);
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    print("par dessous");
                    //ne saute plus, bloqué vers le haut vers jumpScript ne devrais pas arriver dans ce cas
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    print("droite");
                    //ne va plus vers la droite
                    //wall slide
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    print("gauche");
                    //ne va plus vers la gauche
                    //wall slide
                }
            }
            else if (type == Plateform_type.bounce)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    print("par dessus");
                    jumpScript.ChangeFalling(false);
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                    //appeler une fonction qui fait jump (mais pas autant qu'un vrai jump
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    print("par dessous");
                    //ne saute plus, bloqué vers le haut vers jumpScript
                    //renvoyé vers le bas ?
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    print("droite");
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    print("gauche");
                    //ne va plus vers la gauche
                }
            }
            else if (type == Plateform_type.pass)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    print("par dessus");
                    jumpScript.ChangeFalling(false);
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    print("par dessous");
                    //ignore laisse passer 
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    print("droite");
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    print("gauche");
                    //ne va plus vers la gauche
                }
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
