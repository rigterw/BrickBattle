using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    private Vector3 Velocity;
    private Rigidbody2D rb;
    public bool active = false;
    private bool hasFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Velocity = new Vector3(0, speed);

        if(acceleration <= 1){
            Debug.LogError("invalid ball acceleration value");
            acceleration = 1.25f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(active){
        rb.velocity = Velocity;
        }else{
            rb.velocity = Vector2.zero;
        }

        hasFlipped = false;
    }




    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Brick")){
            //GetComponent<ParticleSystem>().Play();
            Debug.Log("boom");
        }
        if(hasFlipped)
            return;
        Vector3 collisionNormal = col.contacts[0].normal;
        bool topDownCollision = Mathf.Abs(collisionNormal.y) > Mathf.Abs(collisionNormal.x);

        if(col.gameObject.CompareTag("Player") && topDownCollision)
            PlayerBounce(col);
        else{
            if(Velocity.magnitude > speed )
            Velocity /= acceleration;
            wallBounce(topDownCollision);
        }
        hasFlipped = true;
    }

    /// <summary>
    /// function that calculates the speed if the ball bounces on a player
    /// </summary>
    /// <param name="player"></param>
    private void PlayerBounce(Collision2D player){
        float speed = Velocity.magnitude;

        float maxDistance = player.gameObject.GetComponent<SpriteRenderer>().size.x/2;


        float xdir = transform.position.x - player.transform.position.x;
        float midDisttance = Mathf.Abs(xdir);
        float ydir = Mathf.Sqrt(1 - midDisttance * midDisttance);

        ydir = Velocity.y >= 0 ? -1 : 1;
        speed *= acceleration;



        Velocity = new Vector2(xdir, ydir) * speed;
    }

    public void Reset(){
        int dir = Velocity.y > 0 ? -1 : 1;
        Velocity = new Vector2(0, dir * speed);
    }

    private void wallBounce(bool tdCollision){
        if(tdCollision)
            Velocity.y = -Velocity.y;
        else
            Velocity.x = -Velocity.x;
    }
}
