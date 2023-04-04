using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerinput : MonoBehaviour
{
    playerController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<playerController>();

        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Direction direction = SystemInfo.supportsGyroscope ? moveByGyro() : moveByTouch();
        controller.Move(direction);
    }

    /// <summary>
    /// returns direction according to phone rotation
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for none</returns>
    private Direction moveByGyro()
    {

       if(Input.gyro.rotationRateUnbiased.z > 3)
            return Direction.right;
        else if(Input.gyro.rotationRateUnbiased.z < -3)
            return Direction.left;
        return Direction.idle;
    }


    /// <summary>
    /// calculates a direction from the touch inputs
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for no direction</returns>
    private Direction moveByTouch(){
        int direction = 0;

        if(Input.touchCount <= 0)
            return Direction.idle;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];

            direction += touch.position.x > Screen.width / 2 ? 1 : -1;
        }

        if(direction == 0)
            return Direction.idle;

        return direction > 0 ? Direction.right : Direction.left;
    }
}
