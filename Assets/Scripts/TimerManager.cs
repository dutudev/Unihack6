using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    
    public int gameState = 2; // 0 - watch, 1 - build, 2 - pause

    public float timeLeft= 5;
    // Start is called before the first frame update
    void Start()
    {
      //  UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
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
            timeLeft = 20f;
        }
        else
        {
            switch (gameState)
            {
                case 0:
                    gameState = 1;
                    timeLeft = 30f;
                    
                    break;
                case 1:
                    gameState = 2;
                    timeLeft = 8f;
                    break;
                case 2:
                    gameState = 0;
                    timeLeft = 20f;
                    
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
                GameManager.Instance.CleanBoard(true);
                GameManager.Instance.PickStructureAndBuild();
                break;
            case 1 :
                GameManager.Instance.CleanBoard(false);
                break;
            case 2 :GameManager.Instance.CleanBoard(true);
                break;
                
                
        }
    }
}
