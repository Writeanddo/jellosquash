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
    audioSource.loop = true;  
    audioSource.Play();
  }
  public void GameStartMusic() //when press play
  {
    StartCoroutine(GameStart());
  }

  public void GameExitMusic() //when press play
  {
    StartCoroutine(GameExit());
  }
  IEnumerator GameStart() //in game
  {
    //press play previous fade out, now another fade in
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeOutVolume));

    yield return new WaitForSeconds(fadeDuration);

    audioSource.clip = gameTheme;
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeInVolume));
    audioSource.loop = true;
    audioSource.Play();
  }

  IEnumerator GameExit() // in menu
  {
    //lowers game theme and change back to menu theme higher
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeOutVolume));

    yield return new WaitForSeconds(fadeDuration);

    audioSource.clip = gameMenu;
    StartCoroutine(FadeMixer.StartFade(mixer, "vol", fadeDuration, fadeInVolume));
    audioSource.loop = true;
    audioSource.Play();
  }


}