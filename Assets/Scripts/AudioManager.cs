using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip musicPunkSong;
    public AudioClip soundShotgun;

    public AudioSource musicSource;
    public AudioSource soundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public AudioClip GetMusic(string name)
    {
        switch (name)
        {
            case "punk_song":
                return musicPunkSong;
        }
        return null;
    }
    public AudioClip GetSound(string name)
    {
        switch (name)
        {
            case "shotgun":
                return soundShotgun;
        }
        return null;
    }
    public void PlayMusic(string musicName,bool loop = true)
    {
        musicSource.clip =  GetMusic(musicName);
        musicSource.loop = loop;
        musicSource.Play();  
    }
    public void PlaySound(string soundName)
    {
        soundSource.clip = GetSound(soundName);
        soundSource.Play();
    }
}
/*
 * Audio Manager Script - WILL BE ONLY ON ONE MANAGER EMPTY OBJECT
 * - It will take an array or list of sound clips for both Music, and SFX
 * = Make an instance of this object
 * - FUNCTIONS
 * - PlayMusic() - When called it will play a certain music track and have it loop
 * - PlaySFX() - When called it will play a one time soundeffect
 * - LoopMusic() - Function will activate and deactivate looping of a sound track.
 * 
 */