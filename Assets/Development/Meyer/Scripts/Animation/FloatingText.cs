using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	[SerializeField] Animator animator;

	private TextMeshPro damageText;

	public void OnEnable() { 
		AnimatorClipInfo[] info = animator.GetCurrentAnimatorClipInfo( 0 );
		damageText = animator.gameObject.GetComponent < TextMeshPro >( );
		Destroy(gameObject, info[ 0 ].clip.length);
	}

	public void SetDamage( string damage, Color color ) {
		damageText.color = color;
		damageText.text = damage;
	}

}


