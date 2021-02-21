using System.Collections;
using UnityEngine;
public class FinalDoor : MonoBehaviour
{
  public float moveSpeed;
  public float moveOut;
  public float moveSideways;
  public AudioClip rockMoving;
  public Collider[] disableCollider;
  public LayerMask playerLayer;
  public float duration;
  public AnimationCurve curve;

  private float _animationTime;
  private bool _opened = false;
  private Vector3 _position;

  void Start()
  {
    _animationTime = duration;
    _position = transform.position;
    _opened = false;
  }

  void Update()
  {
    if (_animationTime < duration)
    {
      _animationTime += Time.deltaTime*moveSpeed;
      if (_animationTime/duration < 0.5f)
        transform.position = Vector3.Lerp(_position, _position + new Vector3(-moveOut,0 ,0), curve.Evaluate(_animationTime/duration*2.0f));
      else transform.position = Vector3.Lerp(_position + new Vector3(-moveOut,0 ,0), _position + new Vector3(-moveOut,0 ,0) + new Vector3(0, 0, moveSideways), curve.Evaluate(_animationTime/duration*2.0f - 1.0f));
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      Player player = collider.GetComponent<Player>();
      if (player.item != null)
      {
        if (player.item.name == "WinKey" && !_opened)
        {
          for (int c=0; c < disableCollider.Length; c++)
            disableCollider[c].enabled = false;
          _animationTime = 0.0f;
          SoundFX.source.PlayOneShot(rockMoving);
          _opened = true;
        }
      }
    }
  }
  
}
