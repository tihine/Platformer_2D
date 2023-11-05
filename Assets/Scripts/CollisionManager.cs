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
    private bool onPente;
    private bool passUnder;
    void Start()
    {
        position = transform.position;
        PhantomPosition = transform.position;
        transform.position = PhantomPosition;
        jumpScript = Player.GetComponent<Jump>();
        penduleScript = Player.GetComponent<Pendule>();
        playerMovesScript = Player.GetComponent<PlayerMoves>();
        onPente = false;
        passUnder = false;
    }
    
    private IEnumerator DieCoroutine(float secondsBeforeRespawn)
    {
        var startPos = playerMovesScript.startingPosition;
        //TODO : play death particules
        playerMovesScript.death_particules.Play();
        var player = playerMovesScript.gameObject;
        player.SetActive(false);
        yield return new WaitForSeconds(secondsBeforeRespawn);
        player.transform.position = playerMovesScript.startingPosition;
        player.SetActive(true);
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
            float distancex_theory = collision.gameObject.transform.localScale.x / 2 + Player.gameObject.transform.localScale.x / 2;
            float distancey_theory = collision.gameObject.transform.localScale.y / 2 + Player.gameObject.transform.localScale.y / 2;
            float plat_size_x = collision.gameObject.transform.localScale.x/2 + Player.gameObject.transform.localScale.x/4;//seuil de tolérance pour le divisé par 4
            float plat_size_y = collision.gameObject.transform.localScale.y/2 + Player.gameObject.transform.localScale.y/4;//seuil de tolérance pour le divisé par 4
            float dist_x = collision.transform.position.x - transform.position.x;
            float dist_y = collision.transform.position.y - transform.position.y;
            if (type == Plateform_type.normal)
            {
                //Si collision par dessus 
                if (Mathf.Abs(dist_x)<=plat_size_x & dist_y<= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
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
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    jumpScript.JumpOnCode();
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
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
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    //print("par dessus");
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(Player.transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    //ignore laisse passer 
                    passUnder = true;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0 & Mathf.Abs(dist_x)>= plat_size_x/2)
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
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y - distancey_theory, 0);
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    jumpScript.SetNbPressedXButton(1);
                    Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    jumpScript.SetNbPressedXButton(1);
                    Vector3 OnLeftPosition = new Vector3(collision.gameObject.transform.position.x - distancex_theory, transform.position.y, 0);
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                }
            }
            else if (type == Plateform_type.pente)
            {
                Vector3 distVec = Player.transform.position-collision.transform.position;
                float teta = collision.transform.rotation.eulerAngles.z;
                Vector3 horizontal = new Vector3(Mathf.Cos(teta * Mathf.PI / 180), Mathf.Sin(teta * Mathf.PI / 180),0);
                Vector3 vertical = new Vector3(-Mathf.Sin(teta * Mathf.PI / 180), Mathf.Cos(teta * Mathf.PI / 180),0);
                dist_x = Vector3.Dot(distVec, horizontal);
                dist_y = Vector3.Dot(distVec, vertical);
                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = collision.transform.position + dist_x * horizontal + distancey_theory * vertical;
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                    Player.transform.Rotate(0, 0, teta);
                    onPente = true;

                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = collision.transform.position + dist_x * horizontal - distancey_theory * vertical;
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                    Player.transform.Rotate(0, 0, teta);
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = collision.transform.position + distancex_theory * horizontal + dist_y * vertical;
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                    Player.transform.Rotate(0, 0, teta);
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = collision.transform.position - distancex_theory * horizontal + dist_y * vertical;
                    Player.transform.position = OnLeftPosition;
                    transform.position = OnLeftPosition;
                    Player.transform.Rotate(0, 0, teta);
                }
            }
            else if (type == Plateform_type.liane)
            {
                penduleScript.OnPenduleEnter();
                penduleScript.SetCurrentPendule(collision.gameObject);
            }
            
            else if (type == Plateform_type.killer)
            {
                StartCoroutine(DieCoroutine(1f));
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
                    jumpScript.setCoeffWall(0.05f);
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
                    jumpScript.setCoeffWall(0.05f);
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
            else if (type == Plateform_type.pente & false)
            {
                Vector3 distVec = Player.transform.position - collision.transform.position;
                float teta = collision.transform.rotation.eulerAngles.z;
                Vector3 horizontal = new Vector3(Mathf.Cos(teta * Mathf.PI / 180), Mathf.Sin(teta * Mathf.PI / 180), 0);
                Vector3 vertical = new Vector3(-Mathf.Sin(teta * Mathf.PI / 180), Mathf.Cos(teta * Mathf.PI / 180), 0);
                dist_x = Vector3.Dot(distVec, horizontal);
                dist_y = Vector3.Dot(distVec, vertical);
                print("player pos init : " + Player.gameObject.transform.position);

                //Si collision par dessus
                if (Mathf.Abs(dist_x) <= plat_size_x & dist_y >= 0)
                {
                    jumpScript.OnGround();
                    Vector3 OnGroundPosition = collision.transform.position + dist_x * horizontal + distancey_theory * vertical;
                    Player.transform.position = OnGroundPosition;
                    transform.position = OnGroundPosition;
                    print("after : " + OnGroundPosition);
                }
                //Si collision par dessous
                else if (Mathf.Abs(dist_x) <= plat_size_x & dist_y <= 0)
                {
                    jumpScript.Plafond();
                    Vector3 UnderSealPosition = collision.transform.position + dist_x * horizontal - distancey_theory * vertical;
                    Player.transform.position = UnderSealPosition;
                    transform.position = UnderSealPosition;
                }
                //Si collision par la droite
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x >= 0)
                {
                    //ne va plus vers la droite
                    playerMovesScript.SetMoving(false);
                    Vector3 OnRightPosition = collision.transform.position + distancex_theory * horizontal + dist_y * vertical;
                    Player.transform.position = OnRightPosition;
                    transform.position = OnRightPosition;
                }
                //Si collision par la gauche
                else if (Mathf.Abs(dist_y) <= plat_size_y & dist_x <= 0)
                {
                    //ne va plus vers la gauche
                    playerMovesScript.SetMoving(false);
                    Vector3 OnLeftPosition = collision.transform.position - distancex_theory * horizontal + dist_y * vertical;
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
                    if (!passUnder)
                    {
                        jumpScript.OnGround();
                        Vector3 OnGroundPosition = new Vector3(transform.position.x, collision.gameObject.transform.position.y + distancey_theory, 0);
                        Player.transform.position = OnGroundPosition;
                        transform.position = OnGroundPosition;
                    }
                }
                //Si collision par la droite
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x <= 0)
                {
                    //ne va plus vers la droite
                    if (!passUnder)
                    {
                        playerMovesScript.SetMoving(false);
                        Vector3 OnRightPosition = new Vector3(collision.gameObject.transform.position.x + distancex_theory, transform.position.y, 0);
                        Player.transform.position = OnRightPosition;
                        transform.position = OnRightPosition;
                    }
                }
                //Si collision par la gauche
                if (Mathf.Abs(dist_y) <= plat_size_y / 2 & dist_x >= 0 & Mathf.Abs(dist_x) >= plat_size_x / 2)
                {
                    if (!passUnder)
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
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject != Player && collision.gameObject.tag == "Plateforme")
        {
            if (collision.gameObject.GetComponent<Plateform>().GetTypePlateform() == Plateform_type.liane)
            {
                penduleScript.OnPenduleExit();
            }
            if (collision.gameObject.GetComponent<Plateform>().GetTypePlateform() == Plateform_type.wall)
            {
                jumpScript.setCoeffWall(1f);
            }
            else if (!onPente)
            {
                jumpScript.OnFall();
            }
            if (collision.gameObject.GetComponent <Plateform>().GetTypePlateform() == Plateform_type.pente)
            {
                Player.gameObject.transform.rotation = Quaternion.identity;
                onPente = false;
            }
            if (collision.gameObject.GetComponent<Plateform>().GetTypePlateform() == Plateform_type.pass)
            {
                passUnder = false;
            }
        }
    }
}
