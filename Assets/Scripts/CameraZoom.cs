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

    private float fov;
    float minFov = 5f,maxFov = 20f;
    public float zoomSpeed = 15f;

    private Vector3 previousPos;

    public float distance = 10.0f;
    public float rotationSpeed = 5.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
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
        Zoom();
        OrbitAround();
    }
    private void OrbitAround() {

        if (Input.GetMouseButton(1))
        {
            //scade in stanga ca sa adune in dreapta
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            currentY = Mathf.Clamp(currentY, -85, 85);
        }
        
        //rotirea se face doar pe 2 axe
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        
        Vector3 offset = rotation * Vector3.back * distance;
        transform.position = target.position + offset;
        //pastreaza distanta presetata dar se uita la target
        transform.LookAt(target);
        
    }
    private void Zoom()
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minFov, maxFov);
        
    }
}
