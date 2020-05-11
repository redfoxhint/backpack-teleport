using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType { SOUNDTRACK, SOUNDEFFECT }

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;
    public bool loop;
    public AudioType audioType;

    [HideInInspector] public AudioSource source;
}
