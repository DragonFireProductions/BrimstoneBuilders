using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject PlayerHud;
    public Camera MainCamera;
    public Camera MenuCamera;
    public PlayableDirector TimelineObject;
 
    public GameObject ThisMenu;

    void Start () {
        
        //MenuManager.audio.PlayMusic("Theme");

        //cam = Camera.main;
        //cam.GetComponent<Animator>();

        //menuAudio = GetComponent<AudioSource>();

        //areYouSurePanel.SetActive(false);
    }
    

    public void PlayGame()
    {
        ShowGameCamera();
        TimelineObject.enabled = false;

        ThisMenu.SetActive(false);
        PlayerHud.SetActive(true);
    }
    
    public void ExitGame()
    {
        Debug.Log("Closed the application.");
        Application.Quit();
    }

    private void ShowMenuCamera()
    {
        MainCamera.enabled = false;
        MenuCamera.enabled = true;
    }

    private void ShowGameCamera()
    {
        MainCamera.enabled = true;
        MenuCamera.enabled = false;
    }
}