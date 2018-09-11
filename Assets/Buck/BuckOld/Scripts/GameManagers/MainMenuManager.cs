using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    AudioSource menuAudio;
    [SerializeField]
    AudioClip menuClick;

    void Start()
    {
        menuAudio = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();
    }

    public void PlayButton()
    {
        menuAudio.clip = menuClick;
        menuAudio.Play();

        Invoke("PlayGame", menuAudio.clip.length);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(1);
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
