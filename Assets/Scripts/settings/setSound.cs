using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class setSound : MonoBehaviour
{
    [SerializeField] private AudioSource preview;
    [SerializeField]private AudioMixer audioMixer;
    private string group;

    void Start(){
        group = preview.outputAudioMixerGroup.ToString();
        Debug.Log(group);

        var slider = GetComponent<Slider>();
        var value = PlayerPrefs.GetFloat(group, 1f);
        slider.value = value;
    }

    /// <summary>
    /// sets the volume of the audio group
    /// </summary>
    /// <param name="amount">the percentage volume</param>
    public void setVolume(float amount){
        if(amount == 0)
            amount = 0.001f;
        Debug.Log(amount);
        audioMixer.SetFloat(group, Mathf.Log10(amount) * 20);
        preview.Play();

        PlayerPrefs.SetFloat(group, Mathf.Log10(amount) * 20);
    }
}
