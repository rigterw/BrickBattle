using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 Velocity;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Velocity = new Vector3(1, speed);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Velocity.normalized);
        rb.velocity = Velocity;
    }

    void OnCollisionEnter2D(Collision2D col){
        Vector3 collisionNormal = col.contacts[0].normal;
        if(col.gameObject.CompareTag("Player"))
            PlayerBounce(collisionNormal);
        else
            wallBounce(collisionNormal);
    }

    private void PlayerBounce(Vector3 cNormal){
        float speed = Velocity.magnitude;
        wallBounce(cNormal);
    }

    private void wallBounce(Vector3 cNormal){
        if(Mathf.Abs(cNormal.y)> Mathf.Abs(cNormal.x))
            Velocity.y = -Velocity.y;
        else
            Velocity.x = -Velocity.x;
    }
}
