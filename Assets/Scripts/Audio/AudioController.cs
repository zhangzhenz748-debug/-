using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour//音频控制器
{
    public AudioData audioData;
    public AudioSource bgmSource;//音频
    public AudioSource sfxSource;
    void OnEnable()
    {
        EventHandler.Audio += OnAudio;
        EventHandler.Audiosfx += OnPlayersfx;
    }
    void OnDisable()
    {
        EventHandler.Audio -= OnAudio;
        EventHandler.Audiosfx -= OnPlayersfx;
    }
    void Awake()
    {
        OnAudio(0, audioData.bgmSource, audioData.sfxSource);
        DontDestroyOnLoad(gameObject);
    }
    private void OnAudio(float master, float bgm, float sfx)
    {
        if (master == 1f)
        {
            bgmSource.volume = bgm;
            audioData.bgmSource = bgm;

        }
        else
        {
            sfxSource.volume = sfx;
            audioData.sfxSource = sfx;
        }

    }
    void OnPlayersfx()
    {
        sfxSource.Play();
    }
}
