using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect
{
    [Header("Sound Clip Configuration")]
    public string name;
    [Range(0, 1)] public float volume;
    [Range(0.1f, 3)] public float pitch;
    public bool loop;

    [Space]

    public AudioClip clip;
    public AudioType audioType;

    [HideInInspector] public AudioSource source;
}
