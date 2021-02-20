using System.Collections;
using UnityEngine;
public class FinalDoor : MonoBehaviour
{
  public float moveSpeed;
  public float moveOut;
  public float moveSideways;
  public AudioClip rockMoving;
  float currentTime;
  public float duration;

  private bool _trigger;

  void Update()
  {
    currentTime += Time.deltaTime*moveSpeed;
    // SoundFX.source.PlayOneShot(rockMoving);
    transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-moveOut,0 ,0),currentTime);
    transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0, moveSideways),currentTime);
  }

  void OnTriggerEnter(Collider collider)
  {
    // if 
    // set trigger to true
  }
  
}
