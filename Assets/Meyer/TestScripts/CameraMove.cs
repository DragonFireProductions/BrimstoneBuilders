using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    private Transform target;

    private Transform cam;
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 newTarget = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        cam = this.transform;
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(cam.position, target.position);
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + 5, target.position.z- 5);
        newTarget.x = targetPosition.x + Input.GetAxis("Horizontal") * 4.0f;
        newTarget.y = targetPosition.y;
        newTarget.z = targetPosition.z + Input.GetAxis("Vertical") * 4.0f;
        transform.position = Vector3.SmoothDamp(transform.position, newTarget, ref velocity, smoothTime);
        

    }
}
