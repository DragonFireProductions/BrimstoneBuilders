using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject areYouSurePanel;

    public Camera cam;
    public Animator camAnimator;

    AudioSource menuAudio;
    [SerializeField]
    AudioClip menuClick;
    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;
        cam.GetComponent<Animator>();

        menuAudio = GetComponent<AudioSource>();

        areYouSurePanel.SetActive(false);
	}

    public void PlayClick()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    //--------------------------Transition Checks----------------------------------------------

    public void MenuToSettings()
    {
        camAnimator.SetBool("settingsActive", true);
    }

    public void SettingsToMenu()
    {
        camAnimator.SetBool("settingsActive", false);
    }

    public void MenuToCredits()
    {
        camAnimator.SetBool("creditsActive", true);
    }

    public void CreditsToMenu()
    {
        camAnimator.SetBool("creditsActive", false);
    }

    //-----------------------------------------------------------------------------------------

    public void ExitGame()
    {
        areYouSurePanel.gameObject.SetActive(true);
    }

    public void No()
    {
        areYouSurePanel.gameObject.SetActive(false);
    }

    public void Yes()
    {
        Application.Quit();
    }
}
