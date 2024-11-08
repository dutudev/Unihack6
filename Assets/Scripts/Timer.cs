using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float newFontSize = 75f;

    float currentTime = 0f;
    float startingTime = 31f;


   void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        
        if(currentTime < 0)
        {
            currentTime = 0;
        }
        else
        {
            countdownText.text = Mathf.FloorToInt(currentTime).ToString();
            currentTime -= Time.deltaTime;
        }
        if(currentTime <= 6)
        {
            countdownText.color = Color.red;
            countdownText.fontSize = newFontSize;
        }
    }
}
