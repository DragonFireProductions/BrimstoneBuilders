using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionContainerScript : ContainerScript {

	public override void OnPointerEnter( PointerEventData data ) {

	}


	public override void OnPointerExit( PointerEventData data ) {

	}

    public override void OnPointerDownDelegate(PointerEventData data)
    {
       GetComponentInParent<PotionAssignment>().potion.Cast( StaticManager.inventories.inventory.character as Companion );
    }

}
