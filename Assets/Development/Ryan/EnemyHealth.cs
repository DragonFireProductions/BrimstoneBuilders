using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Kristal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public Image HPbar;

    private Stat enemy;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
	    enemy = gameObject.GetComponentInParent<BaseCharacter>().stats;

       // transform.LookAt(Camera.main.transform.position);

	    if (enemy != null)
	    {
	        if (HPbar != null)
	        {
	            float hp = enemy.Health / enemy.MaxHealth;

	            HPbar.fillAmount = hp;
	            //HPbar.color = Color.Lerp(Color.red, Color.green, hp);
	        }
	    }
	}
}
