using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] AudioClip Clip;
    [SerializeField] string name;
    [SerializeField] SoundType Type;
    [SerializeField] bool Looping;
    [Range(0, 1)]
    [SerializeField] float Volume;
    [Range(.5f, 1.5f)]
    [SerializeField] float Pitch;

    AudioSource Source;

    public void SetSource(AudioSource _source)
    {
        Source = _source;

        Source.clip = Clip;


        switch (Type)
        {
            case SoundType.Music:
                Source.volume = Volume * PlayerPrefs.GetFloat("MusicVolume") * PlayerPrefs.GetFloat("MasterVolume");
                break;
            case SoundType.Effect:
                Source.volume = Volume * PlayerPrefs.GetFloat("EffectVolume") * PlayerPrefs.GetFloat("MasterVolume");
                break;
            default:
                break;
        }

        Source.pitch = Pitch;
    }

    public void UpdateVolume()
    {
        switch (Type)
        {
            case SoundType.Music:
                Source.volume = Volume * PlayerPrefs.GetFloat("MusicVolume") * PlayerPrefs.GetFloat("MasterVolume");
                break;
            case SoundType.Effect:
                Source.volume = Volume * PlayerPrefs.GetFloat("EffectVolume") * PlayerPrefs.GetFloat("MasterVolume");
                break;
            default:
                break;
        }
    }

    public void PlaySound()
    {
        Source.Play();
    }

    public void StopSound()
    {
        Source.Stop();
    }

    public string GetName() { return name; }

    public SoundType GetSoundType() { return Type; }
}

public enum SoundType { Music, Effect}
