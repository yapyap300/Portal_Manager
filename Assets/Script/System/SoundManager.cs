using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] private AudioMixer audioMixer;
    private AudioMixerGroup audioMixerGroup;
    [Header("#===BGM")]
    [SerializeField] private AudioClip[] bgmClip;
    public float bgmVolume;
    private AudioSource bgmPlayer;
    [Header("#===SFX")]
    [SerializeField] private AudioClip[] sfxClips;
    public float sfxVolume;
    [SerializeField] private int channelCount;
    private int channelIndex;

    private Dictionary<string, AudioClip> soundDictionary;
    private AudioSource[] sfxPlayer;    

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            soundDictionary = new Dictionary<string, AudioClip>();
            PlayerSetUp();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlayerSetUp()
    {
        audioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
        GameObject sfx = new("SFX");
        sfx.transform.SetParent(transform);
        sfxPlayer = new AudioSource[channelCount];
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            sfxPlayer[index] = sfx.AddComponent<AudioSource>();
            sfxPlayer[index].playOnAwake = false;
            sfxPlayer[index].volume = sfxVolume;
            sfxPlayer[index].outputAudioMixerGroup = audioMixerGroup;
        }

        GameObject bgm = new("BGM");
        bgm.transform.SetParent(transform);
        bgmPlayer = bgm.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        foreach (AudioClip clip in sfxClips)
        {
            soundDictionary.Add(clip.name, clip);
        }
    }

    public void PlaySfx(string name)
    {
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
                continue;
            channelIndex = loopIndex;
            sfxPlayer[loopIndex].clip = soundDictionary[name];
            sfxPlayer[loopIndex].Play();
            break;
        }
    }
    public void SetBGM(int index)
    {
        if (bgmPlayer.isPlaying)
            bgmPlayer.Stop();
        bgmPlayer.clip = bgmClip[index];
    }
    public void PlayBGM()
    {
        bgmPlayer.Play();
    }
    public void StopBGM()
    {
        bgmPlayer.Pause();
    }
    public void Volume(float sliderValue)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 10);
    }
}
