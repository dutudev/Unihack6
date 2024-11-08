using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public Vector3 cursorPosition, placeholderPosition;
    public Camera camera;

    public GameObject buildPlaceholder;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        mousePositionOnScreen = Input.mousePosition;
        cursorPosition = camera.ScreenToWorldPoint(new Vector3(mousePositionOnScreen.x, mousePositionOnScreen.y, mousePositionOnScreen.z));*/
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            cursorPosition = hit.point;
        }

        placeholderPosition.x = Mathf.Round(cursorPosition.x);
        //replace
        placeholderPosition.y = 1;
        placeholderPosition.z = Mathf.Round(cursorPosition.z);
        buildPlaceholder.GetComponent<Transform>().position = placeholderPosition;
    }
    
}
