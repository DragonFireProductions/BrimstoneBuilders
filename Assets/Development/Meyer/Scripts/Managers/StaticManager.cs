using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticManager : MonoBehaviour
{

    public static AudioManager Manager;

    public static Character Character;

    public static CharacterUtility Utility;

    public static UIInventory UiInventory;

    public static DamageCalc DamageCalc;

    public static StaticManager Instance;

    public static RealTime RealTime;

    public static ParticleManager particleManager;

    public FloatingText text;

    public static MultipleInventoryHolder inventories;

    public static TabManager tabManager;

    public static Currency currencyManager;

    public static WeaponHolder weaponManager;

    public static UIInventoryManager uiManager;

    public static ItemLevelCalculation levelCalc;
    

    public bool unlimitedPotions = false;

    public bool unlimitedSpawns = false;

    public bool useBrackets = false;
    // Use this for initialization
    public void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Manager = FindObjectOfType<AudioManager>();

        Character = GameObject.Find("Player").GetComponent<Character>();

        Utility = GameObject.Find("ManagerHolder").GetComponent<CharacterUtility>();

        UiInventory = GameObject.Find("ManagerHolder").GetComponent<UIInventory>();
        //UiInventory.Start( );

        DamageCalc = GameObject.Find("ManagerHolder").GetComponent<DamageCalc>();

        RealTime = GameObject.Find("ManagerHolder").GetComponent<RealTime>();

        particleManager = GameObject.Find("ManagerHolder").GetComponent<ParticleManager>();

        InstatiateFloatingText.Initalize();

        inventories = GameObject.Find("ManagerHolder").GetComponent<MultipleInventoryHolder>();

        tabManager = GetComponent < TabManager >( );

        currencyManager = GameObject.Find("ManagerHolder").GetComponent<Currency>();

        weaponManager = GameObject.Find( "ManagerHolder" ).GetComponent < WeaponHolder >( );

        uiManager = GetComponent < UIInventoryManager >( );

        levelCalc = GetComponent < ItemLevelCalculation >( );
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel(GameObject obj ) {
        SceneManager.LoadScene( 1 );
        UnFreeze(obj);
    }

    public void UnFreeze(GameObject obj)
    {
        Time.timeScale = 1;
        obj.SetActive(false);
    }

    public void Freeze()
    {
        Time.timeScale = 0;
    }
    public void EnableSendTo()
    {
        //UiInventory.ItemsInstance.SendToCompanion.SetActive(true);
    }

}