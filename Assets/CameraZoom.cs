using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
//using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class CameraZoom : MonoBehaviour
{
     
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;

    private float fov,minFov = 15f,maxFov = 90f;
    public float zoomSpeed = 15f;
    private bool locked=false;

    private Vector3 previousPos;

    private void Awake()
    {
        cam.transform.position = new Vector3(0, 4, -10);
        cam.transform.rotation = Quaternion.Euler(21, 0, 0);
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = 30; //default
        fov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0) && locked==false) { 
        //    Cursor.lockState = CursorLockMode.Locked;
        //    locked= true;
        //}
        //if(Input.GetKey(KeyCode.Escape)) locked=false;

        Zoom();
        OrbitAround();
    }
    private void OrbitAround() { 
        if(UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            cam.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
        }
        if (UnityEngine.Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 direction=previousPos-cam.ScreenToViewportPoint(UnityEngine.Input.mousePosition);

            cam.transform.RotateAround(target.position, Vector3.up, -direction.x * 180);
            cam.transform.RotateAround(target.position, cam.transform.right, direction.y * 180);

            //cam.transform.position =target.position ;

            //cam.transform.Rotate(new Vector3(1,0,0),direction.y*180);
            //cam.transform.Rotate(new Vector3(0,1,0),-direction.x*180,Space.World);
            //cam.transform.Translate(new Vector3(0,0,-10));

            previousPos = cam.ScreenToViewportPoint(UnityEngine.Input.mousePosition);
        }
    }
    private void Zoom()
    {
        fov -= UnityEngine.Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, Time.deltaTime * zoomSpeed);
    }
}
