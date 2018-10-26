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

    struct Stats {

       public TextMeshProUGUI Obj;

       public string Name;

    }

    private List < Stats > statUiList;

    private List < Stats > companionUiList;

    private List < Stats > weaponUiList;

    private List < Stats > gameStatsUiList;

    private List < Stats > weaponInventoryStatsUiList;

    private List < Stats > gameInventoryStatsUiList;

    public List < WeaponObject > AttachedWeapons;
    
    private Vector3 pos;

    private Vector3 pos2;
    
    public List<GameObject> Slots;

    public List < GameObject > BackpackSlots;

    public bool Dragging = false;
    
    // Use this for initialization
    

    public void StartScript() {
        ItemsInstance = gameObject.GetComponent < UIItems >( );
        ItemsInstance.Start();
        pos = ItemsInstance.InventoryContainer.gameObject.transform.position;
        statUiList = new List<Stats>();
        companionUiList = new List<Stats>();
        weaponUiList = new List < Stats >();
        gameStatsUiList = new List < Stats >( );
        weaponInventoryStatsUiList = new List < Stats >( );
        gameInventoryStatsUiList = new List < Stats >();
        ItemsInstance.AttackConfirmation.SetActive(false);
        //itemsInstance.StatUI = new GameObject();

        var l_i = ItemsInstance.StatLabels.transform.childCount;

        for (var l_j = 0; l_j < l_i; l_j++)
        {
            Stats l_stats;
            l_stats.Obj = ItemsInstance.StatLabels.transform.GetChild(l_j).GetComponent<TextMeshProUGUI>();
            l_stats.Name = ItemsInstance.StatLabels.transform.GetChild(l_j).name;
            statUiList.Add(l_stats);
        }

        l_i = ItemsInstance.CompanionLabels.transform.childCount;

        for (var l_j = 0; l_j < l_i; l_j++)
        {
            Stats l_stats;
            l_stats.Obj = ItemsInstance.CompanionLabels.transform.GetChild(l_j).GetComponent<TextMeshProUGUI>();
            l_stats.Name = ItemsInstance.CompanionLabels.transform.GetChild(l_j).name;
            companionUiList.Add(l_stats);
        }

        l_i = ItemsInstance.WeaponLabels.transform.childCount;

        for ( var l_j = 0 ; l_j < l_i ; l_j++ ){
            Stats l_stats;
            l_stats.Obj = ItemsInstance.WeaponLabels.transform.GetChild( l_j ).GetComponent < TextMeshProUGUI >( );
            l_stats.Name = ItemsInstance.WeaponLabels.transform.GetChild( l_j ).name;
         
            weaponUiList.Add(l_stats);
        }

        l_i = ItemsInstance.GameStatLabels.transform.childCount;

        for ( var l_j = 0 ; l_j < l_i ; l_j++ ){

            Stats l_stats;
            l_stats.Obj = ItemsInstance.GameStatLabels.transform.GetChild( l_j ).GetComponent < TextMeshProUGUI >( );
            l_stats.Name = ItemsInstance.GameStatLabels.transform.GetChild( l_j ).name;

            gameStatsUiList.Add(l_stats);
        }
        l_i = ItemsInstance.WeaponInventoryLabels.transform.childCount;

        for (var l_j = 0; l_j < l_i; l_j++)
        {

            Stats l_stats;
            l_stats.Obj = ItemsInstance.WeaponInventoryLabels.transform.GetChild(l_j).GetComponent<TextMeshProUGUI>();
            l_stats.Name = ItemsInstance.WeaponInventoryLabels.transform.GetChild(l_j).name;

            weaponInventoryStatsUiList.Add(l_stats);
        }
        l_i = ItemsInstance.GameInventoryStatLabels.transform.childCount;

        for (var l_j = 0; l_j < l_i; l_j++)
        {

            Stats l_stats;
            l_stats.Obj = ItemsInstance.GameInventoryStatLabels.transform.GetChild(l_j).GetComponent<TextMeshProUGUI>();
            l_stats.Name = ItemsInstance.GameInventoryStatLabels.transform.GetChild(l_j).name;

            gameInventoryStatsUiList.Add(l_stats);
        }


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
        var l_newContainer = Instantiate(ItemsInstance.InventoryContainer);
        l_newContainer.SetActive(true);
        l_newContainer.transform.SetParent(ItemsInstance.InventoryContainer.transform.parent);
        l_newContainer.transform.position = pos;
        l_newContainer.transform.localScale = ItemsInstance.InventoryContainer.transform.localScale;
        Set(_item, l_newContainer);
        Slots.Add(l_newContainer);
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

    public void UpdateStats(Stat _stats ) {
        for ( var l_i = 0 ; l_i < statUiList.Count ; l_i++ ){
            var l_a = _stats[ statUiList[ l_i ].Name ];
            statUiList[ l_i ].Obj.text = l_a.ToString( );
        }

    }

    public void UpdateWeaponStats(WeaponItem _item ) {
        for ( var l_i = 0 ; l_i < weaponUiList.Count ; l_i++ ){
            var l_a = _item[ weaponUiList[ l_i ].Name ];
            weaponUiList[ l_i ].Obj.text = "+" + l_a.ToString( );
        }
        
    }
    

    public void UpdateGameWeaponStats( GunType _obj ) {
        for ( var l_i = 0 ; l_i < gameStatsUiList.Count ; l_i++ ){
            var l_a = _obj[ gameStatsUiList[ l_i ].Name ];
            gameStatsUiList[ l_i ].Obj.text = l_a.ToString( );
        }
    }

    public void UpdateWeaponInventoryStats(WeaponItem _obj)
    {
        for (var l_i = 0; l_i < weaponInventoryStatsUiList.Count; l_i++)
        {
            var l_a = _obj[weaponInventoryStatsUiList[l_i].Name];
            weaponInventoryStatsUiList[l_i].Obj.text = l_a.ToString();
        }
    }
    public void UpdateGameInventoryStats(GunType _item) {

        for (var l_i = 0; l_i < gameInventoryStatsUiList.Count; l_i++)
        {
            var l_a = _item[gameInventoryStatsUiList[l_i].Name];
            gameInventoryStatsUiList[l_i].Obj.text = l_a.ToString();
        }

    }
    public void UpdateCompanionStats(Stat _stats)
    {
        for (var l_i = 0; l_i < companionUiList.Count; l_i++)
        {
            var l_a = _stats[companionUiList[l_i].Name];
            companionUiList[l_i].Obj.text = l_a.ToString();
        }

    }
    public void Remove(WeaponObject _item)
    {
        for (var l_i = 0; l_i < Slots.Count; l_i++)
        {
            if (Slots[l_i].name  == _item.name + "Slot")
            {
                var l_slot = Slots[l_i].gameObject;
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
    public void Set(WeaponObject _weapons, GameObject _container_p)
    {
        var l_transforms = _container_p.GetComponentsInChildren<TextMeshProUGUI>();
        _container_p.name = _weapons.name + "Slot";
        foreach ( var l_t in l_transforms ){
            switch (l_t.name)
            {
                case "ItemName":
                    l_t.GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.objectName.ToString();
                    break;
                case "ItemValue":
                    l_t.GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.value.ToString();
                    break;
                case "ItemWeight":
                    l_t.GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.weight.ToString();
                    break;
                case "ItemDurability":
                    l_t.GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.durability.ToString();
                    break;
                default:
                    break;
            }
        }
        _container_p.GetComponentInChildren<RawImage>().texture = _weapons.WeaponStats.icon;
    }

    public void SetBackpack( WeaponObject _weapons, GameObject _container ) {
        _container.GetComponentInChildren < TextMeshProUGUI >( ).text = _weapons.WeaponStats.objectName;
        _container.transform.Find( "icon" ).GetComponent < RawImage >( ).texture = _weapons.WeaponStats.icon;
    }
    
    public void ShowPauseMenu(bool _show_pause)
    {
        ItemsInstance.PauseUI.SetActive(_show_pause);
    }

    // Update is called once per frame
    public WeaponObject SelectedItem;

    public GameObject Ob;

    public Vector3 Offset;

    public Vector3 ScreenPoint;

    public bool IsMainInventory = true;
    
    void Update () {
        if ( Dragging /*&& Input.GetMouseButton(0)*/){
            SelectedItem.gameObject.SetActive(true);
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
            l_curPosition.x = Mathf.Clamp( l_curPosition.x , StaticManager.Character.gameObject.transform.position.x - 2 ,StaticManager.Character.gameObject.transform.position.x + 2 );
            l_curPosition.z = Mathf.Clamp( l_curPosition.z ,StaticManager.Character.gameObject.transform.position.z -2 ,StaticManager.Character.gameObject.transform.position.z + 2 );
            SelectedItem.transform.position = l_curPosition;
            
        }

        if (Input.GetMouseButtonUp(0) && Dragging)
        {
            Dragging = false;
            SelectedItem.gameObject.SetActive(false);
        }
        if ( Input.GetKeyDown(KeyCode.Escape) ){
            ShowPauseMenu(Show);
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
                    UpdateStats(l_hitInfo.transform.gameObject.GetComponent<Stat>());
                   
                }
                else if (l_hitInfo.transform.gameObject.tag == "Weapon")
                {
                    UpdateWeaponStats(l_hitInfo.transform.gameObject.GetComponent<WeaponObject>().WeaponStats);
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
