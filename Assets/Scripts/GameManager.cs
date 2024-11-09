using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    [SerializeField] private GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        //singelton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ITimer());
        Pause(default);
    }

    public Structure currentStructure;
    public Block currentBlock;

    void Check()
    {
        int i, j;
        int inFrame = 0, required = currentStructure.coords.Length, placed = currentBlock.pos.Length;
        for (i = 0; i < required; i++)
        {
            for (j = 0; j < placed; j++)
            {
                if (currentBlock.pos[j] == currentStructure.coords[i])
                {
                    inFrame++;
                    j = placed;
                }
            }
        }
        int outFrame = required - placed;
    }

    public void Pause(bool pause=false)
    {
        pauseButton.SetActive(true);
        if(Input.GetKey(KeyCode.Escape) && pause==false)
        {
            Time.timeScale = 0;
            pause = true;
        }
        if (Input.GetKey(KeyCode.Escape) && pause == true)
        {
            Time.timeScale = 1;
        }
    }
    IEnumerator ITimer()
    {
        yield return new WaitForSecondsRealtime(40);
    }
}
