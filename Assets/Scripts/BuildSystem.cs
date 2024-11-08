using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Vector3 cursorPosition, placeholderPosition;
    public Camera camera;
    public float buildCooldown;
    public GameObject buildPlaceholder, buildGameObject, PreviousRemoveGameObject;
    public bool removeMode;
    public Material defaultMaterial, redMaterial;
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
                    if (hit.collider.gameObject != PreviousRemoveGameObject && PreviousRemoveGameObject != null)
                    {
                        PreviousRemoveGameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                    }
                    PreviousRemoveGameObject = hit.collider.gameObject;
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

        if (Input.GetAxisRaw("Fire1") == 1 && buildCooldown <= 0f)
        {
            Instantiate(buildGameObject, buildPlaceholder.GetComponent<Transform>().position, Quaternion.identity);
            buildCooldown = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            removeMode = !removeMode;
        }
        buildCooldown -= Time.deltaTime;
    }
    
}
