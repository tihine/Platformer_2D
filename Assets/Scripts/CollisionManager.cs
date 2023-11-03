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
    private PlayerMoves playerMovesScript;
    
    void Start()
    {
        position = transform.position;
        PhantomPosition = transform.position;
        transform.position = PhantomPosition;
        jumpScript = Player.GetComponent<Jump>();
        penduleScript = Player.GetComponent<Pendule>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = Player.transform.position;
        float vitY = jumpScript.GetVitesseY();
        float vitX = playerMovesScript.GetVitesseX();
        vitesse = new Vector3(vitX, vitY, 0);
        PhantomPosition = position + vitesse*Time.fixedDeltaTime;
        transform.position = PhantomPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Player && collision.gameObject.tag == "Plateforme")
        {
            Plateform_type type = collision.gameObject.GetComponent<Plateform>().GetTypePlateform();
            float distancex_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
            float distancey_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float plat_size_x = collision.gameObject.GetComponent<Renderer>().bounds.size.x + Player.gameObject.GetComponent<Renderer>().bounds.size.x/2;
            float plat_size_y = collision.gameObject.GetComponent<Renderer>().bounds.size.y + Player.gameObject.GetComponent<Renderer>().bounds.size.y/2;
            float dist_x = collision.transform.position.x - transform.position.x;
            float dist_y = collision.transform.position.y - transform.position.y;
            print(type);
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
                if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y/2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }

            }


            else if (type == Plateform_type.bounce)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    jumpScript.JumpOnCode();
                }
                //Si collision par dessous
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y /2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }
            }
            else if (type == Plateform_type.pass)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x/2 & dist_y <= 0)
                {
                    //print("par dessus");
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(Player.transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y >= 0)
                {
                    //ignore laisse passer 
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y /2 & dist_x >= 0 & Mathf.Abs(dist_x)>= plat_size_x/2)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }
            }
            else if (type == Plateform_type.wall)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    jumpScript.SetNbPressedXButton(1);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    jumpScript.SetNbPressedXButton(1);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != Player && collision.gameObject.tag == "Plateforme")
        {
            Plateform_type type = collision.gameObject.GetComponent<Plateform>().GetTypePlateform();
            float distancex_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
            float distancey_theory = collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float plat_size_x = collision.gameObject.GetComponent<Renderer>().bounds.size.x + Player.gameObject.GetComponent<Renderer>().bounds.size.x / 2;
            float plat_size_y = collision.gameObject.GetComponent<Renderer>().bounds.size.y + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2;
            float dist_x = collision.transform.position.x - transform.position.x;
            float dist_y = collision.transform.position.y - transform.position.y;
            if (type == Plateform_type.elevator)
            {
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
            }
            if (type == Plateform_type.normal)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }

            }

            else if (type == Plateform_type.wall)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    if (transform.position.x < OnRightPosition.x)
                    {
                        transform.position = OnRightPosition;
                        Player.transform.position = OnRightPosition;
                    }
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    if(transform.position.x > OnLeftPosition.x)
                    {
                        transform.position = OnLeftPosition;
                        Player.transform.position = OnLeftPosition;
                    }
                }
            }
            else if (type == Plateform_type.bounce)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    jumpScript.JumpOnCode();
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }
            }
            else if (type == Plateform_type.pass)
            {
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x / 2 & dist_y <= 0)
                {
                    //print("par dessus");
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition; 
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0 & Mathf.Abs(dist_x) >= plat_size_x / 2)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != Player && collision.gameObject.tag == "Plateforme")
        {
            if (collision.gameObject.GetComponent<Plateform>().GetTypePlateform() == Plateform_type.liane)
            {
                print("exit pendule");
                penduleScript.OnPenduleExit();
            }
            else
            {
                jumpScript.OnFall();
            }
        }
    }
}
