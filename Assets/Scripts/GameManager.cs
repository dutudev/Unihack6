using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
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
}
