using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently this script is being used on
/// 2D sprites, however in the future this can be converted
/// Into 3d models
/// </summary>



public class ObjectRotation : MonoBehaviour
{
    Vector3 newPosition = new Vector3();
    Vector3 curPosition = new Vector3();

    public float rotationSpeed;
    public float height = 0.5f;
    public float frequency = 1f;

    void Start()
    {
        curPosition = transform.position; 
    }

    // Update is called once per frame
    void Update ()
    {
        Float();
        Rotate();
	}

    void Rotate()
    {
        gameObject.transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f), Space.World);
    }

    void Float()
    {
        newPosition = curPosition;
        newPosition.y = curPosition.y + Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * height;
        transform.position = newPosition;
    }

}
