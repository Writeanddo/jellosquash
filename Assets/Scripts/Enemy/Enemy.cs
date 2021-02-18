using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  public NavMeshAgent agent;
  public LayerMask playerLayer;
  [HideInInspector]
  public bool die = false;
  public bool dead = false;
  public float targetSquashSize = 0.1f;
  public AnimationCurve squashedCurve;
  public float squashedDuration;

  public GameObject item;
  public float offset = 2.0f;
  public float followSpeed = 10.0f;
  public float rotationSpeed = 10.0f;

  private Transform _target;
  private bool _followPlayer = false;
  private float _animationTime;
  private Vector3 _localScale;

  private bool _itemExists = false;

  void Start()
  {
    _animationTime = squashedDuration;
    _localScale = transform.localScale;
    _itemExists = item != null;
  }

  void Update()
  {
    if (_followPlayer && !dead)
    {
      if (_itemExists)
      {
        item.transform.position = Vector3.Lerp(item.transform.position, transform.position + transform.up*transform.localScale.y + transform.up*offset, Time.deltaTime*followSpeed);
        item.transform.Rotate(Vector3.up, Time.deltaTime*rotationSpeed);
      }
      agent.SetDestination(_target.position);
      if (Vector3.Distance(transform.position, _target.position) <= agent.stoppingDistance)
      {
        // rotate to face player
        FaceTarget();
        // then
        // attack
      }
    }


    if (die && !dead)
    {
      _animationTime = 0.0f;
      dead = true;
    }
    if (_animationTime < squashedDuration)
    {
      _animationTime += Time.deltaTime;
      transform.localScale = Vector3.Lerp(_localScale, new Vector3(_localScale.x, targetSquashSize, _localScale.z), squashedCurve.Evaluate(_animationTime/squashedDuration));
    }
  }

  void OnTriggerEnter(Collider collider)
  {
    if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
    {
      _target = collider.transform;
      _followPlayer = true;
    }
  }

  private void FaceTarget()
  {
    Vector3 direction = (_target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
  }

  // void OnTriggerExit(Collider collider)
  // {
  //   if ((playerLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
  //   {
  //     _target = transform;
  //     _followPlayer = false;
  //   }
  // }

}
