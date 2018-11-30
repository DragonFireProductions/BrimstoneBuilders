using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour {

	public Companion companion { get; set; }

	public BaseItems item;

	public RawImage imageContainer;

	public UIItemsWithLabels CharacterStats;

	public UIItemsWithLabels WeaponStats;

	public Image bar;

	public Text Level;

	public Text SubClass;

    public void Attach( ) {
		StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.Equip);
		StaticManager.inventories.selectedObj = item;
	}
}
