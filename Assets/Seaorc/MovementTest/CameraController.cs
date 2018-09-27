using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject Selected;
    [SerializeField] float OrbitSpeed;
    [SerializeField] float MaxAngle;
    [SerializeField] float MinAngel;
    [SerializeField] float MaxZ;
    [SerializeField] float MinZ;
    Vector3 Center;

    float YAngle = 0;
    float XAngle = 45;
    float ZOffset = 30;

    private void Update()
    {
        //caculate Rotation
        if (Input.GetKey(KeyCode.Q))
            YAngle -= OrbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            YAngle += OrbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse2))
        {
            YAngle += (Input.GetAxis("Mouse X") * OrbitSpeed) * Time.deltaTime;
            XAngle -= (Input.GetAxis("Mouse Y") * OrbitSpeed) * Time.deltaTime;
        }
        ZOffset -= (Input.GetAxis("Mouse ScrollWheel") * OrbitSpeed) * Time.deltaTime;
        XAngle = Mathf.Clamp(XAngle, MinAngel, MaxAngle);
        ZOffset = Mathf.Clamp(ZOffset, MinZ, MaxZ);

        if (Selected != null)
        {
            transform.position = Selected.transform.position + transform.up;
            Center = transform.position;
            transform.rotation = new Quaternion(0, 0, 0, 1);
            transform.Rotate(XAngle, YAngle, 0);
            transform.Translate(0, 0, -ZOffset);
        }
        else
        {
            transform.position = Center;
            transform.rotation = new Quaternion(0, 0, 0, 1);
            transform.Rotate(XAngle, YAngle, 0);
            transform.Translate(0, 0, -ZOffset);
        }
    }

    public void SetTarget(GameObject _Target)
    {
        Selected = _Target;
    }
}