using UnityEngine;

public class BgMusic : MonoBehaviour
{
  public AudioClip gameTheme;
  public AudioClip gameMenu;
  public AudioSource audioSource;  
  void Awake() => DontDestroyOnLoad(this.gameObject);
  void Start()
  {
    audioSource.clip = gameMenu;
    audioSource.loop = false;
    audioSource.Play();
  }
  // when press play
  // game start
  void GameStart()
  {
    audioSource.Stop();
    audioSource.clip = gameTheme;
    audioSource.loop = true;
    audioSource.Play();
  }
}