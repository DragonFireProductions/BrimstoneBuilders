using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour {

	public Companion companion;

	public BaseItems item;

	public RawImage imageContainer;

	public UIItemsWithLabels CharacterStats;

	public UIItemsWithLabels WeaponStats;

	public Image bar;

	public Text Level;

	public Text SubClass;

	public Shop shop;

	public int index;

	public ArmorItem.Type type;

    public void Attach( ) {
		StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.Equip);
		StaticManager.inventories.selectedObj = item;
	}
}
