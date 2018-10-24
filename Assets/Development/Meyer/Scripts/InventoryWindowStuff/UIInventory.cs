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

public class Container
{

}

public class UIInventory : MonoBehaviour
{
    public UIItems itemsInstance;

   
    private bool showWindow;

    struct stats {

       public TextMeshProUGUI obj;

       public string name;

    }

    private List < stats > StatUIList;

    private List < stats > CompanionUIList;

    private List < stats > WeaponUIList;

    private List < stats > GameStatsUiList;

    private List < stats > WeaponInventoryStatsUIList;

    private List < stats > GameInventoryStatsUIList;

    public List < WeaponObject > attachedWeapons;

    /// <summary>
    /// Controls where the new UI item will be located
    /// </summary>
    private Vector3 pos;

    private Vector3 pos2;

    /// <summary>
    /// The list of current slots in the UI
    /// </summary>
    public List<GameObject> slots;

    public List < GameObject > backpackSlots;

    public bool Dragging = false;
    
    // Use this for initialization
    

    public void Start() {
        itemsInstance = gameObject.GetComponent < UIItems >( );
        itemsInstance.Start();
        itemsInstance.InventoryContainer.SetActive(false);
        pos = itemsInstance.InventoryContainer.gameObject.transform.position;
        itemsInstance.PlayerUI.SetActive(false);
        itemsInstance.DialogueUI.SetActive(true);
        Show = false;
        StatWindowShow(false);
        ShowPauseMenu(false);
        StatUIList = new List<stats>();
        CompanionUIList = new List<stats>();
        WeaponUIList = new List < stats >();
        GameStatsUiList = new List < stats >( );
        WeaponInventoryStatsUIList = new List < stats >( );
        GameInventoryStatsUIList = new List < stats >();
        CompanionStatShowWindow(false);
        ShowGameOver(false);
        ShowWeaponStats(false);
        ShowBackPackInventory(false);
        ShowWeaponOptions(false);
        ShowInventoryWeaponStats(false);
        ShowPauseMenu(false);
        ShowBattleWon( false );
        itemsInstance.AttackConfirmation.SetActive(false);
        //itemsInstance.StatUI = new GameObject();

        int i = itemsInstance.StatLabels.transform.childCount;

        for (int j = 0; j < i; j++)
        {
            stats l_stats;
            l_stats.obj = itemsInstance.StatLabels.transform.GetChild(j).GetComponent<TextMeshProUGUI>();
            l_stats.name = itemsInstance.StatLabels.transform.GetChild(j).name;
            StatUIList.Add(l_stats);
        }

        i = itemsInstance.CompanionLabels.transform.childCount;

        for (int j = 0; j < i; j++)
        {
            stats l_stats;
            l_stats.obj = itemsInstance.CompanionLabels.transform.GetChild(j).GetComponent<TextMeshProUGUI>();
            l_stats.name = itemsInstance.CompanionLabels.transform.GetChild(j).name;
            CompanionUIList.Add(l_stats);
        }

        i = itemsInstance.WeaponLabels.transform.childCount;

        for ( int j = 0 ; j < i ; j++ ){
            stats l_stats;
            l_stats.obj = itemsInstance.WeaponLabels.transform.GetChild( j ).GetComponent < TextMeshProUGUI >( );
            l_stats.name = itemsInstance.WeaponLabels.transform.GetChild( j ).name;
         
            WeaponUIList.Add(l_stats);
        }

        i = itemsInstance.GameStatLabels.transform.childCount;

        for ( int j = 0 ; j < i ; j++ ){

            stats l_stats;
            l_stats.obj = itemsInstance.GameStatLabels.transform.GetChild( j ).GetComponent < TextMeshProUGUI >( );
            l_stats.name = itemsInstance.GameStatLabels.transform.GetChild( j ).name;

            GameStatsUiList.Add(l_stats);
        }
        i = itemsInstance.WeaponInventoryLabels.transform.childCount;

        for (int j = 0; j < i; j++)
        {

            stats l_stats;
            l_stats.obj = itemsInstance.WeaponInventoryLabels.transform.GetChild(j).GetComponent<TextMeshProUGUI>();
            l_stats.name = itemsInstance.WeaponInventoryLabels.transform.GetChild(j).name;

            WeaponInventoryStatsUIList.Add(l_stats);
        }
        i = itemsInstance.GameInventoryStatLabels.transform.childCount;

        for (int j = 0; j < i; j++)
        {

            stats l_stats;
            l_stats.obj = itemsInstance.GameInventoryStatLabels.transform.GetChild(j).GetComponent<TextMeshProUGUI>();
            l_stats.name = itemsInstance.GameInventoryStatLabels.transform.GetChild(j).name;

            GameInventoryStatsUIList.Add(l_stats);
        }


    }

    public void ShowBattleWon( bool show ) {
        itemsInstance.BattleWon.SetActive(show);

        if ( show ){
            Time.timeScale = 0;
        }
    }
    public void ShowNotification(string _message, float time ) {
        itemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text = _message;
        StartCoroutine( showNotification( time ) );
    }

    private IEnumerator showNotification( float time ) {
        yield return new WaitForSeconds(time);
        itemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text = "";

    }

    public void CompanionStatShowWindow(bool active ) {
        itemsInstance.CompanionUI.SetActive(active);
    }
    
    public void ShowGameOver( bool show ) {
        StaticManager.uiInventory.Freeze();
        itemsInstance.GameOverUI.SetActive(show);
    }

    public void ShowWeaponStats( bool show ) {
        itemsInstance.WeaponStatsUI.SetActive(show);
    }

    public void ShowBackPackInventory( bool show ) {
        itemsInstance.BackPackUI.SetActive(show);
    }
    public void StatWindowShow(bool active)
    {
        itemsInstance.StatUI.SetActive(active);
    }

    public void AppendNotification( string _message ) {
        itemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text += _message;

    }

    public void ShowWeaponOptions( bool active ) {
        itemsInstance.WeaponOptions.SetActive(active);
    }

    public void ShowInventoryWeaponStats( bool active ) {
        itemsInstance.WeaponInventoryStatsUI.SetActive(active);
    }

    private bool active = false;
    public void ToggleShow(GameObject obj ) {
        active = !obj.activeSelf;
        obj.SetActive(active);
    }
    public void Quit(GameObject obj)
    {
        obj.SetActive(false);

    }

    private bool freeze = false;
    public void Freeze( ) {
        freeze = !freeze;

        if ( freeze ){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }
    public bool Show {
        get {
            showWindow = !showWindow;

            return showWindow;
        }
        set { showWindow = value; }
    }
    /// <summary>
    /// Adds a new item slot to UI
    /// </summary>
    /// <param name="item">Weapon that is picked up</param>
    public void AddSlot(WeaponObject item)
    {
        GameObject newContainer = Instantiate(itemsInstance.InventoryContainer);
        newContainer.SetActive(true);
        newContainer.transform.SetParent(itemsInstance.InventoryContainer.transform.parent);
        newContainer.transform.position = pos;
        newContainer.transform.localScale = itemsInstance.InventoryContainer.transform.localScale;
        Set(item, newContainer);
        slots.Add(newContainer);
    }

    public void AddBackpackSlot(WeaponObject item)
    {
        StaticManager.uiInventory.ShowBackPackInventory(true);
        itemsInstance.BackpackContainer.SetActive(true);
        GameObject newContainer = Instantiate( itemsInstance.BackpackContainer );
        newContainer.name = item.gameObject.name + "Slot";
        newContainer.transform.SetParent(itemsInstance.BackpackContainer.transform.parent);
        newContainer.transform.position = pos2;
        newContainer.transform.localScale = itemsInstance.BackpackContainer.transform.localScale;
        backpackSlots.Add(newContainer);
        newContainer.GetComponentInChildren<RawImage>().texture     = item.WeaponStats.icon;
        newContainer.GetComponentInChildren<TextMeshProUGUI>().text = item.WeaponStats.objectName;
        itemsInstance.BackpackContainer.SetActive(false);

    }

    public void UpdateStats(Stat stats ) {
        StatWindowShow(true);

        for ( int i = 0 ; i < StatUIList.Count ; i++ ){
            var a = stats[ StatUIList[ i ].name ];
            StatUIList[ i ].obj.text = a.ToString( );
        }

    }

    public void UpdateWeaponStats(WeaponItem item ) {
        ShowWeaponStats(true);

        for ( int i = 0 ; i < WeaponUIList.Count ; i++ ){
            var a = item[ WeaponUIList[ i ].name ];
            WeaponUIList[ i ].obj.text = "+" + a.ToString( );
        }
        
    }
    

    public void UpdateGameWeaponStats( GunType obj ) {
        for ( int i = 0 ; i < GameStatsUiList.Count ; i++ ){
            var a = obj[ GameStatsUiList[ i ].name ];
            GameStatsUiList[ i ].obj.text = a.ToString( );
        }
    }

    public void UpdateWeaponInventoryStats(WeaponItem obj)
    {
        for (int i = 0; i < WeaponInventoryStatsUIList.Count; i++)
        {
            var a = obj[WeaponInventoryStatsUIList[i].name];
            WeaponInventoryStatsUIList[i].obj.text = a.ToString();
        }
    }
    public void UpdateGameInventoryStats(GunType item)
    {
        ShowInventoryWeaponStats(true);

        for (int i = 0; i < GameInventoryStatsUIList.Count; i++)
        {
            var a = item[GameInventoryStatsUIList[i].name];
            GameInventoryStatsUIList[i].obj.text = a.ToString();
        }

    }
    public void UpdateCompanionStats(Stat stats)
    {
        for (int i = 0; i < CompanionUIList.Count; i++)
        {
            var a = stats[CompanionUIList[i].name];
            CompanionUIList[i].obj.text = a.ToString();
        }

    }
    /// <summary>
    /// Removes an item from the UI
    /// </summary>
    /// <param name="item"></param>
    public void Remove(WeaponObject item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name  == item.name + "Slot")
            {
                GameObject slot = slots[i].gameObject;
                slots.RemoveAt(i);
                Destroy(slot);
            }
        }
    }

    public void RemoveBackpack( WeaponObject item ) {
        for ( int i = 0 ; i < backpackSlots.Count ; i++ ){
            if ( backpackSlots[i].name == item.name + "Slot"){
                GameObject slot = backpackSlots[ i ].gameObject;
                backpackSlots.RemoveAt(i);
                Destroy(slot);
            }
        }
    }
    /// <summary>
    /// Sets the variables in the UI to the weapon stats passed in
    /// </summary>
    /// <param name="_weapons">Weapon added to UI</param>
    /// <param name="_container_p">The UI itemsInstance.InventoryContainer</param>
    public void Set(WeaponObject _weapons, GameObject _container_p)
    {
        TextMeshProUGUI[] transforms = _container_p.GetComponentsInChildren<TextMeshProUGUI>();
        _container_p.name = _weapons.name + "Slot";
        for (int j = 0; j < transforms.Length; j++)
        {
            switch (transforms[j].name)
            {
                case "ItemName":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.objectName.ToString();
                    break;
                case "ItemValue":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.value.ToString();
                    break;
                case "ItemWeight":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.weight.ToString();
                    break;
                case "ItemDurability":
                    transforms[j].GetComponent<TextMeshProUGUI>().text = _weapons.WeaponStats.durability.ToString();
                    break;
                default:
                    break;
            }
        }
        _container_p.GetComponentInChildren<RawImage>().texture = _weapons.WeaponStats.icon;
    }

    public void SetBackpack( WeaponObject _weapons, GameObject container ) {
        container.GetComponentInChildren < TextMeshProUGUI >( ).text = _weapons.WeaponStats.objectName;
        container.transform.Find( "icon" ).GetComponent < RawImage >( ).texture = _weapons.WeaponStats.icon;
    }
    void ShowWeaponStats( ) {

    }

    /// <summary>
    /// Shows the itemsInstance.Pause menu
    /// </summary>
    /// <param name="showPause">Bool that determines whether it's shown or not</param>
    public void ShowPauseMenu(bool showPause)
    {
        itemsInstance.PauseUI.SetActive(showPause);
    }

    // Update is called once per frame
    public WeaponObject selectedItem;

    public GameObject ob;

    public Vector3 offset;

    public Vector3 screenPoint;

    public bool isMainInventory = true;
    
    void Update () {
        if ( Dragging /*&& Input.GetMouseButton(0)*/){
            selectedItem.gameObject.SetActive(true);
            selectedItem.GetComponent < BoxCollider >( ).enabled = true;
            var x = StaticManager.character.gameObject.transform.position.x;
            Vector3 worldPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
            worldPoint.x = StaticManager.character.gameObject.transform.position.x;
            worldPoint.z = StaticManager.character.gameObject.transform.position.z;
            worldPoint.y = StaticManager.character.gameObject.transform.position.z;
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            //If something was hit, the RaycastHit2D.collider will not be null and the the object must have the "Monkey" tag and target has to be null
            if (hit.collider != null && hit.collider.tag == "Weapon")
            {
                selectedItem.transform.position = worldPoint; // Sets the target to be the transform that was hit
            }

            float distance_to_screen = Camera.main.WorldToScreenPoint(selectedItem.transform.position).z;
            var curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            curPosition.x = Mathf.Clamp( curPosition.x , StaticManager.character.gameObject.transform.position.x -4 ,StaticManager.character.gameObject.transform.position.x + 4 );
            curPosition.z = Mathf.Clamp( curPosition.z ,StaticManager.character.gameObject.transform.position.z -4 ,StaticManager.character.gameObject.transform.position.z + 4 );
            selectedItem.transform.position = curPosition;
            
        }

        //if ( Input.GetMouseButtonUp(0) && Dragging ){
        //    Dragging = false;
        //    selectedItem.gameObject.SetActive(false);
        //    StaticManager.character.controller.SetControlled(false);
        //}
        if ( Input.GetKeyDown(KeyCode.Escape) ){
            ShowPauseMenu(Show);
            Time.timeScale = 0;
        }
        StaticManager.uiInventory.ViewEnemyStats();

    }
  
    public void ViewEnemyStats()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Mouse is down");

            RaycastHit l_hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out l_hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + l_hitInfo.transform.gameObject.name);
                if (l_hitInfo.transform.gameObject.tag == "Enemy" || l_hitInfo.transform.gameObject.tag == "Player" || l_hitInfo.transform.gameObject.tag == "Companion")
                {
                    UpdateStats(l_hitInfo.transform.gameObject.GetComponent<Stat>());
                   
                }
                else if (l_hitInfo.transform.gameObject.tag == "Weapon")
                {
                    UpdateWeaponStats(l_hitInfo.transform.gameObject.GetComponent<WeaponObject>().WeaponStats);
                    //UpdateGameWeaponStats(l_hitInfo.transform.gameObject.GetComponent<GunType>());
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");

        }
        else
        {
          StaticManager.uiInventory.StatWindowShow(false);
          StaticManager.uiInventory.ShowWeaponStats(false);
        }
    }

}
