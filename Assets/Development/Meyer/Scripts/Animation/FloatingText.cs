using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	[SerializeField] public Animator animator;

    public AudioClip clip;

    private AudioSource audio;

	private TextMeshPro damageText;

	public void OnEnable() { 
		AnimatorClipInfo[] info = animator.GetCurrentAnimatorClipInfo( 0 );
		damageText = animator.gameObject.GetComponent < TextMeshPro >( );
		Destroy(gameObject, info[ 0 ].clip.length);
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.spatialBlend = 1.0f;
        audio.rolloffMode = AudioRolloffMode.Linear;
    }

	public void SetDamage( string damage, Color color ) {
		damageText.color = color;
		damageText.text = damage;
        if (damageText.color == Color.yellow)
        {
            audio.Play();
        }
	}

}


