using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}