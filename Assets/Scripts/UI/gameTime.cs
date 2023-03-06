using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTime : MonoBehaviour
{
    // Start is called before the first frame update
    public void setTime(int time){
        Time.timeScale = time;
    }
}
