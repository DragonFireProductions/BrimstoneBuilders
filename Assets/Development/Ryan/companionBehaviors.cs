using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class companionBehaviors : MonoBehaviour
{
    [SerializeField] private CompanionNav.AggressionStates currentstate;

    public Companion newFriend;

    public Image XPBar;
    public Image HPBar;
    public Image StaminaBar;
    public Text HpText;
    public Text StaminaText;
    public Text XPText;
    public Text LvlText;
    private Text name;
    private GameObject prev_button;

    // Use this for initialization
    void Start()
    {
        name = transform.Find("character_name").GetComponent<Text>();
        name.text = newFriend.stats.name;
    }

    public void onClick(int state)
    {

        if (prev_button)
        {
            prev_button.GetComponent<Image>().color = Color.white;
        }
        newFriend.GetComponent<CompanionNav>().SetAgreesionState = (CompanionNav.AggressionStates)state;


    }

    public void color(GameObject go)
    {
        go.GetComponent<Image>().color = new Color(29.0f / 255.0f, 188.0f / 255.0f, 8.0f / 255.0f);


        prev_button = go;
    }

    // Update is called once per frame
    void Update()
    {
        if (newFriend != null)
        {
            if (XPBar != null)
            {
                XPBar.fillAmount = newFriend.CurrentSubClass.CurrentLevel - (int)newFriend.CurrentSubClass.CurrentLevel;
         
                LvlText.text = ((int)newFriend.CurrentSubClass.CurrentLevel).ToString();
            }
            if (HPBar != null)
            {
                float HP = newFriend.stats.Health / newFriend.stats.MaxHealth;

                HPBar.fillAmount = HP;
            }

            if (StaminaBar != null)
                StaminaBar.fillAmount = newFriend.stats.Stamina / (newFriend.stats.Endurance * 10);

            if (HpText != null)
                HpText.text = newFriend.stats.Health.ToString() + " / " + newFriend.stats.MaxHealth.ToString();

            if (StaminaText != null)
                StaminaText.text = newFriend.stats.Stamina.ToString() + " / " + (newFriend.stats.Endurance * 10).ToString();
            
        }
    }
    public void Heal()
    {
        if (newFriend.inventory.potions.Count > 0)
        {
            newFriend.inventory.potions[0].Cast(this.newFriend);
        }
        else if (StaticManager.Character.inventory.potions.Count > 0)
        {
            newFriend.inventory.potions.Add(StaticManager.Character.inventory.potions[0]);
            StaticManager.Character.inventory.potions.RemoveAt(0);
            newFriend.inventory.potions[0].Cast(this.newFriend);
        }
    }
}
