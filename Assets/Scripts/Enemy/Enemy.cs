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

  public GameObject stats;

  [Range(0.8f, 1)]
  public float rotationThreshold = 0.9f;
  public float attackInterval = 2.0f;
  public float attackRange = 10.0f;
  public float attackDuration;

  private Transform _target;
  private bool _followPlayer = false;
  private float _animationTime;
  private Vector3 _localScale;

  private bool _itemExists = false;

  private float _attackIntervalTime;
  private float _attackTime;
  private Vector3 _currentPosition;
  public AudioClip squashed;

  void Start()
  {
    _animationTime = squashedDuration;
    _localScale = transform.localScale;
    _itemExists = item != null;
    _attackIntervalTime = attackInterval;
    _attackTime = attackDuration;
    _currentPosition = transform.position;
  }

  void Update()
  {
    if (_followPlayer && !dead)
    {
      if (_itemExists)
      {
        item.transform.position = Vector3.Lerp(item.transform.position, transform.position + transform.up*1.5f*transform.localScale.y + transform.up*offset, Time.deltaTime*followSpeed);
        item.transform.Rotate(Vector3.up, Time.deltaTime*rotationSpeed);
      }

      Vector3 direction = (_target.position - transform.position).normalized;
      if (Vector3.Distance(transform.position, _target.position) <= agent.stoppingDistance)
      {
        // rotate to face player
        if (FaceTarget(ref direction))
        {
          if (_attackTime >= attackDuration) _attackIntervalTime -= Time.deltaTime;
          if (_attackIntervalTime <= 0.0f)
          {
            _attackTime = 0.0f;
            _attackIntervalTime = attackInterval;
            _currentPosition = transform.position;
          }
        }
      }

      if (_attackTime < attackDuration)
      {
        _attackIntervalTime = attackInterval;
        _attackTime += Time.deltaTime;
        agent.stoppingDistance = 0.0f;
      } else agent.stoppingDistance = attackRange;
      agent.SetDestination(_target.position);
    }


    if (die && !dead)
    {
      _animationTime = 0.0f;
      stats.SetActive(false);
      dead = true;
      SoundFX.source.PlayOneShot(squashed);
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

  private bool FaceTarget(ref Vector3 direction)
  {
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*2.0f);

    if (Vector3.Dot(direction, transform.forward) > rotationThreshold) return true;
    return false;

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
