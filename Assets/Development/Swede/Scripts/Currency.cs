using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

using Image = UnityEngine.UI.Image;

public class Currency : MonoBehaviour
{
    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

    public GameObject buyContainer;

    public GameObject sellContainer;

    public List < GameObject > shops;

    public GameObject sellButton;

    public GameObject buyButton;

    public void AddCoins(Shop shop)
    {
        StaticManager.Character.inventory.coinCount += shop.resaleWorth;
    }
    public void AddCoins(int _coins)
    {
        StaticManager.Character.inventory.coinCount += _coins;
    }
    
    public void RemoveCoins(Shop shop)
    {
        if (StaticManager.Character.inventory.coinCount <= shop.companionPrice)
        {
            StaticManager.Character.inventory.coinCount = 0;
        }
        else
        {
            StaticManager.Character.inventory.coinCount -= shop.companionPrice;
        }
    }
    public bool RemoveCoins(int coins) {
        return StaticManager.Character.inventory.coinCount >= coins;
    }

    public void SwitchToBuy( ) {
        StaticManager.UiInventory.ItemsInstance
                        .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "";
        buyButton.GetComponent < Image >( ).color = Color.red;
         sellButton.GetComponent < Image >( ).color = Color.grey;
        _shop.Buy.SetActive(true);
        _shop.Sell.SetActive(false);
    }

    public void SwitchToSell( ) {
         buyButton.GetComponent < Image >( ).color = Color.grey;
         sellButton.GetComponent < Image >( ).color = Color.red;
        _shop.Buy.SetActive(false);
        _shop.Sell.SetActive(true);
    }
    
    public void BuyCompanion(Tab container) {
        
            if (RemoveCoins(container.companion.cost))
            {
                Vector3 position = Random.insideUnitSphere * 5 + StaticManager.Character.transform.position;
                position.y = 0;
                StaticManager.Character.inventory.coinCount -= container.companion.cost;
                container.companion.cost = container.companion.cost / 2;
                StaticManager.UiInventory.ItemsInstance
                            .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "";


                var newCompanion = Instantiate( container.companion.gameObject );
                Destroy(container.gameObject);
                var companion = newCompanion.GetComponent < Companion >( );
                companion.inventoryUI.AddToShop(_shop);
                container.companion = companion;
                companion.inventoryUI.Init(companion);
                companion.startWeapon = Instantiate( companion.startWeapon );
                companion.startWeapon.GetComponent<WeaponObject>().PickUp(companion);
                companion.startWeapon.GetComponent<WeaponObject>().Attach();
                companion.inventoryUI.SellButton.SetActive(true);
                companion.inventoryUI.BuyButton.SetActive(false);
                companion.inventoryUI.tab.gameObject.SetActive(true);
                companion.inventoryUI.UpdateCharacter(container.companion.inventoryUI.ShopCharacterText);
               companion.inventoryUI.CompanionSell.gameObject.transform.SetParent(sellContainer.transform);
                
                StaticManager.particleManager.Play(ParticleManager.states.Spawn, position);

                var location = GameObject.Find( "panel_location" );
                var newButton = Instantiate( Resources.Load < companionBehaviors >( "Panel" ) );
                companion.GetComponent < CompanionNav >( ).behaviors = newButton.GetComponent < companionBehaviors >( );
                newButton.GetComponent < companionBehaviors >( ).newFriend = companion;
                newButton.transform.SetParent(location.transform, false);
                newButton.transform.position = location.transform.position;
                 StaticManager.inventories.behaviors.Add(newButton);
                companion.gameObject.transform.position = position;
                //companion.Nav.Agent.Warp( position );

                StaticManager.RealTime.Companions.Add(companion);
               StaticManager.inventories.alllables.Add(companion.inventory);
                companion.gameObject.SetActive(true);
                companion.inventoryUI.sendToButton.gameObject.SetActive(true);
               companion.Nav.enabled = true;
               Debug.Log("Enabled");
               companion.Nav.SetState = BaseNav.state.IDLE;
               companion.GetComponent<CompanionNav>().SetAgreesionState = CompanionNav.AggressionStates.PASSIVE;
                StartCoroutine( Wait( companion ) );
                companion.inventoryUI.CharacterInventory.SetActive( false );

            for (int i = 0; i < _shop.shopCompanions.Count; i++)
            {
                if (container.companion == _shop.companions[i].companion)
                {
                    companion.mele.CurrentLevel = _shop.companions[i].Melee;
                    companion.magic.CurrentLevel = _shop.companions[i].Magic;
                    companion.range.CurrentLevel = _shop.companions[i].Range;
                    
                    companion.attachedWeapon.AssignDamage();

                    break;
                }
            }
        }
            else
            {

                StaticManager.UiInventory.ItemsInstance
                        .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Not Enough Coins.";
            }
            StaticManager.UiInventory.ItemsInstance
                    .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = "Max Companions.";
    }

    private IEnumerator Wait(Companion companion ) {
        yield return new WaitForSeconds( 1 );

        companion.Nav.enabled = true;

    }
    public void SellCompanion( Tab container) {
        var c = container.companion.Nav as CompanionNav;
        Destroy(c.behaviors.gameObject);
        StaticManager.inventories.Destroy(container.companion.inventory);
        AddCoins(container.companion.cost);
        Destroy(container.companion.gameObject);
        _shop.shopCompanions.Remove( container );
        Destroy(container.gameObject);
    }
}
