using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] public int companionPrice;
    [SerializeField] public int resaleWorth;
    // Use this for initialization
    void Start()
    {

        StaticManager.UiInventory.ItemsInstance
                .GetLabel("CompanionBuyPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = companionPrice.ToString();
        StaticManager.UiInventory.ItemsInstance.GetLabel("CompanionSellPrice", StaticManager.UiInventory.ItemsInstance.ShopUI).text = resaleWorth.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
