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
    public void ReloadLevel()
    {
		UnFreeze();
        SceneManager.LoadScene(1);
    }
	
	

}
