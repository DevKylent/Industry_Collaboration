using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * Programmer: Carlos E. Loubriel
 * Purpose: This code is meant to Control the audio in the Game
 */
public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public Sounds[] sounds;
    

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string clipName)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == clipName);

        if (s == null)
        {
            Debug.Log("Audio " + clipName + " does not exist");
            return;
        }

        Debug.Log("Audio " + clipName + " has been played");
        s.source.Play();
    }

    public void Stop(string clipName)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == clipName);

        if (s == null)
            return;

        s.source.Stop();
    }
    public void MusicVolume(string clipName)
    {
        Sounds s = Array.Find(sounds, sound => sound.clipName == clipName);
        s.source.volume = volumeSlider.value;
    }
}
