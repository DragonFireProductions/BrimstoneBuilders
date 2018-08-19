using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private CharacterController controller;
    [SerializeField] private float speed;
	// Use this for initialization
	void Start () {
        controller = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float dist;
        var plane = new Plane(Vector3.up, gameObject.transform.position + Vector3.up);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //sends a line according to the z and x axis
        
        if (plane.Raycast(ray, out dist))
        {
            if (dist > 18.86)
            {
                var point = ray.GetPoint(dist);
                gameObject.transform.LookAt(point);
            }

        }
        // get the direction to move according to the keyboard input ONLY
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hor, 0f, vert);
        if (direction.magnitude > 0.001)
        {
            direction.Normalize();
        }
        Vector3 velocity = direction * speed * Time.deltaTime;
        controller.Move(velocity);
    }
}
