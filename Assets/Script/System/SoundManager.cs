using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [Header("#===BGM")]
    [SerializeField] AudioClip[] bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    [Header("#===SFX")]
    [SerializeField] AudioClip[] sfxClips;
    public float sfxVolume;
    [SerializeField] int channelCount;
    int channelIndex;

    Dictionary<string, AudioClip> soundDictionary;
    AudioSource[] sfxPlayer;    

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
        GameObject sfx = new("SFX");
        sfx.transform.SetParent(transform);
        sfxPlayer = new AudioSource[channelCount];
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            sfxPlayer[index] = sfx.AddComponent<AudioSource>();
            sfxPlayer[index].playOnAwake = false;
            sfxPlayer[index].volume = sfxVolume;
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

    public void PlaySfx(string name,float time = 0f)
    {
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
                continue;
            channelIndex = loopIndex;
            sfxPlayer[loopIndex].time = time;
            sfxPlayer[loopIndex].clip = soundDictionary[name];
            sfxPlayer[loopIndex].Play();
            break;
        }
    }
    public void PlayBGM(int index)
    {
        if (bgmPlayer.isPlaying == true)
            bgmPlayer.Stop();
        bgmPlayer.clip = bgmClip[index];
        bgmPlayer.Play();
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
}
