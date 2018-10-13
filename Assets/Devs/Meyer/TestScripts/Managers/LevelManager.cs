using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool isEscapedPressed = false;
    public static LevelManager instance = null;

	// Use this for initialization
	void Awake () {
	    if (instance == null)
	        instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
            Pause();
	    }
	}

    public void Pause()
    {
        isEscapedPressed = !isEscapedPressed;
        UIInventory.instance.ShowPauseMenu(isEscapedPressed);
        if (isEscapedPressed)
        {
            Time.timeScale = 0;
        }

        if (!isEscapedPressed)
        {
            Time.timeScale = 1;
        }
    }

    public void LoadMenu()
    {

    }
}
