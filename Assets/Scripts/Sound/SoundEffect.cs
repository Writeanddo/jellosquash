using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
  static AudioSource audioSource;
  // static AudioClip
    // Start is called before the first frame update
    void Start()
    {
      audioSource = GetComponent<AudioSource>();
      
    }

    // Update is called once per frame
    public static void PlaySound(string clip)
    {
      switch(clip)
      {
        case "test":
        break;
        
      } 
    }
}
