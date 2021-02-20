using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightController : MonoBehaviour
{
  public Vector3[] targetRotations;
  public float rotateSpeed;

  // public GameManager gameManager;
  private Vector3 _currRotation;

  void Start()
  {
    _currRotation = transform.rotation.eulerAngles;
  }

  void Update()
  {
    
    switch (GameManager.currScene)
    {
      default: _currRotation = targetRotations[0]; break;
      case "Level1": _currRotation = targetRotations[0]; break;
      case "Level2": _currRotation = targetRotations[1]; break;
      case "Level3": _currRotation = targetRotations[2]; break;
    }
    transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, _currRotation, Time.deltaTime*rotateSpeed));
  }
}
