using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_CameraController : MonoBehaviour
{
    //This is refernced to check the useKeyBoardbool
    MJB_PlayerController playerControllerRef;

    //The object that the camera will follow
    public Transform target;

    public Vector3 offset;

    //How fast the camera zooms at when zooming in/out
    public float zoomSpeed = 4f;

    //The max distance the camera can zoom in to
    public float minZoom = 5f;
    //The max distance the camera can zoom out to
    public float maxZoom = 15f;

    public float pitch = 2;

    //How fast the camera spins around the character
    public float yawSpeed = 100f;

    //Sets teh starting zoom distance
    float currentZoom = 10f;
    //Sets the current yaw position
    float currentYaw = 0f;

    void Start()
    {
        playerControllerRef = FindObjectOfType<MJB_PlayerController>();
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //if(!playerControllerRef.useKeyBoard)
        //{
        //    currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
        //}
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;

        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
