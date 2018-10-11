using System;
using System.Collections;
using System.Collections.Generic;
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
    public static UIInventory instance;

    /// <summary>
    /// InventoryContainer (The whole object containing variables of items)
    /// </summary>
    [SerializeField] GameObject container;

    /// <summary>
    /// The String variables to set in the UI
    /// </summary>
    [SerializeField] GameObject variables;

    /// <summary>
    /// Pause menu
    /// </summary>
    [SerializeField] private GameObject pause;

    /// <summary>
    /// Player UI
    /// </summary>
    [SerializeField] public GameObject PlayerUi;

    /// <summary>
    /// Dialog UI
    /// </summary>
    [ SerializeField ] public GameObject DialogUI;

    [ SerializeField ] public GameObject StatUI;

    [ SerializeField ] public GameObject StatPanel;

    [SerializeField] private TextMeshProUGUI dialogText;

    [ SerializeField ] private GameObject CompanionPanel;

    [ SerializeField ] private GameObject CompanionLabel;

    [ SerializeField ] private GameObject Instructions;

    [ SerializeField ] private GameObject InstructionsPanel;

    private bool showWindow;

    struct stats {

       public TextMeshProUGUI obj;

       public string name;

    }

    private List < stats > StatUIList;

    private List < stats > CompanionUIList;
    /// <summary>
    /// Controls where the new UI item will be located
    /// </summary>
    private Vector3 pos;


    /// <summary>
    /// The list of current slots in the UI
    /// </summary>
    public List<GameObject> slots;
    
    // Use this for initialization
    void Awake () {
	    if (instance == null)
	    {
	        instance = this;
	    }
	    else if (instance != this)
	        Destroy(gameObject);
	    //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        pos = container.gameObject.transform.position;
        DialogUI.SetActive(true);
        Show = false;
        StatWindowShow(false);
        ShowInstructions(false);
        StatUIList = new List < stats >();
        CompanionUIList = new List < stats >( );
        CompanionStatShowWindow(false);
        //StatUI = new GameObject();

        int i = StatPanel.transform.childCount;

        for (int j = 0; j < i ; j++ ){
            stats l_stats;
            l_stats.obj = StatPanel.transform.GetChild( j ).GetComponent<TextMeshProUGUI>();
            l_stats.name = StatPanel.transform.GetChild( j ).name;
            StatUIList.Add(l_stats  );
        }

        i = CompanionLabel.transform.childCount;

        for (int j = 0; j < i; j++)
        {
            stats l_stats;
            l_stats.obj = CompanionLabel.transform.GetChild(j).GetComponent<TextMeshProUGUI>();
            l_stats.name = CompanionLabel.transform.GetChild(j).name;
            CompanionUIList.Add(l_stats);
        }

    }

    public void ShowNotification(string _message, float time ) {
        dialogText.text = _message;
        StartCoroutine( showNotification( time ) );
    }

    private IEnumerator showNotification( float time ) {
        yield return new WaitForSeconds(time);
        UIInventory.instance.dialogText.text = "";

    }

    public void CompanionStatShowWindow(bool active ) {
        CompanionPanel.SetActive(active);
    }

    public void ShowInstructions(bool show) {
        Instructions.SetActive(show);
    }

    public void ResetLevel( ) {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    public void LoadMenu( ) {
        
        SceneManager.LoadScene("ResponsiveMainMenu");
    }
    public void AppendNotification( string _message ) {
        dialogText.text += _message;

    }
    

    public void StatWindowShow( bool active ) {
        StatUI.SetActive(active);
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
        GameObject newContainer = Instantiate(container);
        newContainer.SetActive(true);
        newContainer.transform.SetParent(container.transform.parent);
        newContainer.transform.position = pos;
        newContainer.transform.localScale = container.transform.localScale;
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
    /// <param name="_container_p">The UI container</param>
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
    /// Shows the pause menu
    /// </summary>
    /// <param name="showPause">Bool that determines whether it's shown or not</param>
    public void ShowPauseMenu(bool showPause)
    {
        pause.SetActive(showPause);
    }

    // Update is called once per frame
    void Update () {
        if ( Input.GetKeyDown(KeyCode.Escape) ){
            ShowInstructions(Show);
        }

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
                else
                {
                    Debug.Log("Nothing is selected");
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
            UIInventory.instance.StatWindowShow(false);
        }
    }

}
