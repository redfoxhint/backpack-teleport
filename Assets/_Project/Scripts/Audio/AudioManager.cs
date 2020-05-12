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
    SFX_Teleport2
}

public enum AudioType { SOUNDTRACK, SOUNDEFFECT, BACKGROUND }

public class AudioManager : PersistentSingleton<AudioManager>
{
    [Header("Audio Manager Configuration")]
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup soundGroup;

    // Private Variables
    private AudioSource primaryMusicSource; // The starting music source
    private AudioSource nextMusicSource; // The next music source (this is the source that will be faded into)
    private AudioSource primaryBackgroundSource;
    private AudioSource nextBackgroundSource;
    private AudioSource sfxSource;

    private AudioSource currentMusicSource;
    private AudioSource currentBackgroundSource;

    // Resource Paths
    private const string sfxPath = "Audio/SFX";
    private const string musicPath = "Audio/Music";

    // Tasks
    private Task crossfadeTask;
    private Task musicFadeInTask;
    private Task musicFadeOutTask;
    private Task backGroundFadeInTask;
    private Task backGroundFadeOutTask;



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
            FadeIn(AudioType.SOUNDTRACK, AudioFiles.ST_01);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Crossfade(AudioType.SOUNDTRACK, AudioFiles.ST_02);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            FadeOut(AudioType.SOUNDTRACK);
        }
    }

    #endregion

    #region Public Functions
    public void PlaySoundEffect(AudioFiles audioFile)
    {
        AudioClip clip = GetAudioClipFromFile(audioFile);

        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void FadeIn(AudioType audioType, AudioFiles newAudio, float time = 5f, bool immediate = false)
    {
        switch (audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.SOUNDTRACK:
                FadeInMusic(newAudio, time, immediate);
                break;
            case AudioType.BACKGROUND:
                FadeInBackground(newAudio, time, immediate);
                break;
        }
    }

    public void FadeOut(AudioType audioType, float time = 5f, System.Action OnFinishedFading = null)
    {
        switch (audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.SOUNDTRACK:
                FadeOutMusic(time, () => OnFinishedFading?.Invoke());
                break;
            case AudioType.BACKGROUND:
                FadeOutBackground(time, () => OnFinishedFading?.Invoke());
                break;
        }
    }

    public void Crossfade(AudioType audioType, AudioFiles audioToFadeTo, float time = 5f, bool immediate = false)
    {
        switch(audioType)
        {
            case AudioType.SOUNDEFFECT:
                break;
            case AudioType.SOUNDTRACK:
                //CrossFadeMusic(newAudio, time, immediate);
                break;
            case AudioType.BACKGROUND:
                //CrossFadeBackground(newAudio, time, immediate);
                break;

        }

        if (!primaryMusicSource.isPlaying) return;

        if (crossfadeTask == null)
        {
            crossfadeTask = new Task(AudioCrossfadeRoutine(primaryMusicSource, nextMusicSource, audioToFadeTo, time));
            crossfadeTask.Start();
        }
    }

    #endregion

    #region Private Functions
    private void Init()
    {
        primaryMusicSource = gameObject.AddComponent<AudioSource>();
        primaryMusicSource.outputAudioMixerGroup = musicGroup;

        nextMusicSource = gameObject.AddComponent<AudioSource>();
        nextMusicSource.outputAudioMixerGroup = musicGroup;

        primaryBackgroundSource = gameObject.AddComponent<AudioSource>();
        primaryBackgroundSource.outputAudioMixerGroup = soundGroup;

        nextBackgroundSource = gameObject.AddComponent<AudioSource>();
        nextBackgroundSource.outputAudioMixerGroup = soundGroup;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = soundGroup;
    }
    private void FadeInMusic(AudioFiles newAudio, float time, bool immediate)
    {
        if (primaryMusicSource.isPlaying)
        {
            if (immediate)
            {
                LogUtils.Log($"New music wants to fade in. Will do immediately");

                primaryMusicSource.Stop();
                primaryMusicSource.volume = 0f;

                musicFadeInTask?.Stop();

                musicFadeInTask = new Task(AudioFadeInRoutine(primaryMusicSource, newAudio, time));
                musicFadeInTask.Start();
            }

            else
            {
                LogUtils.Log($"New music wants to fade in. Will do after finished fading out old music");

                FadeOutMusic(time, () =>
                {
                    musicFadeInTask?.Stop();

                    musicFadeInTask = new Task(AudioFadeInRoutine(primaryMusicSource, newAudio, time));
                    musicFadeInTask.Start();
                });
            }
        }

        else
        {
            LogUtils.Log($"New music wants to fade in. Will do now since no other music playing.");

            musicFadeInTask = new Task(AudioFadeInRoutine(primaryMusicSource, newAudio, time));
            musicFadeInTask.Start();
        }
    }

    private void FadeInBackground(AudioFiles newAudio, float time, bool immediate)
    {
        if (primaryBackgroundSource.isPlaying)
        {
            if (immediate)
            {
                primaryBackgroundSource.Stop();
                primaryBackgroundSource.volume = 0f;

                backGroundFadeInTask?.Stop();

                backGroundFadeInTask = new Task(AudioFadeInRoutine(primaryBackgroundSource, newAudio, time));
                backGroundFadeInTask.Start();
            }
            else
            {
                FadeOutBackground(time, () =>
                {
                    backGroundFadeInTask?.Stop();

                    backGroundFadeInTask = new Task(AudioFadeInRoutine(primaryBackgroundSource, newAudio, time));
                    backGroundFadeInTask.Start();
                });
            }
        }
    }

    private void FadeOutMusic(float time, System.Action OnFinishedFading = null)
    {
        if(primaryMusicSource.isPlaying)
        {
            musicFadeOutTask = new Task(AudioFadeOutRoutine(primaryMusicSource, time, OnFinishedFading));
            musicFadeOutTask.Start();
        }
        else
        {
            OnFinishedFading?.Invoke(); // Raise finished fading event right away if no music is currently playing.
        }
    }

    private void FadeOutBackground(float time, System.Action OnFinishedFading = null)
    {
        if(primaryBackgroundSource.isPlaying)
        {
            backGroundFadeOutTask = new Task(AudioFadeOutRoutine(primaryBackgroundSource, time, OnFinishedFading));
            backGroundFadeOutTask.Start();
        }
        else
        {
            OnFinishedFading?.Invoke(); // Raise finished fading event right away if no music is currently playing.
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

    private IEnumerator AudioCrossfadeRoutine(AudioSource currentSource, AudioSource nextSource, AudioFiles newClip, float crossfadeTime)
    {
        LogUtils.Log($"Crossfading audio: {primaryMusicSource?.clip.name} -> {newClip.ToString()}");

        nextSource.clip = GetAudioClipFromFile(newClip);
        nextSource.volume = 0f;
        nextSource.Play();

        float elapsedTime = 0f;

        while (currentSource.volume > 0f)
        {
            elapsedTime += Time.deltaTime;
            currentSource.volume = Mathf.Lerp(1f, 0f, elapsedTime / crossfadeTime);
            nextSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / crossfadeTime);

            yield return null;
        }

        currentSource.Stop();
        currentSource.volume = 0f;
        currentSource.clip = null;

        primaryMusicSource = nextSource;

        yield break;

    }

    private AudioClip GetAudioClipFromFile(AudioFiles audioFile)
    {
        AudioClip sfxClip = Resources.Load<AudioClip>($"{sfxPath}/{audioFile.ToString()}");

        if (sfxClip == null)
        {
            AudioClip musicClip = Resources.Load<AudioClip>($"{musicPath}/{audioFile.ToString()}");

            if (musicClip == null)
            {
                LogUtils.LogError($"Clip with name: {audioFile.ToString()} was not found.");
                return null;
            }

            return musicClip;
        }

        return sfxClip;
    }
    #endregion

    public void Play(string name)
    {

    }
}
