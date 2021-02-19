using UnityEngine;
using TMPro;
public partial class Player : MonoBehaviour
{
  public static bool dead = true;
  public static bool move = false;
  public TextMeshProUGUI status;
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
    if(dead)
    {
      status.text = "Game Over. Press R to restart. Press esc to quit game";
      status.transform.parent.transform.LookAt(Camera.main.transform);
    }
    // if(win)
    // {
    //   status.text = "Victory! Press esc to quit the game."
    // }
  }

  public void RuptureDropJelly(int totalMassLossCount)
  {
    for (int i=0; i < totalMassLossCount; i++)
    {
      DropJelly(new Vector3(Random.Range(0.0f, 1.0f), 0.0f, Random.Range(0.0f, 1.0f)).normalized);
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
