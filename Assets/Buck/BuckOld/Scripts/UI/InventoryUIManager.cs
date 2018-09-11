using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> inventoryTabs;

    public void OpenMateiralsTab()
    {
        foreach (GameObject tab in inventoryTabs)
        {
            tab.SetActive(false);
        }

        inventoryTabs[0].SetActive(true);
    }

    public void OpenWeaponsTab()
    {
        foreach (GameObject tab in inventoryTabs)
        {
            tab.SetActive(false);
        }

        inventoryTabs[1].SetActive(true);
    }

    public void OpenArmorTab()
    {
        foreach (GameObject tab in inventoryTabs)
        {
            tab.SetActive(false);
        }

        inventoryTabs[2].SetActive(true);
    }


}
