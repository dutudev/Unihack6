using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
{
    public Slider sliderVolume;
    public AudioSource bgSound;

    [SerializeField] private GameObject settingsShow;
    private bool show = false;
    void Start()
    {
        sliderVolume.value=0.5f;
        bgSound.Play();
        //if (!PlayerPrefs.HasKey("musicVolume"))
        //{
        //    PlayerPrefs.SetFloat("musicVolume", 1);
        //    sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
        //}
        //else sliderVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void Update() { 
        ChangeVolume();
    }
    public void ShowSettings()
    {
        if (show == false)
        {
            show = true;
            settingsShow.SetActive(show);
        }
        else { show = false;
            settingsShow.SetActive(show);
        }
      
    }
    public void ChangeVolume()
    {
        bgSound.volume = sliderVolume.value;
    }
}
