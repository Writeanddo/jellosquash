using UnityEngine;

public partial class Player : MonoBehaviour
{

  [Header("Size")]
  public LayerMask pickupLayer;
  public AnimationCurve sizeChangeAnimation;
  public float sizeChangeDuration = 1.0f;

  private float _animationTime;
  private Vector3 _localScale;

  private void SizeInit()
  {
    _animationTime = sizeChangeDuration;
    _localScale = transform.localScale;
  }

  private void SizeUpdate()
  {
    if (_animationTime < sizeChangeDuration)
    {
      transform.localScale = Vector3.Lerp(transform.localScale, _localScale, sizeChangeAnimation.Evaluate(_animationTime/sizeChangeDuration));
      _animationTime += Time.deltaTime;
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((pickupLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      _localScale += collider.transform.localScale;
      _animationTime = 0.0f;
      Destroy(collider.gameObject);
    }
  }
}
