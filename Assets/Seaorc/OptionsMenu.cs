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
        Screen.fullScreen = _fullScreen;
    }

    public void SetResolutioin(int _index)
    {
        Resolution resolution = Screen.resolutions[_index];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVsync(bool _Sync)
    {
        if (_Sync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;


    }

    /////////// Graphics Settings ///////////
    public void SetAntiAliasing(int _Multisampling)
    {
        QualitySettings.antiAliasing = _Multisampling;
    }

    public void SetAnisotropicFiltering(bool _Filtering)
    {
        if (_Filtering)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    public void SetFog(bool _fog)
    {
        RenderSettings.fog = _fog;
    }

    public void SetShadowResolution(int _Resolution)
    {
        QualitySettings.shadowResolution = (ShadowResolution)_Resolution;
    }

    public void SetQualityLevel(int _Level)
    {
        QualitySettings.SetQualityLevel(_Level);
    }
}
