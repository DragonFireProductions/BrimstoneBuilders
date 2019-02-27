using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRuins : MonoBehaviour
{
    [SerializeField] GameObject Ruins;

    public bool EscortDone;
    // Start is called before the first frame update
    void Start()
    {
        EscortDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(EscortDone)
        {
            SmashRuins();
        }
    }

    public void SmashRuins()
    {
        Ruins.SetActive(false);
    }
}
