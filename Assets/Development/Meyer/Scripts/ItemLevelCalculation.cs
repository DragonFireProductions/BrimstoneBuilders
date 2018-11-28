using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelCalculation : MonoBehaviour {

	public void IncreaseLevel(BaseItems item, float int_f ) {
		item.objectLevel += int_f;
		item.IncreaseStats(int_f);
	}
	
}
