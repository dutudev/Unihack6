using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    
    public int gameState = 2; // 0 - watch, 1 - build, 2 - pause
    public TMP_Text timerText, percentText;
    public float timeLeft= 5;
    public GameObject percentPanel;
    // Start is called before the first frame update
    void Start()
    {
      //  UpdateState();
      //SpercentPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, -655, 0);
      
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = ((int)timeLeft).ToString();
        if (timeLeft <= 0)
        {
            NextState();
        }
    }
    
    public void NextState()
    {
        if (timeLeft <= 0 && gameState >= 2)
        {
            gameState = 0;
            timeLeft = 10f;
        }
        else
        {
            switch (gameState)
            {
                case 0:
                    gameState = 1;
                    timeLeft = 50f;
                    GameManager.Instance.canBuild = true;
                    break;
                case 1:
                    gameState = 2;
                    timeLeft = 8f;
                    GameManager.Instance.canBuild = false;
                    break;
                case 2:
                    gameState = 0;
                    timeLeft = 10f;
                    GameManager.Instance.canBuild = false;
                    break;
                    
                
            }
        }
        UpdateState();
    }

    public void UpdateState()
    {
        switch (gameState)
        {
            case 0 :
                GameManager.Instance.CleanBoard(false);
                GameManager.Instance.PickStructureAndBuild();
                break;
            case 1 :
                GameManager.Instance.CleanBoard(false);
                break;
            case 2 :GameManager.Instance.CleanBoard(true);
                LeanTween.move(percentPanel.GetComponent<RectTransform>(), new Vector3(0, 40, 0), 1f).setEaseOutExpo();
                LeanTween.move(percentPanel.GetComponent<RectTransform>(), new Vector3(0, -65, 0), 1f).setEaseInExpo().setDelay(5f);
                percentText.text = "You got " + ((int)GameManager.Instance.Check()).ToString() + "%!";
                break;
                
                
        }
    }
}
