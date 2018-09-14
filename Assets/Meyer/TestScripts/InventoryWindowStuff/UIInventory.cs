using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{

    public static UIInventory instance;

    struct inventorySlot
    {
        private static GameObject InventoryContainer;
        private static List<string> itemVariables;
        private static RawImage image;
    }
    

	// Use this for initialization
	void Awake () {
	    if (instance == null)
	    {
	        instance = this;
	    }
	    else if (instance != this)
	        Destroy(gameObject);
	    DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
