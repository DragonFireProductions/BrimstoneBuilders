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
    

    public void PickUp( Potions potions ) {
        PickedUpPotions.Add(potions);
        character.inventoryUI.AddPotion(potions);
        potions.gameObject.SetActive(false);
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
        if ( Input.GetButtonDown( "Inventory" ) ){

            if ( StaticManager.uiManager.playerUI.gameObject.activeSelf ){
                StaticManager.inventories.CloseAll();

            }
            else{
                 StaticManager.inventories.prevPos = StaticManager.Character.transform.position;
                 StaticManager.UiInventory.ShowWindow( StaticManager.UiInventory.ItemsInstance.PlayerUI );
                 character.inventoryUI.UpdateItem();
                 StaticManager.inventories.SwitchInventory(StaticManager.Character.inventoryUI.tab);
                 StaticManager.inventories.inventory.character.projector.gameObject.SetActive(false);
                 StaticManager.inventories.SwitchToWeapons();
            }
        
        }

        if ( Input.GetKeyDown(KeyCode.M) ){
            if ( StaticManager.map.map.activeSelf ){
                StaticManager.inventories.CloseAll();
            }
            else{
                StaticManager.inventories.SwitchToMap();
            }
        }

        if ( Input.GetKeyDown(KeyCode.L) ){
            if ( StaticManager.questManager.questWindow.activeSelf ){
                StaticManager.inventories.CloseAll();
            }
            else{
                StaticManager.inventories.SwitchToQuest();
            }
        }

        if ( Input.GetKeyDown(KeyCode.H) && potions.Count > 0 && character is Character ){
            potions[0].Cast(character);
        }
    }

}