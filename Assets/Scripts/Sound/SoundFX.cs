using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
  public static AudioSource source;

  void Start()
  {
    source = transform.GetComponent<AudioSource>();
  }
}
