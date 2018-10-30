using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseCharacter {
	

	void Remove( BaseCharacter character );

	IEnumerator Damage(BaseCharacter character );

}
