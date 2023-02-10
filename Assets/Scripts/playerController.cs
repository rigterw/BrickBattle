using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private const float speedScale = 0.004f;
    private float radius;
    private void Start(){
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
        }

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        radius = sprite.bounds.size.x / 2;

    }

    private void Update(){
        int direction = SystemInfo.supportsGyroscope ? moveByGyro() : moveByTouch();

        
        transform.position += new Vector3(direction * speed * speedScale, 0,0);
        transform.position = ConstrainPosition(transform.position);

    }

    /// <summary>
    /// prevents the player of going off screen
    /// </summary>
    /// <returns>a position between the area bounds</returns>
    private Vector3 ConstrainPosition(Vector3 position){
        position.x = Mathf.Clamp(position.x, CameraHandler.playArea.xMin + radius, CameraHandler.playArea.xMax - radius);
        return position;
    }

    /// <summary>
    /// returns direction according to phone rotation
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for none</returns>
    private int moveByGyro()
    {
        return 0;

        //TODO: add Gyro input
    }


    /// <summary>
    /// calculates a direction from the touch inputs
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for no direction</returns>
    private int moveByTouch(){
        int direction = 0;

        if(Input.touchCount <= 0)
            return 0;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];

            direction += touch.position.x > Screen.width / 2 ? 1 : -1;
        }

        if(direction == 0)
            return 0;

        return direction > 0 ? 1 : -1;

    }
}
