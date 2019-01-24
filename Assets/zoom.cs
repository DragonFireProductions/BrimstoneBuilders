using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    float minFOV = 15.0f;
    float maxFOV = 90.0f;
    float sensitivity = 10.0f;
    Camera cam;

    float camSensitivity = 0.05f;
    Vector3 nCam;

    float backtoPlayer = 0.0f;
    [SerializeField] GameObject player;

    float default_zoom;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        default_zoom = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        //StaticManager.Character.cam_pos.gameObject.transform.rotation = player.transform.rotation;
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;
       // StaticManager.Character.cam_pos.gameObject.transform.forward = player.transform.forward;
       
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift)){
            
                  nCam = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
            nCam.x -= 0.5f;
            nCam.y -= 0.5f;
            nCam.x *= camSensitivity;
            nCam.y *= camSensitivity;
            nCam.x += 0.5f;
            nCam.y += 0.5f;
            Vector3 VP2SP = cam.ViewportToScreenPoint(nCam);
            Vector3 view = cam.ScreenToWorldPoint(VP2SP);
            transform.LookAt(view, Vector3.up);
            backtoPlayer = 0.0f;
        }
        else 
        {
            if (Input.GetKey(KeyCode.R))
            {
                for (int i = 0; i < 2; ++i)
                    snap_back();
                Camera.main.fieldOfView = default_zoom;
            }

        }
    }

    void snap_back()
    {
       transform.position = StaticManager.Character.cam_pos.gameObject.transform.position;
       transform.rotation = StaticManager.Character.cam_pos.gameObject.transform.rotation;

       gameObject.GetComponent<CameraController>().playercam.transform.position = StaticManager.Character.cam_pos.gameObject.transform.position;
       gameObject.GetComponent<CameraController>().playercam.transform.rotation = StaticManager.Character.cam_pos.gameObject.transform.rotation;
       
    }
}
