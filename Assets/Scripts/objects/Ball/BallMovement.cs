using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Velocity = new Vector3(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity;
    }

    void OnCollisionEnter2D(Collision2D col){
        Velocity = -Velocity;
    }
}
