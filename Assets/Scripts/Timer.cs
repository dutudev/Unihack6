using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float newFontSize = 75f;
    [SerializeField] TextMeshProUGUI waitingText;

    float currentTime = 0f;
    float startingTime = 30f;
    bool pause = false;
    float waitingTime = 10f;


   void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
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
}
