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
    private Pendule penduleScript;
    
    void Start()
    {
        position = transform.position;
        PhantomPosition = transform.position;
        transform.position = PhantomPosition;
        jumpScript = Player.GetComponent<Jump>();
        penduleScript = Player.GetComponent<Pendule>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = Player.transform.position;
        float vitY = jumpScript.GetVitesseY();
        vitesse = new Vector3(0, vitY, 0);
        PhantomPosition = position + vitesse*Time.fixedDeltaTime;
        transform.position = PhantomPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Player)
        {
            Plateform_type type = collision.gameObject.GetComponent<Plateform>().GetTypePlateform();
            float distancex_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
            float distancey_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float plat_size_x = collision.gameObject.GetComponent<Renderer>().bounds.size.x + Player.gameObject.GetComponent<Renderer>().bounds.size.x;
            float plat_size_y = collision.gameObject.GetComponent<Renderer>().bounds.size.y + Player.gameObject.GetComponent<Renderer>().bounds.size.y;
            float dist_x = collision.transform.position.x - transform.position.x;
            float dist_y = collision.transform.position.y - transform.position.y;
            
            if (type == Plateform_type.normal)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x)<=plat_size_x/2 & dist_y<= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                }

            }

            else if (type == Plateform_type.wall)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    //wall slide
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    //wall slide
                }
            }
            else if (type == Plateform_type.bounce)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    //print("par dessus");
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                    jumpScript.JumpOnCode();
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y >= 0)
                {
                    //print("par dessous");
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y /2 & dist_x <= 0)
                {
                    //print("droite");
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    //print("gauche");
                    //ne va plus vers la gauche
                }
            }
            else if (type == Plateform_type.pass)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y <= 0)
                {
                    //print("par dessus");
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y >= 0)
                {
                    //print("par dessous");
                    //ignore laisse passer 
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //print("droite");
                    //ne va plus vers la droite
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y /2 & dist_x >= 0)
                {
                    //print("gauche");
                    //ne va plus vers la gauche
                }
            }
            else if (type == Plateform_type.liane)
            {
                print("Sur la liane");
                penduleScript.OnPenduleEnter();
                penduleScript.SetCurrentPendule(collision.gameObject);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != Player)
        {
            jumpScript.OnFall();
            if (collision.gameObject.GetComponent<Plateform>().GetTypePlateform() == Plateform_type.liane)
            {
                print("exit pendule");
                penduleScript.OnPenduleExit();
            }
        }
    }
}
