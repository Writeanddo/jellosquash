using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public string[] scenes;
  private bool[] _isLoaded;
  List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

  void Awake()
  {
    _isLoaded = new bool[scenes.Length];
    DontDestroyOnLoad(this);
  }

  void Start()
  {
    CheckLoadedScenes();
    StartGame();
  }

  private void CheckLoadedScenes()
  {
    if (SceneManager.sceneCount > 0)
    {
      for (int i=0; i < SceneManager.sceneCount; i++)
      {
        Scene scene = SceneManager.GetSceneAt(i);
        for (int s=0; s < scenes.Length; s++)
          if (scene.name == scenes[s]) _isLoaded[s] = true;
      }
    }
  }

  public void StartGame()
  {
    for (int s=0; s < scenes.Length; s++)
    {
      if (!_isLoaded[s])
      {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(scenes[s], LoadSceneMode.Additive));
        _isLoaded[s] = true;
      }
    }
  }

  void Update()
  {
  }
}
