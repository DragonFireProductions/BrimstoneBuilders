using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageKillDone : MonoBehaviour
{
    public bool VillageKillQuestDone;
    [SerializeField] GameObject NPCs;

    void Start()
    {
        VillageKillQuestDone = false;

    }
    
    void Update()
    {
        if(VillageKillQuestDone)
        {
            ActivateNPCs();
        }
    }

    public void ActivateNPCs()
    {
        NPCs.GetComponentInChildren<GameObject>().SetActive(true);
    }
}
