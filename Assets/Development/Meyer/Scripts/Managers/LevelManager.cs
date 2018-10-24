using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public bool isTurnBasedOn = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("ResponsiveMainMenu");
    }

	public void UnFreeze( ) {
		Time.timeScale = 1;
	}
	public void Block( ) {
		TurnBasedController.instance.Block();
	}
    public void ReloadLevel()
    {
		UnFreeze();
        SceneManager.LoadScene(1);
    }

	public void Attack( ) {
		Time.timeScale = 1;
		StaticManager.uiInventory.itemsInstance.AttackConfirmation.SetActive(false);
		TurnBasedController.instance.continueattack();
	}

	public void DontContinue( ) {
		Time.timeScale = 1;
		TurnBasedController.instance._player.hasSelectedCompanion = false;
		StaticManager.uiInventory.itemsInstance.AttackConfirmation.SetActive(false);
	}

}
