using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] AudioClip Clip;
    [SerializeField] string name;
    [SerializeField] bool IsMusic;
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
        Source.volume = Volume;
        Source.pitch = Pitch;
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

    public bool GetIsMusic() { return IsMusic; }
}
