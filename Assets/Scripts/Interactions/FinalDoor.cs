using System.Collections;
using UnityEngine;
public class FinalDoor : MonoBehaviour
{
  public Transform Door;
  public float moveSpeed;
  public float moveOut;
  public float moveSideways;
  public AudioClip rockMoving;
  float currentTime;
  public float duration;

  void Update()
  {
    // if(playerhaskey)
    if(Input.GetKey("space"))
    {
      currentTime += Time.deltaTime*moveSpeed;
      SoundFX.source.PlayOneShot(rockMoving);
      Door.position = Vector3.Lerp(Door.position, Door.position + new Vector3(-moveOut,0 ,0),currentTime);
      Door.position = Vector3.Lerp(Door.position, Door.position + new Vector3(0, 0, moveSideways),currentTime);
    }
  }

  // void OnTriggerEnter(Collider col)
  // {

  // }
  
}
