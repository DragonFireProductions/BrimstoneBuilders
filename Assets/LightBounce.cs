using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBounce : MonoBehaviour
{
    [SerializeField] public float Intensity_Rate;

    private Vector3 StartPos;
    private float rand;

    private Light ThisLight;

    // Start is called before the first frame update
    void Start()
    {
        ThisLight = GetComponent<Light>();
        rand = Random.Range(0, 65000);
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(rand, Time.time * Intensity_Rate);

        transform.position = new Vector3(transform.position.x, StartPos.y + Mathf.Sin(Time.time), transform.position.z);

    }
}
