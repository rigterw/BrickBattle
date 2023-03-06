using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneSwitch : MonoBehaviour
{
    public void SwitchScene(string name){
        SceneManager.LoadScene(name);
    }
}
