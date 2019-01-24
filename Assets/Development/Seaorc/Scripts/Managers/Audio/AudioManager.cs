using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : MonoBehaviour
{
    static float MasterVolume;
    static float MusicVolume;
    static float EffectVolume;
    static float VoiceVolume;
    static float EnvironmentVolume;


    public static AudioManager manager = null;
    [SerializeField] Sound[] Clips;
    Dictionary<string, Sound> SoundDictionary;
    Sound CurrentSong;

    /// <summary>
    /// Intilizes arrays and makes sure there is only one active audio manager
    /// </summary>
    void Awake()
    {
        SoundDictionary = new Dictionary<string, Sound>();
        foreach (Sound sound in Clips)
        {
            GameObject GO = new GameObject(sound.GetName());

            GO.transform.SetParent(this.transform);

            sound.SetSource(GO.AddComponent<AudioSource>());
           

            SoundDictionary.Add(sound.GetName(), sound);


        }
    }

    /// <summary>
    /// Plays a sound based on the passed in string
    /// </summary>
    /// <param name="_Sound"></param>
    public void PlaySound(string _Sound)
    {
        Sound SoundToPlay = SoundDictionary[_Sound];

        Debug.Log(_Sound);

        Assert.IsNotNull(SoundToPlay, "Sound is null");

        if (SoundToPlay != null)
        {
            SoundToPlay.PlaySound();

        }
    }

    /// <summary>
    /// Plays an audio clip based on the passed in string after stoping the last clip played though this function
    /// </summary>
    /// <param name="_Sound"></param>
    public void PlayMusic(string _Sound)
    {
        Sound SongToPlay = SoundDictionary[_Sound];

        if (SongToPlay != null && SongToPlay.GetSoundType() == SoundType.Music)
        {
            if (CurrentSong != null)
                CurrentSong.StopSound();

            CurrentSong = SongToPlay;
            SongToPlay.PlaySound();
        }
    }
    

    public static float GetMasterVolume() { return MasterVolume; }
    public static float GetMusicVolume() { return MusicVolume; }
    public static float GetEffectVolume() { return EffectVolume; }
    public static float GetVoiceVolume() { return VoiceVolume; }
    public static float GetEnvironmentVolume() { return EnvironmentVolume; }

    public static void SetMasterVolume(float _volume) { MasterVolume = Mathf.Clamp(_volume, 0, 1); }
    public static void SetMusicVolume(float _volume) { MusicVolume = Mathf.Clamp(_volume, 0, 1); }
    public static void SetEffectVolume(float _volume) { EffectVolume = Mathf.Clamp(_volume, 0, 1); }
    public static void SetVoiceVolume(float _volume) { VoiceVolume = Mathf.Clamp(_volume, 0, 1); }
    public static void setEnvironmentalVolume(float _volume) { EnvironmentVolume = Mathf.Clamp(_volume, 0, 1); }
}
