using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup soundGroup;

    public override void Awake()
    {
        base.Awake();

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
            switch(s.audioType)
            {
                case AudioType.SOUNDEFFECT:
                    s.source.outputAudioMixerGroup = soundGroup;
                    break;
                case AudioType.SOUNDTRACK:
                    s.source.outputAudioMixerGroup = musicGroup;
                    break;
            }
        }
    }

    private void Start()
    {
        Play("stTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
            s.source.Play();
        else
            Debug.Log($"Sound {s.name} was not found");
    }
}
