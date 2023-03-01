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
    void Update()
    {
        int direction = SystemInfo.supportsGyroscope ? moveByGyro() : moveByTouch();
        controller.Move(direction);
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
