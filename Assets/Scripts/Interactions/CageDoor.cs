using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoor : MonoBehaviour
{
  public GameObject door;
  public BoxCollider disableCollider;
  public LayerMask playerLayer;
  public AnimationCurve doorAnimationCurve;
  public float animationDuration = 1.0f;
  public Quaternion targetRotation;

  private bool _trigger = false;
  private float _animationTime;
  private Quaternion _localRotation;
  public AudioClip cageOpen;

  void Start()
  {
    _localRotation = door.transform.localRotation;
  }

  void Update()
  {
    if ((_animationTime < animationDuration && _trigger) || (_animationTime > 0.0f && !_trigger))
    {
      float evaluatedTime = doorAnimationCurve.Evaluate(_animationTime);

      if (_trigger) _animationTime += Time.deltaTime;
      else _animationTime -= Time.deltaTime;
      door.transform.localRotation = Quaternion.Lerp(_localRotation, targetRotation, evaluatedTime);
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      if (collider.gameObject.GetComponent<Player>().item != null)
      {
        disableCollider.enabled = false;
        _trigger = true;
        SoundFX.source.PlayOneShot(cageOpen);
      }
    }
  }

  void OnTriggerExit(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      _trigger = false;
      disableCollider.enabled = true;
      if (collider.gameObject.GetComponent<Player>().item != null)
      {
        SoundFX.source.PlayOneShot(cageOpen);
      }
    }
  }
}
