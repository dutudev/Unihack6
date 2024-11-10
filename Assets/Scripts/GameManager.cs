using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public Structure currentStructure;
    public Structure[] structures;
    public BlockStructure[] currentBlock;
    public List<BlockStructure> placedblocks = new List<BlockStructure>();
    public GameObject[] PrefabsObjects;
    public GameObject currentObj;
    
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
        //CreateStructure();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickStructureAndBuild()
    {
        int choosen = Random.Range(0, structures.Length);
        CreateStructure(choosen);
        currentStructure = structures[choosen];
    }
    public void CreateStructure(int build)
    {
        for (int i = 0; i < structures[build].blocks.Length; i++)
        {
            switch (structures[build].blocks[i].type)
            {
                case Type.cube :
                    currentObj = Instantiate(PrefabsObjects[0], structures[build].blocks[i].position, Quaternion.identity);
                    break;
                case Type.cilinder :
                    currentObj = Instantiate(PrefabsObjects[1], structures[build].blocks[i].position, Quaternion.identity);
                    break;
                case Type.pyramid :
                    currentObj = Instantiate(PrefabsObjects[2], structures[build].blocks[i].position, Quaternion.identity);
                    break;
            }

            currentObj.tag = "TempBuild";
            currentObj.name = "TEMP";
        }
    }

    public void CleanBoard(bool cleanOnlyTemp)
    {
        if (cleanOnlyTemp)
        {
            GameObject[] buildedTemp = GameObject.FindGameObjectsWithTag("TempBuild");
            foreach (var obj in buildedTemp)
            {
                Destroy(obj);
            }
        }
        else
        {
            GameObject[] buildedTemp = GameObject.FindGameObjectsWithTag("TempBuild");
            foreach (var obj in buildedTemp)
            {
                Destroy(obj);
            }
            GameObject[] buildedPlr = GameObject.FindGameObjectsWithTag("Placed");
            foreach (var obj in buildedPlr)
            {
                Destroy(obj);
            }
        }
        
        
    }
    
    public float Check()
    {
        int required = currentStructure.blocks.Length;
        int placed = placedblocks.Count;
        int inFrame = 0;
        float positionTolerance = 0.01f; // Tolerance level for position comparison

        // Count how many placed blocks match the target structure blocks within the tolerance
        for (int i = 0; i < required; i++)
        {
            for (int j = 0; j < placed; j++) 
            {
                if (Vector3.Distance(currentStructure.blocks[i].position, placedblocks[j].position) <= positionTolerance 
                    && currentStructure.blocks[i].type == placedblocks[j].type)
                {
                    inFrame++;
                    break; // Stop searching once a match is found
                }
            }
        }

        // Calculate the match percentage based on the required number of blocks
        float matchScore = (float)inFrame / required * 100f;

        // Calculate the completion ratio, capping it at 1
        float completionRatio = Mathf.Min((float)placed / required, 1.0f);

        // Adjust the match score based on how complete the structure is
        float adjustedScore = matchScore * completionRatio;

        return matchScore;
    }


/*
    public float Check()
    {
        int i, j;
        int inFrame = 0, required = currentStructure.blocks.Length, placed = currentBlock.Length;
        for (i = 0; i < required; i++)
        {
            for (j = 0; j < placed; j++) 
            {
                if (currentStructure.blocks[i].position == currentBlock[j].position && currentStructure.blocks[i].type == currentBlock[j].type)
                {
                    inFrame++;
                    j = placed;
                }
            }
        }
        int outFrame = required - placed;
        float percentage = (float)inFrame / placed * 100f;
        return percentage;
    } */
}
