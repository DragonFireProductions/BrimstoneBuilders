using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;

    public GameObject Buy;

    public GameObject Sell;

    public GameObject ShopContainer;

    public List <Companion> companions;

    public GameObject sellContainer;
    // Use this for initialization
    void Start()
    {
       StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellError", StaticManager.UiInventory.ItemsInstance.ShopUI).text = " ";
        Buy = ShopContainer.transform.Find( "Buy" ).gameObject;
        Sell = ShopContainer.transform.Find( "Sell" ).gameObject;
        StartCoroutine( init( ) );
    }

    IEnumerator init( ) {

        foreach ( var l_companion in companions ){
            var a = Instantiate( l_companion.gameObject );
            var o = a.GetComponent < Companion >( );
           
            while ( !o|| !o.inventory || !o.inventoryUI ){
                
                yield return new WaitForEndOfFrame();
                o.inventoryUI.AddToShop(this);
                shopCompanions.Add(o.inventoryUI.CompanionSell);
                o.inventoryUI.CompanionSell.transform.SetParent(Buy.transform);
                o.inventoryUI.CompanionSell.transform.localScale = new Vector3( 1,1,1);
                o.inventoryUI.UpdateCharacter(o.inventoryUI.ShopCharacterText);
                o.inventoryUI.UpdateItem(o.inventoryUI.ShopWeaponsText, o.attachedWeapon);
                o.inventoryUI.tab.gameObject.SetActive(false);


            }
        }
    }
    public List <Tab> shopCompanions = new List < Tab >();
    
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
