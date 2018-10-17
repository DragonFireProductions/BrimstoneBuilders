using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Meyer.TestScripts;

public class UIUpdate : MonoBehaviour
{
    [Header("Status")]
    public Image HPBar;
    public Image StaminaBar;
    public Text HpText;
    public Text StaminaText;

    Stat Selected = null;
    
    
    // Update is called once per frame
    void Update()
    {
        if (TurnBasedController.instance != null)
        {
            Selected = TurnBasedController.instance.PlayerSelectedCompanion.stats;

        }
        else if (FindObjectOfType<CompanionLeader>())
        {
            Selected = FindObjectOfType<CompanionLeader>().stats;
        }


        if (Selected != null)
        {
            if (HPBar != null)
            {
                float HP = Selected.Health / Selected.MaxHealth;

                HPBar.fillAmount = HP;
                HPBar.color = Color.Lerp(Color.red, Color.green, HP);
            }

            if (StaminaBar != null)
                StaminaBar.fillAmount = Selected.Stamina / (Selected.Endurance * 10);

            if (HpText != null)
                HpText.text = Selected.Health.ToString() + " / " + Selected.MaxHealth.ToString();

            if (StaminaText != null)
                StaminaText.text = Selected.Stamina.ToString() + " / " + (Selected.Endurance * 10).ToString();
        }
    }
}