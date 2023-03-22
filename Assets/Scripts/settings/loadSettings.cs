using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class loadSettings : MonoBehaviour
{
    [SerializeField]private AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        mixer.SetFloat("music", PlayerPrefs.GetFloat("music", 1f));
        mixer.SetFloat("audio", PlayerPrefs.GetFloat("audio", 1f));
    }

}
