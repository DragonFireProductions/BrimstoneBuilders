﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController Selected;
    [SerializeField] float OrbitSpeed;
    [SerializeField] float MaxAngle;
    [SerializeField] float MinAngel;
    [SerializeField] float MaxZ;
    [SerializeField] float MinZ;
    float YAngle = 0;
     float XAngle = 45;
     float ZOffset = 30;

    private void Update()
    {

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

        transform.position = Selected.transform.position + transform.up;
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.Rotate(XAngle, YAngle, 0);
        transform.Translate(0, 0, -ZOffset);
    }
}

