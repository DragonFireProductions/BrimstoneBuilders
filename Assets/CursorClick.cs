using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorClick : MonoBehaviour
{
    AudioSource audio;
    UnityEngine.UI.Button button;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        button = gameObject.GetComponent<UnityEngine.UI.Button>();
        audio.clip = clip;
        audio.playOnAwake = false;

        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        audio.Play();
    }
}
