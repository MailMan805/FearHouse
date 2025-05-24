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
      soundEvents = new Dictionary<string, AudioEvent>();
      RegisterMusic();
      RegisterSounds();
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }
  public void Update()
  {
  }
  public void RegisterMusic()
  {

  }
  public void RegisterSounds()
  {
    soundEvents.Add("shotgun", AudioEvent.Of(soundShotgun));
    soundEvents.Add("mood", AudioEvent.Of(soundShotgun));
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
  public void PlaySoundAt(string audio, Vector3 position, float maxDistance = 1f)
  {
    GameObject soundPlayer = new GameObject();
    soundPlayer.transform.position = position;
    AudioSource source = soundPlayer.AddComponent<AudioSource>();
    source.maxDistance = maxDistance;
    Play(source, audio);
    Destroy(soundPlayer, source.clip.length);
  }
  public void PlayMoodSound()
  {
    GameObject pines = GameObject.FindGameObjectWithTag("Pine");
    GameObject racc = GameObject.FindGameObjectWithTag("Racc");
    Transform transform = Random.Range(0, 2) == 0 ? pines.transform : racc.transform;
    Vector3 randomPosition = transform.position + new Vector3(Random.Range(-320f, 320f), 0f, Random.Range(-320f, 320f));
    PlaySoundAt("mood", randomPosition);
  }
}
