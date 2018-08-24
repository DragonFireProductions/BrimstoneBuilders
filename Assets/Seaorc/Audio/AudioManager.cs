using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager manager;
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
            GameObject gameObject = new GameObject("Sound: " + sound.GetName());

            gameObject.transform.SetParent(this.transform);

            sound.SetSource(gameObject.AddComponent<AudioSource>());

            SoundDictionary.Add(sound.GetName(), sound);
        }
    }

    public void PlaySound(string _Sound)
    {
        Sound SoundToPlay = SoundDictionary[_Sound];

        if (SoundToPlay != null)
        {
            if (!SoundToPlay.GetIsMusic())
            {
                SoundToPlay.PlaySound();
            }
            else
                Debug.Log("The sound you tried to play is of type \"Music\" try calling PlayMusic insted");
        }
        else
            Debug.Log("Sound Not Found");
    }

    public void PlayMusic(string _Sound)
    {
        Sound SongToPlay = SoundDictionary[_Sound];

        if (SongToPlay != null)
        {
            if(SongToPlay.GetIsMusic())
            {
                if (CurrentSong != null)
                    CurrentSong.StopSound();

                CurrentSong = SongToPlay;
                SongToPlay.PlaySound();
            }
            else
                Debug.Log("The sound you tried to play is of type \"Sound\" try calling PlaySound insted");
        }
        else
            Debug.Log("Sound Not Found");
    }

    public static AudioManager GetInstance() { return manager; }
}
