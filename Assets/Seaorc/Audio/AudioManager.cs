using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager = null;
    [SerializeField] Sound[] Clips;
    Dictionary<string, Sound> SoundDictionary;
    Sound CurrentSong;

    void Awake()
    {
        if (manager != null)
            Destroy(this);
        else
            manager = this;

        DontDestroyOnLoad(gameObject);

        SoundDictionary = new Dictionary<string, Sound>();
        foreach (Sound sound in Clips)
        {
            GameObject GO = new GameObject("Sound: " + sound.GetName());

            GO.transform.SetParent(this.transform);

            sound.SetSource(GO.AddComponent<AudioSource>());

            SoundDictionary.Add(sound.GetName(), sound);
        }
    }

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

    public static AudioManager GetInstance() { return manager; }
}
