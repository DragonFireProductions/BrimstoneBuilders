using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Meyer.TestScripts;

public class CompanionUIUpdate : MonoBehaviour
{
    [Header("Status")]
    public Image HPBar;

    private Stat Selected;
	
	// Update is called once per frame
	void Update ()
	{
	    Selected = StaticManager.Character.stats;

	    if (Selected != null)
	    {
	        if (HPBar != null)
	        {
	            float HP = Selected.Health / Selected.MaxHealth;

	            HPBar.fillAmount = HP;
	            HPBar.color = Color.Lerp(Color.red, Color.green, HP);
	        }
	    }
    }
}
