using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

public class ArmorItem : BaseItems {

    public enum Type {head, shoulder, shoe, glove, body, belt }

	public Type type;

	public GameObject armor;

	public Tab tab;
	public virtual void PickUp(Companion companion ) {
		
	}

	public override void Attach() {
			var companion = AttachedCharacter as Companion;
			switch ( type ){
                case Type.belt:

	                if ( companion.armor.currentBelt ){
	                companion.armor.currentBelt.armor.SetActive( false );
					companion.armor.currentBelt.tab.gameObject.SetActive(true);
                }
                else if (companion.armor.startBelt)
                {
                    companion.armor.startBelt.SetActive(false);
                }
                companion.armor.currentBelt = this;
					
	                break;
                case Type.shoulder:
                if (companion.armor.currentShoulder)
                {
                    companion.armor.currentShoulder.armor.SetActive(false);
					companion.armor.currentShoulder.tab.gameObject.SetActive(true);
                }
                else if (companion.armor.startShoulder)
                {
                    companion.armor.startShoulder.SetActive(false);
                }
                companion.armor.currentShoulder = this;

	                break;
                case Type.body:
                if (companion.armor.currentBody)
                {
                    companion.armor.currentBody.armor.SetActive(false);
					companion.armor.currentBody.tab.gameObject.SetActive(true);
                }
                else if (companion.armor.startBody)
                {
                    companion.armor.startBody.SetActive(false);
                }
                companion.armor.currentBody = this;

                    break;
                case Type.glove:
                if (companion.armor.currentGlove)
                {
                    companion.armor.currentGlove.armor.SetActive(false);
					companion.armor.currentGlove.tab.gameObject.SetActive(true);
                }
                else if (companion.armor.startGlove)
                {
                    companion.armor.startGlove.SetActive(false);
                }
                companion.armor.currentGlove = this;

                    break;
                case Type.shoe:
                if (companion.armor.currentShoe)
                {
                    companion.armor.currentShoe.armor.SetActive(false);
					companion.armor.currentShoe.tab.gameObject.SetActive(true);

                }
                else if (companion.armor.startShoe)
                {
                    companion.armor.startShoe.SetActive(false);
                }
                companion.armor.currentShoe = this;

                    break;

                case Type.head:
                if (companion.armor.currentHead)
                {
                    companion.armor.currentHead.armor.SetActive(false);
					companion.armor.currentHead.tab.gameObject.SetActive(true);
                }
                else if (companion.armor.startHead)
                {
                    companion.armor.startHead.SetActive(false);
                }
                companion.armor.currentHead = this;

                    break;
            }
			armor.SetActive(true);
		tab.gameObject.SetActive(false);
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
