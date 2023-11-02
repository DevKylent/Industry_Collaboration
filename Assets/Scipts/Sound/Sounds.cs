using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Programmer: Carlos E. Loubriel
 * Purpose: This code is represents all variables for the audio that we want to control directly in the editor
 */
[System.Serializable]
public class Sounds
{
    public string clipName;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(-3, 3)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
