using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

public class ArmorItem : BaseItems {

    public enum Type {Head, Shoulder, Shoe, glove, Clothes, Belt }

	public Type type;

	public GameObject model;

	public Tab tab;

	public ArmorStuff parent;

	public GameObject label;
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

		label = l.obj;

        newlabel.SetActive(true);

        companion.inventory.armorInventory.armor.Add(l);

		parent.Add(l.obj, this);
        
	}
	public override void Attach() { 
		model.transform.SetParent(AttachedCharacter.transform);
		model.GetComponent < SkinnedMeshRenderer >( ).rootBone = AttachedCharacter.transform.Find( "root" );		tab.gameObject.SetActive(false);
		parent.DeleteLabel(tab.gameObject);
		parent.FixLayout();
	}

	public void OnTriggerEnter( Collider collider ) {
		if ( collider.tag == "Player" && tag == "PickUp" ){
			StaticManager.Character.inventory.PickUp(this);
			this.GetComponent < Collider >( ).enabled = false;
			AttachedCharacter = collider.GetComponent < Companion >( );
			model = Instantiate( model );
			model.SetActive(false);
			model.transform.SetParent(AttachedCharacter.transform);
		}
	}

}
