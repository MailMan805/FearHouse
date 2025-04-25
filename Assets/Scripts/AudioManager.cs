using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** 
    The script component of the Audio Manager, which is used to play 

 */
public class AudioEvent
{
  private List<AudioClip> clips;
  private AudioEvent(List<AudioClip> clips)
  {
    this.clips = clips;
  }
  public static AudioEvent Of(params AudioClip[] events)
  {
    return new AudioEvent(new List<AudioClip>(events));
  }
  public AudioClip GetClip()
  {
    return clips[Random.Range(0, clips.Count)];
  }
}
public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioClip musicPunkSong;
  public AudioClip soundShotgun;
  public AudioClip soundChairAttack;

  public AudioSource musicSource;
  public AudioSource soundSource;
  public Dictionary<string, AudioEvent> musicEvents;
  public Dictionary<string, AudioEvent> soundEvents;

  private void Awake()
  {
    if (instance == null)
    {
      musicEvents = new Dictionary<string, AudioEvent>();
      RegisterMusic();
      soundEvents = new Dictionary<string, AudioEvent>();
      RegisterSounds();
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }
  public void RegisterMusic()
  {

  }
  public void RegisterSounds()
  {
    soundEvents.Add("shotgun", AudioEvent.Of(soundShotgun));
  }
  public void Play(AudioSource source, string audio)
  {
    source.clip = soundEvents[audio].GetClip();
    source.Play();
  }
  public void PlayMusic(string audio)
  {
    Play(musicSource, audio);
  }
  public void PlaySound(string audio)
  {
    Play(soundSource, audio);
  }
  public void PlaySoundAt(string audio, Transform transform)
  {
    GameObject soundPlayer = new GameObject();
    soundPlayer.transform.position = transform.position;
    AudioSource source = soundPlayer.AddComponent<AudioSource>();
    Play(source, audio);
    Destroy(soundPlayer, source.clip.length);
  }
}
