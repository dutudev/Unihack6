using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }
    public void Pause(bool pause = false)
    {
        pauseButton.SetActive(true);
        if (Input.GetKey(KeyCode.Escape) && pause == false)
        {
            Time.timeScale = 0;
            pause = true;
        }
        if (Input.GetKey(KeyCode.Escape) && pause == true)
        {
            Time.timeScale = 1;
        }
    }
}
