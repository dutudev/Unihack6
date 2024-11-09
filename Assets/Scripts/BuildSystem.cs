using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Vector3 cursorPosition, placeholderPosition;
    public Camera camera;
    public float buildCooldown, removeCooldown;
    public GameObject buildPlaceholder, PreviousRemoveGameObject;
    public GameObject[] buildGameObject;
    public bool removeMode;
    public Material defaultMaterial, redMaterial;
    public Type currnentShape = Type.cube;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (!removeMode)
            {
                cursorPosition = hit.point;
            }
            else
            {
                if (hit.collider.gameObject.CompareTag("Placed"))
                { 
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material = redMaterial;
                    hit.collider.gameObject.name = "CurrentRemove";
                    if (hit.collider.gameObject != PreviousRemoveGameObject && PreviousRemoveGameObject != null)
                    {
                        PreviousRemoveGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                        PreviousRemoveGameObject.name = "cube(Clone)";
                    }
                    PreviousRemoveGameObject = hit.collider.gameObject;
                }else if (PreviousRemoveGameObject != null)
                {
                    PreviousRemoveGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                    PreviousRemoveGameObject.name = "cube(Clone)";
                }
            }
        }

        placeholderPosition.x = Mathf.Round(cursorPosition.x);
        //replace
        placeholderPosition.y = Mathf.Ceil(cursorPosition.y + 0.1f);
        placeholderPosition.z = Mathf.Round(cursorPosition.z);
        if (!removeMode)
        {
            buildPlaceholder.SetActive(true);
            buildPlaceholder.GetComponent<Transform>().position = placeholderPosition;
        }
        else
        {
            buildPlaceholder.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && buildCooldown <= 0f && !removeMode)
        {
            buildCooldown = 0.25f;
            var block = gameObject;
            switch (currnentShape)
            {
                case Type.cube:
                    block = Instantiate(buildGameObject[0], buildPlaceholder.GetComponent<Transform>().position, Quaternion.identity);
                    block.GetComponent<Block>().block.type = Type.cube;
                    break;
                case Type.cilinder: 
                    block = Instantiate(buildGameObject[1], buildPlaceholder.GetComponent<Transform>().position, Quaternion.identity);
                    block.GetComponent<Block>().block.type = Type.cilinder;
                    break;
                case Type.pyramid:
                     block = Instantiate(buildGameObject[2], buildPlaceholder.GetComponent<Transform>().position, Quaternion.identity);
                     block.GetComponent<Block>().block.type = Type.pyramid;
                     break;
            }
            
            GameManager.Instance.placedblocks.Add(new BlockStructure(block.GetComponent<Transform>().position, Type.cube));
            block.GetComponent<Block>().block = new BlockStructure(block.GetComponent<Transform>().position, Type.cube);

        }

        if (PreviousRemoveGameObject != null)
        {
            Debug.Log(PreviousRemoveGameObject.GetComponent<MeshRenderer>().material);
        }

        if (PreviousRemoveGameObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && removeMode && PreviousRemoveGameObject.name == "CurrentRemove" && removeCooldown <= 0)
            {
                removeCooldown = .2f;
                Debug.Log(PreviousRemoveGameObject.GetComponent<Block>().block.type);
                GameManager.Instance.placedblocks.Remove(PreviousRemoveGameObject.GetComponent<Block>().block);
                Destroy(PreviousRemoveGameObject);
                PreviousRemoveGameObject = null;
            }  
        }

        if (Input.GetKeyDown("1"))
        {
            currnentShape = Type.cube;
        }

        if (Input.GetKeyDown("2"))
        {
            currnentShape = Type.cilinder;
        }
        
        if (Input.GetKeyDown("3"))
        {
            currnentShape = Type.pyramid;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            removeMode = !removeMode;
            if (PreviousRemoveGameObject != null)
            {
                PreviousRemoveGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }
        
        
        buildCooldown -= Time.deltaTime;
        removeCooldown -= Time.deltaTime;
    }
    
}
