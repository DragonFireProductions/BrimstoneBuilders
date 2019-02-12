using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private bool UseCamera;
    [SerializeField] private bool UseCamera1;
    [SerializeField] private bool UseCamera2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(UseCamera)
            {
                StaticManager.map.UseCamera = true;
                StaticManager.map.UseCamera1 = false;
                StaticManager.map.UseCamera2 = false;
            }
            else if(UseCamera1)
            {
                StaticManager.map.UseCamera = false;
                StaticManager.map.UseCamera1 = true;
                StaticManager.map.UseCamera2 = false;
            }
            else if (UseCamera2)
            {
                StaticManager.map.UseCamera = false;
                StaticManager.map.UseCamera1 = false;
                StaticManager.map.UseCamera2 = true;
            }
        }
    }
}
