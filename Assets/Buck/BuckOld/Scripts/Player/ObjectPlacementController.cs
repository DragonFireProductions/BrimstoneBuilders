using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField]
    GameObject[] placeableObjectPrefabs;

    GameObject currentPlaceableObject;

    int floorMask;
    float mouseWheelRotation;
    float camRayLength = 100f;
    int currentPrefabIndex = -1;

    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
    }

    void Update ()
    {
        HandleNewObject();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateByMouseWheel();
            ReleaseIfClicked();
        }
	}

    void HandleNewObject()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (pressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }
                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    private bool pressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    void MoveCurrentObjectToMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            currentPlaceableObject.transform.position = floorHit.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, floorHit.normal);
        }
    }

    void RotateByMouseWheel()
    {
        mouseWheelRotation -= Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    void ReleaseIfClicked()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }
}
