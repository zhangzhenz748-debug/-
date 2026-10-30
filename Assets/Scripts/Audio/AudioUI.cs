using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioUI : MonoBehaviour//UI
{
    public AudioData audioData;
    // public Slider master;
    public Slider bgm;
    public Slider sfx;
    void SetAudio1(float i)
    {
        EventHandler.CallAudio(1f, bgm.value, sfx.value);
    }
    void SetAudio2(float i)
    {
        EventHandler.CallAudio(0, bgm.value, sfx.value);
    }
    void Awake()
    {
        bgm.onValueChanged.AddListener(SetAudio1);
        sfx.onValueChanged.AddListener(SetAudio2);
    }
    void OnEnable()
    {
        EventHandler.SetAudio += SetSlider;
    }
    void OnDisable()
    {
        EventHandler.SetAudio -= SetSlider;
    }
    void SetSlider()
    {
        bgm.value = audioData.bgmSource;
        sfx.value = audioData.sfxSource;
    }
}
