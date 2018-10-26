using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticManager : MonoBehaviour {

    public static AudioManager Manager;

    public static Character Character;

    public static CharacterUtility Utility;

    public static PlayerInventory Inventory;

    public static UIInventory UiInventory;

    public static DamageCalc DamageCalc;

    public static StaticManager Instance;

    public static RealTime RealTime;

    // Use this for initialization
    public void Awake( ) {
        if ( Instance == null ){
            Instance = this;
        }
        else if ( Instance != this ){
            Destroy( gameObject );
        }

        Manager = FindObjectOfType < AudioManager >( );

        Character = GameObject.Find( "Player" ).GetComponent < Character >( );

        Utility = GameObject.Find( "ManagerHolder" ).GetComponent < CharacterUtility >( );

        Inventory = GameObject.Find( "ManagerHolder" ).GetComponent < PlayerInventory >( );

        UiInventory = GameObject.Find( "ManagerHolder" ).GetComponent < UIInventory >( );
        //UiInventory.Start( );

        DamageCalc = GameObject.Find( "ManagerHolder" ).GetComponent < DamageCalc >( );

        RealTime = GameObject.Find( "ManagerHolder" ).GetComponent < RealTime >( );
    }

    public void LoadMainMenu( ) {
        SceneManager.LoadScene( 0 );
    }

    public void ReloadLevel( ) {
        SceneManager.LoadScene( 1 );
    }
    

}