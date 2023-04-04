using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerinput : MonoBehaviour
{
    playerController controller;
    float tilt;

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
        Debug.Log(Input.gyro.rotationRateUnbiased);
        if(SystemInfo.supportsGyroscope)
            moveByGyro();
        else
            moveByTouch();
            
    }

    /// <summary>
    /// returns direction according to phone rotation
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for none</returns>
    private void moveByGyro()
    {
        tilt -= Input.gyro.rotationRateUnbiased.z;
        controller.Move(tilt * 0.1f);
    }


    /// <summary>
    /// calculates a direction from the touch inputs
    /// </summary>
    /// <returns>-1 for left, 1 for right and 0 for no direction</returns>
    private void moveByTouch(){
        int direction = 0;
        Direction dir = Direction.left;
        if(Input.touchCount <= 0)
             dir = Direction.idle;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.touches[i];

            direction += touch.position.x > Screen.width / 2 ? 1 : -1;
        }

        if(direction == 0)
            dir = Direction.idle;
        if(dir != Direction.idle)
            dir = direction > 0 ? Direction.right : Direction.left;
        controller.Move(dir);
    }

    private Vector3 GetTilt()
    {
        Quaternion referenceRotation = Quaternion.Euler(0, 0, 0);
        Quaternion deviceRotation = ReadGyroscopeRotation();

        Quaternion eliminationOfXY = Quaternion.Inverse(
            Quaternion.FromToRotation(referenceRotation * Vector3.forward,
                                      deviceRotation * Vector3.forward)
        );

        Vector3 tilt = eliminationOfXY * (deviceRotation * Vector3.right);

        return tilt;
    }

    private Quaternion ReadGyroscopeRotation()
    {
        Quaternion gyroAttitude = Input.gyro.attitude;
        gyroAttitude = Quaternion.Euler(90, 0, 0) * (new Quaternion(0, 0, 1, 0) * gyroAttitude);
        return gyroAttitude;
    }
}
