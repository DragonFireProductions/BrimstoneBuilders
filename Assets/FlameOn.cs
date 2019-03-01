using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameOn : MonoBehaviour
{
    [SerializeField] public GameObject DesertFlameParticleSystem;
    [SerializeField] public GameObject DesertFlameLight;

    [SerializeField] public Light GameSun;

    public bool EscortDone;

    public bool RunOnce;


    // Start is called before the first frame update
    void Start()
    {
        DesertFlameLight.SetActive(false);
        DesertFlameParticleSystem.SetActive(false);
        RunOnce = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(!RunOnce && EscortDone)
        {
            LetThereBeLight();
        }
    }

    public void LetThereBeLight()
    {
        DesertFlameParticleSystem.SetActive(true);
        DesertFlameLight.SetActive(true);
        RunOnce = true;
    }
}
