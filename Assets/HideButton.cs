using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    private Button SendTo;
    private bool Lonely;

    // Start is called before the first frame update
    void Start()
    {
        SendTo = this.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticManager.RealTime.Companions.Count == 1)
            SendTo.interactable = false;
        else
            SendTo.interactable = true;
    }
}
