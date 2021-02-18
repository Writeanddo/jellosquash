using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class BgMusic : MonoBehaviour
{
  public AudioClip gameTheme;
  public AudioClip gameMenu;
  public AudioSource audioSource;  
  public AudioMixer mixer;
  public float fadeDuration;
  [Header("0 - 1")]
  public float fadeInVolume;
  public float fadeOutVolume;


  void Awake() => DontDestroyOnLoad(this.gameObject);

  void Start()
  {
    // game launch fade in
    audioSource.clip = gameMenu;
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeInVolume));
    audioSource.loop = false;  
    audioSource.Play();
  }

  void Update() // when press play
  {
    if(Input.GetKey("space"))
    {
      StartCoroutine(GameStart());
    }
  }
  IEnumerator GameStart()
  {
    //press play previous fade out, now another fade in
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeOutVolume));

    yield return new WaitForSeconds(fadeDuration);

    audioSource.clip = gameTheme;
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeInVolume));
    audioSource.loop = true;
    audioSource.Play();
  }
}