using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float newFontSize = 75f;
    [SerializeField] TextMeshProUGUI waitingText;
    [SerializeField] GameObject pausePanel;
  //  [SerializeField] private GameObject gameoverPanel;
    public bool pause=false;
/*
    float currentTime = 0f;
    float startingTime = 30f;
    float waitingTime = 10f;
*/

   void Start()
    {
        pausePanel.SetActive(false);
        /*
        currentTime = startingTime;
        gameoverPanel.SetActive(false);
        */
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { PauseTime(pause); }/*
        if (pause == false)
        {
            if (currentTime < 0)
            {
                currentTime = 0;
            }
            else
            {
                countdownText.text = Mathf.FloorToInt(currentTime).ToString();
                currentTime -= Time.deltaTime;
            }
            if (currentTime <= 6)
            {
                countdownText.color = Color.red;
                countdownText.fontSize = newFontSize;
            }*/
        
    }
/*
    void LookingTime()
    {
        if (pause == false)
        {
            if (waitingTime < 0)
            {
                waitingTime = 0;
            }
            else
            {
                waitingText.text = Mathf.FloorToInt(waitingTime).ToString();
                waitingTime -= Time.deltaTime;
            }
        }
    }*/

    public void PauseTime( bool pause)
    {
        if (pause == false)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pause = true;
        }
        else //if ( pause == true)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
