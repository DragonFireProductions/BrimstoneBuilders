using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
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

    public void PlayMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
