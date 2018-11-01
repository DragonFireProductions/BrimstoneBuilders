using System;
using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public UIItems ItemsInstance;

   
    private bool showWindow;

    public struct Stats {

       public TextMeshProUGUI Obj;

       public string Name;

    }

    private List < Stats > statUiList;

    private List < Stats > companionWindowStats;

    private List < Stats > weaponWindowStats;

    private List < Stats > gameStatsUiList;

    private List < Stats > weaponInventoryStatsUiList;

    private List < Stats > gameInventoryStatsUiList;

    public List < WeaponObject > AttachedWeapons;
    
    private Vector3 pos;

    private Vector3 pos2;
    
    public List<UIItemsWithLabels> Slots;

    public List < GameObject > BackpackSlots;

    public bool Dragging = false;
    
    // Use this for initialization
    public void Start( ) {
        AttachedWeapons = new List < WeaponObject >();
        AttachedWeapons.Add(StaticManager.Character.gameObject.transform.Find("Cube/EnemySword").gameObject.GetComponent<WeaponObject>());
        StaticManager.Inventory.PickedUpWeapons.Add(AttachedWeapons[0]);
        StartScript();

    }

    public void StartScript() {
        ItemsInstance = gameObject.GetComponent < UIItems >( );
        ItemsInstance.Start();
        pos = ItemsInstance.InventoryContainer.obj.gameObject.transform.position;
    }
    public void ShowNotification(string _message, float _time ) {
        ItemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text = _message;
        StartCoroutine( ShowNotification( _time ) );
    }

    private IEnumerator ShowNotification( float _time ) {
        yield return new WaitForSeconds(_time);
        ItemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text = "";

    }
    

    private bool active = false;
    public void ToggleShow(GameObject _obj ) {
        active = !_obj.activeSelf;
        _obj.SetActive(active);
    }
    public void Quit(GameObject _obj)
    {
        _obj.SetActive(false);
    }
    
    public bool Show {
        get {
            showWindow = !showWindow;

            return showWindow;
        }
        set { showWindow = value; }
    }
    public void AddSlot(WeaponObject _item)
    {
        var l_newContainer = Instantiate(ItemsInstance.InventoryContainer.obj);
        l_newContainer.SetActive(true);
        l_newContainer.transform.SetParent(ItemsInstance.InventoryContainer.obj.transform.parent);
        l_newContainer.transform.position = pos;
        l_newContainer.transform.localScale = ItemsInstance.InventoryContainer.obj.transform.localScale;
        l_newContainer.name = _item.WeaponStats.objectName + "Slot";
        UIItemsWithLabels newLabel = ItemsInstance.obj( l_newContainer );
        newLabel.obj.SetActive(true);
        Slots.Add(newLabel);
        UpdateStats( _item.WeaponStats, newLabel );

        l_newContainer.transform.Find( "ItemIconContainer/RawImage" ).GetComponent < RawImage >( ).texture = _item.WeaponStats.icon;
    }

    public void AddBackpackSlot(WeaponObject _item)
    {
        ItemsInstance.BackpackContainer.SetActive(true);
        var l_newContainer = Instantiate( ItemsInstance.BackpackContainer );
        l_newContainer.name = _item.gameObject.name + "Slot";
        l_newContainer.transform.SetParent(ItemsInstance.BackpackContainer.transform.parent);
        l_newContainer.transform.position = pos2;
        l_newContainer.transform.localScale = ItemsInstance.BackpackContainer.transform.localScale;
        BackpackSlots.Add(l_newContainer);
        l_newContainer.GetComponentInChildren<RawImage>().texture     = _item.WeaponStats.icon;
        l_newContainer.GetComponentInChildren<TextMeshProUGUI>().text = _item.WeaponStats.objectName;
        ItemsInstance.BackpackContainer.SetActive(false);

    }
    
    public void UpdateStats(Stat _stats, UIItemsWithLabels instanceToUpdate)
    {
        for (var l_i = 0; l_i < instanceToUpdate.Labels.Count; l_i++)
        {
            var l_a = _stats[instanceToUpdate.Labels[l_i].ToString()];
            instanceToUpdate.Labels[l_i].labelText.text = l_a.ToString();
        }

    }

    public void UpdateStats( WeaponItem _object , UIItemsWithLabels instanceToUpdate ) {
        for ( int i = 0 ; i < instanceToUpdate.Labels.Count ; i++ ){
            var a = _object[ instanceToUpdate.Labels[ i ].name ];
            instanceToUpdate.Labels[ i ].labelText.text = a.ToString( );
        }
    }
    public void RemoveMainInventory(WeaponObject _item)
    {
        for (var l_i = 0; l_i < Slots.Count; l_i++)
        {
            if (Slots[l_i].obj.name  == _item.WeaponStats.objectName + "Slot")
            {
                var l_slot = Slots[l_i].obj;
                Slots.RemoveAt(l_i);
                Destroy(l_slot);
            }
        }
    }

    public void RemoveBackpack( WeaponObject _item ) {
        for ( var l_i = 0 ; l_i < BackpackSlots.Count ; l_i++ ){
            if ( BackpackSlots[l_i].name == _item.name + "Slot"){
                var l_slot = BackpackSlots[ l_i ].gameObject;
                BackpackSlots.RemoveAt(l_i);
                Destroy(l_slot);
            }
        }
    }

    public void SelectItem( WeaponObject obj ) {
        SelectedItem = obj;
        SelectedItem.gameObject.SetActive(true);
        Dragging = true;
    }

    // Update is called once per frame
    public WeaponObject SelectedItem;

    public GameObject Ob;

    public Vector3 Offset;

    public Vector3 ScreenPoint;

    public bool IsMainInventory = true;
    
    void Update () {
        if ( Dragging /*&& Input.GetMouseButton(0)*/){
            SelectedItem.GetComponent < BoxCollider >( ).enabled = true;
            var l_x = StaticManager.Character.gameObject.transform.position.x;
            var l_worldPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
            l_worldPoint.x = StaticManager.Character.gameObject.transform.position.x;
            l_worldPoint.z = StaticManager.Character.gameObject.transform.position.z;
            l_worldPoint.y = StaticManager.Character.gameObject.transform.position.z;
            var l_hit = Physics2D.Raycast(l_worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null and the the object must have the "Monkey" tag and target has to be null
            if (l_hit.collider != null && l_hit.collider.tag == "Weapon")
            {
                SelectedItem.transform.position = l_worldPoint; // Sets the target to be the transform that was hit
            }

            var l_distanceToScreen = Camera.main.WorldToScreenPoint(SelectedItem.transform.position).z;
            var l_curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, l_distanceToScreen));
            l_curPosition.x = Mathf.Clamp( l_curPosition.x , StaticManager.Character.gameObject.transform.position.x ,StaticManager.Character.gameObject.transform.position.x );
            l_curPosition.z = Mathf.Clamp( l_curPosition.z ,StaticManager.Character.gameObject.transform.position.z ,StaticManager.Character.gameObject.transform.position.z);
            SelectedItem.transform.position = l_curPosition;
            
        }

        if (Input.GetMouseButtonUp(0) && Dragging)
        {
            Dragging = false;
            SelectedItem.gameObject.SetActive(false);
        }
        if ( Input.GetKeyDown(KeyCode.Escape) ){
            ItemsInstance.PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        StaticManager.UiInventory.ViewEnemyStats();

    }
  
    public void ViewEnemyStats()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Mouse is down");

            RaycastHit l_hitInfo;
            var l_hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);
            if (l_hit)
            {
                Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);
                if (l_hitInfo.transform.gameObject.tag == "Enemy" || l_hitInfo.transform.gameObject.tag == "Player" || l_hitInfo.transform.gameObject.tag == "Companion")
                {
                    //UpdateStats(l_hitInfo.transform.gameObject.GetComponent<Stat>(), ItemsInstance.StatUI);
                   
                }
                else if (l_hitInfo.transform.gameObject.tag == "Weapon")
                {
                   //UpdateStats(l_hitInfo.transform.gameObject.GetComponent<WeaponObject>().WeaponStats, ItemsInstance.WeaponStatsUI);
                    //UpdateGameWeaponStats(l_hitInfo.]transform.gameObject.GetComponent<GunType>());
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");

        }
    }

}
