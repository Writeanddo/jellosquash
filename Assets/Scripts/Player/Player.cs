using UnityEngine;

public partial class Player : MonoBehaviour
{
  private Vector3 _localScale;

  [Header("PlayerStats")]
  public Vector3 targetSize = new Vector3(3.0f, 3.0f, 3.0f);
  public AnimationCurve sizeChangeAnimation;

  void Start()
  {
    _localScale = transform.localScale;
  }

  void Update()
  {
    MovementUpdate();
  }
}
