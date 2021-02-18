using UnityEngine;

public partial class Player : MonoBehaviour
{
  public static bool dead = true;

  void Start()
  {
    dead = false;
    MovementInit();
    SizeInit();
  }

  void Update()
  {
    MovementUpdate();
    SizeUpdate();
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
