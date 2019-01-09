using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;
using TMPro;

public class SettignsMenu : MonoBehaviour
{
    public GameObject videoPanel;
    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject gameplayPanel;
    public GameObject controls;

    public AudioMixer masterAudioMixer;

    public TMP_Dropdown shadowDropdown;

    // Use this for initialization
    void Start ()
    {
    }

    public void VideoPanel()
    {
        videoPanel.gameObject.SetActive(true);
        graphicsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);                                                      
    }

    //----------------Graphics Functions----------------------------------------------------

    public void GraphicsPanel()
    {
        videoPanel.gameObject.SetActive(false);
        graphicsPanel.gameObject.SetActive(true);
        audioPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
    }

    public void ToggleDepthOfField()
    {
    }

    public void ToggleFog()
    {
        RenderSettings.fog = !RenderSettings.fog;
    }

    public void ShadowQuality(int shadowQualityIndex)
    {

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //--------------------------------------------------------------------------------------

    //----------------Audio Functions-------------------------------------------------------

    public void AudioPanel()
    {
        videoPanel.gameObject.SetActive(false);
        graphicsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(true);
        gameplayPanel.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
    }

    public void SetMasterVolume(float masterVolume)
    {
        masterAudioMixer.SetFloat("masterVolume", masterVolume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        masterAudioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetFXVolume(float FXVolume)
    {
        masterAudioMixer.SetFloat("fxVolume", FXVolume);
    }

    //--------------------------------------------------------------------------------------

                                                                                                
    //----------------Gameplay Functions----------------------------------------------------

    public void GamePlayPanel()
    {
        videoPanel.gameObject.SetActive(false);
        graphicsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(true);
        controls.gameObject.SetActive(false);
    }


    //--------------------------------------------------------------------------------------

    public void ControlsPanel()
    {
        videoPanel.gameObject.SetActive(false);
        graphicsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        gameplayPanel.gameObject.SetActive(false);
        controls.gameObject.SetActive(true);
    }
}
