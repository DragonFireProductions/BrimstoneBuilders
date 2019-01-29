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
    public GameObject BackDrop;
    private bool showWindow;


    public struct Stats {

       public TextMeshProUGUI Obj;

       public string Name;

    }
    

    

    private Vector3 pos;

    [HideInInspector] public bool isMouseOver = false;

    [ HideInInspector ] public bool mouse = false;
    private Vector3 pos2;


    public List < GameObject > BackpackSlots;

    public bool Dragging = false;

   

    // Use this for initialization
    public void Awake( ) {
        
        ItemsInstance = gameObject.GetComponent<UIItems>();
        ItemsInstance.Initalize();
        BackDrop.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (StaticManager.UiInventory.ItemsInstance.openedWindow.Count > 0)
            {
                StaticManager.UiInventory.CloseWindow();
            }
            else
            {
                StaticManager.UiInventory.ShowWindow(StaticManager.UiInventory.ItemsInstance.PauseUI);
            }
        }

    }

    public void CloseWindow( GameObject item = null )
    {
        if ( item == null ){
            var window = ItemsInstance.openedWindow[ItemsInstance.openedWindow.Count - 1];

            foreach ( var l_gameObject in window ){
                l_gameObject.SetActive(false);
            }
            ItemsInstance.openedWindow.RemoveAt(ItemsInstance.openedWindow.Count - 1);
        }
        else{
            for ( int i = 0 ; i < ItemsInstance.openedWindow.Count ; i++ ){
                for ( int j = 0 ; j <  ItemsInstance.openedWindow[i].Count; j++ ){
                    if ( ItemsInstance.openedWindow[i][j] == item ){
                        ItemsInstance.openedWindow.RemoveAt(i);

                        break;
                    }
                }
            }
            item.SetActive(false);
        }
        if ( ItemsInstance.openedWindow.Count == 0 ){
            ItemsInstance.windowIsOpen = false;
            Time.timeScale = 1;
            BackDrop.SetActive(false);
        }
        
    }

    public void CloseAll( ) {
        for ( int i = 0 ; i < ItemsInstance.openedWindow.Count ; i++ ){
            for ( int j = 0 ; j < ItemsInstance.openedWindow[i].Count ; j++ ){
                ItemsInstance.openedWindow[i][j].gameObject.SetActive(false);
            }
        }
        ItemsInstance.openedWindow.Clear();
        ItemsInstance.windowIsOpen = false;
        Time.timeScale = 1;
        BackDrop.SetActive(false);

    }
    public void OnMouseOver( ) {
        mouse = true;
    }

    public void OnMouseExit( ) {
        mouse = false;
    }

    public void ShowWindow(GameObject item)
    {
        if ( !item.activeSelf ){
            item.SetActive(true);
            ItemsInstance.windowIsOpen = item;
            List <GameObject> list = new List < GameObject >();
            list.Add(item);
            ItemsInstance.openedWindow.Add(list);
            Time.timeScale = 0.0f;
            BackDrop.SetActive(true);
        }
        

    }

    public void ShowWindow( List < GameObject > list ) {
        
        foreach ( var l_gameObject in list ){
             l_gameObject.SetActive(true);
            ItemsInstance.windowIsOpen = l_gameObject;
        }
        ItemsInstance.openedWindow.Add(list);
        Time.timeScale = 0.0f;
        BackDrop.SetActive(true);
    }

}
