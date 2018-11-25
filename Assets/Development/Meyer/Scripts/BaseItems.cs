using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BaseItems : MonoBehaviour {

	public GameObject item;

	public WeaponItem stats;

	public ItemStats itemStats;

	public BaseCharacter AttachedCharacter;

	public string objectName;
	// Use this for initialization
	protected virtual void Start () {
        stats = StaticManager.inventories.GetItemFromAssetList(objectName);
        Assert.IsNotNull(stats, "WeaponItem name not added in inspector " + stats.objectName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
