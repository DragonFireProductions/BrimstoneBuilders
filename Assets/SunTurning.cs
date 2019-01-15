using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunTurning : MonoBehaviour
{
    [SerializeField] private float DaySpeed = 1;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localEulerAngles = new Vector3(Time.time * DaySpeed, -30, 0);
    }
}
