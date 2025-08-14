using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] AudioSource audioSource;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAudio(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
