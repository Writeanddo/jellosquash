using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public partial class Player : MonoBehaviour
{
  public static bool dead = true;
  public static bool move = false;
  public static bool win = false;
  public TextMeshProUGUI status;

  public GameManager gameManager;
  public CinemachineFreeLook cinemachineFreeLook;

  public float[] defaultRadius = new float[3] {25.0f, 20.0f, 10.0f};

  void Start()
  {
    status.text = "";
    move = false;
    dead = false;
    MovementInit();
    SizeInit();
  }

  void Update()
  {
    if (move && !dead)
      MovementUpdate();
    SizeUpdate();

    cinemachineFreeLook.m_Orbits[0].m_Radius = defaultRadius[0] + transform.localScale.x;
    cinemachineFreeLook.m_Orbits[1].m_Radius = defaultRadius[1] + transform.localScale.x;
    cinemachineFreeLook.m_Orbits[2].m_Radius = defaultRadius[2] + transform.localScale.x;

    if(dead)
    {
      status.text = "Game Over. Press R to restart. Press esc to quit game";
      status.transform.parent.transform.LookAt(Camera.main.transform);
    }

    // if(win)
    // {
    //   status.text = "Victory! Press esc to quit the game."
    // }

    if (move)
    {
      if (Input.GetKeyDown(KeyCode.R))
      {
        gameManager.RepositionPlayer();
        dead = false;
        status.text = "";
        SceneManager.UnloadSceneAsync(GameManager.currScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        SceneManager.LoadSceneAsync(GameManager.currScene, LoadSceneMode.Additive);
      }
    }
  }

  public void RuptureDropJelly(int totalMassLossCount)
  {
    for (int i=0; i < totalMassLossCount; i++)
    {
      DropJelly(new Vector3(Random.Range(0.0f, 1.0f), 0.0f, Random.Range(0.0f, 1.0f)).normalized*3.0f);
    }
    GameManager.currSize = _localScale.x;
    for (int s=0; s < gameManager.scenes.Length; s++)
    {
      if (GameManager.currScene == gameManager.scenes[s] && s < gameManager.scenes.Length-1)
      {
        GameManager.currScene = gameManager.scenes[s+1];
        break;
      }
    }
  }

  public void DestroyItem()
  {
    if (_itemExists)
    {
      Destroy(item);
      item = null;
      _itemExists = false;
    }
  }

  private void DropJelly(Vector3 direction)
  {
    if (transform.localScale.y > 1.0f)
    {
      GameObject littleJelly = Instantiate(jellyPrefab,
        transform.position + direction*transform.localScale.z + direction*spawnOffset + transform.up*spawnOffset,
        transform.rotation);
      littleJelly.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

      SceneManager.MoveGameObjectToScene(littleJelly, SceneManager.GetSceneByName(GameManager.currScene));
      Rigidbody littleJellyRB = littleJelly.GetComponent<Rigidbody>();
      littleJellyRB.isKinematic = false;
      littleJellyRB.velocity = direction*throwVelocity + transform.up*throwVelocity*2;

      SphereCollider littleJellySC = littleJelly.GetComponent<SphereCollider>();
      littleJellySC.isTrigger = false;

      _localScale -= new Vector3(0.5f, 0.5f, 0.5f);
      _animationTime = 0.0f;
    }
  }
}
