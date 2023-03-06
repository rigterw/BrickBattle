using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private float margin;
    [SerializeField] private float defendFollowStrength;
    [SerializeField] private float followStrength;

    private Transform ball;
    private Rigidbody2D ballrb;
    private playerController controller;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        ballrb = ball.GetComponent<Rigidbody2D>();
        controller = GetComponent<playerController>();

    }

    // Update is called once per frame
    void Update()
    {
        Direction direction = Direction.idle;
        var value = Random.value;

        //ball moving away
        if(ballrb.velocity.y < 0 && value < followStrength)
        direction = defend();
        //ball moving up
        else if(ballrb.velocity.y > 0 && value < defendFollowStrength)
        direction = defend();
        else
        direction = Idle(value);

        controller.Move(direction);
    }

    /// <summary>
    /// gets a random direction to move
    /// </summary>
    /// <param name="rValue">the value from the state randomiser</param>
    /// <returns>a direction based on the random value</returns>
    private Direction Idle(float rValue){
        if(rValue % 3 == 0)
            return Direction.left;
        else if(rValue % 3 == 1)
            return Direction.right;
        return Direction.idle;
    }

    private Direction defend(){
        if( margin > Mathf.Abs(ball.transform.position.x - transform.position.x))
            return Direction.idle;
        if(ball.transform.position.x > transform.position.x)
            return Direction.right;

        return Direction.left;
    }


}
