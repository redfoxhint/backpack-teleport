using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioFiles
{
    ST_01,
    ST_02,
    SFX_ArtiAttack1,
    SFX_ArtiAttack2,
    SFX_ArtiAttack3,
    SFX_ArtiGrunt1,
    SFX_ArtiGrunt2,
    SFX_ArtiGrunt3,
    SFX_Campfire,
    SFX_GrassWalk1,
    SFX_GrassWalk2,
    SFX_GrassWalk3,
    SFX_GrassWalk4,
    SFX_RecallBackpack,
    SFX_Teleport1,
    SFX_Teleport2,
    SFX_Click1,
    SFX_Select1
}

public enum AudioType { MUSIC, SOUNDEFFECT, AMBIENT }

public class AudioManager : PersistentSingleton<AudioManager>
{
    [Header("Audio Manager Configuration")]
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup soundGroup;
    [SerializeField] private AudioMixerGroup uiGroup;

    [Header("Debug")]
    [SerializeField] private bool debug;

    // Private Variables
    private AudioSource musicSource1; // The starting music source
    private AudioSource musicSource2; // The next music source (this is the source that will be faded into)

    private AudioSource ambientSource1;
    private AudioSource ambientSource2;

    private AudioSource sfxSource;
    private AudioSource uiSource;

    private AudioSource currentMusicSource;
    private AudioSource currentAmbientSource;

    // Resource Paths
    private const string sfxPath = "Audio/SFX";
    private const string musicPath = "Audio/Music";
    private const string uiPath = "Audio/UI";

    // Tasks
    private Task crossfadeMusicTask;
    private Task crossfadeAmbientTask;

    private Task musicFadeInTask;
    private Task musicFadeOutTask;

    private Task ambientFadeInTask;
    private Task ambientFadeOutTask;

    #region Unity Functions
    public override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FadeIn(AudioType.MUSIC, AudioFiles.ST_01, 5f);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Crossfade(AudioType.MUSIC, AudioFiles.ST_02);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Crossfade(AudioType.MUSIC, AudioFiles.ST_01);
        }
    }

    #endregion

    #region Public Functions
    public void PlaySoundEffect(AudioFiles audioFile, float volumeScale = 1)
    {
        AudioClip clip = GetAudioClipFromFile(audioFile);

        if (clip != null)
            sfxSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayUISoundEffect(AudioFiles audioFile)
    {
        AudioClip clip = GetAudioClipFromFile(audioFile);

        if (clip != null)
            uiSource.PlayOneShot(clip);
    }

    public void FadeIn(AudioType audioType, AudioFiles audioName, float fadeInTime = 5f, bool immediate = false)
    {
        AudioSource availableSource = GetNextAvailableSource(audioType);

        if (availableSource != null)
        {
            switch (audioType)
            {
                case AudioType.SOUNDEFFECT:
                    break;
                case AudioType.MUSIC:
                    FadeInMusic(audioName, availableSource, fadeInTime, immediate);
                    break;
                case AudioType.AMBIENT:
                    FadeInAmbient(audioName, availableSource, fadeInTime, immediate);
                    break;
            }
        }
        else
        {
            Debug.Log("Available audio source not found.");
        }
    }

    public void FadeOut(AudioType audioType, float time = 5f, System.Action OnFinishedFading = null)
    {
        switch (audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.MUSIC:
                FadeOutMusic(time, () => OnFinishedFading?.Invoke());
                break;
            case AudioType.AMBIENT:
                FadeOutAmbient(time, () => OnFinishedFading?.Invoke());
                break;
        }
    }

    public void Crossfade(AudioType audioType, AudioFiles audioToFadeTo, float time = 5f, bool immediate = false)
    {
        switch (audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.MUSIC:
                CrossFadeMusic(audioType, audioToFadeTo, time);
                break;
            case AudioType.AMBIENT:
                CrossFadeBackground(audioType, audioToFadeTo, time);
                break;

        }
    }

    #endregion

    #region Private Functions
    private void Init()
    {
        GameObject primarySourceObject = new GameObject("Music Source 1 Object");
        primarySourceObject.transform.SetParent(transform);

        musicSource1 = primarySourceObject.AddComponent<AudioSource>();
        musicSource1.outputAudioMixerGroup = musicGroup;

        GameObject nextSourceObject = new GameObject("Music Source 2 Object");
        nextSourceObject.transform.SetParent(transform);

        musicSource2 = nextSourceObject.AddComponent<AudioSource>();
        musicSource2.outputAudioMixerGroup = musicGroup;

        GameObject primaryBackgroundSourceObject = new GameObject("Background Source 1 Object");
        primaryBackgroundSourceObject.transform.SetParent(transform);

        ambientSource1 = primaryBackgroundSourceObject.AddComponent<AudioSource>();
        ambientSource1.outputAudioMixerGroup = soundGroup;

        GameObject nextBackgroundSourceObject = new GameObject("Background Source 2 Object");
        nextBackgroundSourceObject.transform.SetParent(transform);

        ambientSource2 = nextBackgroundSourceObject.AddComponent<AudioSource>();
        ambientSource2.outputAudioMixerGroup = soundGroup;

        GameObject sfxSourceObject = new GameObject("SFX Source Object");
        sfxSourceObject.transform.SetParent(transform);

        sfxSource = sfxSourceObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = soundGroup;

        GameObject uiSourceObject = new GameObject("UI Source Object");
        uiSourceObject.transform.SetParent(transform);

        uiSource = sfxSourceObject.AddComponent<AudioSource>();
        uiSource.outputAudioMixerGroup = uiGroup;
    }
    private void FadeInMusic(AudioFiles newAudio, AudioSource availableSource, float fadeInTime, bool immediate)
    {
        if (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            if (immediate)
            {
                RestartSourceImmediate(currentMusicSource, GetAudioClipFromFile(newAudio));
            }
            else // If not play immediate fade out and fade into new audio
            {
                FadeOutMusic(fadeInTime, () =>
                {
                    musicFadeInTask = new Task(AudioFadeInRoutine(currentMusicSource, newAudio, fadeInTime));
                    musicFadeInTask.Start();
                });
            }
        }

        else if(currentMusicSource == null)
        {
            if(availableSource != null)
            {
                currentMusicSource = availableSource;

                if(immediate)
                {
                    RestartSourceImmediate(currentMusicSource, GetAudioClipFromFile(newAudio));
                }
                else // If not play immediate fade out and fade into new audio
                {
                    FadeOutMusic(fadeInTime, () =>
                    {
                        musicFadeInTask = new Task(AudioFadeInRoutine(currentMusicSource, newAudio, fadeInTime));
                        musicFadeInTask.Start();
                    });
                }
            }
        }
    }
    private void FadeInAmbient(AudioFiles newAudio, AudioSource availableSource, float fadeInTime, bool immediate)
    {
        if(currentAmbientSource != null && currentAmbientSource.isPlaying)
        {
            if (immediate)
            {
                RestartSourceImmediate(currentAmbientSource, GetAudioClipFromFile(newAudio));
            }
            else // If not play immediate fade out and fade into new audio
            {
                FadeOutAmbient(fadeInTime, () =>
                {
                    ambientFadeInTask = new Task(AudioFadeInRoutine(currentAmbientSource, newAudio, fadeInTime));
                    ambientFadeInTask.Start();
                });
            }
        }

        else if (currentAmbientSource == null)
        {
            if (availableSource != null)
            {
                currentAmbientSource = availableSource;

                if (immediate)
                {
                    RestartSourceImmediate(currentAmbientSource, GetAudioClipFromFile(newAudio));
                }
                else // If not play immediate fade out and fade into new audio
                {
                    FadeOutAmbient(fadeInTime, () =>
                    {
                        ambientFadeInTask = new Task(AudioFadeInRoutine(currentAmbientSource, newAudio, fadeInTime));
                        ambientFadeInTask.Start();
                    });
                }
            }
        }
    }
    private void FadeOutMusic(float fadeOutTime, System.Action OnFinishedFading = null)
    {
        if (currentMusicSource.isPlaying)
        {
            musicFadeOutTask = new Task(AudioFadeOutRoutine(currentMusicSource, fadeOutTime, OnFinishedFading));
            musicFadeOutTask.Start();
        }
        else
        {
            OnFinishedFading?.Invoke(); // Raise finished fading event right away if no music is currently playing.
        }
    }
    private void FadeOutAmbient(float fadeOutTime, System.Action OnFinishedFading = null)
    {
        if (currentAmbientSource.isPlaying)
        {
            ambientFadeOutTask = new Task(AudioFadeOutRoutine(currentAmbientSource, fadeOutTime, OnFinishedFading));
            ambientFadeOutTask.Start();
        }
        else
        {
            OnFinishedFading?.Invoke(); // Raise finished fading event right away if no music is currently playing.
        }
    }

    private void CrossFadeMusic(AudioType audioType, AudioFiles audioFile, float time)
    {
        AudioSource newSource = GetNextAvailableSource(audioType);

        if(newSource != null)
        {
            if (crossfadeMusicTask != null && crossfadeMusicTask.Running) return;

            crossfadeMusicTask = new Task(AudioCrossfadeRoutine(currentMusicSource, newSource, audioFile, time));
            crossfadeMusicTask.Start();
        }
    }

    private void CrossFadeBackground(AudioType audioType, AudioFiles audioFile, float time)
    {
        AudioSource newSource = GetNextAvailableSource(audioType);

        if (newSource != null)
        {
            if (crossfadeAmbientTask != null && crossfadeAmbientTask.Running) return;

            crossfadeAmbientTask = new Task(AudioCrossfadeRoutine(currentAmbientSource, newSource, audioFile, time));
            crossfadeAmbientTask.Start();
        }
    }
    private IEnumerator AudioFadeInRoutine(AudioSource source, AudioFiles newAudio, float fadeInTime)
    {
        source.clip = GetAudioClipFromFile(newAudio);
        source.volume = 0f;
        source.Play();

        float elapsedTime = 0f;

        while (source.volume < 1f)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeInTime);

            yield return null;
        }

        yield break;
    }

    private IEnumerator AudioFadeOutRoutine(AudioSource source, float fadeOutTime, System.Action OnFinishedFading)
    {
        float elapsedTime = 0f;

        while (source.volume > 0f)
        {
            elapsedTime += Time.deltaTime;
            source.volume = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);

            yield return null;
        }

        OnFinishedFading?.Invoke();

        yield break;
    }

    private IEnumerator AudioCrossfadeRoutine(AudioSource currentSource, AudioSource newSource, AudioFiles newClip, float crossfadeTime)
    {
        if (currentSource == null) yield break;

        currentMusicSource = newSource;

        newSource.clip = GetAudioClipFromFile(newClip);
        newSource.volume = 0f;
        newSource.Play();

        float elapsedTime = 0f;

        while (currentSource.volume > 0f)
        {
            elapsedTime += Time.deltaTime;
            currentSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / crossfadeTime);
            newSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / crossfadeTime);

            yield return null;
        }

        currentSource.Stop();
        currentSource.volume = 0f;
        currentSource.clip = null;

        yield break;
    }

    public AudioClip GetAudioClipFromFile(AudioFiles audioFile)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>($"{sfxPath}/{audioFile.ToString()}");

        if (sfxClip == null)
        {
            AudioClip musicClip = Resources.Load<AudioClip>($"{musicPath}/{audioFile.ToString()}");

            if (musicClip == null)
            {
                AudioClip uiClip = Resources.Load<AudioClip>($"{uiPath}/{audioFile.ToString()}");

                if(uiClip == null)
                {
                    LogUtils.LogError($"Clip with name: {audioFile.ToString()} was not found.");
                    return null;
                }

                return uiClip;
            }

            return musicClip;
        }

        return sfxClip;
    }

    private AudioSource GetNextAvailableSource(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.MUSIC:

                if (!musicSource1.isPlaying)
                {
                    return musicSource1;
                }

                return musicSource2;

            case AudioType.AMBIENT:

                if (!ambientSource1.isPlaying)
                {
                    return ambientSource1;
                }

                return ambientSource2;
        }

        Debug.Log("No source is available");

        return null;
    }

    private void RestartSourceImmediate(AudioSource source, AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    #endregion
}
