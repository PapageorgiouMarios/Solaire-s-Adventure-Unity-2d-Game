using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;
    private AudioSource bossIntro;
    private AudioSource bossMusic;

    private void Awake()
    {
        instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != null && instance != this) 
        {
            Destroy(gameObject);
        }

        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    private void Update()
    {
        if(bossIntro != null && bossMusic != null) 
        {
            bossIntro.volume = musicSource.volume;
            bossMusic.volume = musicSource.volume;
        }
    }

    public void PlaySound(AudioClip sound) 
    {
        soundSource.PlayOneShot(sound);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source) 
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1, "soundVolume", change, soundSource);
    }

    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", change, musicSource);
    }

    public void PlayBossMusic()
    {
        musicSource.Stop(); // Stop any non-boss music playing

        bossIntro = transform.GetChild(1).GetComponent<AudioSource>();
        bossMusic = transform.GetChild(2).GetComponent<AudioSource>();

        if (bossIntro != null && bossMusic != null)
        {
            bossIntro.Play();
            Invoke("PlayBossLoop", bossIntro.clip.length - 2); // Start loop after intro finishes
        }
    }

    private void PlayBossLoop()
    {
        bossMusic.loop = true;
        bossMusic.Play();
    }

    public void ChangeMusicClip(AudioClip newClip)
    {
        if (musicSource != null && newClip != null && newClip != musicSource)
        {
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();
        }
    }

    public void StopMusic() 
    {
        if(musicSource != null) 
        {
            musicSource.Stop();
        }

        if(bossIntro != null) 
        {
            bossIntro.Stop();
        }

        if (bossMusic != null) 
        {
            bossMusic.Stop();
        }
    }
}
