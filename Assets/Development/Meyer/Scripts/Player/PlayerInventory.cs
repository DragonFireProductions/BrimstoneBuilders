using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Meyer.TestScripts.Player;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public static UIInventory uiInventory = StaticManager.UiInventory;

    public CharacterArmorInventory armorInventory;

    public CharacterWeaponsInventory WeaponInventory;

    

    [ HideInInspector ] public List < Potions > potions;

    [ HideInInspector ] public List < Potions > PickedUpPotions;

    public GameObject parent;

    public List<UIItemsWithLabels> Slots;

    public List < UIItemsWithLabels > PotionSlots;

    public Companion character;

    public GameObject potionsContainer;

    public int coinCount;


    public void Awake( ) {
       
       // AttachedWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        character = GetComponent < Companion >( );
        Slots = new List < UIItemsWithLabels >();
    }
    
    public void Start( ) {
       // PickedUpWeapons.Add(gameObject.transform.Find("Cube").gameObject.GetComponentInChildren<WeaponObject>());
        coinCount = 0;
    }

    public void PickUp( Potions _potions ) {
        PickedUpPotions.Add(_potions);
        //_potions.gameObject.SetActive(false);
        character.inventoryUI.HealCount.text = potions.Count.ToString();
    }

    public virtual void PickUp( BaseItems item ) {
      
    }
    public void PickUpCoin(int _coinWorth)
    {
        StaticManager.currencyManager.AddCoins(_coinWorth);
    }

    public virtual void Close( ) {
        character.inventoryUI.PotionsInventory.gameObject.SetActive(false);
    }
    
    private void Update( ) {

    }

}