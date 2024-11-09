using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public Structure currentStructure;
    public BlockStructure[] currentBlock;
    public List<BlockStructure> placedblocks = new List<BlockStructure>();
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
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ITimer());
        Pause(default);
    }

    

   /* void Check()
    {
        int i, j;
        int inFrame = 0, required = currentStructure.blocks.Length, placed = currentBlock.Length;
        for (i = 0; i < required; i++)
        {
            for (j = 0; j < placed; j++)
            {
                if (currentBlock[j].position == currentStructure.blocks[i].position)
                {
                    inFrame++;
                    j = placed;
                }
            }
        }
        int outFrame = required - placed;
    }*/

    

    public void Pause(bool pause=false)
    {
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
