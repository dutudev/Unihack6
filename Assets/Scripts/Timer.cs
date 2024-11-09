using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float newFontSize = 75f;
    [SerializeField] TextMeshProUGUI waitingText;
    [SerializeField] GameObject canvas;

    float currentTime = 0f;
    float startingTime = 30f;
    private bool pause=false;
    float waitingTime = 10f;


   void Start()
    {
        currentTime = startingTime;
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        PauseTime();
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
            }
        }
    }

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
    }

    public void PauseTime(bool pause=false)
    {
        if (Input.GetKey(KeyCode.Escape) && pause == false)
        {
            Debug.Log("pressed");
            Time.timeScale = 0;
            canvas.SetActive(true);
            pause = true;
        }
        if (Input.GetKey(KeyCode.Escape) || pause == true)
        {
            Time.timeScale = 1;
            canvas.SetActive(false);
        }
        Debug.Log(pause);
    }
}
