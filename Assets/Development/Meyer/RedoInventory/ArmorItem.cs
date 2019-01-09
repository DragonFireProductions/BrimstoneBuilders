using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

public class ArmorItem : BaseItems {

    public enum Type {Head, Shoulder, Shoe, glove, Clothes, Belt }

	public Type type;

	public GameObject armor;

	public Tab tab;

	public ArmorStuff parent;
	public virtual void PickUp(Companion companion ) {
        var newlabel = Instantiate(StaticManager.uiManager.Armor.gameObject);
        newlabel.GetComponent<Tab>().companion = companion;
        newlabel.GetComponent<Tab>().item = this;

        var i = this as ArmorItem;
        i.tab = newlabel.GetComponent<Tab>();

        var l = newlabel.GetComponent<UIItemsWithLabels>();

        l.obj = newlabel;

        l.item = this;

        l.obj.transform.position = StaticManager.uiManager.Armor.gameObject.transform.position;

        l.obj.transform.SetParent(companion.inventory.armorInventory.transform);

        l.obj.transform.localScale = new Vector3(1, 1, 1);

        l.FindLabels();

        AttachedCharacter = companion;

        newlabel.SetActive(true);

        companion.inventory.armorInventory.armor.Add(l);

		parent.Add(l.obj, this);
        
	}
	public override void Attach() { 
		armor.transform.SetParent(AttachedCharacter.transform);
		armor.GetComponent < SkinnedMeshRenderer >( ).rootBone = AttachedCharacter.transform.Find( "root" );		tab.gameObject.SetActive(false);
		
	}

	

	public void OnTriggerEnter( Collider collider ) {
		if ( collider.tag == "Player" && tag == "PickUp" ){
			StaticManager.Character.inventory.PickUp(this);
			this.GetComponent < Collider >( ).enabled = false;
			AttachedCharacter = collider.GetComponent < Companion >( );
			armor = Instantiate( armor );
			armor.SetActive(false);
			armor.transform.SetParent(AttachedCharacter.transform);
		}
	}

}
