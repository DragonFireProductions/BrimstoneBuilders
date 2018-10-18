using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Assets.Meyer.TestScripts;
using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticManager : MonoBehaviour {

    public static AudioManager manager;

    public static Character character;


    public static CharacterUtility utility;


    public static PlayerInventory inventory;


    public static UIInventory uiInventory;


    public static DamageCalc DamageCalc;

    public static StaticManager instance;
	

    // Use this for initialization
    public void Awake( ) {


        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(this);
        manager = GameObject.FindObjectOfType<AudioManager>();

        character = GameObject.Find("Player").GetComponent<Character>();


        utility = GameObject.Find("ManagerHolder").GetComponent<CharacterUtility>();


        inventory = GameObject.Find("ManagerHolder").GetComponent<PlayerInventory>();


        uiInventory = GameObject.Find("ManagerHolder").GetComponent<UIInventory>();
        uiInventory.Start();

        DamageCalc = GameObject.Find("ManagerHolder").GetComponent<DamageCalc>();

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame

}
