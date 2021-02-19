using UnityEngine;

public partial class Player : MonoBehaviour
{

  [Header("Size")]
  public LayerMask pickupLayer;
  public LayerMask enemyDieLayer;
  public AnimationCurve sizeChangeAnimation;
  public float sizeChangeDuration = 1.0f;

  public GameObject item;
  public float knockbackForce;
  public float knockupForce;

  private float _animationTime;
  private Vector3 _localScale;

  private bool _itemExists;
  private float _rotationSpeed;
  private float _itemOffset;
  private float _itemFollowSpeed;

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

    if (_itemExists)
    {
      item.transform.position = Vector3.Lerp(item.transform.position, transform.position + transform.up*1.5f*transform.localScale.y + transform.up*_itemOffset, Time.deltaTime*_itemFollowSpeed);
      item.transform.Rotate(Vector3.up, Time.deltaTime*_rotationSpeed);
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((pickupLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      if (collider.isTrigger == true)
      {
        _localScale += collider.transform.localScale;
        _animationTime = 0.0f;
        Destroy(collider.gameObject);
        
      }
    }

    if ((enemyDieLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      Enemy enemy = collider.transform.parent.GetComponent<Enemy>();
      if (attack && _localScale.x > collider.transform.parent.localScale.x)
      {
        if (!enemy.dead)
        {
          enemy.die = true;
          _localScale += new Vector3(0.5f, 0.5f, 0.5f);

          if (enemy.item != null)
          {
            item = enemy.item;
            _rotationSpeed = enemy.rotationSpeed;
            _itemOffset = enemy.offset;
            _itemFollowSpeed = enemy.followSpeed;
            _itemExists = true;
          }
        }
      } else
      {
        if (!enemy.dead)
        {
          _velocity += enemy.transform.forward*knockbackForce + enemy.transform.up*(knockupForce-gravity*0.5f);
          if (_localScale.x <= 1.0f) dead = true;
          else if (_localScale.x > 1.0f) DropJelly(enemy.transform.forward);
        }
      }
    }
  }
}
