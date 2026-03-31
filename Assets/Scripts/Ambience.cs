using UnityEngine;

public class Ambience : MonoBehaviour
{
  public static Ambience instance { get; private set; }

  private AudioSource ambientSource;

  private void Awake()
  {
    if (instance != null && instance != this)
    {
    Destroy(gameObject);
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);

    ambientSource = GetComponent<AudioSource>();
    if (ambientSource == null)
    {
        ambientSource = gameObject.AddComponent<AudioSource>();
    }

    ambientSource.loop = true;
    ambientSource.playOnAwake = true;
    ambientSource.playOnAwake = false;
  }

  public void PlayAmbientSound(AudioClip clip, float volume = 0.5f)
  {
    ambientSource.clip = clip;
    ambientSource.volume = volume;
    ambientSource.Play();
  }

  public void StopAmbientSound()
  {
    ambientSource.Stop();
  }

  public void SetAmbientVolume(float volume)
  {
    ambientSource.volume = Mathf.Clamp01(volume);
  }
}
