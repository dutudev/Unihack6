using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildSystem : MonoBehaviour
{
    public Vector3 cursorPosition, placeholderPosition;
    public Camera camera;
    public float buildCooldown, removeCooldown;
    public GameObject buildPlaceholder, PreviousRemoveGameObject;
    public GameObject[] buildGameObject, PlaceholderGameObject;
    public bool removeMode;
    public Material defaultMaterial, redMaterial;
    public Type currnentShape = Type.cube;
    public Image shapeImage;
    public Sprite cubeSprite, pyramideSprite, cylinderSprite, binSprite;
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
            PlaceholderGameObject[0].SetActive(true);
            PlaceholderGameObject[1].SetActive(false);
            PlaceholderGameObject[2].SetActive(false);
            if (shapeImage.sprite != cubeSprite && !removeMode)
            {
                LeanTween.cancelAll();
                shapeImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                shapeImage.sprite = cubeSprite;
                LeanTween.scale(shapeImage.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f)
                    .setEaseOutExpo();
            }
            
        }

        if (Input.GetKeyDown("2"))
        {
            currnentShape = Type.cilinder;
            PlaceholderGameObject[0].SetActive(false);
            PlaceholderGameObject[1].SetActive(true);
            PlaceholderGameObject[2].SetActive(false);
            if (shapeImage.sprite != cylinderSprite && !removeMode)
            {
                LeanTween.cancelAll();
                shapeImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                shapeImage.sprite = cylinderSprite;
                LeanTween.scale(shapeImage.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f)
                    .setEaseOutExpo();
            }
        }
        
        if (Input.GetKeyDown("3"))
        {
            currnentShape = Type.pyramid;
            PlaceholderGameObject[0].SetActive(false);
            PlaceholderGameObject[1].SetActive(false);
            PlaceholderGameObject[2].SetActive(true);
            if (shapeImage.sprite != pyramideSprite && !removeMode)
            {
                LeanTween.cancelAll();
                shapeImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                shapeImage.sprite = pyramideSprite;
                LeanTween.scale(shapeImage.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f)
                    .setEaseOutExpo();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            removeMode = !removeMode;
            if (PreviousRemoveGameObject != null)
            {
                PreviousRemoveGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
            if (shapeImage.sprite != binSprite && removeMode)
            {
                LeanTween.cancelAll();
                shapeImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                shapeImage.sprite = binSprite;
                LeanTween.scale(shapeImage.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f)
                    .setEaseOutExpo();
            }
            else
            {
                LeanTween.cancelAll();
                shapeImage.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
                switch(currnentShape)
                {
                    case Type.cube:
                        shapeImage.sprite = cubeSprite;
                        break;
                    case Type.cilinder:
                        shapeImage.sprite = cylinderSprite;
                        break;
                    case Type.pyramid:
                        shapeImage.sprite = pyramideSprite;
                        break;
                }
                LeanTween.scale(shapeImage.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f)
                    .setEaseOutExpo();
            }
        }
        
        
        buildCooldown -= Time.deltaTime;
        removeCooldown -= Time.deltaTime;
    }
    
}
