using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour
{

    [SerializeField] public float Intensity_Min;
    [SerializeField] public float Intensity_Max;

    [SerializeField] public float Intensity_Rate;

    private float rand;


    private Light ThisLight;

    // Start is called before the first frame update
    void Start()
    {
        ThisLight = GetComponent<Light>();
        rand = Random.Range(0, 65000);
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(rand, Time.time * Intensity_Rate);
        ThisLight.intensity = Mathf.Lerp(Intensity_Min, Intensity_Max, noise);
    }
}
