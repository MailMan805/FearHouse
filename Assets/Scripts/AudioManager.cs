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

  public AudioSource MusicSource;
  public AudioSource SoundSource;
  private Dictionary<string, AudioEvent> MusicEvents;
  private Dictionary<string, AudioEvent> SoundEvents;

  private void Awake()
  {
    if (instance == null)
    {
      MusicEvents = new Dictionary<string, AudioEvent>();
      SoundEvents = new Dictionary<string, AudioEvent>();
      RegisterMusic();
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

  }
  public void Play(AudioSource source, string audio)
  {
    source.clip = SoundEvents[audio].GetClip();
    source.Play();
  }
  public void PlayMusic(string audio)
  {
    Play(MusicSource, audio);
  }
  public void PlaySound(string audio)
  {
    Play(SoundSource, audio);
  }

  //Plays a sound at a specific location by creating and destroying an object with an audio source.
  public void PlayAt(string audio, Vector3 position, float maxDistance = 1f)
  {
    GameObject soundPlayer = new GameObject();
    soundPlayer.transform.position = position;
    AudioSource source = soundPlayer.AddComponent<AudioSource>();
    source.maxDistance = maxDistance;
    Play(source, audio);
    Destroy(soundPlayer, source.clip.length);
  }
  // A specific implementation of PlaySoundAt with Mood sounds, where the object is placed at a random location offset from either player
  public void PlayMood()
  {
    GameObject pines = GameObject.FindGameObjectWithTag("Pine");
    GameObject racc = GameObject.FindGameObjectWithTag("Racc");
    Transform transform = Random.Range(0, 2) == 0 ? pines.transform : racc.transform;
    Vector3 randomPosition = transform.position + new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
    PlayAt("Mood", randomPosition);
  }
}
