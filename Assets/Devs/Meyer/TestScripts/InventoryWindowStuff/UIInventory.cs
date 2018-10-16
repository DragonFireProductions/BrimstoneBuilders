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

    private List < stats > WeaponStatsUIList;
    /// <summary>
    /// Controls where the new UI item will be located
    /// </summary>
    private Vector3 pos;


    /// <summary>
    /// The list of current slots in the UI
    /// </summary>
    public List<GameObject> slots;
    
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
        ShowInstructions(false);
        StatUIList = new List<stats>();
        CompanionUIList = new List<stats>();
        WeaponUIList = new List < stats >();
        WeaponStatsUIList = new List < stats >( );
        CompanionStatShowWindow(false);
        ShowGameOver(false);
        ShowWeaponStats(false);
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

            WeaponStatsUIList.Add(l_stats);
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

    public void ShowInstructions(bool show) {
        itemsInstance.Instructions.SetActive(show);
    }

    public void ShowGameOver( bool show ) {
        itemsInstance.GameOverUI.SetActive(show);
    }

    public void ShowWeaponStats( bool show ) {
        itemsInstance.WeaponStatsUI.SetActive(show);
    }
    public void AppendNotification( string _message ) {
        itemsInstance.DialogueUI.GetComponentInChildren<TextMeshProUGUI>().text += _message;

    }
    

    public void StatWindowShow( bool active ) {
        itemsInstance.StatUI.SetActive(active);
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
            WeaponUIList[ i ].obj.text = a.ToString( );
        }
        
    }

    public void UpdateGameWeaponStats( GunType obj ) {
        for ( int i = 0 ; i < WeaponStatsUIList.Count ; i++ ){
            var a = obj[ WeaponStatsUIList[ i ].name ];
            WeaponStatsUIList[ i ].obj.text = a.ToString( );
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

    /// <summary>
    /// Shows the itemsInstance.Pause menu
    /// </summary>
    /// <param name="showPause">Bool that determines whether it's shown or not</param>
    public void ShowPauseMenu(bool showPause)
    {
        itemsInstance.PauseUI.SetActive(showPause);
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetKeyDown(KeyCode.Escape) ){
            ShowInstructions(Show);
        }
        StaticManager.uiInventory.ViewEnemyStats();


    }
    /// <summary>
    /// Selects the item in the UI to attach to player
    /// </summary>
    /// <param name="weapon"></param>
    public void selected(WeaponObject weapon)
    {
        Debug.Log(weapon.WeaponStats.objectName + "was clicked");
        weapon.SelectItem();
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
