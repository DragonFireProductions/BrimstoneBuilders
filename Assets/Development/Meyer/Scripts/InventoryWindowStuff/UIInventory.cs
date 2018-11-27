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
    

    

    private Vector3 pos;

    private Vector3 pos2;


    public List < GameObject > BackpackSlots;

    public bool Dragging = false;

   

    // Use this for initialization
    public void Awake( ) {
        
        ItemsInstance = gameObject.GetComponent<UIItems>();
        ItemsInstance.Initalize();
        StartScript();

    }

    public void StartScript() {

       ItemsInstance.DialogueUI.SetActive(true);
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
    public void UpdateStats(Stat _stats, UIItemsWithLabels instanceToUpdate, bool comparative)
    {
       
        for (var l_i = 0; l_i < instanceToUpdate.Labels.Count; l_i++)
        {
            var l_a = _stats[instanceToUpdate.Labels[l_i].name.ToString()];
            if (comparative)
            {
                if ( Convert.ToInt32(l_a.ToString()) > 0){
                     instanceToUpdate.Labels[l_i].labelText.text = "+" + l_a.ToString();
                }
                else{
                     instanceToUpdate.Labels[l_i].labelText.text = l_a.ToString();
                }
            }
            else{
            instanceToUpdate.Labels[l_i].labelText.text = l_a.ToString();

            }
        }

    }

    public void UpdateStats( ItemStats _object , UIItemsWithLabels instanceToUpdate ) {
        for ( int i = 0 ; i < instanceToUpdate.Labels.Count ; i++ ){
            var a = _object[ instanceToUpdate.Labels[ i ].name ];
            instanceToUpdate.Labels[ i ].labelText.text = a.ToString( );
        }
    }
    public void RemoveMainInventory(WeaponObject _item, PlayerInventory inventory)
    {
        for (var l_i = 0; l_i < inventory.Slots.Count; l_i++)
        {
            if ( inventory.Slots[l_i].obj.name  == _item.stats.objectName + "Slot")
            {
                var l_slot =  inventory.Slots[l_i].obj;
                 inventory.Slots.RemoveAt(l_i);
                Destroy(l_slot);
            }
        }
    }
    public void RemoveMainInventory(Potions _item, PlayerInventory inventory)
    {
        for (var l_i = 0; l_i < inventory.PotionSlots.Count; l_i++)
        {
            if (inventory.PotionSlots[l_i].obj.name == _item.stats.objectName + "Slot")
            {
                var l_slot = inventory.PotionSlots[l_i].obj;
                inventory.PotionSlots.RemoveAt(l_i);
                inventory.potions.Remove( _item );
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
            ShowWindow(ItemsInstance.PauseUI);
            Time.timeScale = 0;
        }
        StaticManager.UiInventory.ViewEnemyStats();

    }

    public void CloseWindow( GameObject item = null )
    {
        if ( item == null ){
            var window = ItemsInstance.openedWindow[ItemsInstance.openedWindow.Count - 1];
            window.SetActive(false);
            ItemsInstance.openedWindow.RemoveAt(ItemsInstance.openedWindow.Count - 1);
        }
        else{
            item.SetActive(false);
            ItemsInstance.openedWindow.Remove( item );
        }
        if ( ItemsInstance.openedWindow.Count == 0 ){
            ItemsInstance.windowIsOpen = false;
        }
        
    }
    public void ShowWindow(GameObject item)
    {
        item.SetActive(true);
        ItemsInstance.windowIsOpen = item;
        ItemsInstance.openedWindow.Add(item);

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
                   //UpdateStats(l_hitInfo.transform.gameObject.GetComponent<WeaponObject>().Stats, ItemsInstance.WeaponStatsUI);
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
