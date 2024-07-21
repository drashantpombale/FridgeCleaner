using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingletonGeneric<SoundManager>
{
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource sfxSource;


    public AudioClip itemSound;
    public AudioClip spraySound;
    public AudioClip cleanSound;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void PlaySFX(AudioClip clip, bool loop)
    {
        sfxSource.loop = loop;
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void StopSFX() 
    {
        sfxSource.Stop();
    }

    public void MuteSFX()
    {
        sfxSource.mute = true;
    }

    public void MuteMusic()
    {
        musicSource.mute = true;
    }

    public void UnMuteSFX()
    {
        sfxSource.mute = false;
    }

    public void UnMuteMusic() 
    {
        musicSource.mute = false;
    }
}
