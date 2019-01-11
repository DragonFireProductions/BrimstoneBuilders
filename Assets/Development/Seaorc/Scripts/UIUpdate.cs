using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Meyer.TestScripts;

public class UIUpdate : MonoBehaviour
{
    [Header("Status")]
    public Image HPBar;
    public Image StaminaBar;
    public Text HpText;
    public Text StaminaText;
    
    public Text CoinText;

    private Stat Selected;
    private int Coins;

    // Update is called once per frame
    void Update()
    {
        Selected = StaticManager.Character.stats;

        Coins = StaticManager.Character.inventory.coinCount;
        //if (TurnBasedController.instance != null && TurnBasedController.instance._player.selectedAttacker!= null)
        //{
        //    Selected = TurnBasedController.instance._player.selectedAttacker.stats;

        //}
        //else if (FindObjectOfType<CompanionLeader>())
        //{
        //    Selected = FindObjectOfType<CompanionLeader>().stats;
        //}


        if (Selected != null)
        {
            if (HPBar != null)
            {
                float HP = Selected.Health / Selected.MaxHealth;

                HPBar.fillAmount = HP;
                HPBar.color = Color.Lerp(Color.red, Color.blue, HP);
            }

            if (StaminaBar != null)
                StaminaBar.fillAmount = Selected.Stamina / (Selected.Endurance * 10);

            if (HpText != null)
                HpText.text = Selected.Health.ToString() + " / " + Selected.MaxHealth.ToString();

            if (StaminaText != null)
                StaminaText.text = Selected.Stamina.ToString() + " / " + (Selected.Endurance * 10).ToString();
        }

        if(Coins == 0)
            CoinText.text = "0";
        else
            CoinText.text = Coins.ToString();
    }
}