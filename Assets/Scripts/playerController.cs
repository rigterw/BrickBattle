using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    left = -1,
    idle = 0,
    right = 1
}

public class playerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private const float speedScale = 0.004f;
    private float radius;
    private void Start(){
       

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        radius = sprite.bounds.size.x / 2;

    }

    public void Move(float amount){
        transform.position += new Vector3(amount * speed * speedScale, 0, 0);
        transform.position = ConstrainPosition(transform.position);
    }
    public void Move(Direction direction){
        Move((float)direction);


    }

    /// <summary>
    /// prevents the player of going off screen
    /// </summary>
    /// <returns>a position between the area bounds</returns>
    private Vector3 ConstrainPosition(Vector3 position){
        position.x = Mathf.Clamp(position.x, CameraHandler.playArea.xMin + radius, CameraHandler.playArea.xMax - radius);
        return position;
    }

    void OnCollisionEnter2D(Collision2D col) { 

        if(col.gameObject.CompareTag("Ball"))
            GetComponent<AudioSource>().Play();
    }




}
