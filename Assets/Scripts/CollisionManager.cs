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
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        PhantomPosition = position;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        position = Player.transform.position;
        float vity = Player.GetComponent<Jump>().GetVitesseY();
        vitesse = new Vector3(0, vity, 0);
        PhantomPosition = position + vitesse;
        this.transform.position = PhantomPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != Player)
        {
            Vector3 OnGroundPosition = new Vector3(PhantomPosition.x, collision.gameObject.transform.position.y + collision.gameObject.GetComponent<Renderer>().bounds.size.y / 2 + Player.gameObject.GetComponent<Renderer>().bounds.size.y / 2, 0);
            Player.transform.position = OnGroundPosition;
            this.transform.position = OnGroundPosition;
        }
    }
}
