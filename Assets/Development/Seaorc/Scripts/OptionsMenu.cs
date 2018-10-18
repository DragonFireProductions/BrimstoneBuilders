using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;

    /// <summary>
    /// gets screens posable resolotions and populats resolution drop down list
    /// </summary>
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " X " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolution = i;
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolution;
        resolutionDropDown.RefreshShownValue();
    }

    /////////// Audio Setings ///////////
    public void SetMasterVolume(float _volume)
    {
        Debug.Log("Master Volume: " + _volume);
        AudioManager.SetMasterVolume(_volume);
    }

    public void SetMusicVolume(float _volume)
    {
        Debug.Log("Music Volume: " + _volume);
        AudioManager.SetMusicVolume(_volume);
    }

    public void SetEffectVolume(float _volume)
    {
        Debug.Log("Effect Volume: " + _volume);
        AudioManager.SetEffectVolume(_volume);
    }

    public void SetVoiceVolume(float _volume)
    {
        Debug.Log("Voice Volume: " + _volume);
        AudioManager.SetVoiceVolume(_volume);
    }

    public void SetEnvironmentalVolume(float _volume)
    {
        Debug.Log("Environmental Volume: " + _volume);
        AudioManager.setEnvironmentalVolume(_volume);
    }

    /////////// Video Settings ///////////
    public void SetFullScreen(bool _fullScreen)
    {
        Debug.Log("setting Changed");
        Screen.fullScreen = _fullScreen;
    }

    public void SetFullScreen(int _fullScreen)
    {
        Debug.Log("setting Changed");
        if (_fullScreen == 0)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }

    public void SetResolutioin(int _index)
    {
        Debug.Log("setting Changed");
        Resolution resolution = Screen.resolutions[_index];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVsync(bool _Sync)
    {
        Debug.Log("setting Changed");
        if (_Sync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;


    }

    /////////// Graphics Settings ///////////
    public void SetAntiAliasing(int _Multisampling)
    {
        Debug.Log("setting Changed");
        QualitySettings.antiAliasing = _Multisampling;
    }

    public void SetAnisotropicFiltering(bool _Filtering)
    {
        Debug.Log("setting Changed");
        if (_Filtering)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    public void SetFog(bool _fog)
    {
        Debug.Log("setting Changed");
        RenderSettings.fog = _fog;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 20;
        RenderSettings.fogEndDistance = 1000;
    }

    public void SetShadowResolution(int _Resolution)
    {
        Debug.Log("setting Changed");
        QualitySettings.shadowResolution = (ShadowResolution)_Resolution;
    }

    public void SetQualityLevel(int _Level)
    {
        Debug.Log("setting Changed");
        QualitySettings.SetQualityLevel(_Level);
    }
}
