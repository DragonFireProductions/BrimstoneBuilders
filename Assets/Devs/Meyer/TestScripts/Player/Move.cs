using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Move : MonoBehaviour {
    private CharacterController controller;
    [SerializeField] private float speed;


	void Start () {
        controller = gameObject.GetComponent<CharacterController>();
	}
	
	void Update () {
	    float Speed = 5;
	    RaycastHit hit;
	    float dist;
	    var plane = new Plane(Vector3.up, gameObject.transform.position + Vector3.up);
	    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);          
	    Vector3 newVector3;
	    if (plane.Raycast(ray, out dist))
	    {
	        if (dist > 18.86)
	        {
	            var point = ray.GetPoint(dist);
	            gameObject.transform.LookAt(point);
	        }

	    }
	    float hor = Input.GetAxis("Horizontal");
	    float vert = Input.GetAxis("Vertical");

	    Vector3 direction = new Vector3(hor, 0f, vert);
	    if (direction.magnitude > 0.001)
	    {
	        direction.Normalize();
	    }
	    Vector3 velocity = direction * 8 * Time.deltaTime;
	    controller.Move(velocity);
    }

    
}
