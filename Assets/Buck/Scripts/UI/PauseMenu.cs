using System.Collections;
using System.Collections.Generic;

using Assets.Meyer.TestScripts.Player;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    AudioSource menuAudio;
    [SerializeField]
    AudioClip menuClick;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        menuAudio = GetComponent<AudioSource>();
    }

    void Update ()
    {
     
	}

    public void ResumeButton()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
      StaticManager.UiInventory.ItemsInstance.PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenuButton()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();

        Invoke("LoadMenu", menuAudio.clip.length);
    }

   void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();

        Invoke("QuitApplication", menuAudio.clip.length);
    }

    void QuitApplication()
    {
        Application.Quit();
    }
}
