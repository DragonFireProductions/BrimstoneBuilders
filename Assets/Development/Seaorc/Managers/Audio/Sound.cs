using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    /// <remarks>Set in inspector</remarks>
    [SerializeField] AudioClip Clip;
    [SerializeField] string name;
    [SerializeField] SoundType Type;
    [SerializeField] bool Looping;
    [Range(0, 1)]
    [SerializeField] float Volume;
    [Range(.5f, 1.5f)]
    [SerializeField] float Pitch;

    AudioSource Source;

    /// <summary>
    /// sets the audio source for the sound to be played from
    /// </summary>
    /// <param name="_source"></param>
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

    /// <summary>
    /// Sets the valume to current valume settings
    /// </summary>
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

    /// <summary>
    /// Plays the set sound
    /// </summary>
    public void PlaySound()
    {
        Source.volume = 1.0f;
        Source.Play();
        Debug.Log("Audio is playing");
    }

    public void StopSound()
    {
        Source.Stop();
    }

    public string GetName() { return name; }

    public SoundType GetSoundType() { return Type; }
}

public enum SoundType { Music, Effect}
