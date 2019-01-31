using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Currency : MonoBehaviour {

    public int companionBuyCost;

    public int companionSellCost;

    public Shop _shop;

    public ShopContainer containerHolder;

    public CompanionContainer container; 

    public List < GameObject > shops = new List < GameObject >();

    public GameObject sellButton;

    public GameObject buyButton;

    public GameObject ShopUI;

    public int companions = 0;
    public void AddCoins( Shop shop ) {
        StaticManager.Character.inventory.coinCount += shop.resaleWorth;
    }

    public void AddCoins( int _coins ) {
        StaticManager.Character.inventory.coinCount += _coins;
    }

    public void RemoveCoins( Shop shop ) {
        if ( StaticManager.Character.inventory.coinCount <= shop.companionPrice ){
            StaticManager.Character.inventory.coinCount = 0;
        }
        else{
            StaticManager.Character.inventory.coinCount -= shop.companionPrice;
        }
    }

    public bool RemoveCoins( int coins ) {
        return StaticManager.Character.inventory.coinCount > coins;
    }

    public void SwitchToBuy( ) {
        StaticManager.UiInventory.ItemsInstance
                .GetLabel( "CompanionBuyError" , StaticManager.UiInventory.ItemsInstance.ShopUI )
                .text = "";

        buyButton.GetComponent < Image >( ).color  = Color.red;
        sellButton.GetComponent < Image >( ).color = Color.grey;
        _shop.shopContainer.buy.SetActive( true );
        _shop.shopContainer.sell.SetActive( false );
    }

    public void SwitchToSell( ) {
        buyButton.GetComponent < Image >( ).color  = Color.grey;
        sellButton.GetComponent < Image >( ).color = Color.red;
        _shop.shopContainer.buy.SetActive( false );
        _shop.shopContainer.sell.SetActive( true );
        
    }

    public void BuyCompanion( CompanionContainer container ) {
        if ( RemoveCoins( container.companion.cost )  ){
            var position = Random.insideUnitSphere * 5 + StaticManager.Character.transform.position;
            position.y                                  =  0;
            StaticManager.Character.inventory.coinCount -= container.companion.cost;
            container.companion.cost                    =  container.companion.cost / 2;

            StaticManager.UiInventory.ItemsInstance
                    .GetLabel( "CompanionBuyError" , StaticManager.UiInventory.ItemsInstance.ShopUI )
                    .text = "";

            container.shop.ShopCompanions--;
            container.companion.inventoryUI.UpdateCharacter( container.companion.inventoryUI.CompanionSell.characterstats );
            container.companion.inventoryUI.CompanionSell.gameObject.transform.SetParent( _shop.shopContainer.sell.transform );
            container.companion.GetComponent < NavMeshAgent >( ).Warp( _shop.transform.position );

            StaticManager.particleManager.Play( ParticleManager.states.Spawn , position );

            var location  = GameObject.Find( "panel_location" );

            var newButton = Instantiate( Resources.Load < companionBehaviors >( "Panel" ) );
            container.companion.GetComponent < CompanionNav >( ).behaviors       = newButton.GetComponent < companionBehaviors >( );
            newButton.GetComponent < companionBehaviors >( ).newFriend = container.companion;
            newButton.transform.SetParent( location.transform , false );
            newButton.transform.position = location.transform.position;
            StaticManager.inventories.behaviors.Add( newButton );

            StaticManager.RealTime.Companions.Add( container.companion );
            StaticManager.inventories.alllables.Add( container.companion.inventory );
            container.companion.gameObject.SetActive( true );
            container.companion.inventoryUI.sendToButton.gameObject.SetActive( true );
            container.companion.Nav.enabled = true;
            container.companion.Nav.SetState                                       = BaseNav.state.IDLE;
            container.companion.GetComponent < CompanionNav >( ).SetAgreesionState = CompanionNav.AggressionStates.PASSIVE;
            StartCoroutine( Wait( container.companion ) );
            container.companion.inventoryUI.CharacterInventory.SetActive( false );
            container.companion.attachedWeapon.AssignDamage();

            newButton.GetComponent<companionBehaviors>().color(newButton.transform.Find("Passive").gameObject);

           container.companion.inventoryUI.sendToButton.gameObject.SetActive(true);
           container.companion.inventoryUI.tab.gameObject.SetActive(true);
            container.buyButton.SetActive(false);
            container.sellButton.SetActive(true);
            ///doesn't work
        }
        else if ( companions >= 5 ){
            StaticManager.UiInventory.ItemsInstance
               .GetLabel("CompanionBuyError", StaticManager.UiInventory.ItemsInstance.ShopUI)
               .text = "Max Companions.";
        }
        else{
            StaticManager.UiInventory.ItemsInstance
                    .GetLabel( "CompanionBuyError" , StaticManager.UiInventory.ItemsInstance.ShopUI )
                    .text = "Not Enough Coins.";
        }
       
    }

    private IEnumerator Wait( Companion companion ) {
        yield return new WaitForSeconds( 1 );

        companion.Nav.enabled = true;
    }

    public void SellCompanion( CompanionContainer container ) {
        var c = container.companion.Nav as CompanionNav;
        Destroy( c.behaviors.gameObject );
        StaticManager.inventories.Destroy( container.companion.inventory );
        AddCoins( container.companion.cost );
        Destroy( container.companion.gameObject );
        _shop.shopCompanions.Remove( container );
        Destroy( container.gameObject );
    }

}