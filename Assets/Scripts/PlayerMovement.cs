using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    Transform player_transform;
    float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        player_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal = Input.GetAxis("Horizontal");
        //moveCharacter(new Vector3 (horizontal, 0,0));
    }

    public void moveCharacter(Vector3 direction)
    {
        player_transform.position += direction * Time.deltaTime * speed;
        Debug.Log(direction.x * Time.deltaTime * speed);
    }
}
