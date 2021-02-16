using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
  public Rigidbody rb;
  public SphereCollider sphereCollider;

  public float stopThreshold = 0.2f;

  void Update()
  {
    if (rb.velocity.magnitude < stopThreshold)
    {
      rb.isKinematic = true;
      sphereCollider.isTrigger = true;
    }
  }
}
