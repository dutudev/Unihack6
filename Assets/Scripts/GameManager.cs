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
        CreateStructure();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateStructure()
    {
        for (int i = 0; i < structures[0].blocks.Length; i++)
        {
            switch (structures[0].blocks[i].type)
            {
                case Type.cube :
                    currentObj = Instantiate(PrefabsObjects[0], structures[0].blocks[i].position, Quaternion.identity);
                    break;
                case Type.cilinder :
                    currentObj = Instantiate(PrefabsObjects[1], structures[0].blocks[i].position, Quaternion.identity);
                    break;
                case Type.pyramid :
                    currentObj = Instantiate(PrefabsObjects[2], structures[0].blocks[i].position, Quaternion.identity);
                    break;
            }

            currentObj.tag = "Untagged";
            currentObj.name = "TEMP";
        }
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

 
}
