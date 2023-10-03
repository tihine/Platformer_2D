using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionManager : MonoBehaviour
{
    Vector3 position;
    Vector3 vitesse;
    [SerializedField]
    GameObject Player;
    
    Vector3 PhantomPosition
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        Phantomposition = position;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        PhantomPosition = position + vitesse;
        PhantomCube.transform.position = PhantomPosition;
    }

    void OnCollisionEnter()
    {

    }
}
