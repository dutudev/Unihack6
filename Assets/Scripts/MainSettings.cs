using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
{
    public Slider sliderVolume;
    public AudioSource bgSound;
    void Start()
    {
        bgSound.Play();
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void ChangeVolume()
    {
        AudioListener.volume = sliderVolume.value;
    }
}
