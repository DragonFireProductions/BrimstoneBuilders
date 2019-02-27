using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageKillDone : MonoBehaviour
{
    public bool VillageKillQuestDone;
    private bool done;
    [SerializeField] GameObject NPCs;

    void Start()
    {
        VillageKillQuestDone = false;
        done = false;
    }
    
    void Update()
    {
        if(VillageKillQuestDone && done == false)
        {
            ActivateNPCs();
            done = true;
        }
    }

    public void ActivateNPCs()
    {
        NPCs.SetActive(true);
    }
}
